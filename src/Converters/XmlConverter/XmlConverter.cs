using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Aspose.Cells;
using Aspose.Cells.Rendering;
using Aspose.Words;
using EmailImport.Conversion;

namespace XmlConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "XML", IsMultiple = true)]
    class XmlConverter : IConverter
    {
        #region Static Constructor

        static XmlConverter()
        {
            var c = new Aspose.Cells.License();
            c.SetLicense("Aspose.Total.lic");

            var w = new Aspose.Words.License();
            w.SetLicense("Aspose.Total.lic");
        }

        #endregion

        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            if (IsSpreadsheetML(inputFile))
            {
                return ConvertWithCells(inputFile, options);
            }
            else
            {
                return ConvertWithWords(inputFile, options);
            }
        }

        private List<PageInfo> ConvertWithWords(String inputFile, ImageConversionOptions options)
        {
            Document doc = new Document(inputFile);

            // Adjust margins
            foreach (Section section in doc.Sections)
            {
                section.PageSetup.LeftMargin = (section.PageSetup.LeftMargin / 2.54) / 2;
                section.PageSetup.RightMargin = (section.PageSetup.RightMargin / 2.54) / 2;
                section.PageSetup.TopMargin = (section.PageSetup.TopMargin / 2.54) / 2;
                section.PageSetup.BottomMargin = (section.PageSetup.BottomMargin / 2.54) / 2;
            }

            if (options.BinarisationAlgorithm == BinarisationAlgorithm.Default)
                options.BinarisationAlgorithm = BinarisationAlgorithm.OtsuThreshold;

            var pages = new List<PageInfo>();

            var saveOptions = new Aspose.Words.Saving.ImageSaveOptions(Aspose.Words.SaveFormat.Png);
            saveOptions.Resolution = options.Resolution;
            saveOptions.PageCount = 1;

            for (int i = 0; i < doc.PageCount; i++)
            {
                saveOptions.PageIndex = i;

                using (MemoryStream ms = new MemoryStream())
                {
                    // Save the page to the memory stream
                    doc.Save(ms, saveOptions);

                    // Set the position back to the start of the stream
                    ms.Seek(0, SeekOrigin.Begin);

                    // Convert the page and add it to the list
                    pages.AddRange(ImageProcessingEngine.Instance.Convert(ms, options));
                }
            }

            return pages;
        }

        private List<PageInfo> ConvertWithCells(String inputFile, ImageConversionOptions options)
        {
            Workbook book = new Workbook(inputFile);

            ImageOrPrintOptions saveOptions = new ImageOrPrintOptions();
            saveOptions.HorizontalResolution = options.Resolution;
            saveOptions.VerticalResolution = options.Resolution;
            saveOptions.ImageFormat = ImageFormat.Png;
            //saveOptions.IsCellAutoFit = true;
            //saveOptions.OnePagePerSheet = true;

            if (options.BinarisationAlgorithm == BinarisationAlgorithm.Default)
                options.BinarisationAlgorithm = BinarisationAlgorithm.OtsuThreshold;

            var pages = new List<PageInfo>();

            foreach (Worksheet sheet in book.Worksheets)
            {
                // Adjust the margins
                sheet.PageSetup.TopMargin = 0.5;
                sheet.PageSetup.BottomMargin = 0.5;
                sheet.PageSetup.LeftMargin = 0.5;
                sheet.PageSetup.RightMargin = 0.5;

                SheetRender render = new SheetRender(sheet, saveOptions);

                for (int i = 0; i < render.PageCount; i++)
                {
                    using (var bmp = render.ToImage(i))
                    {
                        // Convert the page and add it to the list
                        pages.AddRange(ImageProcessingEngine.Instance.Convert(bmp, options));
                    }
                }
            }

            return pages;
        }

        private Boolean IsSpreadsheetML(String filename)
        {
            try
            {
                var xml = XDocument.Load(filename);

                var progid = (from node in xml.Nodes()
                              where node.NodeType == XmlNodeType.ProcessingInstruction && ((XProcessingInstruction)node).Target.ToLower() == "mso-application"
                              select Regex.Match(((XProcessingInstruction)node).Data, "progid=\"(?<progid>.*?)\"", RegexOptions.IgnoreCase).Groups["progid"].Value).SingleOrDefault();

                if (!String.IsNullOrEmpty(progid) && progid.Equals("Excel.Sheet", StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }
            catch
            { }

            return false;
        }
    }
}
