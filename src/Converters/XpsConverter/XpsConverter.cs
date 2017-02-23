using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using EmailImport.Conversion;
using System.Threading.Tasks;
using System.Threading.Tasks.Schedulers;

namespace XpsConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "XPS", IsMultiple = true)]
    class XpsConverter : IConverter
    {
        StaTaskScheduler scheduler = new StaTaskScheduler(1);

        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            if (options.BinarisationAlgorithm == BinarisationAlgorithm.Default)
                options.BinarisationAlgorithm = BinarisationAlgorithm.OtsuThreshold;

            // Create and start a new task
            var task = Task<List<PageInfo>>.Factory.StartNew(() =>
                {
                    var pages = new List<PageInfo>();

                    using (XpsDocument xpsDoc = new XpsDocument(inputFile, FileAccess.Read))
                    {
                        FixedDocumentSequence docSeq = xpsDoc.GetFixedDocumentSequence();

                        for (int i = 0; i < docSeq.DocumentPaginator.PageCount; ++i)
                        {
                            using (DocumentPage docPage = docSeq.DocumentPaginator.GetPage(i))
                            {
                                int width = (int)(((float)options.Resolution / 96f) * docPage.Size.Width);
                                int height = (int)(((float)options.Resolution / 96f) * docPage.Size.Height);

                                RenderTargetBitmap renderTarget = new RenderTargetBitmap(width, height, options.Resolution, options.Resolution, PixelFormats.Default);
                                renderTarget.Render(docPage.Visual);

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    BitmapEncoder encoder = new TiffBitmapEncoder();
                                    encoder.Frames.Add(BitmapFrame.Create(renderTarget));
                                    encoder.Save(ms);

                                    ms.Seek(0, SeekOrigin.Begin);

                                    pages.AddRange(ImageProcessingEngine.Instance.Convert(ms, options));
                                }
                            }
                        }

                        xpsDoc.Close();
                    }

                    return pages;
                },
                CancellationToken.None,
                TaskCreationOptions.None,
                scheduler);

            // Wait for the task to complete
            task.Wait();
            
            // Return the list of pages
            return task.Result;
        }
    }
}
