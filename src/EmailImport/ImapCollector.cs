using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using Aspose.Email;
using Aspose.Email.Mail;
using BitFactory.Logging;
using Decipha.Net.Mail;
using EmailImport.Conversion;
using EmailImport.Conversion.Configuration;
using EmailImport.DataLayer;

namespace EmailImport
{
    class ImapCollector : IDisposable
    {
        #region Fields

        private int threadLockCount;
        private DateTime currentDate;
        private AutoResetEvent autoEvent;
        private System.Timers.Timer timer;

        #endregion

        #region Constructor

        public ImapCollector()
        {
            this.threadLockCount = 0;
            this.currentDate = DateTime.Today;
            this.autoEvent = new AutoResetEvent(true);

            this.timer = new System.Timers.Timer();
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            this.timer.Interval = 1000;
            this.timer.Start();
        }

        #endregion
        
        #region Event Handlers

        private void timer_Elapsed(object sender, EventArgs e)
        {
            ConfigLogger.Instance.LogDebug("ImapCollector", "Timer Thread - Is Alive");

            if (autoEvent.WaitOne(TimeSpan.Zero, false))
            {
                threadLockCount = 0;

                try
                {
                    ConfigLogger.Instance.LogDebug("ImapCollector", "Timer Thread - Acquired");

                    // Reset the timer interval
                    timer.Interval = Settings.ImapCollectorInterval.TotalMilliseconds;

                    // Only perform the following tasks if the date changes (ie. every 24 hours)
                    if (currentDate != DateTime.Today)
                    {
                        // Cleanup
                        HouseKeeping();

                        // Reset the current date
                        currentDate = DateTime.Today;
                    }

                    // Download Emails 
                    while (DownloadEmail() != 0)
                    {
                        if (!timer.Enabled)
                            break;
                    }
                }
                catch (OutOfMemoryException ex)
                {
                    ConfigLogger.Instance.LogCritical("ImapCollector", ex);

                    RestartService();
                }
                catch (Exception ex)
                {
                    ConfigLogger.Instance.LogError("ImapCollector", ex);
                }
                finally
                {
                    autoEvent.Set();

                    ConfigLogger.Instance.LogDebug("ImapCollector", "Timer Thread - Released");
                }
            }
            else
            {
                ConfigLogger.Instance.Log((++threadLockCount == 5) ? LogSeverity.Warning : LogSeverity.Debug, "ImapCollector", "Timer Thread - Locked");
            }
        }

