using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Windows.Forms;
using System.Xml.Serialization;
using Aspose.Email;
using Aspose.Email.Mail;
using Decipha.Controls;
using Decipha.Net.Mail;
using EmailImport.Conversion.Configuration;

namespace EmailImport.Viewer
{
    public partial class MainForm : Form
    {
        #region Fields/Constants

        // update this if new columns are added
        const int ProcessedCountColumn = 4;
        private const String NormalTitle = "Email Import - Viewer";
        private const String LiteViewerTitle = "Email Import - Viewer Lite";
        private const String NOEMailsMessage = "No EMails with this Criteria exist for available Profiles.";
        private const String PCNotAuthorisedLiteViewerError = "Sorry, this PC is Not Authorised to use Email Import Viewer - Lite. Please contact your Administrator.";
        private const String NoProfilesLiteViewerError = "Sorry, there are no 'LiteViewer Enabled' Mailboxes on this Server. Please contact your Administrator.";

        Filter filter = new Filter();

        ToolStripMenuItem selectedStatusFilter = null;

        ListViewColumnSorter lvcsEmails = new ListViewColumnSorter();

        #endregion

        #region Public Properties

        public String HostName { get; private set; }
        public Boolean PCLiteViewerEnabled { get; private set; }
        public Boolean LiteViewerMode { get; private set; }
        public Boolean LiteViewerMailboxesAvailable { get; private set; }

        private MailboxProfile SelectedProfile
        {
            get
            {
                if (listViewProfiles.SelectedItems.Count == 1)
                    return (MailboxProfile)listViewProfiles.SelectedItems[0].Tag;
                else
                    return null;
            }
        }

        private Email SelectedEmail
        {
            get
            {
                if (listViewEmails.SelectedItems.Count == 1)
                    return (Email)listViewEmails.SelectedItems[0].Tag;
                else
                    return null;
            }
        }

        private IEnumerable<Email> SelectedEmails
        {
            get
            {
                if (listViewEmails.SelectedItems.Count >= 1)
                    return listViewEmails.SelectedItems.Cast<ListViewItem>().Select(i => (Email)i.Tag);
                else
                    return Enumerable.Empty<Email>();
            }
        }

        private Attachment SelectedAttachment
        {
            get
            {
                if (listViewAttachments.SelectedItems.Count == 1)
                    return (Attachment)listViewAttachments.SelectedItems[0].Tag;
                else
                    return null;
            }
        }
        #endregion

        #region Constructor

        public MainForm(Boolean runInLiteViewerMode)
        {
            this.LiteViewerMode = runInLiteViewerMode;

            License license = new License();
            license.SetLicense("Aspose.Total.lic");

            InitializeComponent();

            ClearMailViewer();
        }

        #endregion

        #region MainForm Event Handlers

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "EmailImport.Viewer");

            if (Directory.Exists(tempFilePath))
                Directory.Delete(tempFilePath, true);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Validate PC for Lite View
            ValidatePCForLiteView();

            string tempFilePath = Path.Combine(Path.GetTempPath(), "EmailImport.Viewer");

            Directory.CreateDirectory(tempFilePath);

            listViewEmails.ListViewItemSorter = lvcsEmails;

            LoadProfiles();

