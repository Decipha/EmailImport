using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Aspose.Email;
using Aspose.Email.Mail;
using EmailImport.Conversion;
using EmailImport.Conversion.Configuration;
using Roslyn.Scripting.CSharp;

namespace EmailImport.Configuration
{
    public partial class MainForm : Form
    {
        #region Member Fields

        private int? lastTimeSlot = null;
        private MailboxProfile lastProfile = null;
        private Dictionary<Guid, String> testFiles = new Dictionary<Guid, String>();

        #endregion

        #region Public Properties

        private MailboxProfile SelectedProfile
        {
            get
            {
                if (listViewMailboxes.SelectedItems.Count == 1)
                    return (MailboxProfile)listViewMailboxes.SelectedItems[0].Tag;
                else
                    return null;
            }
        }

        private MailboxProfile SelectedProfileOrNew
        {
            get { return SelectedProfile ?? new MailboxProfile(); }
        }

        #endregion

        #region Constructor

        public MainForm()
        {
            License license = new License();
            license.SetLicense("Aspose.Total.lic");

            InitializeComponent();

            InitialiseScheduler();
            InitialiseDataGridViews();
            InitialiseComboBoxes();
            InitialiseSnippets();
        }

        private void InitialiseScheduler()
        {
            var doubleBuffered = panelSchedule.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            doubleBuffered.SetValue(panelSchedule, true);

            labelTimeSlot.Text = null;
        }

        private void InitialiseDataGridViews()
        {
            dataGridViewBatchFields.AutoGenerateColumns = false;
            dataGridViewIndexFields.AutoGenerateColumns = false;
            dataGridViewFileTypes.AutoGenerateColumns = false;
            dataGridViewErrorBatchFields.AutoGenerateColumns = false;
            BitDepthFileTypeColumn.Items.AddRange("", 1, 8, 24);
            BinarisationAlgorithmFileTypeColumn.Items.AddRange(new Object[] { "" }.Concat(Enum.GetValues(typeof(BinarisationAlgorithm)).Cast<Object>()).ToArray());
        }

        private void InitialiseComboBoxes()
        {
            comboBoxBitDepth.Items.AddRange(new object[] { 1, 8, 24 });
            comboBoxResolution.Items.AddRange(new object[] { 200, 300, 600 });

            comboBoxBatchStyle.Items.AddRange(Enum.GetValues(typeof(BatchStyle)).Cast<Object>().ToArray());
            comboBoxBodyConversion.Items.AddRange(Enum.GetValues(typeof(BodyConversion)).Cast<Object>().ToArray());
            comboBoxBodyPosition.Items.AddRange(Enum.GetValues(typeof(BodyPosition)).Cast<Object>().ToArray());
            comboBoxAttachmentConversion.Items.AddRange(Enum.GetValues(typeof(AttachmentConversion)).Cast<Object>().ToArray());
            comboBoxNativeFiles.Items.AddRange(Enum.GetValues(typeof(RetainNativeFileOptions)).Cast<Object>().ToArray());
            comboBoxBinarisationAlgorithm.Items.AddRange(Enum.GetValues(typeof(BinarisationAlgorithm)).Cast<Object>().ToArray());
            comboBoxErrorUnsupported.Items.AddRange(Enum.GetValues(typeof(ErrorHandlingActions)).Cast<Object>().ToArray());
            comboBoxErrorUnprocessable.Items.AddRange(Enum.GetValues(typeof(ErrorHandlingActions)).Cast<Object>().ToArray());
            comboBoxErrorUnknown.Items.AddRange(Enum.GetValues(typeof(ErrorHandlingActions)).Cast<Object>().ToArray());
            comboPdfConversion.Items.AddRange(Enum.GetValues(typeof(PdfConversion)).Cast<Object>().ToArray());
            ProcessAsFileTypeColumn.Items.Add("");

            using (EmailImportDataContext ctx = new EmailImportDataContext())
            {
                var items = ctx.Settings.FirstOrDefault(s => s.Name == "ProcessAsItems");

                if (items != null && !String.IsNullOrWhiteSpace(items.Value))
                    ProcessAsFileTypeColumn.Items.AddRange(items.Value.ToUpper().Split('|').OrderBy(i => i).ToArray());
            }
        }

        private void InitialiseSnippets()
        {
            using (var ctx = new EmailImportDataContext())
            {
                foreach (var snippet in ctx.CodeSnippets)
                {
                    if (String.IsNullOrWhiteSpace(snippet.Code))
                        continue;

                    var item = new ToolStripButton(snippet.Name);
                    item.Tag = snippet.Code;
                    item.Click += (sender, e) =>
                        {
                            textBoxScript.InsertText((String)item.Tag);
                        };

                    toolStripDropDownButtonSnippets.DropDownItems.Add(item);
                }

                if (toolStripDropDownButtonSnippets.DropDownItems.Count == 0)
                {
                    toolStripDropDownButtonSnippets.Visible = false;
                }
            }
        }

        #endregion

        #region MainForm Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProfiles();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tabControlConfig.SelectedTab.Focus();

