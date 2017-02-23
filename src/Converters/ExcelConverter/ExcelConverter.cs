using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Aspose.Cells;
using Aspose.Cells.Rendering;
using EmailImport.Conversion;

namespace ExcelConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "XLS", IsMultiple = true)]
    [ExportMetadata("Format", "XLSX", IsMultiple = true)]
    [ExportMetadata("Format", "XLSM", IsMultiple = true)]
    [ExportMetadata("Format", "XLR", IsMultiple = true)]
    [ExportMetadata("Format", "CSV", IsMultiple = true)]
    [ExportMetadata("Format", "TSV", IsMultiple = true)]
    class ExcelConverter : IConverter
    {
        #region Static Constructor

        static ExcelConverter()
        {
            License lic = new License();
            lic.SetLicense("Aspose.Total.lic");
        }

        #endregion

        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            var ext = (options.ProcessAs ?? Path.GetExtension(inputFile).Trim('.', ' ')).ToUpper();

            Workbook book = OpenWorkbook(inputFile, ext);

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
                if (ext == "CSV" || ext == "TSV")
                {
                    sheet.PageSetup.Orientation = PageOrientationType.Landscape;

                    foreach (Cell cell in sheet.Cells)
                        cell.PutValue(cell.StringValue.Replace("\0", "").Trim());

                    sheet.AutoFitColumns();
                    sheet.AutoFitRows();
                }

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

            // TODO: DONT THROW HERE, ALLOW THIS IF REMOVE BLANK PAGES IS ENABLED
            // BUT WILL NEED TO BE DONE BACK IN EMAILCONVERTER
            if (!pages.Any())
                throw new Exception("Aspose.Cells did not find anything to print.");

            return pages;
        }

        private Workbook OpenWorkbook(String inputFile, String extension)
        {
            if (extension == "TSV")
            {
                return new Workbook(inputFile, new LoadOptions(LoadFormat.TabDelimited));
            }
            else if (extension == "CSV")
            {
                using (StreamReader sr = new StreamReader(inputFile))
                {
                    if (sr.ReadLine().Count(c => c == '\t') > 0)
                        return new Workbook(inputFile, new LoadOptions(LoadFormat.TabDelimited));
                }
            }

            return new Workbook(inputFile);
        }
    }
}