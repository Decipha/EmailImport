using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Decipha.Net.Mail;
using EmailImport.Conversion.Configuration;
using MailBee.Mime;
using MailBee.SmtpMail;

namespace MailboxClient
{
    public partial class MailboxClient : Form
    {
        ServiceController svc = null;
        Boolean clearMessages = true;
        Boolean autoCheck = false;

        public MailboxClient()
        {
            InitializeComponent();
            
            Smtp.LicenseKey = "MN200-834B4CC94BCA4BF74BB7FF7041C8-451C";
        }

        private void MailboxClient_Load(object sender, EventArgs e)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration("EmailImport.exe");

            EmailImportConfiguration config = (EmailImportConfiguration)configFile.GetSection("emailImport");

            var unique = new List<String>();

            foreach (MailboxElement mailbox in config.Mailboxes)
            {
                if (String.IsNullOrEmpty(mailbox.Description))
                    continue;

                if (unique.Contains(mailbox.Description.ToLower()))
                    continue;

                unique.Add(mailbox.Description.ToLower());
                comboBoxMailbox.Items.Add(new MailboxItem(mailbox));
            }

            svc = ServiceController.GetServices().SingleOrDefault(s => s.ServiceName == "EmailImport");

            if (svc == null)
            {
                labelStatus.Text = "Status: Not Installed";
                groupBoxService.Enabled = false;
            }
            else
            {
                RefreshServiceStatus();
            }
        }

        private void RefreshServiceStatus()
        {
            svc.Refresh();

            labelStatus.Text = String.Format("Status: {0}", svc.Status);

            buttonStop.Enabled = (svc.Status == ServiceControllerStatus.Running);
            buttonStart.Enabled = (svc.Status != ServiceControllerStatus.Running);
        }

        private Boolean IsServiceRunning()
        {
            if (svc == null)
                return false;

            svc.Refresh();

            return (svc.Status == ServiceControllerStatus.Running);
        }

        private MailboxElement Mailbox
        {
            get
            {
                if (comboBoxMailbox.SelectedItem == null)
                    return null;

                return ((MailboxItem)comboBoxMailbox.SelectedItem).Mailbox;
            }
        }

        String imapRoot, imapDelimiter;

        private void comboBoxMailbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var mailbox = Mailbox;