        private void RestartService()
        {
            try
            {
                var serviceName = String.Format("Email Import{0}", Program.EnableProcess ? "" : " - Collect");
                ConfigLogger.Instance.LogCritical("ImapCollector", String.Format("{0} service restarting due to memory overuse.", serviceName));
                Process.Start("cmd.exe", String.Format("/C NET STOP \"{0}\" & NET START \"{0}\"", serviceName));

                // Wait till we receive the stop signal (up to 10 seconds)
                for (int i = 0; i < 100; i++)
                {
                    if (!timer.Enabled)
                        break;

                    Thread.Sleep(100);
                }
            }
            catch
            { }
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

        #region Download Email

        private int DownloadEmail()
        {
            int count = 0;

            // Order the mailboxes by defined priority (or default of 5)
            var profiles = from profile in Settings.MailboxProfiles.Values
                           orderby profile.Priority
                           select profile;

            // Loop through each mailbox defined in the configuration file
            foreach (var profile in profiles)
            {
                // If the timer has been stopped, ImapCollector has been asked to stop, so stop the processing loop
                if (!timer.Enabled)
                    break;

                // Ignore if no imap host is defined
                if (String.IsNullOrWhiteSpace(profile.ImapHost))
                    continue;

                ConfigLogger.Instance.LogDebug("ImapCollector", String.Format("Processing mailbox {0}...", profile.Description));

                // Get the storage path (base path)
                var storagePath = String.IsNullOrWhiteSpace(profile.StoragePath) ? Settings.DefaultStoragePath : profile.StoragePath;

                // Log a warning and continue onto the next profile if no storage path available
                if (String.IsNullOrWhiteSpace(storagePath))
                {
                    ConfigLogger.Instance.LogWarning("ImapCollector", "Storage Path configuration not found.");
                    continue;
                }

                try
                {
                    // Create an imap session (auto connect/login/select)
                    using (Imap imap = new Imap(profile))
                    {
                        // Download the actual mail message
                        count += DownloadEmail(imap, profile, storagePath);

                        // Expunge any deleted messages
                        imap.ExpungeMessages();
                    }
                }
                catch (OutOfMemoryException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    ConfigLogger.Instance.LogError("ImapCollector", e);
                }
            }

            return count;
        }

        private int DownloadEmail(Imap imap, MailboxProfile profile, String storagePath)
        {
            int count = 0;

            // Build the MailQuery
            var query = new MailQuery(String.IsNullOrEmpty(profile.ImapQuery) ? "('Deleted' = 'False')" : String.Format("({0}&('Deleted' = 'False'))", profile.ImapQuery));

            // Get all messages matching to the query
            var infos = imap.ListMessages(query);

            // If there are any messages to process, then process them
            if (infos.Any())
            {
                ConfigLogger.Instance.LogInfo("ImapCollector", String.Format("Downloading {0} message{1} from {2}.", infos.Count, infos.Count == 1 ? "" : "s", profile.Description));

                // Download each message
                foreach (var info in infos)
                {
                    if (!timer.Enabled)
                        break;

                    // Just check to ensure its valid
                    if (info.Deleted || String.IsNullOrWhiteSpace(info.UniqueId))
                        continue;

                    // Track the start time for debug purposes
                    var start = DateTime.Now;

                    MailMessage message = null;

                    try
                    {
                        // Download the message
                        message = imap.FetchMessage(info.UniqueId);

                        // Calculate the time taken to fetch the message
                        var fetchTime = DateTime.Now.Subtract(start);

                        // Process the message (So long as the fetch succeeded)
                        if (message != null)
                        {
                            // Setup the data context
                            using (var ctx = new EmailImportDataContext())
                            {
                                long emailID = 0;

                                // Truncate the subject to avoid data commit errors
                                message.Subject = Truncate(message.Subject, 500);

                                // Check for duplicate
                                if (IsDuplicate(ctx, profile.MailboxGUID, message, ref emailID))
                                {
                                    // Log the duplicate error
                                    ConfigLogger.Instance.LogWarning("ImapCollector", String.Format("Message already downloaded, moved to duplicate folder (existing EmailID = {0}).", emailID));

                                    // Move the message to the duplicate sub folder
                                    imap.MoveMessage(info.UniqueId, "Duplicate", true, false);
                                }
                                else
                                {
                                    // Create an instance of the email database object
                                    var email = new Email();

                                    // Assign properties
                                    email.MailboxGUID = profile.MailboxGUID;
                                    email.DateSent = message.DateSent();
                                    email.DateReceived = message.DateReceived();
                                    email.From = message.From.GetAddressOrDisplayName();
                                    email.MessageID = message.MessageId;
                                    if (CreditCardHelper.ExistsCCNumber(message.Subject))
                                        email.Subject = CreditCardHelper.MaskCCNumbers(message.Subject, '#');
                                    else
                                        email.Subject = message.Subject;
                                    email.Timestamp = DateTime.Now;

                                    // Create the dated storage path
                                    var path = Path.Combine(storagePath, email.Timestamp.Value.ToString("yyyyMMdd"));
                                    FileSystemHelper.CreateDirectory(path);

                                    // Insert the new record
                                    ctx.Emails.InsertOnSubmit(email);

                                    // Submit the email (we need to get the email ID)
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        // Initial submit of changes
                                        ctx.SubmitChanges();

                                        // Build the mail message file name
                                        email.MessageFilePath = Path.Combine(path, String.Format("{0:00000000}.eml", email.EmailID));

                                        // Save to disk (delete anything that already exists)
                                        message.Save(email.MessageFilePath, MessageFormat.Eml);

                                        // Get the batch number - THIS SHOULD NEVER HAPPEN IN A MULTI THREAD SCENARIO WITHOUT A LOCK
                                        var batchNumber = ctx.BatchNumbers.SingleOrDefault(b => b.Group == profile.Group);

                                        // If there is no batchNumber defined yet, create and insert one
                                        if (batchNumber == null)
                                        {
                                            batchNumber = new BatchNumber();
                                            batchNumber.Group = profile.Group;
                                            ctx.BatchNumbers.InsertOnSubmit(batchNumber);
                                        }

                                        // Init to 0 if null
                                        if (!batchNumber.Value.HasValue)
                                            batchNumber.Value = 0;

                                        // Set the new batch number to this email
                                        email.BatchNumber = String.Format(String.IsNullOrWhiteSpace(profile.BatchNumberFormat) ? "{0:00000000}" : profile.BatchNumberFormat, ++batchNumber.Value);

                                        // Final submit of updates
                                        ctx.SubmitChanges();

                                        // Finally, commit to the database
                                        scope.Complete();
                                    }

                                    // Move the email to the archive (if this fails, but the download is complete this
                                    // will just result in a duplicate next time round if the deleted flag is not set)
                                    imap.MoveMessage(info.UniqueId, "Archive", true, false);

                                    // Log message level download stats
                                    ConfigLogger.Instance.LogDebug("ImapCollector", String.Format("Message downloaded (EmailID = {0}, Fetch Time = {1}s, Total Time = {2}s).", email.EmailID, (int)fetchTime.TotalSeconds, (int)DateTime.Now.Subtract(start).TotalSeconds));

                                    // Increment the download count
                                    count++;
                                }
                            }
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        ConfigLogger.Instance.LogError("ImapCollector", e);
                    }
                    finally
                    {
                        if (message != null)
                            message.Dispose();
                    }
                }
            }

            return count;
        }

        private String Truncate(String value, int maxLength)
        {
            if (!String.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                return value.Substring(0, maxLength);
            }

            return value;
        }

        #endregion

        #region HouseKeeping

        private void HouseKeeping()
        {
            ConfigLogger.Instance.LogInfo("ImapCollector", "Performing house keeping tasks...");

            // Save the default timeout value
            var timeout = Imap.Timeout;

            try
            {
                // Set timeout to 5 mins for housekeeping
                Imap.Timeout = 300000;

                // Loop through each mailbox defined in the configuration file
                foreach (var profile in Settings.MailboxProfiles.Values)
                {
                    // Ignore if no imap host is defined
                    if (String.IsNullOrWhiteSpace(profile.ImapHost))
                        continue;

                    // Determine date to purge from based on retention setting
                    var date = DateTime.Today.AddDays(0 - Math.Abs(profile.ImapRetention));

                    try
                    {
                        // Create an imap session (auto connect/login/select)
                        using (Imap imap = new Imap(profile))
                        {
                            // Build the MailQuery
                            var query = new MailQuery(String.IsNullOrEmpty(profile.ImapQuery) ? String.Format("('InternalDate' < '{0:dd-MMM-yyyy}')", date) : String.Format("({0}&('InternalDate' < '{1:dd-MMM-yyyy}'))", profile.ImapQuery, date));

                            // Select the Archive folder
                            if (imap.SelectFolderIfExists("Archive", true))
                            {
                                // Find all messages matching the query
                                var infos = imap.ListMessages(query);

                                // Delete all of the messages found
                                foreach (var info in infos)
                                    imap.DeleteMessage(info.UniqueId, false);

                                ConfigLogger.Instance.LogInfo("ImapCollector", String.Format("Deleted {0} message{1} from {2}.", infos.Count, infos.Count == 1 ? "" : "s", profile.Description));
                            }

                            // Expunge the deleted messages
                            imap.ExpungeMessages();
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        throw;
                    }
                    catch (Exception e)
                    {
                        // Add custom data for logging purposes
                        e.Data.Add("MailboxGUID", profile.MailboxGUID);

                        // Log the warning (not error)
                        ConfigLogger.Instance.LogWarning("ImapCollector", e);
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                // We don't want housekeeping to cause a failure, but we want someone to 
                // know about it so that they can investigate the issue
                ConfigLogger.Instance.LogWarning("ImapCollector", e);
            }
            finally
            {
                // Reset the original timeout value
                Imap.Timeout = timeout;
            }
        }

        #endregion

        #region Database Methods

        private Boolean IsDuplicate(EmailImportDataContext ctx, Guid guid, MailMessage message, ref long emailID)
        {
            var subject = CreditCardHelper.ExistsCCNumber(message.Subject) ? CreditCardHelper.MaskCCNumbers(message.Subject, '#') : message.Subject;

            var email = ctx.Emails.FirstOrDefault(e => Object.Equals(e.MailboxGUID, guid) &&
                                                       Object.Equals(e.MessageID, message.MessageId) &&
                                                       Object.Equals(e.From, message.From.GetAddressOrDisplayName()) &&
                                                       Object.Equals(e.DateSent, message.DateSent()) &&
                                                       Object.Equals(e.DateReceived, message.DateReceived()) &&
                                                       Object.Equals(e.Subject, subject));

            if (email != null)
                emailID = email.EmailID;

            return (email != null);
        }

        #endregion

        #region Imap Helper Class

        class Imap : Decipha.Net.Mail.Imap
        {
            public Imap(MailboxProfile profile)
                : base(profile.ImapHost, profile.ImapPort, profile.ImapUserName, profile.ImapPassword, profile.ImapFolder)
            { }
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
