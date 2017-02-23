using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AForge.Imaging;
using AForge.Imaging.Filters;
using Inlite.ClearImage;
using Inlite.ClearImageNet;

namespace EmailImport.Conversion
{
    public sealed class ImageProcessingEngine
    {
        #region Singleton

        private static readonly Lazy<ImageProcessingEngine> lazy = new Lazy<ImageProcessingEngine>(() => new ImageProcessingEngine());

        private ImageProcessingEngine()
        {
            for (int i = 0; i < ConcurrencyLevel.GetValueOrDefault(2); i++)
            {
                Task.Run(() =>
                {
                    Worker();
                });
            }
        }

        #endregion

        #region Static Properties

        public static int? ConcurrencyLevel { get; set; }
        public static ImageProcessingEngine Instance { get { return lazy.Value; } }

        #endregion

        #region Static Methods

        public static void Complete()
        {
            if (lazy.IsValueCreated)
                lazy.Value.queue.CompleteAdding();
        }

        #endregion

        #region Instance Members

        private BlockingCollection<ImageProcessingArgs> queue = new BlockingCollection<ImageProcessingArgs>();

        #endregion

        #region Public Properties

        public int Queued { get { return queue.Count; } }

        #endregion

        #region Public Methods

        public List<PageInfo> Convert(String fileName, ImageConversionOptions options)
        {
            // A file extension with a length greater than 10 will cause a buffer overflow
            // when opened with Clear Image, so load into a memory stream instead
            if (Path.GetExtension(fileName).Length > 10)
            {
                using (var ms = new MemoryStream(File.ReadAllBytes(fileName)))
                {
                    return QueueAndWait(new ImageProcessingArgs(ms, options));
                }
            }
            else
            {
                return QueueAndWait(new ImageProcessingArgs(fileName, options));
            }
        }

        public List<PageInfo> Convert(Stream stream, ImageConversionOptions options)
        {
            return QueueAndWait(new ImageProcessingArgs(stream, options));
        }

        public List<PageInfo> Convert(Bitmap bitmap, ImageConversionOptions options)
        {
            return QueueAndWait(new ImageProcessingArgs(bitmap, options));
        }

        public void Concat(String fileName, IEnumerable<PageInfo> pages)
        {
            QueueAndWait(new ImageProcessingArgs(fileName, pages));
        }

        #endregion

        #region Private Methods

        private List<PageInfo> QueueAndWait(ImageProcessingArgs args)
        {
            queue.Add(args);

            args.SyncEvent.WaitOne();

            if (args.ExceptionObject != null)
                throw new Exception(args.ExceptionObject.Message, args.ExceptionObject);

            return (args.Operation == ImageProcessingArgs.OperationType.Concat) ? null : args.Pages;
        }

        private void Worker()
        {
            ImageProcessingArgs args;

            ImageEditor repair = new ImageEditor();

            while (queue.TryTake(out args, Timeout.Infinite))
            {
                try
                {
                    Process(repair, args);
                }
                catch (Exception e)
                {
                    args.ExceptionObject = e;
                }
                finally
                {
                    try
                    {
                        repair.Image.Close();
                    }
                    catch { }

                    args.SyncEvent.Set();
                }
            }
        }

        private void Process(ImageEditor repair, ImageProcessingArgs args)
        {
            int page = 0;

            switch (args.Operation)
            {
                case ImageProcessingArgs.OperationType.File:
                    int total = 0;

                    do
                    {
                        // Open the next page
                        repair.Image.Open((String)args.ImageData, ++page);

                        // Get the total page count (just incase PageCount gets reset on close in future versions)
                        if (total == 0)
                            total = repair.Image.PageCount;

                        // Process the current page
                        Repair(repair, args);

                        // Close the image
                        repair.Image.Close();

                    } while (page < total);

                    break;

                case ImageProcessingArgs.OperationType.Stream:
                    do
                    {
                        // Open the image from the stream (or next page)
                        if (page == 0)
                            repair.Image.Open((Stream)args.ImageData, ++page);
                        else
                            repair.Image.OpenPage(++page);

                        // Process the current page
                        Repair(repair, args);

                    } while (page < repair.Image.PageCount);

                    break;

                case ImageProcessingArgs.OperationType.Bitmap:
                    var bitmap = (Bitmap)args.ImageData;

                    for (int i = 0; i < bitmap.GetFrameCount(FrameDimension.Page); i++)
                    {
                        // Select the page
                        bitmap.SelectActiveFrame(FrameDimension.Page, i);

                        // Open the pages
                        repair.Image.Open(bitmap);

                        // Process and current page
                        Repair(repair, args);
                    }

                    break;

                case ImageProcessingArgs.OperationType.Concat:
                    var file = (String)args.ImageData;

                    foreach (var pg in args.Pages)
                    {
                        repair.Image.Open(pg.FileName, 1);

                        if (repair.Image.IsBitonal())
                        {
                            repair.Image.pComprBitonal = EComprBitonal.citbCCITTFAX4;
                        }
                        else
                        {
                            repair.Image.pComprColor = EComprColor.citcJPEG;
                            repair.Image.JpegQuality = 85;
                        }

                        repair.Image.Append(file, EFileFormat.ciEXT);
                    }

                    break;
            }
        }