            try
            {
                comboBoxFolder.Items.Clear();
                comboBoxMove.Items.Clear();

                using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, mailbox.ImapFolder))
                {
                    imapRoot = imap.RootFolder;
                    imapDelimiter = imap.Delimiter;

                    var folders = imap.GetFolderNames();
                    comboBoxFolder.Items.AddRange(folders);
                    comboBoxMove.Items.AddRange(folders);
                }

                comboBoxFolder.Enabled = true;
                comboBoxMove.Text = null;
                buttonDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clearMessages)
            {
                listViewMessages.Items.Clear();
            }

            if (comboBoxFolder.SelectedItem != null)
            {
                var pattern = "^Inbox$|^Inbox.Archive$|^Inbox.Error$|^Inbox.Force$|^Inbox.Forwarded$|^Inbox.Ignore$";
                pattern = pattern.Replace("Inbox", imapRoot);

                var item = (String)comboBoxFolder.SelectedItem;

                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                if (regex.IsMatch(item))
                {
                    buttonDelete.Enabled = false;
                }
                else
                {
                    buttonDelete.Enabled = true;
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (comboBoxMailbox.SelectedItem == null)
            {
                MessageBox.Show(this, "Please select a mailbox.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBoxFolder.SelectedItem == null)
            {
                MessageBox.Show(this, "Please select a folder.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (checkBoxExhaustive.Checked && String.IsNullOrEmpty(textBoxMessageID.Text))
            {
                MessageBox.Show(this, "Please provide MessageID(s) for exhaustive searching.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //groupBoxResults.Enabled = !(String.Compare((String)comboBoxFolder.SelectedItem, imapRoot, true) == 0 && IsServiceRunning());
            groupBoxResults.Enabled = true;

            var mailbox = Mailbox;
            var timeout = Imap.Timeout;

            try
            {
                Imap.Timeout = 300000;

                listViewMessages.Items.Clear();

                using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, mailbox.ImapFolder))
                {
                    imap.SelectFolder((String)comboBoxFolder.SelectedItem);

                    var uids = Search(imap);

                    if (uids == null)
                        return;

                    if (uids.Count == 0)
                    {
                        MessageBox.Show(this, "No messages found!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        var messages = imap.DownloadMessageHeaders(uids).Cast<MailMessage>();

                        foreach (MailMessage message in messages.OrderBy(m => m.DateReceived))
                        {
                            ListViewItem item = new ListViewItem();
                            item.SubItems.Add(message.From.Email);
                            item.SubItems.Add(message.Subject);
                            item.SubItems.Add(message.Date.ToString("dd/MM/yyyy hh:mm:ss tt"));
                            item.SubItems.Add(message.DateReceived.ToString("dd/MM/yyyy hh:mm:ss tt"));
                            item.SubItems.Add(GetSize(message.SizeOnServer));
                            item.SubItems.Add(message.HasAttachments ? "Yes" : "No");
                            item.SubItems.Add(message.UidOnServer.ToString());
                            item.SubItems.Add(message.MessageID);

                            listViewMessages.Items.Add(item);
                        }

                        columnHeader1.Width = -2;
                        columnHeader4.Width = -2;
                        columnHeader5.Width = -2;
                        columnHeader6.Width = -2;
                        columnHeader7.Width = -2;
                        columnHeader9.Width = -2;
                        columnHeader10.Width = -2;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Imap.Timeout = timeout;
            }
        }

        private MailBee.ImapMail.UidCollection Search(Imap imap)
        {
            if (String.IsNullOrEmpty(textBoxIMAP.Text))
            {
                return imap.Search();
            }
            else
            {
                MailBee.ImapMail.UidCollection uids = new MailBee.ImapMail.UidCollection();

                if (checkBoxExhaustive.Checked)
                {
                    foreach (MailMessage header in imap.DownloadMessageHeaders(imap.Search()))
                    {
                        if (textBoxMessageID.Lines.Contains(header.MessageID))
                        {
                            uids.Add((long)header.UidOnServer);
                        }
                    }
                }
                else
                {
                    foreach (var line in textBoxIMAP.Lines)
                    {
                        if (String.IsNullOrEmpty(line))
                            continue;

                        var msg = imap.Search(line);

                        if (msg.Count > 0)
                            uids.AddSet(msg.ToString());
                    }
                }

                return uids;
            }
        }

        private String GetSize(int bytes)
        {
            if (bytes < 1024)
                return String.Format("{0} B", bytes);
            
            float kb = (float)bytes / 1024f;

            if (kb >= 1024)
            {
                float mb = kb / 1024f;

                return String.Format("{0:0} MB", mb);
            }

            return String.Format("{0:0} KB", kb);
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            autoCheck = true;

            foreach (ListViewItem item in listViewMessages.Items)
            {
                item.Checked = true;
            }

            autoCheck = false;
        }

        private void buttonDeselectAll_Click(object sender, EventArgs e)
        {
            autoCheck = true;

            foreach (ListViewItem item in listViewMessages.Items)
            {
                item.Checked = false;
            }

            autoCheck = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listViewMessages.Items.Clear();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var mailbox = Mailbox;

            if (listViewMessages.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select one or more messages to save.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.Compare((String)comboBoxFolder.SelectedItem, imapRoot, true) == 0 && IsServiceRunning() && mailbox.Enabled)
            {
                MessageBox.Show(this, "Cannot save message from the root folder whilst Email Import service is running.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var fbd = new FolderBrowserDialog();
            fbd.Description = "Please select the destination folder...";
            fbd.ShowNewFolderButton = true;
            fbd.RootFolder = Environment.SpecialFolder.Desktop;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                if (Directory.Exists(fbd.SelectedPath))
                {
                    try
                    {
                        using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, mailbox.ImapFolder))
                        {
                            imap.SelectFolder((String)comboBoxFolder.SelectedItem);

                            foreach (ListViewItem item in listViewMessages.CheckedItems)
                            {
                                var uid = Convert.ToInt64(item.SubItems[7].Text);
                                var msgID = item.SubItems[8].Text;

                                var fileName = Path.Combine(fbd.SelectedPath, msgID + ".eml");
                                var message = imap.DownloadMessage(uid);
                                message.SaveMessage(fileName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBoxMessageID_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxMessageID.Text))
            {
                groupBoxOther.Enabled = true;
                RefreshExpression();
            }
            else
            {
                groupBoxOther.Enabled = false;

                List<String> imap = new List<String>();

                foreach (var line in textBoxMessageID.Lines)
                {
                    if (String.IsNullOrEmpty(line))
                        continue;

                    imap.Add("HEADER Message-ID " + MailBee.ImapMail.ImapUtils.ToQuotedString(line));
                }

                textBoxIMAP.Lines = imap.ToArray();
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            var mailbox = Mailbox;

            if (listViewMessages.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select one or more messages to move.", "Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.IsNullOrEmpty(comboBoxMove.Text))
            {
                MessageBox.Show(this, "Please select or enter a folder to move to.", "Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.Compare((String)comboBoxFolder.SelectedItem, imapRoot, true) == 0 && IsServiceRunning() && mailbox.Enabled)
            {
                MessageBox.Show(this, "Cannot move messages from the root folder whilst Email Import service is running.", "Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.Compare(comboBoxMove.Text, comboBoxFolder.Text, true) == 0)
            {
                MessageBox.Show(this, "Folder to move to cannot be the same as the containing folder.", "Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;                
            }

            Boolean create = false;

            if (String.Compare(comboBoxMove.Text, imapRoot, true) != 0)
            {
                // If the folder isn't from the list, we need to create a new folder
                if (comboBoxMove.SelectedItem == null)
                {
                    var prefix = imapRoot + imapDelimiter;

                    if (!comboBoxMove.Text.StartsWith(prefix, StringComparison.CurrentCultureIgnoreCase))
                    {
                        MessageBox.Show(this, String.Format("Invalid folder name, folder must begin with '{0}'.", prefix), "Move", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        create = true;
                    }
                }
            }

            try
            {
                using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, mailbox.ImapFolder))
                {
                    if (create)
                    {
                        imap.CreateFolder(comboBoxMove.Text, false);

                        var cbFolderSelected = comboBoxFolder.SelectedItem;
                        var cbMoveSelected = comboBoxMove.Text;

                        clearMessages = false;

                        comboBoxFolder.Items.Clear();
                        comboBoxMove.Items.Clear();

                        var folders = imap.GetFolderNames();
                        comboBoxFolder.Items.AddRange(folders);
                        comboBoxMove.Items.AddRange(folders);

                        comboBoxFolder.SelectedItem = cbFolderSelected;
                        comboBoxMove.SelectedIndex = comboBoxMove.FindString(cbMoveSelected);

                        clearMessages = true;
                    }

                    imap.SelectFolder((String)comboBoxFolder.SelectedItem);

                    var items = listViewMessages.CheckedItems.Cast<ListViewItem>();

                    foreach (var item in items)
                    {
                        var uid = Convert.ToInt64(item.SubItems[7].Text);
                        imap.MoveMessage(uid, (String)comboBoxMove.SelectedItem);
                        listViewMessages.Items.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Move", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }               
        }

        private void linkLabelDate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabelDate.Text == "Sent Date:")
                linkLabelDate.Text = "Received Date:";
            else
                linkLabelDate.Text = "Sent Date:";

            RefreshExpression();
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            RefreshExpression();
        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {
            RefreshExpression();
        }

        private void textBoxSender_TextChanged(object sender, EventArgs e)
        {
            RefreshExpression();
        }

        private void textBoxSubject_TextChanged(object sender, EventArgs e)
        {
            RefreshExpression();
        }

        private void textBoxBody_TextChanged(object sender, EventArgs e)
        {
            RefreshExpression();
        }

        public void RefreshExpression()
        {
            StringBuilder sb = new StringBuilder();

            String prefix = linkLabelDate.Text.StartsWith("Sent") ? "SENT" : "";

            if (dtpFrom.Checked && dtpTo.Checked)
            {
                if (dtpFrom.Value.Date == dtpTo.Value.Date)
                {
                    sb.AppendFormat("{0}ON \"{1}\" ", prefix, dtpFrom.Text);
                }
                else
                {
                    sb.AppendFormat("{0}SINCE \"{1}\" {0}BEFORE \"{2}\" ", prefix, dtpFrom.Text, dtpTo.Text);
                }
            }
            else if (dtpFrom.Checked)
            {
                sb.AppendFormat("{0}SINCE \"{1}\" ", prefix, dtpFrom.Text);
            }
            else if (dtpTo.Checked)
            {
                sb.AppendFormat("{0}BEFORE \"{1}\" ", prefix, dtpTo.Text);
            }

            if (!String.IsNullOrEmpty(textBoxSender.Text))
            {
                sb.AppendFormat("FROM \"{0}\" ", textBoxSender.Text);
            }

            if (!String.IsNullOrEmpty(textBoxSubject.Text))
            {
                sb.AppendFormat("SUBJECT \"{0}\" ", textBoxSubject.Text);
            }

            if (!String.IsNullOrEmpty(textBoxBody.Text))
            {
                sb.AppendFormat("BODY \"{0}\" ", textBoxBody.Text);
            }

            textBoxIMAP.Text = sb.ToString().TrimEnd(' ');
        }

        private void buttonClearCriteria_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today;
            dtpFrom.Checked = false;

            dtpFrom.Value = DateTime.Today;
            dtpTo.Checked = false;

            textBoxSender.Text = String.Empty;
            textBoxSubject.Text = String.Empty;
            textBoxBody.Text = String.Empty;

            textBoxMessageID.Text = String.Empty;
            textBoxIMAP.Text = String.Empty;
        }

        private void listViewMessages_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (autoCheck)
                return;

            // Get the location of the mouse pointer
            var loc = listViewMessages.PointToClient(Cursor.Position);

            // Get the bounds of the selected item
            var rec = listViewMessages.GetItemRect(e.Index, ItemBoundsPortion.ItemOnly);

            // Set the width the this rectangle so it accounts for the check box only
            rec.Width = 20;

            // If the pointer is not within the check box bounds, then dont check the item
            if (!rec.Contains(loc))
            {
                e.NewValue = e.CurrentValue;
            }           
        }

        private void listViewMessages_DoubleClick(object sender, EventArgs e)
        {
            var mailbox = Mailbox;

            if (String.Compare((String)comboBoxFolder.SelectedItem, imapRoot, true) == 0 && IsServiceRunning() && mailbox.Enabled)
            {
                MessageBox.Show(this, "Cannot view messages from the root folder whilst Email Import service is running.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (listViewMessages.SelectedItems.Count > 0)
            {
                try
                {
                    var item = listViewMessages.SelectedItems[0];
                    var uid = Convert.ToInt64(item.SubItems[7].Text);

                    using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, (String)comboBoxFolder.SelectedItem))
                    {
                        var message = imap.DownloadMessage(uid);

                        var tmp = Path.Combine(Path.GetTempPath(), "tmp.eml");
                        message.SaveMessage(tmp);

                        Process.Start(tmp);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.ToString(), "View", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var mailbox = Mailbox;

            if (listViewMessages.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select one or more messages to send.", "Send", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.IsNullOrEmpty(textBoxEmailAddress.Text) || !textBoxEmailAddress.Text.EndsWith("@decipha.com.au", StringComparison.CurrentCultureIgnoreCase))
            {
                MessageBox.Show(this, "Please enter a valid Decipha email address.", "Send", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.Compare((String)comboBoxFolder.SelectedItem, imapRoot, true) == 0 && IsServiceRunning() && mailbox.Enabled)
            {
                MessageBox.Show(this, "Cannot send messages from the root folder whilst Email Import service is running.", "Send", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, (String)comboBoxFolder.SelectedItem))
                {
                    foreach (ListViewItem item in listViewMessages.CheckedItems)
                    {
                        var uid = Convert.ToInt64(item.SubItems[7].Text);
                        var message = imap.DownloadMessage(uid);

                        // Attach to new mail message
                        var msg = message.ForwardAsAttachment();

                        // Setup the Mail Message properties
                        msg.From = EmailAddress.Parse("EmailImport.MailboxClient@decipha.com.au");
                        msg.To.AddFromString(textBoxEmailAddress.Text);
                        msg.Subject = mailbox.Description;

                        // Send the Mail Message via SMTP
                        using (Smtp smtp = new Smtp())
                        {                            
                            smtp.SmtpServers.Add(Properties.Settings.Default.SmtpHost);
                            smtp.Message = msg;
                            smtp.Send();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Send", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxFolder.SelectedItem != null)
            {
                var folder = (String)comboBoxFolder.SelectedItem;

                if (MessageBox.Show(this, String.Format("Are you sure you want to delete {0} and all contents?", folder), "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    var mailbox = Mailbox;

                    using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, mailbox.ImapFolder))
                    {
                        imap.DeleteFolder(folder);

                        var cbMoveSelected = comboBoxMove.Text;

                        clearMessages = false;

                        comboBoxFolder.Items.Clear();
                        comboBoxMove.Items.Clear();
                        comboBoxMove.Text = String.Empty;

                        var folders = imap.GetFolderNames();
                        comboBoxFolder.Items.AddRange(folders);
                        comboBoxMove.Items.AddRange(folders);

                        comboBoxMove.SelectedIndex = comboBoxMove.FindString(cbMoveSelected);
                    }
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            svc.Refresh();

            if (svc.Status != ServiceControllerStatus.Running)
            {
                svc.Start();
                svc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));

                RefreshServiceStatus();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            svc.Refresh();

            if (svc.Status != ServiceControllerStatus.Stopped)
            {
                svc.Stop();
                svc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 10));

                RefreshServiceStatus();
            }
        }

        private void labelStatus_DoubleClick(object sender, EventArgs e)
        {
            RefreshServiceStatus();
        }

        private void buttonDeDuplicate_Click(object sender, EventArgs e)
        {
            var mailbox = Mailbox;

            if (listViewMessages.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Please select one or more messages to De-Duplicate.", "De-Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (String.Compare((String)comboBoxFolder.SelectedItem, imapRoot, true) == 0 && IsServiceRunning() && mailbox.Enabled)
            {
                MessageBox.Show(this, "Cannot De-Duplicate messages in the root folder whilst Email Import service is running.", "De-Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int count = 0;

            try
            {
                List<String> keys = new List<String>();

                using (var imap = new Imap(mailbox.HostName, mailbox.Port, mailbox.UserName, mailbox.Password, mailbox.ImapFolder))
                {
                    imap.SelectFolder((String)comboBoxFolder.SelectedItem);

                    foreach (ListViewItem item in listViewMessages.CheckedItems)
                    {
                        var key = String.Format("{0}|{1}|{2}|{3}", item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[4].Text, item.SubItems[8].Text);
                        var uid = item.SubItems[7].Text;

                        if (keys.Contains(key))
                        {
                            imap.DeleteMessages(MailBee.ImapMail.UidCollection.Parse(uid));
                            listViewMessages.Items.Remove(item);
                            count++;
                        }
                        else
                        {
                            keys.Add(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            MessageBox.Show(this, String.Format("{0} duplicated message{1} removed.", count, (count == 1) ? "" : "s"), "De-Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
    }
}
