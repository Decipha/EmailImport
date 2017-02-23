using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Aspose.Email;
using Aspose.Email.Mail;
using BitFactory.Logging;
using Decipha.Net.Mail;
using EmailImport.Conversion;
using EmailImport.Conversion.Configuration;
using EmailImport.DataLayer;
using Roslyn.Scripting;

namespace EmailImport
{
    [Export]
    public class EmailConverter
    {
        #region Static Methods

        static int count = 0;

        public static int Queued { get { return count; } }

        static EmailConverter()
        {
            License license = new License();
            license.SetLicense("Aspose.Total.lic");
        }

        public static void WaitOnComplete()
        {
            WaitOnComplete(Timeout.Infinite);
        }

        public static Boolean WaitOnComplete(int millisecondsTimeout)
        {
            int timer = millisecondsTimeout;

            while (count != 0)
            {
                if (millisecondsTimeout != Timeout.Infinite)
                {
                    if (timer <= 0)
                        return false;

                    timer -= 250;
                }

                Thread.Sleep(250);
            }

            return true;
        }

        #endregion

        #region Composition

        [ImportMany]
        private IEnumerable<Lazy<IConverter, IConverterMetadata>> Converters = null;

        #endregion

        #region Member Fields

        private MailMessage message;
        private MailboxProfile profile;
        private Email email;

        private Session session = null;
        private ScriptContext context = null;

        #endregion

        #region Database Methods

        private void UpdateStatus(EmailStatus status)
        {
            UpdateStatus(status, null);
        }

        private void UpdateStatus(EmailStatus status, XElement errors)
        {
            int count = 0;

            while (true)
            {
                try
                {
                    using (EmailImportDataContext ctx = new EmailImportDataContext())
                    {
                        var eml = ctx.Emails.Single(e => e.EmailID == email.EmailID);

                        if (status == EmailStatus.InProgress)
                        {
                            eml.InProgress = true;
                            eml.StartTime = DateTime.Now;
                            eml.EndTime = null;
                        }
                        else
                        {
                            eml.Status = status.ToString();
                            eml.ProcessedCount = eml.ProcessedCount.GetValueOrDefault() + 1;
                            eml.EndTime = DateTime.Now;
                            eml.InProgress = null;
                            eml.Errors = errors;
                        }

                        ctx.SubmitChanges();
                    }

                    break;
                }
                catch (Exception e)
                {
                    // Increment the retry counter
                    count++;

                    // Log the exception
                    ConfigLogger.Instance.Log(count < 5 ? LogSeverity.Warning : LogSeverity.Error, email.EmailID, e);

                    // Retry up to 5 times
                    if (count >= 5)
                        break;

                    // Sleep for a fraction
                    Thread.Sleep(50);
                }
            }
        }

        #endregion

        #region Public Methods

        public void BeginAsync(Email email, MailboxProfile profile)
        {
            if (email == null)
                throw new ArgumentNullException("email", "Value cannot be null.");

            if (profile == null)
                throw new ArgumentNullException("profile", "Value cannot be null.");

            Initialise(email, profile);

            CreateScriptSession();
            ExecuteScript("MailMessage_Loaded");

            if (context != null && context.IgnoreMessage)
            {
                UpdateStatus(EmailStatus.Ignored);

                ConfigLogger.Instance.LogInfo(email.EmailID, "Message ignored via script.");
            }
            else
            {
                UpdateStatus(EmailStatus.InProgress);

                ManualResetEvent waiter = new ManualResetEvent(false);

                System.Threading.Tasks.Task.Run(() =>
                {
                    Worker(waiter);
                });

                waiter.WaitOne();
            }
        }

        #endregion

        #region Scripting

        private void CreateScriptSession()
        {
            if (!String.IsNullOrWhiteSpace(profile.Script))
            {
                try
                {
                    context = new ScriptContext() { Message = message };

                    session = ScriptHelper.Instance.CreateSession(context);
                    session.Execute(profile.Script);
                }
                catch (Exception e)
                {
                    ConfigLogger.Instance.LogError(email.EmailID, e);

                    context = null;
                    session = null;
                }
            }
        }

        private void ExecuteScript(String methodName)
        {
            try
            {
                ScriptHelper.Instance.Execute(session, profile, methodName);
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogError(email.EmailID, e);
            }
        }

        #endregion

        #region Private Methods

