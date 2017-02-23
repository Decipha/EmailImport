using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Aspose.Slides;
using Aspose.Slides.Export;
using EmailImport.Conversion;

namespace PowerPointConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "PPT", IsMultiple = true)]
    [ExportMetadata("Format", "PPTX", IsMultiple = true)]
    class PowerPointConverter : IConverter
    {
        #region Static Constructor

        static PowerPointConverter()
        {
            License license = new License();
            license.SetLicense("Aspose.Total.lic");
        }

        #endregion

        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            var ext = (options.ProcessAs ?? Path.GetExtension(inputFile).Trim('.', ' ')).ToUpper();

            using (MemoryStream stream = new MemoryStream())
            {
                Presentation ppt = new Presentation(inputFile);
                ppt.Save(stream, SaveFormat.Pdf); // SaveFormat.PdfNotes not yet implemented

                stream.Seek(0, SeekOrigin.Begin);

                return ImageProcessingEngine.Instance.Convert(stream, options);
            }
        }
    }
}