        private void Repair(ImageEditor repair, ImageProcessingArgs args)
        {
            // Increment the internal Page Index for the current ImageConversionOptions object
            args.Options.PageIndex++;

            // Build the filename part based on the PageIndex (will be persisted across calls so long as the same Options are used)
            var filePart = String.Format("{0:000}.tif", args.Options.PageIndex);

            // Create the Page object
            var page = new PageInfo()
            {
                FileName = Path.Combine(args.Options.SaveToPath, filePart),
                RelativePath = Path.Combine(args.Options.RelativePath, filePart),
                Width = repair.Image.Width,
                Height = repair.Image.Height
            };

            if (!repair.Image.IsBitonal())
            {
                // Not already bitonal, so process as necessary
                switch (args.Options.BitDepth)
                {
                    case 1:
                        if (args.Options.BinarisationAlgorithm == BinarisationAlgorithm.Default || args.Options.BinarisationAlgorithm == BinarisationAlgorithm.ClearImage)
                        {
                            repair.AdvancedBinarize(args.Options.Resolution);
                        }
                        else
                        {
                            // Source image must be grayscaled first
                            repair.ToGrayscale();

                            // Using an unmanaged image we will operate directly on the source images memory
                            using (UnmanagedImage image = new UnmanagedImage(repair.Image.Buffer, repair.Width, repair.Height, repair.Image.LineBytes, PixelFormat.Format8bppIndexed))
                            {
                                IInPlaceFilter filter = null;

                                if (args.Options.BinarisationAlgorithm == BinarisationAlgorithm.OtsuThreshold)
                                    filter = new OtsuThreshold();
                                else if (args.Options.BinarisationAlgorithm == BinarisationAlgorithm.FloydSteinbergDither)
                                    filter = new FloydSteinbergDithering();
                                else
                                    filter = new BradleyLocalThresholding();

                                filter.ApplyInPlace(image);
                            }

                            // Image is still in 8 bit format (but is bitonal) so we need to convert to a 1 bit image
                            repair.ToBitonal();
                        }

                        break;

                    case 8:
                        repair.ToGrayscale();
                        break;
                }

                // Set the Resolution
                repair.Image.HorzDpi = args.Options.Resolution;
                repair.Image.VertDpi = args.Options.Resolution;
            }
            else
            {
                // Is already bitonal, so quite possibly a fax
                repair.FaxStandardToFine();

                // Remove the Fax Header if exists
                if (args.Options.RemoveFaxHeader)
                    repair.FaxRemoveHeader();
            }

            // Perform Auto Deskew
            if (args.Options.AutoDeskew.GetValueOrDefault())
            {
                page.Skew = repair.AutoDeskew();
            }

            // Perform Auto Rotate
            if (args.Options.AutoRotate.GetValueOrDefault())
            {
                page.Rotation = repair.AutoRotate();
            }

            // Check if the image is blank and store with the Page object
            page.IsBlank = repair.IsBlankImage();

            // Setup the image save options
            if (repair.Image.IsBitonal())
            {
                repair.Image.pComprBitonal = EComprBitonal.citbCCITTFAX4;
            }
            else
            {
                repair.Image.pComprColor = EComprColor.citcJPEG;
                repair.Image.JpegQuality = 85;
            }

            // Save the image
            repair.Image.SaveAs(page.FileName, EFileFormat.ciEXT);

            // Add the page to the collection
            args.Pages.Add(page);
        }

        #endregion
    }
}