        private void Initialise(Email email, MailboxProfile profile)
        {
            // Assign parameters to class members
            this.email = email;
            this.profile = profile;

            // Load the Mail Message
            var options = new MailMessageLoadOptions();
            options.FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking;
            options.MessageFormat = email.MessageFilePath.ToLower().EndsWith("msg") ? MessageFormat.Msg : MessageFormat.Eml;

            this.message = MailMessage.Load(email.MessageFilePath, options);
        }

        private void Worker(Object waiter)
        {
            Batch batch = null;

            Interlocked.Increment(ref count);

            ((ManualResetEvent)waiter).Set();

            try
            {
                var subject = CreditCardHelper.ExistsCCNumber(message.Subject) ? CreditCardHelper.MaskCCNumbers(message.Subject, '#') : message.Subject;

                ConfigLogger.Instance.LogInfo(email.EmailID, "Batch Conversion Started...");
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> Profile:      {0}", profile.Description));
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> Mailbox GUID: {0}", profile.MailboxGUID));
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> Message-ID:   {0}", message.MessageId));
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> Subject:      {0}", subject.Replace("\"", "'")));
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> From:         {0}", message.From.GetAddressOrDisplayName().Replace("\"", "'")));
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> Received:     {0}", message.DateReceived()));
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> Batch Number: {0}", email.BatchNumber));
                ConfigLogger.Instance.LogInfo(email.EmailID, String.Format(" >> Email-ID:     {0}", email.EmailID));

                batch = new Batch(email, message, profile);

                if (batch.Documents.Any())
                {
                    var startTime = DateTime.Now;

                    foreach (var document in batch.Documents)
                    {
                        // Get the filetype config element if one is defined
                        var fileType = profile.FileTypes.FirstOrDefault(f => Regex.IsMatch(document.OriginalName, f.Pattern??string.Empty, RegexOptions.IgnoreCase));

                        // Setup common properties
                        document.Resolution = profile.Resolution;
                        document.BitDepth = (fileType != null && fileType.BitDepth.GetValueOrDefault() > 0) ? fileType.BitDepth.GetValueOrDefault() : profile.BitDepth;
                        document.Passthrough = (profile.AttachmentConversion == AttachmentConversion.Passthrough && !document.IsBody) || (fileType != null && fileType.Passthrough.GetValueOrDefault());
                        document.Mailbox = profile.ImapUserName;
                        document.BinarisationAlgorithm = (fileType != null) ? fileType.BinarisationAlgorithm.GetValueOrDefault(profile.BinarisationAlgorithm) : profile.BinarisationAlgorithm;
                        document.AutoDeskew = (fileType != null) ? fileType.AutoDeskew : null;
                        document.AutoRotate = (fileType != null) ? fileType.AutoRotate : null;
                        document.RemoveFaxHeader = profile.RemoveFaxHeader;
                        document.ProcessAs = (fileType != null) ? fileType.ProcessAs : null;
                        document.PdfConversion = profile.PdfConversion;
                    
                        if (!document.Passthrough)
                        {
                            document.ErrorHandling.Unknown = profile.ErrorHandling.Unknown;
                            document.ErrorHandling.Unsupported = profile.ErrorHandling.Unsupported;
                            document.ErrorHandling.Unprocessable = profile.ErrorHandling.Unprocessable;
                            
                            // Check for error handling overrides
                            if (email.Errors != null)
                            {
                                var error = email.Errors.Elements().Where(e => (String)e.Attribute("key") == Path.GetFileName(document.AttachmentFile)).FirstOrDefault();

                                if (error != null)
                                {
                                    ErrorHandlingActions value;

                                    if (Enum.TryParse<ErrorHandlingActions>((String)error.Attribute("override"), out value))
                                    {
                                        document.ErrorHandling.Unknown = value;
                                        document.ErrorHandling.Unsupported = value;
                                        document.ErrorHandling.Unprocessable = value;
                                    }

                                    // Set ProcessAs
                                    var processAs = (String)error.Attribute("processAs");

                                    if (!String.IsNullOrWhiteSpace(processAs))
                                        document.ProcessAs = processAs;
                                }
                            }

                            // Get the converter
                            document.Converter = GetConverter(document);
                        }
                    }

                    // Convert documents in parallel
                    Parallel.ForEach(batch.Documents, document =>
                        {
                            document.Convert();
                        });

                    // XElement to store errors
                    XElement errors = null;

                    // If there are any documents containing error message, add these to an xml document and add to the database
                    if (batch.Documents.Any(d => !String.IsNullOrWhiteSpace(d.ErrorMessage)))
                    {
                        errors = new XElement("Errors",
                                    new XAttribute("retry", batch.Documents.Any(d => d.ShouldRetry && !String.IsNullOrWhiteSpace(d.ErrorMessage))),
                                    from document in batch.Documents.Where(d => !String.IsNullOrEmpty(d.ErrorMessage))
                                    select new XElement("Error",
                                        new XAttribute("key", Path.GetFileName(document.AttachmentFile)),
                                        new XAttribute("fileName", document.OriginalName),
                                        new XAttribute("reason", document.FailureReason),
                                        new XAttribute("message", RemoveInvalidXmlChars(document.ErrorMessage)),
                                        new XAttribute("action", document.ActionToTake.Value)
                                        )
                                    );
                    }

                    // Remove all documents where ActionToTake is Ignore
                    batch.Documents.RemoveAll(d => d.ActionToTake.HasValue && d.ActionToTake.Value == ErrorHandlingActions.Ignore);
                    
                    // If IgnoreErrors is false and one or more documents contains errors, 
                    if (batch.Documents.Any(d => d.Success == false))
                    {
                        UpdateStatus(EmailStatus.Error, errors);

                        // Delete the working folder
                        try
                        {
                            FileSystemHelper.DeleteDirectory(batch.OutputPath, true);
                        }
                        catch (Exception e)
                        {
                            ConfigLogger.Instance.LogWarning(email.EmailID, e);
                        }

                        ConfigLogger.Instance.LogInfo(email.EmailID, "Batch Conversion Failed.");
                    }
                    else
                    {
                        ConfigLogger.Instance.LogInfo(email.EmailID, String.Format("Documents Converted in {0:c}.", DateTime.Now.Subtract(startTime)));

                        // Check and remove blank pages (and documents) if enabled
                        if (profile.RemoveBlankPages)
                        {
                            // No need to check the email body as they will always contain the header details
                            // Blank check only applies to tif images (pdf document via passthrough excluded)
                            foreach (var document in batch.Documents.Where(d => d.Source == DocumentSource.Attachment))
                            {
                                // Remove pages that are blank
                                var blanks = document.Pages.RemoveAll(p => p.IsBlank);

                                // If any blanks, log the number removed
                                if (blanks > 0)
                                    ConfigLogger.Instance.LogInfo(email.EmailID, String.Format("Removed {0} blank page{1} from document {2}.", blanks, (blanks > 1) ? "s" : "", document.RelativePath));
                            }
                        }

                        // Apply any filtering rules based on resulting image attributes
                        foreach (var document in batch.Documents)
                        {
                            // Get the filetype config element if one is defined
                            var fileType = profile.FileTypes.FirstOrDefault(f => Regex.IsMatch(document.OriginalName, f.Pattern??string.Empty, RegexOptions.IgnoreCase));

                            // Only proceed if there is a file type element for the current extension
                            if (fileType != null)
                            {
                                // Remove all pages that fall under the Minimum Pixel threshold
                                var filtered = document.Pages.RemoveAll(p => p.Area < fileType.MinPixels);

                                // If any pages were filtered out, log how many
                                if (filtered > 0)
                                    ConfigLogger.Instance.LogInfo(email.EmailID, String.Format("Filtered out {0} page{1} from document {2}.", filtered, (filtered > 1) ? "s" : "", document.RelativePath));
                            }
                        }

                        // Remove any empty documents following blank page removal and filtering
                        int empty = batch.Documents.RemoveAll(d => !d.Pages.Any());

                        // If any blanks, log the number removed
                        if (empty > 0)
                            ConfigLogger.Instance.LogInfo(email.EmailID, String.Format("Removed {0} empty document{1} from batch.", empty, (empty > 1) ? "s" : ""));

                        // Remove Email Body on batch empty (if enabled)
                        if (profile.BodyConversion == BodyConversion.OnBatchEmpty)
                        {
                            // Check if the count of body documents equals the total count
                            // If not, this means there is at least 1 non-body document therefore
                            // the body documents can be removed from the batch
                            if (batch.Documents.Count(d => d.IsBody) != batch.Documents.Count)
                            {
                                // Remove the email body from the batch
                                batch.Documents.RemoveAll(d => d.IsBody);
                                ConfigLogger.Instance.LogInfo(email.EmailID, "Removed email body from batch.");
                            }
                        }

                        // Check for empty batch after removing documents
                        if (batch.Documents.Any())
                        {
                            // Create the output
                            batch.CreateOutput();

                            // Update the tracking table to mark the email as complete
                            UpdateStatus(EmailStatus.Complete, errors);

                            ConfigLogger.Instance.LogInfo(email.EmailID, "Batch Conversion Completed.");
                        }
                        else
                        {
                            HandleEmptyBatch(batch);
                        }
                    }
                }
                else
                {
                    HandleEmptyBatch(batch);
                }
            }
            catch (Exception e)
            {
                // Add custom data for logging purposes
                e.Data.Add("MailboxGUID", profile.MailboxGUID);

                // Log the error
                ConfigLogger.Instance.LogError(email.EmailID, e);

                // Create the error xml based on the exception thrown
                var errors = new XElement("Errors",
                                 new XAttribute("retry", "true"),
                                 new XElement("Error",
                                     new XAttribute("key", "MailMessage.msg"),
                                     new XAttribute("fileName", "MailMessage.msg"),
                                     new XAttribute("reason", "Unknown error."),
                                     new XAttribute("message", RemoveInvalidXmlChars(e.ToString())),
                                     new XAttribute("action", profile.ErrorHandling.Unknown)
                                     )
                                 );

                // If the batch was created, include document level errors
                if (batch != null)
                {
                    errors.Element("Errors").Add(from document in batch.Documents.Where(d => !String.IsNullOrWhiteSpace(d.ErrorMessage))
                                                 select new XElement("Error",
                                                     new XAttribute("key", Path.GetFileName(document.AttachmentFile)),
                                                     new XAttribute("fileName", document.OriginalName),
                                                     new XAttribute("reason", document.FailureReason),
                                                     new XAttribute("message", RemoveInvalidXmlChars(document.ErrorMessage)),
                                                     new XAttribute("action", document.ActionToTake.Value)
                                                     )
                                                 );
                }

                // Update the email status
                UpdateStatus(EmailStatus.Error, errors);

                // Delete the working folder
                if (batch != null)
                {
                    try
                    {
                        FileSystemHelper.DeleteDirectory(batch.OutputPath, true);
                    }
                    catch (Exception ex)
                    {
                        ConfigLogger.Instance.LogWarning(email.EmailID, ex);
                    }
                }

                ConfigLogger.Instance.LogInfo(email.EmailID, "Batch Conversion Failed.");
            }
            finally
            {
                Interlocked.Decrement(ref count);
            }
        }

        private void HandleEmptyBatch(Batch batch)
        {
            // Update the batches status to empty
            UpdateStatus(EmailStatus.Empty);

            // Batch contains no documents so delete the empty folder
            if (Directory.Exists(batch.OutputPath))
                Directory.Delete(batch.OutputPath, true);

            ConfigLogger.Instance.LogInfo(email.EmailID, "Batch contains no documents, message ignored.");
        }

        private String RemoveInvalidXmlChars(String xml)
        {
            if (String.IsNullOrWhiteSpace(xml))
                return xml;

            return new String(xml.Where(c => XmlConvert.IsXmlChar(c)).ToArray());
        }

        private IConverter GetConverter(Document document)
        {
            var converter = GetConverter(document.ProcessAs ?? document.Extension);

            if (converter == null)
            {
                document.ProcessAs = DetermineExtensionFromFileSignature(document.AttachmentFile);

                if (document.ProcessAs != null)
                    converter = GetConverter(document.ProcessAs);
            }

            return converter;
        }

        private IConverter GetConverter(String format)
        {
            var export = Converters.FirstOrDefault(c => c.Metadata.Format.Contains(format.ToUpper()));

            if (export != null)
                return export.Value;
            else
                return null;
        }

        private String DetermineExtensionFromFileSignature(String fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                if (FileSignatures.IsPdf(stream))
                    return "PDF";

                if (FileSignatures.IsJpg(stream))
                    return "JPG";

                if (FileSignatures.IsTif(stream))
                    return "TIF";

                if (FileSignatures.IsPng(stream))
                    return "PNG";

                if (FileSignatures.IsGif(stream))
                    return "GIF";

                if (FileSignatures.IsBmp(stream))
                    return "BMP";
            }

            return null;
        }

        #endregion
    }
}