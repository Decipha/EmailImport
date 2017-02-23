using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using BitFactory.Logging;
using EmailImport.Conversion;
using EmailImport.Conversion.Configuration;
using EmailImport.DataLayer;
using Inlite.ClearImageNet;

namespace EmailImport
{
    public class Document
    {
        #region Static Cache

        private static IEnumerable<ErrorTranslation> translations = null;

        private static IEnumerable<ErrorTranslation> Translations
        {
            get
            {
                if (translations == null)
                {
                    using (var ctx = new EmailImportDataContext())
                    {
                        translations = ctx.ErrorTranslations.ToList();
                    }
                }

                return translations;
            }
        }

        #endregion

        #region Member Fields

        private long emailID;
        private String saveToPath, batchNumber;
        private ImageConversionOptions options;

        #endregion

        #region Public Properties

        public String AttachmentFile { get; private set; }
        public String OriginalName { get; private set; }
        public String RelativePath { get; private set; }
        public String Extension { get; private set; }
        public String Mailbox { get; set; }

        public String ErrorMessage { get; internal set; }
        public String FailureReason { get; internal set; }
        public ErrorHandlingActions? ActionToTake { get; internal set; }
        public ErrorHandling ErrorHandling { get; internal set; }
        public Boolean ShouldRetry { get; private set; }

        public int Resolution
        {
            get { return options.Resolution; }
            set { options.Resolution = value; }
        }

        public int BitDepth
        {
            get { return options.BitDepth; }
            set { options.BitDepth = value; }
        }

        public BinarisationAlgorithm BinarisationAlgorithm
        {
            get { return options.BinarisationAlgorithm; }
            set { options.BinarisationAlgorithm = value; }
        }

        public Boolean? AutoDeskew
        {
            get { return options.AutoDeskew; }
            set { options.AutoDeskew = value; }
        }

        public Boolean? AutoRotate
        {
            get { return options.AutoRotate; }
            set { options.AutoRotate = value; }
        }

        public Boolean RemoveFaxHeader
        {
            get { return options.RemoveFaxHeader; }
            set { options.RemoveFaxHeader = value; }
        }

        public String ProcessAs
        {
            get { return options.ProcessAs; }
            set { options.ProcessAs = value; }
        }

        public Boolean Complete { get; private set; }
        public Boolean Success { get; private set; }
        public Boolean IsBody { get; private set; }

        public List<PageInfo> Pages { get; private set; }
        public Dictionary<String, String> Fields { get; private set; }
        public DocumentSource Source { get; private set; }
        public IConverter Converter { private get; set; }
        public Boolean Passthrough { get; set; }

        public PdfConversion PdfConversion
        {
            get { return options.PdfConversion; }
            set { options.PdfConversion = value; }
        }

        #endregion

        #region Constructor

        public Document(String attachmentFile, String originalName, String saveToPath, String batchNumber, long emailID, DocumentSource source)
        {
            Fields = new Dictionary<String, String>();

            this.saveToPath = saveToPath;
            this.batchNumber = batchNumber;
            this.emailID = emailID;

            OriginalName = originalName;
            RelativePath = Path.GetFileName(saveToPath);
            AttachmentFile = attachmentFile;
            Extension = Path.GetExtension(attachmentFile).Trim('.', ' ').ToLower();
            Source = source;
            ErrorHandling = new ErrorHandling();

            options = new ImageConversionOptions(saveToPath, RelativePath);

            Complete = false;
            Success = false;
            IsBody = (source == DocumentSource.Body);
            ShouldRetry = true;

            Fields.Add("%Document.Source", source.ToString());
            Fields.Add("%Document.IsBody", IsBody.ToString());
            Fields.Add("%Document.Extension", Extension);
            Fields.Add("%Document.AttachmentFileName", OriginalName);
            Fields.Add("%Document.FailureReason", String.Empty);
        }

        #endregion

        #region Public Methods

