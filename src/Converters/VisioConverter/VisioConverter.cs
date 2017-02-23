using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using EmailImport.Conversion;

namespace VisioConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "VDW", IsMultiple = true)]
    [ExportMetadata("Format", "VDX", IsMultiple = true)]
    [ExportMetadata("Format", "VSD", IsMultiple = true)]
    [ExportMetadata("Format", "VSS", IsMultiple = true)]
    [ExportMetadata("Format", "VST", IsMultiple = true)]
    [ExportMetadata("Format", "VSX", IsMultiple = true)]
    [ExportMetadata("Format", "VTX", IsMultiple = true)]
    class VisioConverter : IConverter
    {
        #region Static Constructor

        static VisioConverter()
        {
            License lic = new License();
            lic.SetLicense("Aspose.Total.lic");
        }

        #endregion

        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            Diagram diagram = new Diagram(inputFile);

            if (options.BinarisationAlgorithm == BinarisationAlgorithm.Default)
                options.BinarisationAlgorithm = BinarisationAlgorithm.OtsuThreshold;

            var pages = new List<PageInfo>();

            var saveOptions = new ImageSaveOptions(SaveFileFormat.PNG);
            saveOptions.Resolution = options.Resolution;
            saveOptions.PageCount = 1;

            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                saveOptions.PageIndex = i;

                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the page to the memory stream
                    diagram.Save(ms, saveOptions);

                    // Set the position back to the start of the stream
                    ms.Seek(0, SeekOrigin.Begin);

                    // Convert the page and add it to the list
                    pages.AddRange(ImageProcessingEngine.Instance.Convert(ms, options));
                }
            }

            return pages;
        }
    }
}
