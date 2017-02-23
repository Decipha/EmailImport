using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Aspose.Email.Mail;

namespace EmailImport.Viewer
{
    public partial class ErrorsDialog : Form
    {
        #region Fields

        int currentErrorNumber = -1;
        int totalErrors = 0;

        bool inDisplayError = false;

        #endregion

        #region Properties

        public Email Email { get; set; }

        private static String[] processAsItems = null;

        private static String[] ProcessAsItems
        {
            get
            {
                if (processAsItems == null)
                {
                    using (EmailImportDataContext ctx = new EmailImportDataContext())
                    {
                        var items = ctx.Settings.FirstOrDefault(s => s.Name == "ProcessAsItems");

                        if (items != null && !String.IsNullOrWhiteSpace(items.Value))
                            processAsItems = items.Value.ToUpper().Split('|').OrderBy(i => i).ToArray();
                    }
                }

                return processAsItems;
            }
        }

        #endregion

        #region Constructors

        public ErrorsDialog()
        {
            InitializeComponent();

            // Fill the ProcessAs combo box
            comboBoxProcessAs.Items.Add("");
            comboBoxProcessAs.Items.AddRange(ProcessAsItems);
        }

        #endregion

        #region Event Handlers

        private void ErrorsDialog_Load(object sender, EventArgs e)
        {
            // Get the total number of errors
            totalErrors = Email.Errors.Elements().Count();

            // if at least one error exists, display it
            if (totalErrors > 0)
                DisplayError(0);
        }

        private void ErrorsDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void linkLabelFileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (currentErrorNumber >= 0 && currentErrorNumber < totalErrors)
            {
                var key = (String)Email.Errors.Elements().ElementAt(currentErrorNumber).Attribute("key");

                string tempFilePath = Path.Combine(Path.GetTempPath(), "EmailImport.Viewer", key);

                Cursor = Cursors.WaitCursor;

                SaveAttachment(key, tempFilePath);

                using (Process process = Process.Start(tempFilePath))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                    }
                }

                Cursor = Cursors.Default;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (currentErrorNumber >= 0 && currentErrorNumber < totalErrors)
            {
                var key = (String)Email.Errors.Elements().ElementAt(currentErrorNumber).Attribute("key");

                if (!String.IsNullOrEmpty(key))
                {
                    SaveFileDialog dialog = new SaveFileDialog();

                    dialog.FileName = key;
                    if (dialog.ShowDialog() == DialogResult.OK)
                        SaveAttachment(key, dialog.FileName);
                }
            }
        }

        private void comboBoxOverride_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentErrorNumber >= 0 && currentErrorNumber < totalErrors && !inDisplayError)
            {
                var error = Email.Errors.Elements().ElementAt(currentErrorNumber);

                error.SetAttributeValue("override", (string)comboBoxOverride.SelectedItem);

                UpdateDatabase();
            }
        }

        private void comboBoxProcessAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentErrorNumber >= 0 && currentErrorNumber < totalErrors && !inDisplayError)
            {
                var error = Email.Errors.Elements().ElementAt(currentErrorNumber);

                error.SetAttributeValue("processAs", (string)comboBoxProcessAs.SelectedItem);

                UpdateDatabase();
            }
        }

        private void UpdateDatabase()
        {
            using (EmailImportDataContext dc = new EmailImportDataContext())
            {
                var email = dc.Emails.Where(em => em.EmailID == Email.EmailID).FirstOrDefault();

                if (email != null)
                {
                    email.Errors = Email.Errors;
                    dc.SubmitChanges();
                }
            }
        }

        private void buttonPreviousError_Click(object sender, EventArgs e)
        {
            DisplayError(currentErrorNumber - 1);
        }

        private void buttonNextError_Click(object sender, EventArgs e)
        {
            DisplayError(currentErrorNumber + 1);
        }

        #endregion

        #region Processing

        private void DisplayError(int errorNumber)
        {
            inDisplayError = true;

            if (errorNumber >= 0 && errorNumber < totalErrors)
            {
                var error = Email.Errors.Elements().ElementAt(errorNumber);
                var key = (String)error.Attribute("key");

                // set fields
                linkLabelFileName.Enabled = !String.IsNullOrEmpty(key);
                linkLabelFileName.Text = !String.IsNullOrEmpty(key) ? key : "<None>";
                buttonSave.Enabled = !String.IsNullOrEmpty(key);
                textBoxReason.Text = (String)error.Attribute("reason");
                textBoxMessage.Text = (String)error.Attribute("message");
                textBoxAction.Text = (String)error.Attribute("action");
                comboBoxOverride.Enabled = true;
                comboBoxOverride.SelectedItem = (String)error.Attribute("override");
                comboBoxProcessAs.Enabled = true;
                comboBoxProcessAs.SelectedItem = (String)error.Attribute("processAs");

                // update next/previous buttons
                buttonPreviousError.Enabled = errorNumber > 0;
                buttonNextError.Enabled = errorNumber < totalErrors - 1;

                // save current error number being displayed
                currentErrorNumber = errorNumber;
            }

            inDisplayError = false;
        }

        private void SaveAttachment(string key, string fileName)
        {
            var message = MailMessage.Load(Email.MessageFilePath);

            if (message != null)
            {
                var attachment = message.Attachments.Where(a => a.Name == key).FirstOrDefault();

                if (attachment != null)
                    attachment.Save(fileName);
            }
        }

        #endregion
    }
}
