using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows.Forms;
using System.Xml.Serialization;
using Aspose.Email;
using Aspose.Email.Mail;
using Aspose.Email.Outlook.Pst;
using Decipha.Net.Mail;
using EmailImport.Conversion.Configuration;

namespace EmailImport.FileUpload
{
    public partial class MainForm : Form
    {
        String defaultStoragePath = null;
        List<MailboxProfile> profiles = new List<MailboxProfile>();

        public MainForm()
        {
            License license = new License();
            license.SetLicense("Aspose.Total.lic");

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using (var ctx = new EmailImportDataContext())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MailboxProfile));

                foreach (var mailbox in ctx.Mailboxes)
                {
                    using (TextReader reader = new StringReader(mailbox.ProfileObject))
                    {
                        var profile = (MailboxProfile)serializer.Deserialize(reader);

                        if (profile.Enabled)
                            profiles.Add(profile);
                    }
                }

                var setting = ctx.Settings.SingleOrDefault(s => s.Name == "DefaultStoragePath");
                defaultStoragePath = (setting == null) ? null : setting.Value;

                if (!Directory.Exists(defaultStoragePath))
                    Directory.CreateDirectory(defaultStoragePath);
            }

            comboBoxProfile.Items.AddRange(profiles.ToArray());
        }

        private void buttonCollect_Click(object sender, EventArgs e)
        {
            // Select a mailbox profile to load into
            if (comboBoxProfile.SelectedItem == null)
            {
                MessageBox.Show(this, "Please select a mailbox profile.", "Mail Message File Collector", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Enter a path to collect from
            if (radioButtonMSG.Checked && !Directory.Exists(textBoxPath.Text))
            {
                MessageBox.Show(this, "Please enter a valid collection path.", "Mail Message File Collector", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Enter a valid PST file
            if (radioButtonPST.Checked && !File.Exists(textBoxPath.Text))
            {
                MessageBox.Show(this, "Please enter a valid pst file name.", "Mail Message File Collector", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Get the profile (from the combo box)
            var profile = (MailboxProfile)comboBoxProfile.SelectedItem;

            // Get the storage path (base path)
            var storagePath = String.IsNullOrWhiteSpace(profile.StoragePath) ? defaultStoragePath : profile.StoragePath;

            // Do not continue if there is no storage path defined
            if (String.IsNullOrEmpty(storagePath))
            {
                MessageBox.Show(this, "Storage path not defined.", "Mail Message File Collector", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            textBoxPath.Enabled = false;
            comboBoxProfile.Enabled = false;
            buttonBrowse.Enabled = false;
            buttonCollect.Enabled = false;

            progressBar.Visible = true;
            Height += progressBar.Height;

            backgroundWorker.RunWorkerAsync(new object[] { profile, storagePath });
        }

        private Boolean UploadMessage(String fileName, MailboxProfile profile, String storagePath)
        {
            var options = new MailMessageLoadOptions();
            options.FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking;
            options.MessageFormat = fileName.ToLower().EndsWith("msg") ? MessageFormat.Msg : MessageFormat.Eml;

            using (MailMessage message = MailMessage.Load(fileName, options))
            using (EmailImportDataContext ctx = new EmailImportDataContext())
            {
                // Truncate the subject to avoid data commit errors
                message.Subject = Truncate(message.Subject, 500);

                if (!IsDuplicate(ctx, profile.MailboxGUID, message))
                {
                    // Create an instance of the email database object
                    var email = new Email();

                    // Assign properties
                    email.MailboxGUID = profile.MailboxGUID;
                    email.DateSent = message.DateSent();
                    email.DateReceived = message.DateReceived();
                    email.From = message.From.GetAddressOrDisplayName();
                    email.MessageID = message.MessageId;
                    if (ExistsCCNumber(message.Subject))
                        email.Subject = MaskCCNumbers(message.Subject, '#');
                    else
                        email.Subject = message.Subject;
                    email.Timestamp = DateTime.Now;

                    // Create the dated storage path
                    var path = Path.Combine(storagePath, email.Timestamp.Value.ToString("yyyyMMdd"));

                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    // Insert the new record
                    ctx.Emails.InsertOnSubmit(email);

                    // Submit the email (we need to get the email ID)
                    using (TransactionScope scope = new TransactionScope())
                    {
                        // Initial submit of changes
                        ctx.SubmitChanges();

                        // Build the mail message file name
                        email.MessageFilePath = Path.Combine(path, String.Format("{0:00000000}.eml", email.EmailID));

                        // Copy the eml file if its already in this format, if msg then save as eml
                        if (fileName.EndsWith("eml", StringComparison.OrdinalIgnoreCase))
                        {
                            File.Copy(fileName, email.MessageFilePath, true);
                        }
                        else
                        {
                            // Save in eml format
                            message.Save(email.MessageFilePath, MessageFormat.Eml);
                        }

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

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private String Truncate(String value, int maxLength)
        {
            if (!String.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                return value.Substring(0, maxLength);
            }

            return value;
        }

        private Boolean IsDuplicate(EmailImportDataContext ctx, Guid guid, MailMessage message)
        {
            var subject = ExistsCCNumber(message.Subject) ? MaskCCNumbers(message.Subject, '#') : message.Subject;

            return ctx.Emails.Any(e => Object.Equals(e.MailboxGUID, guid) &&
                                       Object.Equals(e.MessageID, message.MessageId) &&
                                       Object.Equals(e.From, message.From.GetAddressOrDisplayName()) &&
                                       Object.Equals(e.DateSent, message.DateSent()) &&
                                       Object.Equals(e.DateReceived, message.DateReceived()) &&
                                       Object.Equals(e.Subject, subject));
        }

        static public string REGEX_CC_NUMBER = @"(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})";

        static public bool ExistsCCNumber(string s)
        {
            string ccCheck = Regex.Replace(s, @"[ \-,.]", "");
            Regex ccRegex = new Regex(REGEX_CC_NUMBER);

            return ccRegex.IsMatch(ccCheck);
        }

        static public string MaskCCNumbers(string s, char maskChar)
        {
            Regex ccRegex = new Regex(REGEX_CC_NUMBER);

            StringBuilder ss = new StringBuilder(s);
            int ssIndex = 0;

            // ccCheck is what we will use to search for CC #s - it's all the digits within the string of interest
            string ccCheck = Regex.Replace(s, @"[ \-,.]", "");
            int ccCheckIndex = 0;

            Match match;

            // process every match that was found
            do
            {
                var prevCheckIndex = ccCheckIndex;

                match = Regex.Match(ccCheck.Substring(ccCheckIndex), REGEX_CC_NUMBER);

                if (match.Success)
                {
                    bool wasMasked = false;
                    int masked = 0;

                    // skip over any characters in ccCheck that don't fall within the match, designated by match.Index and match.Length
                    for (; ccCheckIndex < ccCheck.Length && ccCheckIndex < match.Index + prevCheckIndex; ccCheckIndex++)
                    {
                        // find this character in the actual string of interest and skip it, as it is not part of the CC match

                        char c = ccCheck[ccCheckIndex];
                        int indexOf = ss.ToString().IndexOf(c, ssIndex);

                        if (indexOf >= 0)
                        {
                            ssIndex = indexOf;
                        }
                    }

                    // loop over each character in ccCheck that falls within match
                    for (; ccCheckIndex < ccCheck.Length && masked < match.Length - 4; ccCheckIndex++)
                    {
                        // find this character in the actual string of interest and mask it, as it is part of the CC match

                        char c = ccCheck[ccCheckIndex];
                        int indexOf = ss.ToString().IndexOf(c, ssIndex);

                        if (indexOf >= 0)
                        {
                            ss[indexOf] = maskChar;
                            ssIndex = indexOf;
                            wasMasked = true;
                            masked++;
                        }
                    }

                    // update check index to go to end of match
                    if (wasMasked)
                    {
                        for (int i = 0; i < 4 && ccCheckIndex < ccCheck.Length; i++, ccCheckIndex++)
                        {
                            // find this character in the actual string of interest and skip it

                            char c = ccCheck[ccCheckIndex];
                            int indexOf = ss.ToString().IndexOf(c, ssIndex);

                            if (indexOf >= 0)
                            {
                                ssIndex = indexOf;
                            }
                        }
                    }
                }
            } while (match.Success);

            return ss.ToString();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (radioButtonMSG.Checked)
            {
                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Please select folder containing msg files...";
                fbd.SelectedPath = textBoxPath.Text;
                fbd.ShowNewFolderButton = false;

                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    textBoxPath.Text = fbd.SelectedPath;
                }
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.CheckFileExists = true;
                ofd.Filter = "Outlook Data File|*.pst";
                ofd.Multiselect = false;

                if (!String.IsNullOrWhiteSpace(textBoxPath.Text))
                {
                    if (File.Exists(textBoxPath.Text))
                        ofd.FileName = textBoxPath.Text;
                    else
                        ofd.InitialDirectory = textBoxPath.Text;
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxPath.Text = ofd.FileName;
                }
            }
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var parms = (object[])e.Argument;

            var profile = (MailboxProfile)parms[0];
            var storagePath = (String)parms[1];

            int total = 0, uploaded = 0;

            if (radioButtonMSG.Checked)
            {
                // Collect and upload msg files!!
                var files = Directory.GetFiles(textBoxPath.Text, "*.eml", SearchOption.AllDirectories)
                                     .Concat(Directory.GetFiles(textBoxPath.Text, "*.msg", SearchOption.AllDirectories));

                progressBar.Invoke(new Action(() =>
                    {
                        progressBar.Minimum = 0;
                        progressBar.Maximum = files.Count();
                        progressBar.Value = 0;
                    }));

                foreach (var file in files)
                {
                    total++;

                    if (UploadMessage(file, profile, storagePath))
                        uploaded++;

                    backgroundWorker.ReportProgress(0);
                }
            }
            else
            {
                // Process pst file
                using (PersonalStorage pst = PersonalStorage.FromFile(textBoxPath.Text, false))
                {
                    var inbox = pst.RootFolder.GetSubFolders().Where(f => f.DisplayName.ToUpper() == "INBOX").FirstOrDefault();

                    if (inbox != null)
                    {
                        var entries = inbox.EnumerateMessagesEntryId();

                        progressBar.Invoke(new Action(() =>
                        {
                            progressBar.Minimum = 0;
                            progressBar.Maximum = entries.Count();
                            progressBar.Value = 0;
                        }));

                        try
                        {
                            foreach (var entry in entries)
                            {
                                total++;

                                pst.ExtractMessage(entry).Save("temp.msg");

                                if (UploadMessage("temp.msg", profile, storagePath))
                                    uploaded++;

                                backgroundWorker.ReportProgress(0);
                            }
                        }
                        finally
                        {
                            File.Delete("temp.msg");
                        }
                    }
                }
            }

            e.Result = new int[] { total, uploaded };
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Invoke(new Action(() =>
            {
                progressBar.Value++;
            }));
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            textBoxPath.Enabled = true;
            comboBoxProfile.Enabled = true;
            buttonBrowse.Enabled = true;
            buttonCollect.Enabled = true;

            if (e.Error != null)
            {
                MessageBox.Show(this, e.Error.ToString(), "File Upload", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int[] counts = (int[])e.Result;

                MessageBox.Show(this, String.Format("Upload complete - {0} messages found, {1} uploaded.", counts[0], counts[1]), "File Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);

                progressBar.Value = 0;
            }

            progressBar.Visible = false;
            Height -= progressBar.Height;
        }
    }
}
