using Aspose.Email.Mail;
using BitFactory.Logging;
using Decipha.Net.Mail;
using EmailImport.Conversion;
using EmailImport.DataLayer;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace EmailImport
{
    class EmailMonitor : IDisposable
    {
        #region Fields

        [Import]
        private ExportFactory<EmailConverter> EmailConverterFactory = null;

        private ErrorEmailConverter errorbatchprocess = null;
        private int threadLockCount;
        private AutoResetEvent autoEvent;
        private System.Timers.Timer timer;
        private DateTime currentDate;

        #endregion

        #region Constructor

        public EmailMonitor()
        {
            Compose();

            ResetInProgress();

            this.threadLockCount = 0;
            this.currentDate = DateTime.Today;
            this.autoEvent = new AutoResetEvent(true);

            this.timer = new System.Timers.Timer();
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            this.timer.Interval = 1000;
            this.timer.Start();
        }

        private void Compose()
        {
            String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Converters");

            var catalog = new AggregateCatalog(new DirectoryCatalog(path), new AssemblyCatalog(GetType().Assembly));
            var container = new CompositionContainer(catalog);
            container.SatisfyImportsOnce(this);
        }

        #endregion

        #region Event Handlers

        private void timer_Elapsed(object sender, EventArgs e)
        {
            ConfigLogger.Instance.LogDebug("EmailMonitor", String.Format("Timer Thread - Is Alive (EmailConverter.Queued = {0}, ImageProcessingEngine.Queued = {1}, ConcurrencyLevel = {2})", EmailConverter.Queued, ImageProcessingEngine.Instance.Queued, Settings.ConcurrencyLevel));

            if (ImageProcessingEngine.Instance.Queued <= Settings.ConcurrencyLevel)
            {
                if (autoEvent.WaitOne(TimeSpan.Zero, false))
                {
                    threadLockCount = 0;

                    try
                    {
                        ConfigLogger.Instance.LogDebug("EmailMonitor", "Timer Thread - Acquired");

                        // Reset the timer interval
                        timer.Interval = Settings.EmailMonitorInterval.TotalMilliseconds;

                        // Only perform the following tasks if the date changes (ie. every 24 hours)
                        if (currentDate != DateTime.Today)
                        {
                            // Cleanup
                            HouseKeeping();

                            // Reset the current date
                            currentDate = DateTime.Today;
                        }

                        // Perform Error Processing
                        ErrorRejectionAndEscalation();
                        //Process Error Batch with conversion error
                        ErrorBatchOutput();
                        // Normal email processing
                        while (ProcessEmail() != 0)
                        {
                            if (!timer.Enabled)
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        ConfigLogger.Instance.LogError("EmailMonitor", ex);
                    }
                    finally
                    {
                        autoEvent.Set();

                        ConfigLogger.Instance.LogDebug("EmailMonitor", "Timer Thread - Released");
                    }
                }
                else
                {
                    ConfigLogger.Instance.Log((++threadLockCount == 5) ? LogSeverity.Warning : LogSeverity.Debug, "EmailMonitor", "Timer Thread - Locked");
                }
            }
            else
            {
                // Maximum queue size has been reached (no free slots) so lets go to sleep and try again soon
                ConfigLogger.Instance.LogDebug("EmailMonitor", "Maximum concurrency level reached or exceeded...");

                // Set timer interval to 1 second to keep checking
                timer.Interval = 1000;
            }
        }

        #endregion

        #region HouseKeeping

        private void HouseKeeping()
        {
            ConfigLogger.Instance.LogInfo("EmailMonitor", "Performing house keeping tasks...");

            try
            {
                using (var ctx = new EmailImportDataContext())
                {
                    // Loop through each profile
                    foreach (var profile in Settings.MailboxProfiles.Values)
                    {
                        try
                        {
                            // Determine date to purge from based on retention setting
                            var date = DateTime.Today.AddDays(0 - Math.Abs(profile.StorageRetention));

                            // Get emails to be purged
                            var emails = ctx.Emails.Where(e => (e.MailboxGUID == profile.MailboxGUID) &&
                                                               (e.InProgress == null || e.InProgress == false) &&
                                                               (e.Status == "Complete" || e.Status == "Empty" || e.Status == "Rejected" || e.Status == "Ignored") &&
                                                               (e.EndTime != null && e.EndTime.Value < date)).ToList();

                            // Delete each email msg file
                            foreach (var email in emails)
                                File.Delete(email.MessageFilePath);

                            // Delete the email entries from the Emails table
                            ctx.Emails.DeleteAllOnSubmit(emails);

                            // Submit the changes
                            ctx.SubmitChanges();

                            // Log how many emails were deleted
                            ConfigLogger.Instance.LogInfo("EmailMonitor", String.Format("Deleted {0} stored email{1} from {2}.", emails.Count, emails.Count == 1 ? "" : "s", profile.Description));

                            // Remove empty folders from the storage path
                            RemoveEmptyFolders(profile.StoragePath);

                            // Archive path deletion
                            if (!String.IsNullOrWhiteSpace(profile.ArchivePath) && Directory.Exists(profile.ArchivePath))
                            {
                                // Determine date to purge from based on retention setting
                                date = DateTime.Today.AddDays(0 - Math.Abs(profile.ArchiveRetention));

                                // Delete dated archive folders earlier than the retention date
                                foreach (var folder in Directory.GetDirectories(profile.ArchivePath))
                                {
                                    DateTime dateValue;

                                    if (DateTime.TryParseExact(Path.GetFileName(folder), "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out dateValue))
                                    {
                                        if (dateValue < date)
                                        {
                                            Directory.Delete(folder, true);

                                            ConfigLogger.Instance.LogInfo("EmailMonitor", String.Format("Deleted archive folder {0}.", folder));
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            // Add custom data for logging purposes
                            e.Data.Add("MailboxGUID", profile.MailboxGUID);

                            // Log the warning (not error)
                            ConfigLogger.Instance.LogWarning("EmailMonitor", e);
                        }
                    }
                }

                // Remove empty folders from the default storage path
                RemoveEmptyFolders(Settings.DefaultStoragePath);
            }
            catch (Exception e)
            {
                // We don't want housekeeping to cause a failure, but we want someone to 
                // know about it so that they can investigate the issue
                ConfigLogger.Instance.LogWarning("EmailMonitor", e);
            }
        }

        private void RemoveEmptyFolders(String path)
        {
            if (!String.IsNullOrWhiteSpace(path) && Directory.Exists(path))
            {
                foreach (var directory in Directory.EnumerateDirectories(path))
                {
                    if (!Directory.EnumerateFileSystemEntries(directory).Any())
                        Directory.Delete(directory);
                }
            }
        }

        #endregion

        #region Error Processing

        private IEnumerable<Email> GetErrorEmails()
        {
            List<Email> emails = new List<Email>();

            using (var ctx = new EmailImportDataContext())
            {
                foreach (var profile in Settings.MailboxProfiles.Values.Where(m => m.IsActive))
                {
                    try
                    {
                        var errors = from email in ctx.Emails
                                     where (email.MailboxGUID == profile.MailboxGUID) &&
                                           (email.InProgress == null || email.InProgress == false) &&
                                           (email.Status == "Error")
                                     select email;

                        emails.AddRange(errors.ToList().Where(e => e.ProcessedCount.GetValueOrDefault() > profile.MaximumRetries || (e.Errors != null && (String)e.Errors.Attribute("retry") == "false")));
                    }
                    catch (Exception e)
                    {
                        // Add custom data for logging purposes
                        e.Data.Add("MailboxGUID", profile.MailboxGUID);

                        // Log the error
                        ConfigLogger.Instance.LogError("EmailMonitor", e);
                    }
                }
            }

            return emails;
        }
        private IEnumerable<Email> GetConversionErrorEmails()
        {
            List<Email> emails = new List<Email>();

            using (var ctx = new EmailImportDataContext())
            {
                foreach (var profile in Settings.MailboxProfiles.Values.Where(m => m.IsActive))
                {
                    try
                    {
                        var errors = from email in ctx.Emails
                                     where (email.MailboxGUID == profile.MailboxGUID && profile.ProcessErrorBatch== true) &&
                                           (email.InProgress == null || email.InProgress == false) &&
                                           (email.Status == "Rejected"|| email.Status == "Ignored")
                                     select email;

                        emails.AddRange(errors.ToList());
                    }
                    catch (Exception e)
                    {
                        // Add custom data for logging purposes
                        e.Data.Add("MailboxGUID", profile.MailboxGUID);

                        // Log the error
                        ConfigLogger.Instance.LogError("EmailMonitor", e);
                    }
                }
            }

            return emails;
        }
        private void ErrorRejectionAndEscalation()
        {
            try
            {
                // Get available emails
                var emails = GetErrorEmails();

                // Process any emails
                if (emails.Any())
                {
                    // Get the SMTP config from app config
                    var smtpSection = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;

                    // Setup the SmtpClient to send the error emails out with
                    using (var smtp = new SmtpClient(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)))
                    {
                        // Set the timeout to 1 minute
                        smtp.Timeout = 60000;

                        // Return the emails in error
                        foreach (var email in emails)
                        {
                            if (!timer.Enabled)
                                break;

                            try
                            {
                                // Get the mailbox required for the conversion
                                var profile = Settings.MailboxProfiles[email.MailboxGUID.Value];

                            String from = null;
                                MailMessage message = null;

                                // If there are any errors with an action of Escalate then escalate these as opposed to rejecting
                                var escalate = (email.Errors != null && email.Errors.Descendants("Error").Any(e => (String)e.Attribute("action") == "Escalate")) ||
                                               (String.IsNullOrWhiteSpace(profile.TemplateTo) && String.IsNullOrWhiteSpace(profile.TemplateCc) && String.IsNullOrWhiteSpace(profile.TemplateBcc));

                                // Determine if the message will exceed the smtp size limit
                                var oversize = (new FileInfo(email.MessageFilePath)).Length > Settings.SmtpSizeLimit;

                                // Get the from address
                                if (!String.IsNullOrWhiteSpace(profile.TemplateFrom) && !escalate)
                                    from = profile.TemplateFrom;
                                else if (!String.IsNullOrWhiteSpace(smtpSection.From))
                                    from = smtpSection.From;
                                else
                                    from = "Decipha Email Import <noreply@decipha.com.au>";

                                MailMessage error = null;

                                try
                                {
                                    if (!oversize)
                                    {
                                        try
                                        {
                                            var options = new MailMessageLoadOptions();
                                            options.FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking;
                                            options.MessageFormat = email.MessageFilePath.ToLower().EndsWith("msg") ? MessageFormat.Msg : MessageFormat.Eml;

                                            error = MailMessage.Load(email.MessageFilePath, options);
                                        }
                                        catch
                                        { }
                                    }

                                    if (profile.ForwardAsAttachment || escalate || oversize || error == null)
                                    {
                                        // Create a new MailMessage
                                        message = new MailMessage();

                                        // Set the from address
                                        message.From = from;

                                        if (escalate || oversize || error == null)
                                        {
                                            message.To.Add(String.IsNullOrWhiteSpace(profile.EscalationEmail) ? Settings.DefaultEscalationEmail : profile.EscalationEmail);
                                            message.Subject = String.Format("ESCALATION: Unable to process email (EmailID = {0})", email.EmailID);
                                        }
                                        else
                                        {
                                            // Set To, Cc and Bcc recipients
                                            if (!String.IsNullOrWhiteSpace(profile.TemplateTo))
                                                message.To.Add(profile.TemplateTo);

                                            if (!String.IsNullOrWhiteSpace(profile.TemplateCc))
                                                message.CC.Add(profile.TemplateCc);

                                            if (!String.IsNullOrWhiteSpace(profile.TemplateBcc))
                                                message.Bcc.Add(profile.TemplateBcc);

                                            // Set the subject
                                            message.Subject = String.IsNullOrWhiteSpace(profile.TemplateSubject) ? "Unable to process email..." : profile.TemplateSubject;
                                        }

                                        // Set the body
                                        if (oversize)
                                            message.HtmlBody = AddReasonsToHtml(GetEscalationBodyHtml(profile.Description, "not attached due to SMTP size limit"), email.Errors);
                                        else if (error == null)
                                            message.HtmlBody = AddReasonsToHtml(GetEscalationBodyHtml(profile.Description, "not attached as the message failed to load"), email.Errors);
                                        else if (escalate)
                                            message.HtmlBody = AddReasonsToHtml(GetEscalationBodyHtml(profile.Description, null), email.Errors);
                                        else
                                            message.HtmlBody = AddReasonsToHtml(profile.TemplateBodyHtml, email.Errors);

                                        message.IsBodyHtml = true;

                                        // Attach the error email to the template message if not over the smtp size limit
                                        if (!oversize && error != null)
                                            error.ForwardAsAttachment(message);
                                    }
                                    else
                                    {
                                        // Get the html body
                                        var htmlBody = AddReasonsToHtml(profile.TemplateBodyHtml, email.Errors);

                                        // Get the message for inline forwarding
                                        message = error.Forward(from, profile.TemplateTo, profile.TemplateCc, profile.TemplateBcc, htmlBody);

                                        // Override the subject if defined
                                        if (!String.IsNullOrWhiteSpace(profile.TemplateSubject))
                                            message.Subject = profile.TemplateSubject;
                                    }

                                    // Send the message synchronously
                                    smtp.Send(message);
                                }
                                finally
                                {
                                    if (error != null)
                                        error.Dispose();
                                }

                                // Update the status to Forward
                                UpdateStatus(escalate ? EmailStatus.Escalated : EmailStatus.Rejected, email);

                                // Log each email being forwarded for traceability
                                ConfigLogger.Instance.LogInfo(email.EmailID, escalate ? String.Format("Email escalated to {0}.", String.Join(" AND ", message.To.Select(a => a.GetAddressOrDisplayName()))) : "Email rejected as per defined error template.");
                            }
                            catch (Exception e)
                            {
                                ConfigLogger.Instance.LogError(email.EmailID, e);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogError("EmailMonitor", e);
            }
        }
        private void ErrorBatchOutput()
        {
            try
            {
                // Get available emails
                var emails = GetConversionErrorEmails();

                // Process any emails
                if (emails.Any())
                {
                        // Loop through the emails in error
                        foreach (var email in emails)
                        {
                            if (!timer.Enabled)
                                break;
                            try
                            {
                                // Get the mailbox required for the conversion
                                var profile = Settings.MailboxProfiles[email.MailboxGUID.Value];
                                //produce error output in asynchronous thread
                                try
                                {
                                errorbatchprocess = new ErrorEmailConverter();
                                errorbatchprocess.BeginAsync(email, profile);
                                }
                                catch (Exception e)
                                {
                                    // Add custom data for logging purposes
                                    e.Data.Add("MailboxGUID", profile.MailboxGUID);

                                    // Log the error
                                    ConfigLogger.Instance.LogError(email.EmailID, e);

                                    // Update the email status
                                    UpdateStatus(EmailStatus.Error, email, GetErrorXml(e.ToString(), profile.ErrorHandling.Unknown.ToString(), true));
                                }
                                // If there are any errors with an action of Escalate then escalate these as opposed to rejecting
                                //var escalate = (email.Errors != null && email.Errors.Descendants("Error").Any(e => (String)e.Attribute("action") == "Escalate")) ||
                                //                   (String.IsNullOrWhiteSpace(profile.TemplateTo) && String.IsNullOrWhiteSpace(profile.TemplateCc) && String.IsNullOrWhiteSpace(profile.TemplateBcc));

                                MailMessage error = null;

                                try
                                {
                                    try
                                    {
                                        var options = new MailMessageLoadOptions();
                                        options.FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking;
                                        options.MessageFormat = email.MessageFilePath.ToLower().EndsWith("msg") ? MessageFormat.Msg : MessageFormat.Eml;

                                        error = MailMessage.Load(email.MessageFilePath, options);
                                    }
                                    catch
                                    { }

                                }
                                finally
                                {
                                    if (error != null)
                                        error.Dispose();
                                }

                                // Update the status to Forward
                                UpdateStatus(EmailStatus.Complete, email);

                                // Log each email being forwarded for traceability
                                // ConfigLogger.Instance.LogInfo(email.EmailID, escalate ? String.Format("Email escalated to {0}.", String.Join(" AND ", message.To.Select(a => a.GetAddressOrDisplayName()))) : "Email rejected as per defined error template.");
                            }
                            catch (Exception e)
                            {
                                ConfigLogger.Instance.LogError(email.EmailID, e);
                            }
                        }
                    }
                
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogError("EmailMonitor", e);
            }
        }
        private String GetEscalationBodyHtml(String profileDescription, String unattachedReason)
        {
            var html = Properties.Resources.EscalationBodyHtml;

            html = html.Replace("%HOST_NAME%", Environment.MachineName);
            html = html.Replace("%PROFILE_DESCRIPTION%", profileDescription);
            html = html.Replace("%ESCALATION_MESSAGE%", String.IsNullOrWhiteSpace(unattachedReason) ? "The attached email could not be processed for the following reasons:" : String.Format("The email ({0}) could not be processed for the following reasons:", unattachedReason));

            return html;
        }

        private String AddReasonsToHtml(String html, XElement errors)
        {
            if (String.IsNullOrWhiteSpace(html))
                return "<html><head></head><body></body></html>";

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            foreach (var table in doc.DocumentNode.SelectNodes("//table"))
            {
                // Get the template row from the table
                var template = GetTemplateRow(table);

                // Not null template means we have got the correct table
                if (template != null)
                {
                    // Get the parent node to add new rows to
                    var parent = template.ParentNode;

                    // Remove the template row from the table
                    template.Remove();

                    if (errors != null)
                    {
                        // Create and add new rows for each error
                        foreach (var error in errors.Descendants("Error"))
                        {
                            // Clone a new row
                            var row = template.Clone();

                            // Update the cell contents
                            row.InnerHtml = row.InnerHtml.Replace("%ATTACHMENT_NAME%", (String)error.Attribute("fileName") ?? String.Empty);
                            row.InnerHtml = row.InnerHtml.Replace("%REASON%", (String)error.Attribute("reason") ?? String.Empty);
                            row.InnerHtml = row.InnerHtml.Replace("%MESSAGE%", (String)error.Attribute("message") ?? String.Empty);
                            row.InnerHtml = row.InnerHtml.Replace("%ACTION%", (String)error.Attribute("action") ?? String.Empty);

                            // Add to the table
                            parent.AppendChild(row);
                        }
                    }

                    // Return the updated html string
                    return doc.DocumentNode.OuterHtml;
                }
            }

            return html;
        }

        private HtmlNode GetTemplateRow(HtmlNode node)
        {
            var rows = node.SelectNodes("tr");

            if (rows == null)
            {
                foreach (var child in node.ChildNodes)
                {
                    var row = GetTemplateRow(child);

                    if (row != null)
                        return row;
                }
            }
            else
            {
                foreach (var row in rows)
                {
                    if (row.InnerText.Contains("%ATTACHMENT_NAME%"))
                        return row;
                }
            }

            return null;
        }

        #endregion

        #region Email Status Updates

        private void ResetInProgress()
        {
            using (var ctx = new EmailImportDataContext())
            {
                foreach (var email in ctx.Emails.Where(e => e.InProgress == true))
                    email.InProgress = null;

                ctx.SubmitChanges();
            }
        }

        private void UpdateStatus(EmailStatus status, Email email)
        {
            UpdateStatus(status, email, null);
        }

        private void UpdateStatus(EmailStatus status, Email email, XElement errors)
        {
            int count = 0;

            while (true)
            {
                try
                {
                    using (EmailImportDataContext ctx = new EmailImportDataContext())
                    {
                        var eml = ctx.Emails.Single(e => e.EmailID == email.EmailID);
                        eml.Status = status.ToString();

                        if (status == EmailStatus.Error)
                        {
                            eml.ProcessedCount = eml.ProcessedCount.GetValueOrDefault() + 1;
                            eml.StartTime = DateTime.Now;
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

        #region Process Email

        private int ProcessEmail()
        {
            int count = 0;

            try
            {
                // Get available emails
                var emails = GetEmails();

                // Process any emails
                if (emails.Any())
                {
                    ConfigLogger.Instance.LogInfo("EmailMonitor", String.Format("Processing {0} email{1}...", emails.Count(), emails.Count() == 1 ? "" : "s"));

                    // Queue the emails
                    foreach (var email in emails)
                    {
                        // Throttle throughput
                        while (EmailConverter.Queued >= Settings.ConcurrencyLevel)
                        {
                            if (!timer.Enabled)
                                break;

                            Thread.Sleep(100);
                        }

                        // If the timer has been stopped, EmailMonitor has been asked to stop, so stop the processing loop
                        if (!timer.Enabled)
                            break;

                        // Process the next available emails
                        ConfigLogger.Instance.LogDebug(email.EmailID, String.Format("Processing Email-ID {0}...", email.EmailID));

                        // Can't proceed if the mailbox profile isn't found
                        if (!Settings.MailboxProfiles.ContainsKey(email.MailboxGUID.GetValueOrDefault()))
                        {
                            var message = email.MailboxGUID.HasValue ? String.Format("Mailbox Profile not found (MailboxGUID = {0}).", email.MailboxGUID) : "Mailbox Profile not found.";

                            // Log the error
                            ConfigLogger.Instance.LogError(email.EmailID, message);

                            // Update the email status
                            UpdateStatus(EmailStatus.Error, email, GetErrorXml(message, "Escalate", false));

                            continue;
                        }

                        // Get the mailbox profile required for the conversion   
                        var profile = Settings.MailboxProfiles[email.MailboxGUID.Value];

                        try
                        {
                            // Create an EmailConverter
                            var converter = EmailConverterFactory.CreateExport().Value;

                            // Start the conversion process
                            converter.BeginAsync(email, profile);

                            // Increment the processed count
                            count++;
                        }
                        catch (Exception e)
                        {
                            // Add custom data for logging purposes
                            e.Data.Add("MailboxGUID", profile.MailboxGUID);

                            // Log the error
                            ConfigLogger.Instance.LogError(email.EmailID, e);

                            // Update the email status
                            UpdateStatus(EmailStatus.Error, email, GetErrorXml(e.ToString(), profile.ErrorHandling.Unknown.ToString(), true));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogError("EmailMonitor", e);
            }

            return count;
        }

        private XElement GetErrorXml(String message, String action, Boolean allowRetry)
        {
            if (!String.IsNullOrWhiteSpace(message))
                message = new String(message.Where(c => XmlConvert.IsXmlChar(c)).ToArray());

            // Create the error xml based on the exception thrown
            return new XElement("Errors",
                       new XAttribute("retry", allowRetry ? "true" : "false"),
                       new XElement("Error",
                           new XAttribute("key", "MailMessage.msg"),
                           new XAttribute("fileName", "MailMessage.msg"),
                           new XAttribute("reason", "Unknown error."),
                           new XAttribute("message", message),
                           new XAttribute("action", action)
                           )
                       );
        }

        private IEnumerable<Email> GetEmails()
        {
            List<Email> emails = new List<Email>();

            using (var ctx = new EmailImportDataContext())
            {
                foreach (var profile in Settings.MailboxProfiles.Values.Where(m => m.IsActive))
                {
                    var retryTime = DateTime.Now.Subtract(profile.TimeBetweenRetries);

                    try
                    {
                        // Get email with status == null || (status == error && lastProcessed > timespan)
                        emails.AddRange((from email in ctx.Emails
                                         where (email.MailboxGUID == profile.MailboxGUID) &&
                                               (email.InProgress == null || email.InProgress == false) &&
                                               ((email.Status == null) ||
                                                (email.Status == "Error" && (email.EndTime == null || email.EndTime.Value < retryTime)))
                                         select email));
                    }
                    catch (Exception e)
                    {
                        // Add custom data for logging purposes
                        e.Data.Add("MailboxGUID", profile.MailboxGUID);

                        // Log the error
                        ConfigLogger.Instance.LogError("EmailMonitor", e);
                    }
                }
            }

            return emails.OrderBy(e => e.EmailID);
        }

        #endregion

        #region Pause/Resume

        public void Pause()
        {
            if (autoEvent != null)
            {
                timer.Stop();
                autoEvent.WaitOne();
            }
        }

        public void Resume()
        {
            if (autoEvent != null)
            {
                autoEvent.Set();
                timer.Start();
            }
        }

        #endregion

        #region IDisposable Implementation

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (autoEvent != null)
                    {
                        timer.Stop();
                        autoEvent.WaitOne();

                        timer.Dispose();
                        timer = null;

                        autoEvent.Close();
                        autoEvent = null;
                    }
                }

                disposed = true;
            }
        }

        #endregion
    }
}