            if (AnyChangesPending(false))
            {
                if (MessageBox.Show(this, "Any unsaved changes will be lost.  Are you sure?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private Boolean AnyChangesPending(Boolean ignoreSelected)
        {
            foreach (ListViewItem item in listViewMailboxes.Items)
            {
                if (item.Selected && ignoreSelected)
                    continue;

                var profile = (MailboxProfile)item.Tag;

                if (profile.HasChanged())
                    return true;
            }

            return false;
        }

        #endregion

        #region Load Profiles

        private void LoadProfiles()
        {
            listViewMailboxes.Clear();
            listViewMailboxes.Columns.Add("Mailbox", listViewMailboxes.Width - 22); //8);

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
            }

            foreach (var profile in profiles.OrderBy(p => p.Group).ThenBy(p => p.Description))
            {
                AddProfile(profile);
            }
        }

        private void AddProfile(MailboxProfile profile)
        {
            var group = listViewMailboxes.Groups[profile.Group ?? "Default"];

            if (group == null)
                group = listViewMailboxes.Groups.Add(profile.Group ?? "Default", profile.Group ?? "Default");

            var item = listViewMailboxes.Items.Add(profile.MailboxGUID.ToString(), profile.Description, null);
            item.Tag = profile;
            item.Group = group;
        }

        private void RemoveProfile(MailboxProfile profile)
        {
            var key = profile.MailboxGUID.ToString();

            var item = listViewMailboxes.Items[key];
            var group = item.Group;

            item.Remove();

            if (group.Items.Count == 0)
                listViewMailboxes.Groups.Remove(group);
        }

        #endregion

        #region Mailbox Selection

        private void CheckProfileForChanges()
        {
            CheckProfileForChanges(SelectedProfile);
        }

        private void CheckProfileForChanges(MailboxProfile profile)
        {
            if (profile != null)
            {
                var item = listViewMailboxes.Items[profile.MailboxGUID.ToString()];

                if (profile.HasChanged())
                {
                    if (!item.Text.EndsWith("*"))
                        item.Text += "*";
                }
                else
                {
                    if (item.Text.EndsWith("*"))
                        item.Text = item.Text.Substring(0, item.Text.Length - 1);
                }
            }
        }

        private void listViewMailboxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var profile = SelectedProfile;

            if (profile != null)
            {
                SelectProfile(profile);
                lastProfile = profile;
            }
            else
            {
                Clear();

                CheckProfileForChanges(lastProfile);
                lastProfile = null;
            }
        }

        private void Clear()
        {
            // Unbind the datagridview's
            dataGridViewBatchFields.DataSource = null;
            dataGridViewIndexFields.DataSource = null;
            dataGridViewFileTypes.DataSource = null;

            // General Settings
            checkBoxEnabled.Checked = false;
            comboBoxGroup.Text = String.Empty;
            textBoxDescription.Text = String.Empty;
            numericUpDownPriority.Value = 5;
            checkBoxLiteViewerEnabled.Checked = false;

            // Schedule
            checkBoxEnableScheduler.Checked = false;
            checkBoxWeekdaysOnly.Checked = false;
            panelSchedule.Invalidate();

            // IMAP Collector
            textBoxImapHost.Text = String.Empty;
            textBoxImapFolder.Text = String.Empty;
            textBoxImapQuery.Text = String.Empty;
            textBoxImapUserName.Text = String.Empty;
            textBoxImapPassword.Text = String.Empty;
            numericUpDownImapPort.Value = 143;
            numericUpDownImapRetention.Value = 14;

            // Output Settings
            checkBoxZip.Checked = false;
            checkBoxBatchClassSubFolder.Checked = false;
            textBoxArchivePath.Text = String.Empty;
            textBoxOutputPath.Text = String.Empty;
            textBoxStoragePath.Text = String.Empty;
            textBoxBatchNumberFormat.Text = String.Empty;
            textBoxOutputFolderFormat.Text = String.Empty;
            numericUpDownStorageRetention.Value = 64;
            numericUpDownArchiveRetention.Value = 64;

            // Kofax Settings
            checkBoxAutoSeparation.Checked = false;
            checkBoxSingleDoc.Checked = false;
            textBoxBatchClass.Text = String.Empty;
            textBoxFolderClass.Text = String.Empty;
            textBoxDocumentClass.Text = String.Empty;
            textBoxFormType.Text = String.Empty;

            // Conversion Settings
            checkBoxRemoveBlanks.Checked = false;
            checkBoxRemoveFaxHeader.Checked = false;
            comboBoxBitDepth.SelectedItem = null;
            comboBoxBatchStyle.SelectedItem = null;
            comboBoxBodyConversion.SelectedItem = null;
            comboBoxBodyPosition.SelectedItem = null;
            comboBoxAttachmentConversion.SelectedItem = null;
            comboBoxNativeFiles.SelectedItem = null;
            comboBoxResolution.SelectedItem = null;
            comboBoxBinarisationAlgorithm.SelectedItem = null;
            comboPdfConversion.SelectedItem = null;
            // Error Handling
            comboBoxErrorUnknown.SelectedItem = null;
            comboBoxErrorUnprocessable.SelectedItem = null;
            comboBoxErrorUnsupported.SelectedItem = null;
            textBoxEscalationEmail.Text = String.Empty;
            textBoxTimeBetweenRetries.Text = String.Empty;
            numericUpDownMaxRetries.Value = 0;
            radioButtonForwardInline.Checked = false;
            radioButtonForwardAttachment.Checked = false;

            // Template Designer
            textBoxTemplateFrom.Text = String.Empty;
            textBoxTemplateTo.Text = String.Empty;
            textBoxTemplateCc.Text = String.Empty;
            textBoxTemplateBcc.Text = String.Empty;
            textBoxTemplateSubject.Text = String.Empty;
            htmlEditorTemplateBody.BodyHtml = String.Empty;

            // Script Editor
            textBoxScript.Text = String.Empty;
        }

        private void SelectProfile(MailboxProfile profile)
        {
            try
            {
                // General settings
                checkBoxEnabled.Checked = profile.Enabled;
                numericUpDownPriority.Value = profile.Priority;
                comboBoxGroup.Text = listViewMailboxes.SelectedItems[0].Group.Name;
                textBoxDescription.Text = profile.Description ?? String.Empty;
                checkBoxLiteViewerEnabled.Checked = profile.LiteViewerEnabled;
                RefreshGroupComboBox();

                // Schedule
                checkBoxEnableScheduler.Checked = profile.EnableScheduler;
                checkBoxWeekdaysOnly.Checked = profile.WeekdayScheduleOnly;
                panelSchedule.Invalidate();

                // IMAP Collector
                textBoxImapHost.Text = profile.ImapHost ?? String.Empty;
                textBoxImapFolder.Text = profile.ImapFolder ?? "Inbox";
                textBoxImapQuery.Text = profile.ImapQuery ?? String.Empty;
                textBoxImapUserName.Text = profile.ImapUserName ?? String.Empty;
                textBoxImapPassword.Text = profile.ImapPassword ?? String.Empty;
                numericUpDownImapPort.Value = profile.ImapPort;
                numericUpDownImapRetention.Value = profile.ImapRetention;

                // Output Settings
                checkBoxZip.Checked = profile.Zip;
                checkBoxBatchClassSubFolder.Checked = profile.BatchClassSubfolder;
                textBoxOutputPath.Text = profile.OutputPath ?? String.Empty;
                textBoxArchivePath.Text = profile.ArchivePath ?? String.Empty;
                textBoxStoragePath.Text = profile.StoragePath ?? String.Empty;
                textBoxBatchNumberFormat.Text = profile.BatchNumberFormat ?? String.Empty;
                textBoxOutputFolderFormat.Text = profile.OutputFolderFormat ?? String.Empty;
                numericUpDownStorageRetention.Value = profile.StorageRetention;
                numericUpDownArchiveRetention.Value = profile.ArchiveRetention;

                // Kofax Settings
                checkBoxAutoSeparation.Checked = profile.AutomaticSeparationAndFormID;
                checkBoxSingleDoc.Checked = profile.SingleDocumentProcessing;
                textBoxBatchClass.Text = profile.BatchClassName ?? String.Empty;
                textBoxFolderClass.Text = profile.FolderClassName ?? String.Empty;
                textBoxDocumentClass.Text = profile.DocumentClassName ?? String.Empty;
                textBoxFormType.Text = profile.FormTypeName ?? String.Empty;

                // Conversion Settings
                checkBoxRemoveBlanks.Checked = profile.RemoveBlankPages;
                checkBoxRemoveFaxHeader.Checked = profile.RemoveFaxHeader;
                comboBoxBitDepth.SelectedItem = profile.BitDepth;
                comboBoxBatchStyle.SelectedItem = profile.BatchStyle;
                comboBoxBodyConversion.SelectedItem = profile.BodyConversion;
                comboBoxBodyPosition.SelectedItem = profile.BodyPosition;
                comboPdfConversion.SelectedItem = profile.PdfConversion;
                comboBoxAttachmentConversion.SelectedItem = profile.AttachmentConversion;
                comboBoxResolution.SelectedItem = profile.Resolution;
                comboBoxNativeFiles.SelectedItem = profile.RetainNativeFiles;
                comboBoxBinarisationAlgorithm.SelectedItem = profile.BinarisationAlgorithm;

                // Error Handling
                comboBoxErrorUnknown.SelectedItem = profile.ErrorHandling.Unknown;
                comboBoxErrorUnprocessable.SelectedItem = profile.ErrorHandling.Unprocessable;
                comboBoxErrorUnsupported.SelectedItem = profile.ErrorHandling.Unsupported;
                textBoxEscalationEmail.Text = profile.EscalationEmail;
                textBoxTimeBetweenRetries.Text = profile.TimeBetweenRetries.ToString();
                numericUpDownMaxRetries.Value = profile.MaximumRetries;
                radioButtonForwardAttachment.Checked = profile.ForwardAsAttachment;
                radioButtonForwardInline.Checked = !profile.ForwardAsAttachment;
                checkBoxProcessErrorBatch.Checked = profile.ProcessErrorBatch;
                textBoxErrorOutputPath.Text = profile.ErrorOutputPath;

                // Template Designer
                textBoxTemplateFrom.Text = profile.TemplateFrom;
                textBoxTemplateTo.Text = profile.TemplateTo;
                textBoxTemplateCc.Text = profile.TemplateCc;
                textBoxTemplateBcc.Text = profile.TemplateBcc;
                textBoxTemplateSubject.Text = profile.TemplateSubject;
                htmlEditorTemplateBody.BodyHtml = profile.TemplateBodyHtml ?? String.Empty;

                // Script Editor
                textBoxScript.Text = profile.Script;

                // Bind the batch field, index field and file types to the datagridview's
                dataGridViewBatchFields.DataSource = new BindingSource(profile.BatchFields, null);
                dataGridViewIndexFields.DataSource = new BindingSource(profile.IndexFields, null);
                dataGridViewFileTypes.DataSource = new BindingSource(profile.FileTypes, null);
                dataGridViewErrorBatchFields.DataSource = new BindingSource(profile.ErrorBatchFields, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(this, e.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
            }
        }

        #endregion

        #region Save Mailbox

        private void Save(MailboxProfile profile)
        {
            using (var ctx = new EmailImportDataContext())
            {
                var mailbox = ctx.Mailboxes.SingleOrDefault(m => m.MailboxGUID == profile.MailboxGUID);

                if (mailbox == null)
                {
                    mailbox = new Mailbox();
                    mailbox.MailboxGUID = profile.MailboxGUID;

                    ctx.Mailboxes.InsertOnSubmit(mailbox);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(MailboxProfile));

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.NewLineHandling = NewLineHandling.Entitize;

                using (TextWriter textWriter = new StringWriter())
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {                    
                    serializer.Serialize(xmlWriter, profile);
                    mailbox.ProfileObject = textWriter.ToString();
                }

                ctx.SubmitChanges();

                profile.OriginalSerializedObject = mailbox.ProfileObject;
            }

            var item = listViewMailboxes.Items[profile.MailboxGUID.ToString()];

            if (item != null && item.Text.EndsWith("*"))
                item.Text = item.Text.Substring(0, item.Text.Length - 1);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckProfileForChanges();

                var selected = SelectedProfile;

                List<MailboxProfile> other = new List<MailboxProfile>();

                foreach (ListViewItem item in listViewMailboxes.Items)
                {
                    var profile = (MailboxProfile)item.Tag;

                    if (profile == selected)
                        continue;

                    if (profile.HasChanged())
                        other.Add(profile);
                }

                if (other.Any())
                {
                    if (MessageBox.Show(this, "Save all modified profiles?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        foreach (var profile in other)
                            Save(profile);
                    }
                }

                if (selected != null)
                    Save(selected);

                if (other.Any() || selected != null)
                    ForceServiceReload();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString(), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ForceServiceReload()
        {
            if (checkBoxReloadOnSave.Checked)
            {
                var machine = String.IsNullOrWhiteSpace(Properties.Settings.Default.EmailImportMachineName) ? "." : Properties.Settings.Default.EmailImportMachineName;

                foreach (var svc in ServiceController.GetServices(machine).Where(s => s.ServiceName.StartsWith("Email Import")))
                {
                    try
                    {
                        if (svc.Status == ServiceControllerStatus.Running)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            svc.ExecuteCommand((int)EmailImportServiceCommand.ReloadMailboxProfiles);

                            Cursor.Current = Cursors.Default;
                        }
                    }
                    finally
                    {
                        svc.Dispose();
                    }
                }
            }
        }

        #endregion

        #region DataGridView - Batch Fields, Index Fields & File Types

        private void dataGridViewBatchFields_MouseClick(object sender, MouseEventArgs e)
        {
            OnDataGridViewFieldsMouseClick((DataGridView)sender, e);
        }

        private void dataGridViewIndexFields_MouseClick(object sender, MouseEventArgs e)
        {
            OnDataGridViewFieldsMouseClick((DataGridView)sender, e);
        }

        private void dataGridViewFileTypes_MouseClick(object sender, MouseEventArgs e)
        {
            OnDataGridViewFieldsMouseClick((DataGridView)sender, e);
        }
        private void dataGridViewErrorBatchFields_MouseClick(object sender, MouseEventArgs e)
        {
            OnDataGridViewFieldsMouseClick((DataGridView)sender, e);
        }

        private void OnDataGridViewFieldsMouseClick(DataGridView dataGridView, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                var ht = dataGridView.HitTest(e.X, e.Y);

                if (ht.ColumnIndex >= 0 && ht.RowIndex >= 0)
                {
                    dataGridView.CurrentCell = dataGridView[ht.ColumnIndex, ht.RowIndex];

                    if (tabControlConfig.SelectedTab != tabPageFileTypes && ht.ColumnIndex == 1)
                    {
                        copyFromToolStripMenuItem.Visible = false;
                        indexFieldsToolStripMenuItem.Visible = (tabControlConfig.SelectedTab == tabPageIndexFields);
                        batchFieldsToolStripMenuItem.Visible = true;

                        contextMenuDataGrid.Show(dataGridView, e.Location);
                    }
                }
                else if (ht.ColumnIndex == -1 && ht.RowIndex == -1)
                {
                    indexFieldsToolStripMenuItem.Visible = false;
                    batchFieldsToolStripMenuItem.Visible = false;

                    copyFromToolStripMenuItem.Visible = true;
                    copyFromToolStripMenuItem.DropDownItems.Clear();

                    foreach (ListViewItem item in listViewMailboxes.Groups[listViewMailboxes.SelectedItems[0].Group.Name].Items)
                    {
                        if (item.Text == listViewMailboxes.SelectedItems[0].Text)
                            continue;

                        var subItem = new ToolStripMenuItem(item.Text);
                        subItem.Tag = item.Tag;
                        subItem.Click += CopyFrom_Click;
                        copyFromToolStripMenuItem.DropDownItems.Add(subItem);
                    }

                    contextMenuDataGrid.Show(dataGridView, e.Location);
                }
            }
        }

        private void CopyFrom_Click(object sender, EventArgs e)
        {
            var profile = SelectedProfile;
            var copyFromProfile = (MailboxProfile)((ToolStripMenuItem)sender).Tag;

            if (tabControlConfig.SelectedTab == tabPageBatchFields)
            {
                profile.BatchFields.AddRange(copyFromProfile.BatchFields);
            }
            else if (tabControlConfig.SelectedTab == tabPageIndexFields)
            {
                profile.IndexFields.AddRange(copyFromProfile.IndexFields);
            }
            else if (tabControlConfig.SelectedTab == tabPageFileTypes)
            {
                profile.FileTypes.AddRange(copyFromProfile.FileTypes);
            }

            CheckProfileForChanges();
        }

        private void predefinedValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControlConfig.SelectedTab == tabPageBatchFields)
            {
                dataGridViewBatchFields.CurrentCell.Value = ((ToolStripMenuItem)sender).Text;
            }
            else if (tabControlConfig.SelectedTab == tabPageIndexFields)
            {
                dataGridViewIndexFields.CurrentCell.Value = ((ToolStripMenuItem)sender).Text;
            }

            CheckProfileForChanges();
        }

        private void dataGridViewFileTypes_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                e.Control.KeyPress -= NumericColumn_KeyPress;

                if (dataGridViewFileTypes.CurrentCell.OwningColumn == MinPixelsFileTypeColumn)
                {
                    e.Control.KeyPress += NumericColumn_KeyPress;
                }
            }
        }