        public void Convert()
        {
            if (Passthrough)
            {
                SkipConversion();
            }
            else
            {
                try
                {
                    try
                    {
                        if (Converter != null)
                        {
                            // Convert using the found Converter
                            Pages = Converter.Convert(AttachmentFile, options);

                            // Conversion completed successfully
                            Success = true;
                        }
                        else
                        {
                            // Set the reason to unsupported file type
                            ErrorMessage = "File type is unknown.";
                            FailureReason = ErrorMessage;
                            ActionToTake = ErrorHandling.Unsupported;
                            ShouldRetry = false;
                        }
                    }
                    catch (Exception e)
                    {
                        // Add custom data for logging
                        e.Data.Add("FileName", OriginalName);
                        e.Data.Add("Mailbox", Mailbox);

                        // Get ErrorMessage and FailureReason and set ActionToTake
                        ProcessError(e);

                        // Log the issues
                        ConfigLogger.Instance.Log((ActionToTake.Value <= ErrorHandlingActions.Substitute) ? LogSeverity.Warning : LogSeverity.Error, emailID, e);
                    }

                    // If the conversion failed, for what ever reason...
                    if (!Success && ActionToTake.HasValue && ActionToTake.Value == ErrorHandlingActions.Substitute)
                    {
                        ConfigLogger.Instance.LogWarning(emailID, String.Format(Converter == null ? "Converter not found for '{0}'" : "Conversion of '{0}' failed", OriginalName) + String.Format(", an error image will be substituted. (Mailbox = '{0}')", Mailbox));

                        try
                        {
                            Reset();

                            using (var bmp = Properties.Resources.EmailImportError.Clone() as Bitmap)
                            {
                                Graphics g = Graphics.FromImage(bmp);

                                StringFormat format = new StringFormat();
                                format.Alignment = StringAlignment.Center;
                                format.LineAlignment = StringAlignment.Center;

                                // Add the filename that failed the conversion
                                g.DrawString(String.Format("'{0}'", OriginalName), new Font("Calibri", 44), Brushes.Black, new RectangleF(0, 800, 1654, 200), format);

                                // Add the EmailID as Decioha ID to provide traceability back to the original email
                                g.DrawString(String.Format("Decipha ID: {0}", emailID), new Font("Calibri", 20), Brushes.Black, new RectangleF(0, 2200, 1654, 100), format);

                                // Add the Reason if there is one
                                if (FailureReason != null)
                                {
                                    format.LineAlignment = StringAlignment.Near;
                                    g.DrawString(FailureReason, new Font("Calibri", 54), Brushes.Black, new RectangleF(0, 1100, 1654, 300), format);
                                }

                                // Set the resolution to 200
                                bmp.SetResolution(200, 200);

                                // Default the image conversion options
                                options.BinarisationAlgorithm = BinarisationAlgorithm.OtsuThreshold;
                                options.Resolution = 200;
                                options.BitDepth = 1;

                                // Convert the image
                                Pages = ImageProcessingEngine.Instance.Convert(bmp, options);
                            }

                            // Set the success flag
                            Success = true;
                        }
                        catch (Exception e)
                        {
                            // Add custom data for logging
                            e.Data.Add("FileName", OriginalName);
                            e.Data.Add("Mailbox", Mailbox);

                            // Set the error properties (if there is not already an error message)
                            if (String.IsNullOrEmpty(ErrorMessage))
                                ProcessError(e);

                            // Log the error
                            ConfigLogger.Instance.LogError(emailID, e);
                        }
                    }
                }
                finally
                {
                    // Update the Failure Reason field
                    if (!String.IsNullOrWhiteSpace(FailureReason))
                        Fields["%Document.FailureReason"] = FailureReason;

                    Complete = true;
                }
            }
        }

        #endregion

        #region Private Methods

        private void Reset()
        {
            FileSystemHelper.CleanDirectory(saveToPath, true);
            Pages = null;
        }

        private void SkipConversion()
        {
            try
            {
                // Create a Page object
                var page = new PageInfo()
                {
                    FileName = Path.Combine(options.SaveToPath, "001.tif"),
                    RelativePath = Path.Combine(options.RelativePath, "001.tif"),
                    Width = 1654,
                    Height = 2339,
                    IsBlank = false,
                    Skew = 0.0,
                    Rotation = PageRotation.none
                };

                // Save a "this is intentionally blank..." image
                File.WriteAllBytes(page.FileName, Properties.Resources.EmailImportBlank);

                // Add to the documents pages collection
                Pages = new List<PageInfo>();
                Pages.Add(page);

                // Set the success flag
                Success = true;
            }
            catch (Exception e)
            {
                // Add custom data for logging
                e.Data.Add("FileName", OriginalName);
                e.Data.Add("Mailbox", Mailbox);

                // Set the error properties
                ProcessError(e);

                // Log the error
                ConfigLogger.Instance.LogError(emailID, e);
            }
            finally
            {
                // Update the Failure Reason field
                if (!String.IsNullOrWhiteSpace(FailureReason))
                    Fields["%Document.FailureReason"] = FailureReason;

                Complete = true;
            }
        }

        private void ProcessError(Exception exception)
        {
            Boolean match = false;

            ErrorMessage = exception.ToString();

            var message = exception.Message.ToUpper();

            foreach (var translation in Translations)
            {
                var msg = translation.Message.ToUpper();

                if ((msg.StartsWith("%") && msg.EndsWith("%") && message.Contains(msg.Trim('%'))) ||
                    (msg.StartsWith("%") && message.EndsWith(msg.Trim('%'))) ||
                    (msg.EndsWith("%") && message.StartsWith(msg.Trim('%'))) ||
                    (message.Equals(msg)))
                {
                    FailureReason = translation.Reason;
                    ActionToTake = translation.IsUnprocessable.GetValueOrDefault() ? ErrorHandling.Unprocessable : ErrorHandling.Unknown;
                    ShouldRetry = !translation.IsUnprocessable.GetValueOrDefault();

                    match = true;
                    break;
                }
            }

            if (!match)
            {
                FailureReason = "Unknown error.";
                ActionToTake = ErrorHandling.Unknown;
                ShouldRetry = true;
            }
        }

        #endregion
    }
}
