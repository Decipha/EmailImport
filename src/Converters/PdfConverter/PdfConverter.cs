using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Devices;
using Aspose.Pdf.Exceptions;
using Aspose.Pdf.InteractiveFeatures.Forms;
using EmailImport.Conversion;

namespace PdfConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "PDF", IsMultiple = true)]
    class PdfConverter : IConverter
    {
        #region Static Constructor

        static PdfConverter()
        {
            Aspose.Pdf.License license = new Aspose.Pdf.License();
            license.SetLicense("Aspose.Total.lic");
        }

        #endregion

        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            if (options.AutoDeskew == null)
                options.AutoDeskew = true;

            if (options.AutoRotate == null)
                options.AutoRotate = true;        

            if (options.PdfConversion == PdfConversion.ClearImage)
                return ImageProcessingEngine.Instance.Convert(inputFile, options);
            return ConverByAppose(inputFile,options);
        }       

        private List<PageInfo> ConverByAppose(string inputFile, ImageConversionOptions options)
        {
            var pages = new List<PageInfo>();

            try
            {
                using (var doc = new Aspose.Pdf.Document(inputFile))
                {
                    // Dynamic XFA to Standard AcroForm PDF conversion currently has issues so don't even attempt
                    if (doc.Form.Type == FormType.Dynamic)
                        throw new Exception("PDF file in XFA format is not supported.");

                    // Flatten the pdf
                    doc.Flatten();

                    // Setup the device for rendering the PDF pages
                    var device = new PngDevice(new Resolution(options.Resolution));

                    foreach (Aspose.Pdf.Page page in doc.Pages)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            // Render the PDF
                            device.Process(page, ms);

                            // Set the stream position back to 0
                            ms.Seek(0, SeekOrigin.Begin);

                            try
                            {
                                // Convert and add the image to the list of pages
                                pages.AddRange(ImageProcessingEngine.Instance.Convert(ms, options));
                            }
                            catch (Exception e)
                            {
                                if (e.Message == "Decode: Unknown or wrong format [Stream][CiImage::Open]")
                                {
                                    // This is a known error, there is an occasional issue with the ClearImage Open method
                                    // Loading the stream into a Bitmap object works around the issue for now...
                                    using (var stream = new MemoryStream(ms.ToArray()))
                                    using (var bitmap = (Bitmap)Bitmap.FromStream(stream, true, false))
                                    {
                                        pages.AddRange(ImageProcessingEngine.Instance.Convert(bitmap, options));
                                    }
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }
                    }
                }
            }
            catch (InvalidPasswordException)
            {
                throw;
            }
            catch
            {
                // Reset the Page Index
                options.PageIndex = 0;

                // Try to process using ClearImage
                pages = ImageProcessingEngine.Instance.Convert(inputFile, options);
            }
            return pages;
        }
    }
}