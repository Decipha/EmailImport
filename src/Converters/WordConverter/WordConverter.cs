using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;
using EmailImport.Conversion;
using HtmlAgilityPack;

namespace WordConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "DOC", IsMultiple = true)]
    [ExportMetadata("Format", "DOCX", IsMultiple = true)]
    [ExportMetadata("Format", "DOT", IsMultiple = true)]
    [ExportMetadata("Format", "DOTX", IsMultiple = true)]
    [ExportMetadata("Format", "DOCM", IsMultiple = true)]
    [ExportMetadata("Format", "DOTM", IsMultiple = true)]
    [ExportMetadata("Format", "RTF", IsMultiple = true)]
    [ExportMetadata("Format", "TXT", IsMultiple = true)]
    [ExportMetadata("Format", "HTM", IsMultiple = true)]
    [ExportMetadata("Format", "HTML", IsMultiple = true)]
    [ExportMetadata("Format", "MHT", IsMultiple = true)]
    [ExportMetadata("Format", "ODT", IsMultiple = true)]
    class WordConverter : IConverter
    {
        #region Static Constructor

        static WordConverter()
        {
            License license = new License();
            license.SetLicense("Aspose.Total.lic");
        }

        #endregion

        public List<PageInfo> Convert(String inputFile, ImageConversionOptions options)
        {
            var ext = (options.ProcessAs ?? Path.GetExtension(inputFile).Trim('.', ' ')).ToUpper();
            var isTextFile = (ext == "TXT");

            // Open the document
            Document doc = OpenDocument(inputFile, ext);

            // Specific processing for MHT, HTM and HTML
            if (ext == "MHT" || ext == "HTM" || ext == "HTML" || isTextFile)
            {
                // Adjust Margins, Paper Size and Orientation
                foreach (Section section in doc.Sections)
                {
                    section.PageSetup.PaperSize = PaperSize.A4;
                    section.PageSetup.Orientation = isTextFile ? Orientation.Landscape : Orientation.Portrait;

                    section.PageSetup.LeftMargin = (section.PageSetup.LeftMargin / 2.54) / 2;
                    section.PageSetup.RightMargin = (section.PageSetup.RightMargin / 2.54) / 2;
                    section.PageSetup.TopMargin = (section.PageSetup.TopMargin / 2.54) / 2;
                    section.PageSetup.BottomMargin = (section.PageSetup.BottomMargin / 2.54) / 2;

                    if (isTextFile)
                    {
                        FontChanger changer = new FontChanger("Courier New");
                        doc.Accept(changer);
                    }
                    else
                    {
                        // Get the portrait and landscape widths
                        var portraitWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
                        var landscapeWidth = section.PageSetup.PageHeight - section.PageSetup.TopMargin - section.PageSetup.BottomMargin;

                        // Adjust table fit
                        foreach (Table table in section.Body.Tables)
                        {
                            var width = GetTableWidth(table);

                            if (width > portraitWidth)
                            {
                                if (section.PageSetup.Orientation != Orientation.Landscape)
                                    section.PageSetup.Orientation = Orientation.Landscape;

                                if (width > landscapeWidth)
                                    table.AutoFit(AutoFitBehavior.AutoFitToWindow);
                            }
                        }

                        // Get the usable width/height
                        var usableWidth = section.PageSetup.PageWidth - section.PageSetup.LeftMargin - section.PageSetup.RightMargin;
                        var usableHeight = section.PageSetup.PageHeight - section.PageSetup.TopMargin - section.PageSetup.BottomMargin;

                        // Adjust image size
                        foreach (Shape shape in section.GetChildNodes(NodeType.Shape, true))
                        {
                            if (shape.HasImage && (shape.Width > usableWidth || shape.Height > usableHeight))
                            {
                                // Adjust shape size
                                var scale = Math.Min((usableWidth / shape.Width), (usableHeight / shape.Height));

                                shape.Width = shape.Width * scale;
                                shape.Height = shape.Height * scale;
                            }
                        }
                    }
                }

                if (!isTextFile)
                {
                    // Update table layout following adjustments
                    doc.UpdateTableLayout();
                }
            }

            if (options.BinarisationAlgorithm == BinarisationAlgorithm.Default)
                options.BinarisationAlgorithm = BinarisationAlgorithm.OtsuThreshold;

            var pages = new List<PageInfo>();

            var saveOptions = new Aspose.Words.Saving.ImageSaveOptions(SaveFormat.Png);
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

                    try
                    {
                        // Convert the page and add it to the list
                        pages.AddRange(ImageProcessingEngine.Instance.Convert(ms, options));
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "Decode: Unknown or wrong format [Stream][CiImage::Open]")
                        {
                            // This is a known error, there is an occasional issue with  the ClearImage Open method
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

            return pages;
        }

        private double GetTableWidth(Table table)
        {
            double width = 0.0;

            foreach (Row row in table.Rows)
            {
                double w = 0.0;

                foreach (Cell cell in row.Cells)
                    w += cell.CellFormat.Width;

                if (w > width)
                    width = w;
            }

            return width;
        }

        private Document OpenDocument(String inputFile, String extension)
        {
            Document document = null;
            LoadOptions options = null;

            // Setup load options for text files
            if (extension == "TXT")
            {
                options = new LoadOptions()
                {
                    LoadFormat = LoadFormat.Text,
                    Encoding = Encoding.Default
                };
            }

            // Open the document
            try
            {
                document = new Document(inputFile, options);
            }
            catch (FileCorruptedException e)
            {
                // If the file is a html based file and the error is due to too many styles, clean up the styles and retry
                if ((extension == "MHT" || extension == "HTM" || extension == "HTML") && e.ToString().Contains("There are too many styles in the document"))
                {
                    // Read the html into a string
                    var text = File.ReadAllText(inputFile);

                    // Open the document with the Html Agility Pack
                    HtmlDocument html = new HtmlDocument();
                    html.LoadHtml(text);

                    // Find the <style> element
                    var styles = FindNode(html.DocumentNode.ChildNodes, "style");

                    // If <style> is not found, must be something else wrong...
                    if (styles != null)
                    {
                        // Get the current style element position and length in the html string
                        var position = styles.StreamPosition;
                        var length = styles.OuterHtml.Length;

                        // Remove unreferenced styles
                        var nodes = html.DocumentNode.SelectNodes("//*[@class]");

                        List<String> classes = new List<String>();

                        foreach (var node in nodes)
                        {
                            var value = node.Attributes["class"].Value;

                            if (value.StartsWith("3D", StringComparison.OrdinalIgnoreCase))
                                value = value.Substring(2);

                            value = value.Trim('"');

                            if (!classes.Contains(value))
                                classes.Add(value);
                        }

                        CssParser css = new CssParser(styles.InnerText);

                        css.RemoveAll(c => !classes.Any(cls => c.Key.Contains(cls)));
                        styles.InnerHtml = css.ToString();

                        // Use a string builder to rebuild the html string
                        var sb = new StringBuilder();

                        sb.Append(text.Substring(0, position));
                        sb.Append(styles.OuterHtml);
                        sb.Append(text.Substring(position + length));

                        try
                        {
                            // Try to open the corrected html document
                            using (MemoryStream ms = new MemoryStream(UTF8Encoding.Default.GetBytes(sb.ToString())))
                            {
                                document = new Document(ms);
                            }
                        }
                        catch (FileCorruptedException ex)
                        {
                            // If there are still too many styles, remove them all together!
                            if (ex.ToString().Contains("There are too many styles in the document"))
                            {
                                sb.Clear();
                                sb.Append(text.Substring(0, position));
                                sb.Append(text.Substring(position + length));

                                // Try to open once more without any styles
                                using (MemoryStream ms = new MemoryStream(UTF8Encoding.Default.GetBytes(sb.ToString())))
                                {
                                    document = new Document(ms);
                                }
                            }
                        }
                    }
                }

                // If we don't have a document opened, rethrow the exception
                if (document == null)
                    throw;
            }

            return document;
        }

        private HtmlNode FindNode(HtmlNodeCollection nodes, String name)
        {
            HtmlNode node = nodes[name];

            if (node != null)
            {
                return node;
            }
            else
            {
                foreach (var nd in nodes)
                {
                    node = FindNode(nd.ChildNodes, name);

                    if (node != null)
                        return node;
                }
            }

            return null;
        }
    }
}