            // Alter the form for Possible Lite Mode
            AlterFormForLiteView();
        }

        #endregion

        #region Toolbar Event Handlers

        private void dateSentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDateRangeFilter(FilterType.DateSent, dateSentToolStripMenuItem);
        }

        private void dateReceivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDateRangeFilter(FilterType.DateReceived, dateReceivedToolStripMenuItem);
        }

        private void filterByStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearFilter();

            selectedStatusFilter = sender as ToolStripMenuItem;
            if (selectedStatusFilter != null)
            {
                var status = (string)selectedStatusFilter.Tag;

                if (status != null)
                {
                    selectedStatusFilter.Checked = true;

                    filter.Type = FilterType.Status;
                    filter.StringValue = status;

                    currentFilterToolStripStatusLabel.Text = filter.Type.ToString() + " [" + filter.StringValue + "]";
                }
            }

            ShowEmails(SelectedProfile);
        }

        private void batchNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStringValueFilter(FilterType.BatchNumber, batchNumberToolStripMenuItem, "Please enter batch number");
        }

        private void fromToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStringValueFilter(FilterType.From, fromToolStripMenuItem, "Please enter email address or name");
        }

        private void subjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStringValueFilter(FilterType.Subject, subjectToolStripMenuItem, "Please enter subject search term");
        }

        private void messageIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetStringValueFilter(FilterType.MessageID, messageIDToolStripMenuItem, "Please enter message ID search term");
        }

        private void emailIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLongValueFilter(FilterType.EmailID, emailIDToolStripMenuItem, "Please enter email ID", "Email ID must be an integer value");
        }

        private void toolStripButtonFindEMailID_Click(object sender, EventArgs e)
        {
            SetLongValueFilter(FilterType.EmailID, emailIDToolStripMenuItem, "Please enter email ID", "Email ID must be an integer value");
        }

        private void toolStripTextBoxMaximumEmails_Leave(object sender, EventArgs e)
        {
            int n = 0;

            if (Int32.TryParse(toolStripTextBoxMaximumEmails.Text, out n) == false)
            {
                MessageBox.Show("Maximum # of Emails must be an integer value.", "Invalid Value", MessageBoxButtons.OK, MessageBoxIcon.Error);

                toolStripTextBoxMaximumEmails.SelectAll();
                toolStripTextBoxMaximumEmails.Focus();
            }

            ShowEmails(SelectedProfile);
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            if (listViewProfiles.SelectedItems.Count > 0)
                ShowEmails(SelectedProfile);
        }

        #endregion

        #region Filter Setup

        private void SetDateRangeFilter(FilterType filterType, ToolStripMenuItem toolStripMenuItem)
        {
            if (filter.Type == filterType)
            {
                ClearFilter();
                ShowEmails(SelectedProfile);
            }
            else
            {
                DateRangeDialog dialog = new DateRangeDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ClearFilter();

                    toolStripMenuItem.Checked = true;

                    filter.Type = filterType;
                    filter.Date1 = dialog.From;
                    filter.Date2 = dialog.To;

                    currentFilterToolStripStatusLabel.Text = filterType.ToString() + " [" + filter.Date1.ToString() + " to " + filter.Date2.AddDays(1).AddMilliseconds(-1).ToString() + "]";

                    ShowEmails(SelectedProfile);
                }
            }
        }

        private void SetStringValueFilter(FilterType filterType, ToolStripMenuItem toolStripMenuItem, string prompt)
        {
            if (filter.Type == filterType)
            {
                ClearFilter();
                ShowEmails(SelectedProfile);
            }
            else
            {
                TextInputDialog dialog = new TextInputDialog();

                dialog.Title = prompt;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ClearFilter();

                    toolStripMenuItem.Checked = true;

                    filter.Type = filterType;
                    filter.StringValue = dialog.Value;

                    currentFilterToolStripStatusLabel.Text = filterType.ToString() + " [" + filter.StringValue + "]";

                    ShowEmails(SelectedProfile);
                }
            }
        }

        private void SetLongValueFilter(FilterType filterType, ToolStripMenuItem toolStripMenuItem, string prompt, string error)
        {
            // Do not clear the Filter if LiteViewer Mode
            if (filter.Type == filterType && !LiteViewerMode)
            {
                ClearFilter();
                ShowEmails(SelectedProfile);
            }
            else
            {
                TextInputDialog dialog = new TextInputDialog();

                dialog.Title = prompt;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    long value = 0;

                    if (long.TryParse(dialog.Value, out value))
                    {
                        ClearFilter();

                        toolStripMenuItem.Checked = true;

                        filter.Type = filterType;
                        filter.LongValue = value;

                        currentFilterToolStripStatusLabel.Text = filterType.ToString() + " [" + filter.LongValue.ToString() + "]";

                        ShowEmails(SelectedProfile);
                    }
                    else
                    {
                        MessageBox.Show(error, "Invalid Value", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void ClearFilter()
        {
            dateSentToolStripMenuItem.Checked = false;
            dateReceivedToolStripMenuItem.Checked = false;
            if (selectedStatusFilter != null)
                selectedStatusFilter.Checked = false;
            batchNumberToolStripMenuItem.Checked = false;
            fromToolStripMenuItem.Checked = false;
            subjectToolStripMenuItem.Checked = false;
            messageIDToolStripMenuItem.Checked = false;
            emailIDToolStripMenuItem.Checked = false;

            filter.Type = FilterType.None;

            currentFilterToolStripStatusLabel.Text = "<None>";
        }

        #endregion
        
        #region Profile Management

        private void LoadProfiles()
        {
            listViewProfiles.Clear();
            listViewProfiles.Columns.Add("Mailbox", listViewProfiles.Width - 22);

            var profiles = new List<MailboxProfile>();

            using (var ctx = new EmailImportDataContext())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MailboxProfile));

                foreach (var mailbox in ctx.Mailboxes)
                {
                    try
                    {
                        using (TextReader reader = new StringReader(mailbox.ProfileObject))
                        {
                            var profile = (MailboxProfile)serializer.Deserialize(reader);
                            profile.OriginalSerializedObject = mailbox.ProfileObject;
                            profiles.Add(profile);
                        }
                    }
                    catch // Ignore bad objects for now
                    { }
                }

                // If running in LiteViewer Mode and Not Authorised
                if (this.LiteViewerMode && !PCLiteViewerEnabled)
                {
                    labelLiteViewError.Text = PCNotAuthorisedLiteViewerError;
                    labelLiteViewError.Visible = true;
                    return;
                }

                labelLiteViewError.Visible = false;
            }

            // Do not add the All Mailbox if LiteViewer Mode
            if (!this.LiteViewerMode)
                AddAllAllProfile();

            LiteViewerMailboxesAvailable = false;
            foreach (var profile in profiles.OrderBy(p => p.Group).ThenBy(p => p.Description))
            {
                // If LiteViewer Mode and Profile not LiteviewerEnabled
                if (this.LiteViewerMode && !profile.LiteViewerEnabled)
                   continue;

                AddProfile(profile);
                LiteViewerMailboxesAvailable = true;
            }

                // If LiteViewer Mode and no LiteviewerEnabled Profiles
            if (this.LiteViewerMode && !LiteViewerMailboxesAvailable)
            {
                labelLiteViewError.Text = NoProfilesLiteViewerError;
                labelLiteViewError.Visible = true;
            }
        }

        private void AddAllAllProfile()
        {
            listViewProfiles.Groups.Add("All", "All");

            var group = listViewProfiles.Groups["All"];

            if (group != null)
            {
                var item = listViewProfiles.Items.Add("All", "All", null);

                item.Group = group;
            }
        }

        private void AddProfile(MailboxProfile profile)
        {
            var group = listViewProfiles.Groups[profile.Group ?? "Default"];

            if (group == null)
                group = listViewProfiles.Groups.Add(profile.Group ?? "Default", profile.Group ?? "Default");

            var item = listViewProfiles.Items.Add(profile.MailboxGUID.ToString(), profile.Description, null);
            
            item.Tag = profile;
            item.Group = group;
        }

        #endregion

        #region Mailbox Selection

        private void listViewProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewProfiles.SelectedItems.Count > 0)
            {
                ShowEmails(SelectedProfile);
            }
            else
            {
                listViewEmails.Items.Clear();
                ClearMailViewer();
            }
        }

        private List<Email> GetEmails(MailboxProfile profile)
        {
            IEnumerable<Email> emails;
            int numberOfEmails = Int32.Parse(toolStripTextBoxMaximumEmails.Text);

            using (EmailImportDataContext dc = new EmailImportDataContext())
            {
                if (profile != null)
                    emails = dc.Emails.Where(e => e.MailboxGUID == profile.MailboxGUID);
                else
                    emails = dc.Emails;

                switch (filter.Type)
                {
                    case FilterType.DateSent:
                        emails = emails.Where(e => e.DateSent >= filter.Date1 && e.DateSent < filter.Date2.AddDays(1));

                        break;

                    case FilterType.DateReceived:
                        emails = emails.Where(e => e.DateReceived >= filter.Date1 && e.DateReceived < filter.Date2.AddDays(1));

                        break;

                    case FilterType.Status:
                        emails = emails.Where(e => e.Status == filter.StringValue);

                        break;

                    case FilterType.BatchNumber:
                        emails = emails.Where(e => e.BatchNumber.ToLower() == filter.StringValue.ToLower());

                        break;

                    case FilterType.From:
                        emails = emails.Where(e => e.From.ToLower().Contains(filter.StringValue.ToLower()));

                        break;

                    case FilterType.Subject:
                        emails = emails.Where(e => e.Subject.ToLower().Contains(filter.StringValue.ToLower()));

                        break;

                    case FilterType.MessageID:
                        emails = emails.Where(e => e.MessageID.ToLower().Contains(filter.StringValue.ToLower()));

                        break;

                    case FilterType.EmailID:
                        emails = emails.Where(e => e.EmailID == filter.LongValue);

                        break;
                }
                
                emails = emails.Take(numberOfEmails);

                return emails.ToList();
            }
        }

        private void ShowEmails(MailboxProfile profile)
        {
            Cursor = Cursors.WaitCursor;

            listViewEmails.Items.Clear();

            var emails = GetEmails(profile);

            using (EmailImportDataContext dc = new EmailImportDataContext())
            {
                foreach (var email in emails)
                {
                    // If LiteViewer Mode then check if emails Profile is Lite Viewer Enabled
                    if (LiteViewerMode && !ProfileLiteViewerEnabled((Guid) email.MailboxGUID))
                        continue;

                    ListViewItem lvi = new ListViewItem(new string[] {
                        email.From,
                        email.Subject,
                        email.DateSent.HasValue ? email.DateSent.Value.ToString() : String.Empty,
                        email.DateReceived.HasValue ? email.DateReceived.Value.ToString() : String.Empty,
                        Convert.ToString(email.ProcessedCount.GetValueOrDefault()),
                        email.Status,
                        email.StartTime.HasValue ? email.StartTime.Value.ToString() : String.Empty,
                        email.EndTime.HasValue ? email.EndTime.Value.ToString() : String.Empty,
                        email.Errors != null ? "Y" : String.Empty
                    });
                    lvi.Tag = email;

                    listViewEmails.Items.Add(lvi);
                }
            };

            // If ListView contains Items then Select First 1 else display Message
            if (listViewEmails.Items.Count > 0)
                listViewEmails.Items[0].Selected = true;
            else
            {
                ListViewItem lvi = new ListViewItem(new string[] {null, NOEMailsMessage});
                listViewEmails.Items.Add(lvi);
            }

            Cursor = Cursors.Default;
        }

        #endregion

        #region Email Selection

        private void ClearMailViewer()
        {
            listViewAttachments.Items.Clear();

            labelFrom.Text = String.Empty;
            labelTo.Text = String.Empty;
            labelCC.Text = String.Empty;
            labelSubject.Text = String.Empty;

            webBrowser.Url = null;
        }

        private void listViewEmails_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvcsEmails.SortColumn)
            {
                if (lvcsEmails.Order == SortOrder.Ascending)
                    lvcsEmails.Order = SortOrder.Descending;
                else
                    lvcsEmails.Order = SortOrder.Ascending;
            }
            else
            {
                lvcsEmails.CompareType = e.Column == ProcessedCountColumn ? typeof(int) : typeof(string);
                lvcsEmails.SortColumn = e.Column;
                lvcsEmails.Order = SortOrder.Ascending;
            }

            listViewEmails.Sort();
        }

        private void listViewEmails_DoubleClick(object sender, EventArgs e)
        {
            var email = SelectedEmail;

            if (email != null && email.Errors != null)
            {
                ErrorsDialog dialog = new ErrorsDialog();

                dialog.Email = email;
                dialog.ShowDialog();
            }
        }

        private void listViewEmails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                foreach (ListViewItem item in listViewEmails.Items)
                {
                    item.Selected = true;
                }
            }
        }

        private void listViewEmails_SelectedIndexChanged(object sender, EventArgs e)
        {
            var email = SelectedEmail;

            if (email != null)
            {
                ShowEmail(email);
            }
            else
            {
                ClearMailViewer();
            }
        }

        public Icon LoadIconFromExtension(string extension)
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), "EmailImport.Viewer", "dummy" + extension);

            using (File.Create(tempFilePath)) { }

            Icon icon = Icon.ExtractAssociatedIcon(tempFilePath);
            
            File.Delete(tempFilePath);
            
            return icon;
        }

        private void ShowEmail(Email email)
        {
            Cursor = Cursors.WaitCursor;

            MailMessage message = MailMessage.Load(email.MessageFilePath);

            if (message != null)
            {
                // set basic email header information
                labelFrom.Text = message.From.ToStringEx();
                labelTo.Text = message.To.ToStringEx();
                labelCC.Text = message.CC.ToStringEx();
                labelSubject.Text = message.Subject;

                // load attachments list
                listViewAttachments.Clear();
                listViewAttachments.Columns.Add("Attachment", listViewAttachments.Width - 22);
                foreach (Attachment attachment in message.Attachments)
                {
                    var extension = Path.GetExtension(attachment.Name);

                    if (attachment.IsEmbeddedMessage())
                        extension = ".msg";

                    // create list view item
                    ListViewItem lvi = new ListViewItem(attachment.Name);
                    lvi.ImageKey = extension;
                    lvi.Tag = attachment;

                    // load icon and add to image list if necessary
                    Icon icon = LoadIconFromExtension(extension);
                    if (!imageListIcons.Images.ContainsKey(extension))
                        imageListIcons.Images.Add(extension, icon);

                    listViewAttachments.Items.Add(lvi);
                }

                // show message body
                string fileName = string.Concat(message.MessageId.Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)) + ".mht";
                string tempFilePath = Path.Combine(Path.GetTempPath(), "EmailImport.Viewer", fileName);

                using (TextWriter writer = new StreamWriter(tempFilePath))
                {
                    writer.Write(message.GetMht());
                }
                webBrowser.Url = new Uri(tempFilePath);
            }
            else
            {
                ClearMailViewer();
            }

            Cursor = Cursors.Default;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            File.Delete(webBrowser.Url.AbsolutePath);
        }

        private bool resizingEmails = false;

        private void listViewEmails_SizeChanged(object sender, EventArgs e)
        {
            // Don't allow overlapping of SizeChanged calls
            if (!resizingEmails)
            {
                // Set the resizing flag
                resizingEmails = true;

                ListView listView = sender as ListView;
                if (listView != null)
                {
                    float totalColumnWidth = 0;

                    // Get the sum of all column tags
                    for (int i = 0; i < listView.Columns.Count; i++)
                        totalColumnWidth += Convert.ToInt32(listView.Columns[i].Tag);

                    // Calculate the percentage of space each column should 
                    // occupy in reference to the other columns and then set the 
                    // width of the column to that percentage of the visible space.
                    for (int i = 0; i < listView.Columns.Count; i++)
                    {
                        float colPercentage = (Convert.ToInt32(listView.Columns[i].Tag) / totalColumnWidth);
                        listView.Columns[i].Width = (int)(colPercentage * listView.ClientRectangle.Width);
                    }
                }
            }

            // Clear the resizing flag
            resizingEmails = false;
        }

        #endregion

        #region Attachment Selection

        private void listViewAttachments_DoubleClick(object sender, EventArgs e)
        {
            var attachment = SelectedAttachment;

            if (attachment != null)
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), "EmailImport.Viewer");

                if (attachment.IsEmbeddedMessage())
                {
                    tempFilePath = Path.Combine(tempFilePath, attachment.GetSafeFileName() + ".eml");

                    var options = new MailMessageLoadOptions()
                    {
                        FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking,
                        MessageFormat = MessageFormat.Eml
                    };

                    var message = attachment.GetEmbeddedMessage(options);

                    message.Save(tempFilePath, MessageFormat.Eml);
                }
                else
                {
                    tempFilePath = Path.Combine(tempFilePath, attachment.GetSafeFileName());
                    attachment.Save(tempFilePath);
                }

                using (Process process = Process.Start(tempFilePath))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                    }
                }
            }
        }

        #endregion

        #region Context Menu Event Handlers

        private void contextMenuEmails_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var emails = SelectedEmails;

            saveEmailToolStripMenuItem.Enabled = false;
            forwardToolStripMenuItem.Enabled = false;
            reprocessToolStripMenuItem.Enabled = false;

            if (emails.Count() > 0)
            {
                saveEmailToolStripMenuItem.Enabled = true;
                forwardToolStripMenuItem.Enabled = true;
                reprocessToolStripMenuItem.Enabled = true;
            }

            // Remove Forward and ReProcess for LiteMode
            if (LiteViewerMode)
            {
                forwardToolStripMenuItem.Visible = false;
                reprocessToolStripMenuItem.Visible = false;
            }
        }

        private void saveEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var emails = SelectedEmails;

            if (emails.Count() > 0)
            {
                if (emails.Count() == 1)
                {
                    SaveFileDialog dialog = new SaveFileDialog();

                    dialog.FileName = Path.GetFileName(emails.First().MessageFilePath);
                    if (dialog.ShowDialog() == DialogResult.OK)
                        File.Copy(emails.First().MessageFilePath, dialog.FileName, true);
                }
                else
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (var email in emails)
                            File.Copy(email.MessageFilePath, Path.Combine(dialog.SelectedPath, Path.GetFileName(email.MessageFilePath)), true);
                    }
                }
            }
        }

        private void forwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var emails = SelectedEmails;

            if (emails.Count() > 0)
                ForwardEmails(emails);
        }

        private void reprocessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var emails = SelectedEmails;
            int n = 0;

            if (emails.Count() > 0)
                if (MessageBox.Show("Selected emails will be flagged for reprocessing. Continue?", "Confirm Reprocess Emails", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    n = ReprocessEmails(emails);

            if (n > 0)
            {
                //MessageBox.Show(string.Format("{0} emails have been flagged for reprocessing", n), "Reprocess Emails", MessageBoxButtons.OK);

                ShowEmails(SelectedProfile);
            }
        }

        private void showErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var email = SelectedEmail;

            if (email != null)
            {
                XmlViewForm xmlViewForm = new XmlViewForm();

                xmlViewForm.SetXml(email.Errors.ToString());
                xmlViewForm.ShowDialog();
            }
        }

        private void contextMenuAttachments_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var attachment = SelectedAttachment;

            if (attachment != null)
                saveAttachmentToolStripMenuItem.Enabled = true;
            else
                saveAttachmentToolStripMenuItem.Enabled = false;
        }

        private void saveAttachmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var attachment = SelectedAttachment;

            if (attachment != null)
            {
                SaveFileDialog dialog = new SaveFileDialog();

                dialog.FileName = attachment.Name;
                if (dialog.ShowDialog() == DialogResult.OK)
                    attachment.Save(dialog.FileName);
            }
        }

        private void saveAttachmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var email = SelectedEmail;

            if (email != null)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    MailMessage message = MailMessage.Load(email.MessageFilePath);

                    if (message != null)
                    {
                        foreach (var attachment in message.Attachments)
                        {
                            attachment.Save(Path.Combine(dialog.SelectedPath, attachment.GetSafeFileName()));
                        }
                    }
                }
            }
        }

        #endregion

        #region Email Forwarding

        private void ForwardEmails(IEnumerable<Email> emails)
        {
            TextInputDialog dialog = new TextInputDialog();

            dialog.Title = "Enter forwarding email addresses";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var smtpSection = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection; 
                var addresses = dialog.Value.Split(new char[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                using (var smtp = new SmtpClient(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)))
                {
                    foreach (var email in emails)
                    {
                        MailMessage message = MailMessage.Load(email.MessageFilePath);

                        foreach (var address in addresses)
                            smtp.Send(message.ForwardAsAttachment(smtpSection.From, address));
                    }
                }
            }
        }

        #endregion

        #region Email Reprocessing

        private int ReprocessEmails(IEnumerable<Email> emails)
        {
            var emailIDs = emails.Select(e => e.EmailID);
            int n = 0;

            using (EmailImportDataContext dc = new EmailImportDataContext())
            {
                var reprocessEmails = dc.Emails.Where(e => emailIDs.Contains(e.EmailID));

                foreach (var email in reprocessEmails)
                {
                    if (email.Status != null)
                    {
                        email.Status = null;
                        n++;
                    }
                }

                dc.SubmitChanges();
            }

            return n;
        }

        #endregion

        #region LiteViewer Methods
        private void ValidatePCForLiteView()
        {
            // Check Settings Table to see if PC is Authorised
            HostName = Dns.GetHostName().ToUpper();

            var liteViewAuthorisedPCsList = Settings.LiteViewerAuthorisedPCs;

            PCLiteViewerEnabled = liteViewAuthorisedPCsList != null && liteViewAuthorisedPCsList.ToUpper().Split(';', '|', ',').Contains(HostName);
        }

        private void AlterFormForLiteView()
        {
            // Change Items on Top Tool Strip
            toolStripDropDownButtonFilterBy.Visible = !LiteViewerMode;
            toolStripLabel1.Visible = !LiteViewerMode;
            toolStripTextBoxMaximumEmails.Visible = !LiteViewerMode;
            toolStripButtonRefresh.Visible = !LiteViewerMode;
            toolStripSeparator1.Visible = !LiteViewerMode;
            toolStripSeparator2.Visible = !LiteViewerMode;
            listViewProfiles.Enabled = !LiteViewerMode;

            toolStripButtonFindEMailID.Visible = LiteViewerMode && PCLiteViewerEnabled && LiteViewerMailboxesAvailable;

            // Change Title of Form
            this.Text = LiteViewerMode ? LiteViewerTitle : NormalTitle;
        }

        private Boolean ProfileLiteViewerEnabled(Guid guid)
        {
            foreach (ListViewItem item in listViewProfiles.Items)
            {
                if (((MailboxProfile)item.Tag).MailboxGUID == guid && ((MailboxProfile)item.Tag).LiteViewerEnabled)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
