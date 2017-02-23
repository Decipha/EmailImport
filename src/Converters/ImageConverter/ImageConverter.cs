using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using EmailImport.Conversion;

namespace ImageConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "JPG", IsMultiple = true)]
    [ExportMetadata("Format", "JPEG", IsMultiple = true)]
    [ExportMetadata("Format", "GIF", IsMultiple = true)]
    [ExportMetadata("Format", "BMP", IsMultiple = true)]
    [ExportMetadata("Format", "PNG", IsMultiple = true)]
    [ExportMetadata("Format", "TIF", IsMultiple = true)]
    [ExportMetadata("Format", "TIFF", IsMultiple = true)]
    class ImageConverter : IConverter
    {
        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            if (options.AutoDeskew == null)
                options.AutoDeskew = true;

            if (options.AutoRotate == null)
                options.AutoRotate = true;

            return ImageProcessingEngine.Instance.Convert(inputFile, options);
        }
    }
}