        private void NumericColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == '\b'))
            {
                //is NOT number or is Backspace key
                e.Handled = true;
            }
        }

        private void dataGridViewFileTypes_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CheckProfileForChanges();
        }

        private void dataGridViewFileTypes_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CheckProfileForChanges();
        }

        private void dataGridViewBatchFields_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CheckProfileForChanges();
        }
        private void dataGridViewErrorBatchFields_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CheckProfileForChanges();
        }
        private void dataGridViewBatchFields_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CheckProfileForChanges();
        }
        private void dataGridViewErrorBatchFields_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CheckProfileForChanges();
        }
        private void dataGridViewIndexFields_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            CheckProfileForChanges();
        }

        private void dataGridViewIndexFields_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CheckProfileForChanges();
        }

        #endregion

        #region Group Combo Box

        private void RefreshGroupComboBox()
        {
            comboBoxGroup.Items.Clear();

            foreach (ListViewGroup group in listViewMailboxes.Groups)
            {
                if (group.Items.Count == 0)
                    continue;

                comboBoxGroup.Items.Add(group.Name);
            }
        }

        private ListViewGroup GetGroup(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
                name = "Default";

            ListViewGroup group = null;

            foreach (ListViewGroup g in listViewMailboxes.Groups)
            {
                if (g.Name == name)
                {
                    group = g;
                    break;
                }
            }

            if (group == null)
            {
                group = new ListViewGroup(name, name);
                listViewMailboxes.Groups.Add(group);
            }            

            return group;
        }

        private void comboBoxGroup_Leave(object sender, EventArgs e)
        {
            if (listViewMailboxes.SelectedItems.Count > 0)
            {
                var group = GetGroup(comboBoxGroup.Text);
                listViewMailboxes.SelectedItems[0].Group = group;
                RefreshGroupComboBox();
                comboBoxGroup.Text = group.Name;
            }
        }

        private void comboBoxGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && listViewMailboxes.SelectedItems.Count > 0)
            {
                var group = GetGroup(comboBoxGroup.Text);
                listViewMailboxes.SelectedItems[0].Group = group;
                RefreshGroupComboBox();
                comboBoxGroup.Text = group.Name;
            }
        }

        #endregion

        #region Button Event Handlers

        private void buttonOutputPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please select an Output folder...";
            fbd.SelectedPath = textBoxOutputPath.Text;
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBoxOutputPath.Text = fbd.SelectedPath;
                SelectedProfileOrNew.OutputPath = GetValueOrNull(fbd.SelectedPath);
                CheckProfileForChanges();
            }
        }

        private void buttonArchivePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please select an Archive folder...";
            fbd.SelectedPath = textBoxArchivePath.Text;
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBoxArchivePath.Text = fbd.SelectedPath;
                SelectedProfileOrNew.ArchivePath = GetValueOrNull(fbd.SelectedPath);
                CheckProfileForChanges();
            }
        }

        private void buttonStoragePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please select a Storage folder...";
            fbd.SelectedPath = textBoxStoragePath.Text;
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBoxStoragePath.Text = fbd.SelectedPath;
                SelectedProfileOrNew.StoragePath = GetValueOrNull(fbd.SelectedPath);
                CheckProfileForChanges();
            }
        }

        private void buttonImapQuery_Click(object sender, EventArgs e)
        {
            QueryBuilderDialog dialog = new QueryBuilderDialog();

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxImapQuery.Text = dialog.ImapQuery;
                SelectedProfileOrNew.ImapQuery = GetValueOrNull(dialog.ImapQuery);
                CheckProfileForChanges();
            }
        }

        private void buttonImapFolder_Click(object sender, EventArgs e)
        {
            var profile = SelectedProfile;

            if (profile != null)
            {
                ImapFolderDialog dialog = new ImapFolderDialog();

                if (String.IsNullOrWhiteSpace(profile.ImapHost))
                {
                    MessageBox.Show(this, "Please enter the host name or IP address.", "Imap Folder Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxImapHost.Focus();
                    return;
                }

                if (String.IsNullOrWhiteSpace(profile.ImapUserName))
                {
                    MessageBox.Show(this, "Please enter the user name.", "Imap Folder Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxImapUserName.Focus();
                    return;
                }

                if (String.IsNullOrWhiteSpace(profile.ImapPassword))
                {
                    MessageBox.Show(this, "Please enter the password.", "Imap Folder Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBoxImapPassword.Focus();
                    return;
                }

                dialog.HostName = profile.ImapHost;
                dialog.Port = profile.ImapPort;
                dialog.UserName = profile.ImapUserName;
                dialog.Password = profile.ImapPassword;

                try
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxImapFolder.Text = dialog.SelectedFolder;
                        profile.ImapFolder = GetValueOrNull(dialog.SelectedFolder);
                        CheckProfileForChanges();
                    }
                }
                catch
                {
                    MessageBox.Show(this, String.Format("Unable to connect to or login to {0}.", profile.ImapHost), "Imap Folder Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Context Menu Event Handlers

        private void reloadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = SelectedProfile;

            LoadProfiles();

            if (selected != null)
                SelectProfileByGUID(selected.MailboxGUID);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = SelectedProfile;

            var profile = new MailboxProfile();
            profile.Description = "<<< New Profile >>>";
            profile.Group = (selected != null) ? selected.Group : null;

            AddProfile(profile);
            Save(profile);
            SelectProfileByGUID(profile.MailboxGUID);
        }

        private void SelectProfileByGUID(Guid guid)
        {
            foreach (ListViewItem item in listViewMailboxes.Items)
            {
                if (((MailboxProfile)item.Tag).MailboxGUID == guid)
                {
                    listViewMailboxes.SelectedIndices.Clear();
                    item.Selected = true;
                    textBoxDescription.Focus();
                    textBoxDescription.SelectAll();
                    break;
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = SelectedProfile;

            if (selected != null)
            {
                if (MessageBox.Show(this, "Are you sure you want to delete the selected profile?", "Delete Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    using (var ctx = new EmailImportDataContext())
                    {
                        ctx.Mailboxes.DeleteOnSubmit(ctx.Mailboxes.Single(m => m.MailboxGUID == selected.MailboxGUID));
                        ctx.SubmitChanges();
                    }

                    RemoveProfile(selected);
                    Clear();
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = SelectedProfile;

            if (selected != null)
            {
                var copy = new MailboxProfile();

                foreach (var property in selected.GetType().GetProperties())
                {
                    if (property.CanRead && property.CanWrite)
                        property.SetValue(copy, property.GetValue(selected));
                }

                copy.MailboxGUID = Guid.NewGuid();
                copy.Description = String.Format("Copy of {0}", copy.Description);

                Save(copy);
                LoadProfiles();
                SelectProfileByGUID(copy.MailboxGUID);
            }
        }

        private void contextMenuMailboxes_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            deleteToolStripMenuItem.Visible = (listViewMailboxes.SelectedItems.Count == 1);
            copyToolStripMenuItem.Visible = (listViewMailboxes.SelectedItems.Count == 1);
            exportToolStripMenuItem.Visible = (listViewMailboxes.SelectedItems.Count > 0);
        }

        #endregion

        #region Schedule Tab

        private void checkBoxEnableSchedule_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSchedule.Enabled = checkBoxEnableScheduler.Checked;
            checkBoxWeekdaysOnly.Enabled = checkBoxEnableScheduler.Checked;

            SelectedProfileOrNew.EnableScheduler = checkBoxEnableScheduler.Checked;
            CheckProfileForChanges();
        }

        private void panelSchedule_Paint(object sender, PaintEventArgs e)
        {
            int x = 0;

            var profile = SelectedProfile;
            var brush = panelSchedule.Enabled ? Brushes.Green : Brushes.DarkGray;
            var color = (tabPageSchedule.BackColor == SystemColors.ActiveBorder) ? SystemColors.ControlDark : SystemColors.ActiveBorder;

            Pen pen = new Pen(color, 1);
            pen.DashStyle = DashStyle.Dot;

            for (int i = 0; i < 24; i++)
            {
                var rect = new Rectangle(x, 0, 25, 25);

                ControlPaint.DrawBorder(e.Graphics, rect, color, ButtonBorderStyle.Solid);

                if (profile != null)
                {
                    rect.Y = 1;
                    rect.Height = 23;

                    if (profile.Schedule[i * 2])
                    {
                        rect.X = x + 1;
                        rect.Width = 12; // 1;
                        e.Graphics.FillRectangle(brush, rect);
                    }

                    if (profile.Schedule[(i * 2) + 1])
                    {
                        rect.X = x + 12;
                        rect.Width = 12;
                        e.Graphics.FillRectangle(brush, rect);
                    }
                }

                e.Graphics.DrawLine(pen, x + 12, 0, x + 12, 25);

                x += 24;
            }
        }

        private void panelSchedule_MouseLeave(object sender, EventArgs e)
        {
            labelTimeSlot.Text = null;
            lastTimeSlot = null;
        }

        private void panelSchedule_MouseDown(object sender, MouseEventArgs e)
        {
            var profile = SelectedProfile;
            
            if (profile != null)
            {
                var timeSlot = GetTimeSlot(e.Location);

                if (timeSlot.HasValue)
                {
                    profile.Schedule[timeSlot.Value] = !profile.Schedule[timeSlot.Value];
                    lastTimeSlot = timeSlot;
                    panelSchedule.Invalidate();
                    CheckProfileForChanges();
                }
            }
        }

        private void panelSchedule_MouseMove(object sender, MouseEventArgs e)
        {
            var profile = SelectedProfile;

            if (profile != null)
            {
                var timeSlot = GetTimeSlot(e.Location);

                if (timeSlot.HasValue)
                {
                    int hour = timeSlot.Value / 2;
                    var suffix = "AM";

                    if (hour == 0)
                    {
                        hour = 12;
                    }
                    else if (hour == 12)
                    {
                        suffix = "PM";
                    }
                    else if (hour >= 13)
                    {
                        hour -= 12;
                        suffix = "PM";
                    }                       

                    if (timeSlot.Value % 2 == 0)
                        labelTimeSlot.Text = String.Format("{0}:00 {1} - {0}:29 {1}", hour, suffix);
                    else
                        labelTimeSlot.Text = String.Format("{0}:30 {1} - {0}:59 {1}", hour, suffix);

                    if (e.Button == MouseButtons.Left && lastTimeSlot != timeSlot)
                    {
                        profile.Schedule[timeSlot.Value] = !profile.Schedule[timeSlot.Value];
                        lastTimeSlot = timeSlot;
                        panelSchedule.Invalidate();
                        CheckProfileForChanges();
                    }
                }
                else
                {
                    labelTimeSlot.Text = null;
                    lastTimeSlot = null;
                }
            }
        }

        private int? GetTimeSlot(Point point)
        {
            if (point.X < 0 || point.X >= panelSchedule.Width)
                return null;

            if (point.Y < 0 || point.Y >= panelSchedule.Height)
                return null;

            if (point.X == panelSchedule.Width - 1)
                return 47;

            return point.X / 12;
        }

        #endregion

        #region Script Editor

        //
        // Future Enhancements:
        // 
        // Add more script entry points
        // Change interface to allow execution/testing of individual events
        // Change Test split button to Settings and add a new Execute/Test/Debug dropdown/split button for executing individual or selected events
        //

        private void toolStripButtonCreateNewScript_Click(object sender, EventArgs e)
        {
            textBoxScript.Text = SelectedProfileOrNew.Script = Properties.Resources.DefaultScript;
            CheckProfileForChanges();
        }

        private void toolStripSplitButtonTestScript_ButtonClick(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxScript.Text))
                return;

            try
            {
                var engine = new ScriptEngine();

                engine.AddReference("System");
                engine.AddReference("System.Core");
                engine.AddReference(typeof(MailMessage).Assembly);
                engine.AddReference(typeof(ScriptContext).Assembly);

                if (enableWindowsFormsToolStripMenuItem.Checked)
                    engine.AddReference("System.Windows.Forms");

                var profile = SelectedProfile;

                var context = new ScriptContext();
                context.IgnoreMessage = false;

                if (profile != null && testFiles.ContainsKey(profile.MailboxGUID))
                {
                    var options = new MailMessageLoadOptions();
                    options.FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking;

                    switch (Path.GetExtension(testFiles[profile.MailboxGUID]).ToLower())
                    {
                        case "eml":
                            options.MessageFormat = MessageFormat.Eml;
                            break;

                        case "msg":
                            options.MessageFormat = MessageFormat.Msg;
                            break;
                    }

                    context.Message = MailMessage.Load(testFiles[profile.MailboxGUID], options);
                }
                else
                {
                    context.Message = new MailMessage();    
                }

                var session = engine.CreateSession(context);
                session.Execute(textBoxScript.Text);
                session.Execute("MailMessage_Loaded();");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Test Script Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectMailMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var profile = SelectedProfile;

            if (profile != null)
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.CheckFileExists = true;
                ofd.Multiselect = false;
                ofd.Filter = "Mail Message File|*.msg;*.eml";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    testFiles[profile.MailboxGUID] = ofd.FileName;
                }
            }
        }

        #endregion

        #region Update Event Handlers

        #region General Settings

        private void checkBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.Enabled = checkBoxEnabled.Checked;
            CheckProfileForChanges();
        }

        private void textBoxDescription_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxDescription.Text))
                textBoxDescription.Text = SelectedProfileOrNew.Description;
            else
                SelectedProfileOrNew.Description = GetValueOrNull(textBoxDescription.Text);

            if (listViewMailboxes.SelectedItems.Count > 0)
                listViewMailboxes.SelectedItems[0].Text = textBoxDescription.Text;

            CheckProfileForChanges();
        }

        private void textBoxDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '*')
            {
                e.Handled = true;
            }
        }

        private void comboBoxGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.Group = GetValueOrNull(comboBoxGroup.Text);
            CheckProfileForChanges();
        }

        private void numericUpDownPriority_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.Priority = (int)numericUpDownPriority.Value;
            CheckProfileForChanges();
        }

        private void checkBoxLiteViewerEnabled_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.LiteViewerEnabled = checkBoxLiteViewerEnabled.Checked;
            CheckProfileForChanges();
        }
        #endregion

        #region Schedule

        private void checkBoxWeekdaysOnly_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.WeekdayScheduleOnly = checkBoxWeekdaysOnly.Checked;
            CheckProfileForChanges();
        }

        #endregion

        #region IMAP Collector

        private void textBoxImapHost_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ImapHost = GetValueOrNull(textBoxImapHost.Text);
            CheckProfileForChanges();
        }

        private void textBoxImapFolder_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ImapFolder = GetValueOrNull(textBoxImapFolder.Text);
            CheckProfileForChanges();
        }

        private void textBoxImapQuery_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ImapQuery = GetValueOrNull(textBoxImapQuery.Text);
            CheckProfileForChanges();
        }

        private void textBoxImapUserName_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ImapUserName = GetValueOrNull(textBoxImapUserName.Text);
            CheckProfileForChanges();
        }

        private void textBoxImapPassword_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ImapPassword = GetValueOrNull(textBoxImapPassword.Text);
            CheckProfileForChanges();
        }

        private void numericUpDownImapPort_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ImapPort = (int)numericUpDownImapPort.Value;
            CheckProfileForChanges();
        }

        private void numericUpDownImapRetention_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ImapRetention = (int)numericUpDownImapRetention.Value;
            CheckProfileForChanges();
        }

        #endregion

        #region Output Settings

        private void textBoxOutputPath_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.OutputPath = GetValueOrNull(textBoxOutputPath.Text);
            CheckProfileForChanges();
        }

        private void textBoxArchivePath_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ArchivePath = GetValueOrNull(textBoxArchivePath.Text);
            CheckProfileForChanges();
        }

        private void textBoxStoragePath_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.StoragePath = GetValueOrNull(textBoxStoragePath.Text);
            CheckProfileForChanges();
        }

        private void textBoxBatchNumberFormat_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.BatchNumberFormat = GetValueOrNull(textBoxBatchNumberFormat.Text);
            CheckProfileForChanges();
        }

        private void textBoxOutputFolderFormat_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.OutputFolderFormat = GetValueOrNull(textBoxOutputFolderFormat.Text);
            CheckProfileForChanges();
        }

        private void checkBoxZip_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.Zip = checkBoxZip.Checked;
            CheckProfileForChanges();
        }

        private void checkBoxBatchClassSubFolder_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.BatchClassSubfolder = checkBoxBatchClassSubFolder.Checked;
            CheckProfileForChanges();
        }

        private void numericUpDownStorageRetention_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.StorageRetention = (int)numericUpDownStorageRetention.Value;
            CheckProfileForChanges();
        }

        private void numericUpDownArchiveRetention_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ArchiveRetention = (int)numericUpDownArchiveRetention.Value;
            CheckProfileForChanges();
        }

        #endregion

        #region Kofax Settings

        private void checkBoxAutoSeparation_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.AutomaticSeparationAndFormID = checkBoxAutoSeparation.Checked;
            CheckProfileForChanges();
        }

        private void checkBoxSingleDoc_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.SingleDocumentProcessing = checkBoxSingleDoc.Checked;
            CheckProfileForChanges();
        }

        private void textBoxBatchClass_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.BatchClassName = GetValueOrNull(textBoxBatchClass.Text);
            CheckProfileForChanges();
        }

        private void textBoxFolderClass_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.FolderClassName = GetValueOrNull(textBoxFolderClass.Text);
            CheckProfileForChanges();
        }

        private void textBoxDocumentClass_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.DocumentClassName = GetValueOrNull(textBoxDocumentClass.Text);
            CheckProfileForChanges();
        }

        private void textBoxFormType_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.FormTypeName = GetValueOrNull(textBoxFormType.Text);
            CheckProfileForChanges();
        }

        #endregion

        #region Conversion Settings

        private void checkBoxRemoveBlanks_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.RemoveBlankPages = checkBoxRemoveBlanks.Checked;
            CheckProfileForChanges();
        }

        private void checkBoxRemoveFaxHeader_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.RemoveFaxHeader = checkBoxRemoveFaxHeader.Checked;
            CheckProfileForChanges();
        }

        private void comboBoxBitDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBitDepth.SelectedItem != null)
            {
                SelectedProfileOrNew.BitDepth = (int)comboBoxBitDepth.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxResolution.SelectedItem != null)
            {
                SelectedProfileOrNew.Resolution = (int)comboBoxResolution.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxBatchStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBatchStyle.SelectedItem != null)
            {
                SelectedProfileOrNew.BatchStyle = (BatchStyle)comboBoxBatchStyle.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxBodyConversion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBodyConversion.SelectedItem != null)
            {
                SelectedProfileOrNew.BodyConversion = (BodyConversion)comboBoxBodyConversion.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxBodyPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBodyPosition.SelectedItem != null)
            {
                SelectedProfileOrNew.BodyPosition = (BodyPosition)comboBoxBodyPosition.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxAttachmentConversion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxAttachmentConversion.SelectedItem != null)
            {
                SelectedProfileOrNew.AttachmentConversion = (AttachmentConversion)comboBoxAttachmentConversion.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxNativeFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxNativeFiles.SelectedItem != null)
            {
                SelectedProfileOrNew.RetainNativeFiles = (RetainNativeFileOptions)comboBoxNativeFiles.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxBinarisationAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBinarisationAlgorithm.SelectedItem != null)
            {
                SelectedProfileOrNew.BinarisationAlgorithm = (BinarisationAlgorithm)comboBoxBinarisationAlgorithm.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboPdfConversion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPdfConversion.SelectedItem != null)
            {
                SelectedProfileOrNew.PdfConversion = (PdfConversion)comboPdfConversion.SelectedItem;
                CheckProfileForChanges();
            }
        }
        #endregion

        #region Error Handling

        private void comboBoxErrorUnsupported_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxErrorUnsupported.SelectedItem != null)
            {
                SelectedProfileOrNew.ErrorHandling.Unsupported = (ErrorHandlingActions)comboBoxErrorUnsupported.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxErrorUnprocessable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxErrorUnprocessable.SelectedItem != null)
            {
                SelectedProfileOrNew.ErrorHandling.Unprocessable = (ErrorHandlingActions)comboBoxErrorUnprocessable.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void comboBoxErrorUnknown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxErrorUnknown.SelectedItem != null)
            {
                SelectedProfileOrNew.ErrorHandling.Unknown = (ErrorHandlingActions)comboBoxErrorUnknown.SelectedItem;
                CheckProfileForChanges();
            }
        }

        private void textBoxEscalationEmail_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.EscalationEmail = GetValueOrNull(textBoxEscalationEmail.Text);
            CheckProfileForChanges();
        }

        private void radioButtonForwardAttachment_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ForwardAsAttachment = radioButtonForwardAttachment.Checked;
            CheckProfileForChanges();
        }

        private void textBoxTimeBetweenRetries_Leave(object sender, EventArgs e)
        {
            TimeSpan time;

            if (TimeSpan.TryParse(textBoxTimeBetweenRetries.Text, out time))
                SelectedProfileOrNew.TimeBetweenRetries = time;
            else if (SelectedProfile != null)
                textBoxTimeBetweenRetries.Text = SelectedProfile.TimeBetweenRetries.ToString();
            else
                textBoxTimeBetweenRetries.Text = null;

            CheckProfileForChanges();
        }

        private void numericUpDownMaxRetries_ValueChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.MaximumRetries = (int)numericUpDownMaxRetries.Value;
            CheckProfileForChanges();
        }
        private void checkBoxProcessErrorBatch_CheckedChanged(object sender, EventArgs e)
        {
            SelectedProfileOrNew.ProcessErrorBatch = checkBoxProcessErrorBatch.Checked;
            CheckProfileForChanges();
        }
        private void btnErrorOutputPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please select an Output folder...";
            fbd.SelectedPath = textBoxErrorOutputPath.Text;
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBoxErrorOutputPath.Text = fbd.SelectedPath;
                SelectedProfileOrNew.ErrorOutputPath = GetValueOrNull(fbd.SelectedPath);
                CheckProfileForChanges();
            }
        }
        #endregion

        #region Template Designer

        private void textBoxTemplateFrom_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.TemplateFrom = GetValueOrNull(textBoxTemplateFrom.Text);
            CheckProfileForChanges();
        }

        private void textBoxTemplateTo_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.TemplateTo = GetValueOrNull(textBoxTemplateTo.Text);
            CheckProfileForChanges();
        }

        private void textBoxTemplateCc_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.TemplateCc = GetValueOrNull(textBoxTemplateCc.Text);
            CheckProfileForChanges();
        }

        private void textBoxTemplateBcc_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.TemplateBcc = GetValueOrNull(textBoxTemplateBcc.Text);
            CheckProfileForChanges();
        }

        private void textBoxTemplateSubject_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.TemplateSubject = GetValueOrNull(textBoxTemplateSubject.Text);
            CheckProfileForChanges();
        }

        private void htmlEditorTemplateBody_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.TemplateBodyHtml = GetValueOrNull(htmlEditorTemplateBody.DocumentHtml);
            CheckProfileForChanges();
        }

        private void linkLabelTemplateEditHtml_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            htmlEditorTemplateBody.HtmlContentsEdit();
            SelectedProfileOrNew.TemplateBodyHtml = GetValueOrNull(htmlEditorTemplateBody.DocumentHtml);
            CheckProfileForChanges();
        }

        private void linkLabelTemplateDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            htmlEditorTemplateBody.BodyHtml = SelectedProfileOrNew.TemplateBodyHtml = Properties.Resources.DefaultTemplateBody;

            if (String.IsNullOrWhiteSpace(textBoxTemplateFrom.Text))
                textBoxTemplateFrom.Text = SelectedProfileOrNew.TemplateFrom = Properties.Resources.DefaultTemplateFrom;

            if (String.IsNullOrWhiteSpace(textBoxTemplateBcc.Text))
                textBoxTemplateBcc.Text = SelectedProfileOrNew.TemplateBcc = Properties.Resources.DefaultTemplateBcc;

            if (String.IsNullOrWhiteSpace(textBoxTemplateSubject.Text))
                textBoxTemplateSubject.Text = SelectedProfileOrNew.TemplateSubject = Properties.Resources.DefaultTemplateSubject;

            CheckProfileForChanges();
        }

        private void linkLabelClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectedProfileOrNew.TemplateBcc = null;
            SelectedProfileOrNew.TemplateBodyHtml = null;
            SelectedProfileOrNew.TemplateCc = null;
            SelectedProfileOrNew.TemplateFrom = null;
            SelectedProfileOrNew.TemplateSubject = null;
            SelectedProfileOrNew.TemplateTo = null;
            
            textBoxTemplateFrom.Text = String.Empty;
            textBoxTemplateTo.Text = String.Empty;
            textBoxTemplateCc.Text = String.Empty;
            textBoxTemplateBcc.Text = String.Empty;
            textBoxTemplateSubject.Text = String.Empty;
            htmlEditorTemplateBody.BodyHtml = String.Empty;

            CheckProfileForChanges();
        }

        #endregion

        #region Script Editor

        private void textBoxScript_Leave(object sender, EventArgs e)
        {
            SelectedProfileOrNew.Script = GetValueOrNull(textBoxScript.Text);
            CheckProfileForChanges();
        }

        #endregion

        #endregion

        #region Import & Export Profile

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Please select a folder to export to...";
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MailboxProfile));

                foreach (ListViewItem item in listViewMailboxes.SelectedItems)
                {
                    var profile = (MailboxProfile)item.Tag;

                    using (TextWriter writer = new StringWriter())
                    {
                        serializer.Serialize(writer, profile);
                        File.WriteAllText(Path.Combine(fbd.SelectedPath, String.Format("[{0}] {1}.eip", profile.Group, profile.Description)), writer.ToString());
                    }
                }

                MessageBox.Show(this, "Export Complete!", "Export Profiles", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = "Email Import Profile|*.eip";
            ofd.Multiselect = true;
            ofd.Title = "Please select profiles to import...";

            if (ofd.ShowDialog(this) == DialogResult.OK && ofd.FileNames.Any())
            {
                List<MailboxProfile> profiles = new List<MailboxProfile>();
                XmlSerializer serializer = new XmlSerializer(typeof(MailboxProfile));

                foreach (var file in ofd.FileNames)
                {
                    using (TextReader reader = new StringReader(File.ReadAllText(file)))
                    {
                        profiles.Add((MailboxProfile)serializer.Deserialize(reader));
                    }
                }

                if (profiles.Any(p => ProfileExists(p.MailboxGUID)) && MessageBox.Show(this, "Existing profiles will be overwritten. Are you sure?", "Import Profiles", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;

                foreach (var profile in profiles)
                {
                    Save(profile);
                }

                LoadProfiles();
                SelectProfileByGUID(profiles.Last().MailboxGUID);
            }
        }

        #endregion

        #region Private Methods

        private String GetValueOrNull(String value)
        {
            return String.IsNullOrEmpty(value) ? null : value;
        }

        private Boolean ProfileExists(Guid mailboxGUID)
        {
            foreach (ListViewItem item in listViewMailboxes.Items)
            {
                var profile = (MailboxProfile)item.Tag;

                if (profile.MailboxGUID == mailboxGUID)
                    return true;
            }

            return false;
        }





        #endregion

        
    }
}
