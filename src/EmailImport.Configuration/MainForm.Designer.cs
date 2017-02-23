namespace EmailImport.Configuration
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listViewMailboxes = new System.Windows.Forms.ListView();
            this.contextMenuMailboxes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.batchFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replyToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replyToDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replyToAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.senderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.senderDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.senderAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ccToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ccDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ccAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.htmlBodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchCreationDateTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateSentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateReceivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.isSignedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensitivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.attachmentFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headerHeaderNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentIsBodyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentAttachmentFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentFailureReasonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabPageFileTypes = new System.Windows.Forms.TabPage();
            this.dataGridViewFileTypes = new System.Windows.Forms.DataGridView();
            this.FilenameRegexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessAsFileTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IgnoreFileTypeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PassthroughFileTypeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AutoDeskewFileTypeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AutoRotateFileTypeColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.BitDepthFileTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BinarisationAlgorithmFileTypeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.MinPixelsFileTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageIndexFields = new System.Windows.Forms.TabPage();
            this.dataGridViewIndexFields = new System.Windows.Forms.DataGridView();
            this.IndexFieldNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndexFieldValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndexFieldRegexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageBatchFields = new System.Windows.Forms.TabPage();
            this.dataGridViewBatchFields = new System.Windows.Forms.DataGridView();
            this.BatchFieldNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchFieldValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchFieldRegexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.groupBoxKofax = new System.Windows.Forms.GroupBox();
            this.labelDocumentClass = new System.Windows.Forms.Label();
            this.textBoxDocumentClass = new System.Windows.Forms.TextBox();
            this.textBoxFormType = new System.Windows.Forms.TextBox();
            this.checkBoxAutoSeparation = new System.Windows.Forms.CheckBox();
            this.labelFormType = new System.Windows.Forms.Label();
            this.checkBoxSingleDoc = new System.Windows.Forms.CheckBox();
            this.textBoxFolderClass = new System.Windows.Forms.TextBox();
            this.labelBatchClass = new System.Windows.Forms.Label();
            this.labelFolderClass = new System.Windows.Forms.Label();
            this.textBoxBatchClass = new System.Windows.Forms.TextBox();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.numericUpDownArchiveRetention = new System.Windows.Forms.NumericUpDown();
            this.labelArchiveRetention = new System.Windows.Forms.Label();
            this.textBoxOutputFolderFormat = new System.Windows.Forms.TextBox();
            this.labelOutputFolderFormat = new System.Windows.Forms.Label();
            this.checkBoxBatchClassSubFolder = new System.Windows.Forms.CheckBox();
            this.labelStorageRetention = new System.Windows.Forms.Label();
            this.numericUpDownStorageRetention = new System.Windows.Forms.NumericUpDown();
            this.labelBatchNumberFormat = new System.Windows.Forms.Label();
            this.textBoxBatchNumberFormat = new System.Windows.Forms.TextBox();
            this.checkBoxZip = new System.Windows.Forms.CheckBox();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.buttonStoragePath = new System.Windows.Forms.Button();
            this.labelOutputPath = new System.Windows.Forms.Label();
            this.labelArchivePath = new System.Windows.Forms.Label();
            this.textBoxStoragePath = new System.Windows.Forms.TextBox();
            this.buttonOutputPath = new System.Windows.Forms.Button();
            this.textBoxArchivePath = new System.Windows.Forms.TextBox();
            this.labelStoragePath = new System.Windows.Forms.Label();
            this.buttonArchivePath = new System.Windows.Forms.Button();
            this.groupBoxGeneral = new System.Windows.Forms.GroupBox();
            this.checkBoxLiteViewerEnabled = new System.Windows.Forms.CheckBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.comboBoxGroup = new System.Windows.Forms.ComboBox();
            this.labelGroup = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelPriority = new System.Windows.Forms.Label();
            this.numericUpDownPriority = new System.Windows.Forms.NumericUpDown();
            this.groupBoxConversion = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboPdfConversion = new System.Windows.Forms.ComboBox();
            this.comboBoxBodyPosition = new System.Windows.Forms.ComboBox();
            this.labelBodyPos = new System.Windows.Forms.Label();
            this.checkBoxRemoveFaxHeader = new System.Windows.Forms.CheckBox();
            this.comboBoxBinarisationAlgorithm = new System.Windows.Forms.ComboBox();
            this.labelBinarisationAlgorithm = new System.Windows.Forms.Label();
            this.comboBoxNativeFiles = new System.Windows.Forms.ComboBox();
            this.labelNativeFiles = new System.Windows.Forms.Label();
            this.comboBoxResolution = new System.Windows.Forms.ComboBox();
            this.checkBoxRemoveBlanks = new System.Windows.Forms.CheckBox();
            this.comboBoxBodyConversion = new System.Windows.Forms.ComboBox();
            this.comboBoxAttachmentConversion = new System.Windows.Forms.ComboBox();
            this.comboBoxBatchStyle = new System.Windows.Forms.ComboBox();
            this.labelBatchStyle = new System.Windows.Forms.Label();
            this.labelBodyConv = new System.Windows.Forms.Label();
            this.labelAttachmentConv = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxBitDepth = new System.Windows.Forms.ComboBox();
            this.labelResolution = new System.Windows.Forms.Label();
            this.groupBoxCollection = new System.Windows.Forms.GroupBox();
            this.buttonImapFolder = new System.Windows.Forms.Button();
            this.buttonImapQuery = new System.Windows.Forms.Button();
            this.textBoxImapQuery = new System.Windows.Forms.TextBox();
            this.textBoxImapFolder = new System.Windows.Forms.TextBox();
            this.numericUpDownImapRetention = new System.Windows.Forms.NumericUpDown();
            this.labelImapRetention = new System.Windows.Forms.Label();
            this.labelImapQuery = new System.Windows.Forms.Label();
            this.labelImapFolder = new System.Windows.Forms.Label();
            this.textBoxImapPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxImapUserName = new System.Windows.Forms.TextBox();
            this.labelUserName = new System.Windows.Forms.Label();
            this.numericUpDownImapPort = new System.Windows.Forms.NumericUpDown();
            this.labelImapPort = new System.Windows.Forms.Label();
            this.labelImapHost = new System.Windows.Forms.Label();
            this.textBoxImapHost = new System.Windows.Forms.TextBox();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.tabControlConfig = new System.Windows.Forms.TabControl();
            this.tabPageSchedule = new System.Windows.Forms.TabPage();
            this.groupBoxSchedule = new System.Windows.Forms.GroupBox();
            this.panelSchedule = new System.Windows.Forms.Panel();
            this.labelTimeSlot = new System.Windows.Forms.Label();
            this.checkBoxEnableScheduler = new System.Windows.Forms.CheckBox();
            this.checkBoxWeekdaysOnly = new System.Windows.Forms.CheckBox();
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.textBoxScript = new FastColoredTextBoxNS.FastColoredTextBox();
            this.toolStripScript = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCreateNewScript = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonSnippets = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSplitButtonTestScript = new System.Windows.Forms.ToolStripSplitButton();
            this.selectMailMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableWindowsFormsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageErrorHandling = new System.Windows.Forms.TabPage();
            this.groupBoxErrorBatchFields = new System.Windows.Forms.GroupBox();
            this.dataGridViewErrorBatchFields = new System.Windows.Forms.DataGridView();
            this.groupBoxConversionErrorBatch = new System.Windows.Forms.GroupBox();
            this.textBoxErrorOutputPath = new System.Windows.Forms.TextBox();
            this.btnErrorOutputPath = new System.Windows.Forms.Button();
            this.labelProcessErrorBatch = new System.Windows.Forms.Label();
            this.checkBoxProcessErrorBatch = new System.Windows.Forms.CheckBox();
            this.labelErrorPath = new System.Windows.Forms.Label();
            this.groupBoxErrorGeneral = new System.Windows.Forms.GroupBox();
            this.labelEscalationEmail = new System.Windows.Forms.Label();
            this.textBoxEscalationEmail = new System.Windows.Forms.TextBox();
            this.labelErrorUnprocessable = new System.Windows.Forms.Label();
            this.labelErrorUnknown = new System.Windows.Forms.Label();
            this.labelErrorUnsupported = new System.Windows.Forms.Label();
            this.comboBoxErrorUnprocessable = new System.Windows.Forms.ComboBox();
            this.comboBoxErrorUnknown = new System.Windows.Forms.ComboBox();
            this.comboBoxErrorUnsupported = new System.Windows.Forms.ComboBox();
            this.radioButtonForwardAttachment = new System.Windows.Forms.RadioButton();
            this.radioButtonForwardInline = new System.Windows.Forms.RadioButton();
            this.textBoxTimeBetweenRetries = new System.Windows.Forms.TextBox();
            this.numericUpDownMaxRetries = new System.Windows.Forms.NumericUpDown();
            this.labelMaxRetries = new System.Windows.Forms.Label();
            this.labelTimeBetweenRetries = new System.Windows.Forms.Label();
            this.tabPageTemplateDesigner = new System.Windows.Forms.TabPage();
            this.linkLabelClear = new System.Windows.Forms.LinkLabel();
            this.linkLabelTemplateEditHtml = new System.Windows.Forms.LinkLabel();
            this.linkLabelTemplateDefault = new System.Windows.Forms.LinkLabel();
            this.textBoxTemplateFrom = new System.Windows.Forms.TextBox();
            this.htmlEditorTemplateBody = new MSDN.Html.Editor.HtmlEditorControl();
            this.textBoxTemplateTo = new System.Windows.Forms.TextBox();
            this.labelTemplateSubject = new System.Windows.Forms.Label();
            this.textBoxTemplateCc = new System.Windows.Forms.TextBox();
            this.labelTemplateBcc = new System.Windows.Forms.Label();
            this.textBoxTemplateBcc = new System.Windows.Forms.TextBox();
            this.labelTemplateCc = new System.Windows.Forms.Label();
            this.textBoxTemplateSubject = new System.Windows.Forms.TextBox();
            this.labelTemplateTo = new System.Windows.Forms.Label();
            this.labelTemplateFrom = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxReloadOnSave = new System.Windows.Forms.CheckBox();
            this.ErrorBatchFieldNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorBatchFieldValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuMailboxes.SuspendLayout();
            this.contextMenuDataGrid.SuspendLayout();
            this.tabPageFileTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileTypes)).BeginInit();
            this.tabPageIndexFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndexFields)).BeginInit();
            this.tabPageBatchFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBatchFields)).BeginInit();
            this.tabPageConfig.SuspendLayout();
            this.groupBoxKofax.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveRetention)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStorageRetention)).BeginInit();
            this.groupBoxGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).BeginInit();
            this.groupBoxConversion.SuspendLayout();
            this.groupBoxCollection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImapRetention)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImapPort)).BeginInit();
            this.tabControlConfig.SuspendLayout();
            this.tabPageSchedule.SuspendLayout();
            this.groupBoxSchedule.SuspendLayout();
            this.tabPageScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxScript)).BeginInit();
            this.toolStripScript.SuspendLayout();
            this.tabPageErrorHandling.SuspendLayout();
            this.groupBoxErrorBatchFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrorBatchFields)).BeginInit();
            this.groupBoxConversionErrorBatch.SuspendLayout();
            this.groupBoxErrorGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxRetries)).BeginInit();
            this.tabPageTemplateDesigner.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewMailboxes
            // 
            this.listViewMailboxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewMailboxes.ContextMenuStrip = this.contextMenuMailboxes;
            this.listViewMailboxes.FullRowSelect = true;
            this.listViewMailboxes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewMailboxes.HideSelection = false;
            this.listViewMailboxes.Location = new System.Drawing.Point(11, 10);
            this.listViewMailboxes.Margin = new System.Windows.Forms.Padding(4);
            this.listViewMailboxes.Name = "listViewMailboxes";
            this.listViewMailboxes.Size = new System.Drawing.Size(291, 856);
            this.listViewMailboxes.TabIndex = 0;
            this.listViewMailboxes.UseCompatibleStateImageBehavior = false;
            this.listViewMailboxes.View = System.Windows.Forms.View.Details;
            this.listViewMailboxes.SelectedIndexChanged += new System.EventHandler(this.listViewMailboxes_SelectedIndexChanged);
            // 
            // contextMenuMailboxes
            // 
            this.contextMenuMailboxes.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuMailboxes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadAllToolStripMenuItem,
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.toolStripSeparator1,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.contextMenuMailboxes.Name = "contextMenuMailboxes";
            this.contextMenuMailboxes.Size = new System.Drawing.Size(154, 166);
            this.contextMenuMailboxes.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuMailboxes_Opening);
            // 
            // reloadAllToolStripMenuItem
            // 
            this.reloadAllToolStripMenuItem.Name = "reloadAllToolStripMenuItem";
            this.reloadAllToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.reloadAllToolStripMenuItem.Text = "Reload All";
            this.reloadAllToolStripMenuItem.Click += new System.EventHandler(this.reloadAllToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.importToolStripMenuItem.Text = "Import...";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(153, 26);
            this.exportToolStripMenuItem.Text = "Export...";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // contextMenuDataGrid
            // 
            this.contextMenuDataGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.batchFieldsToolStripMenuItem,
            this.indexFieldsToolStripMenuItem,
            this.copyFromToolStripMenuItem});
            this.contextMenuDataGrid.Name = "contextMenuPredefinedValues";
            this.contextMenuDataGrid.Size = new System.Drawing.Size(164, 82);
            // 
            // batchFieldsToolStripMenuItem
            // 
            this.batchFieldsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromToolStripMenuItem,
            this.fromDisplayNameToolStripMenuItem,
            this.fromAddressToolStripMenuItem,
            this.replyToToolStripMenuItem,
            this.replyToDisplayNameToolStripMenuItem,
            this.replyToAddressToolStripMenuItem,
            this.senderToolStripMenuItem,
            this.senderDisplayNameToolStripMenuItem,
            this.senderAddressToolStripMenuItem,
            this.toToolStripMenuItem,
            this.toDisplayNameToolStripMenuItem,
            this.toAddressToolStripMenuItem,
            this.ccToolStripMenuItem,
            this.ccDisplayNameToolStripMenuItem,
            this.ccAddressToolStripMenuItem,
            this.subjectToolStripMenuItem,
            this.bodyToolStripMenuItem,
            this.textBodyToolStripMenuItem,
            this.htmlBodyToolStripMenuItem,
            this.batchCreationDateTimeToolStripMenuItem,
            this.dateSentToolStripMenuItem,
            this.dateReceivedToolStripMenuItem,
            this.isSignedToolStripMenuItem,
            this.messageIDToolStripMenuItem,
            this.priorityToolStripMenuItem,
            this.sensitivityToolStripMenuItem,
            this.batchNumberToolStripMenuItem,
            this.emailIDToolStripMenuItem,
            this.attachmentFileNameToolStripMenuItem,
            this.headerHeaderNameToolStripMenuItem});
            this.batchFieldsToolStripMenuItem.Name = "batchFieldsToolStripMenuItem";
            this.batchFieldsToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.batchFieldsToolStripMenuItem.Text = "Batch Fields";
            // 
            // fromToolStripMenuItem
            // 
            this.fromToolStripMenuItem.Name = "fromToolStripMenuItem";
            this.fromToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.fromToolStripMenuItem.Text = "%From";
            this.fromToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // fromDisplayNameToolStripMenuItem
            // 
            this.fromDisplayNameToolStripMenuItem.Name = "fromDisplayNameToolStripMenuItem";
            this.fromDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.fromDisplayNameToolStripMenuItem.Text = "%From.DisplayName";
            this.fromDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // fromAddressToolStripMenuItem
            // 
            this.fromAddressToolStripMenuItem.Name = "fromAddressToolStripMenuItem";
            this.fromAddressToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.fromAddressToolStripMenuItem.Text = "%From.Address";
            this.fromAddressToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // replyToToolStripMenuItem
            // 
            this.replyToToolStripMenuItem.Name = "replyToToolStripMenuItem";
            this.replyToToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.replyToToolStripMenuItem.Text = "%ReplyTo";
            this.replyToToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // replyToDisplayNameToolStripMenuItem
            // 
            this.replyToDisplayNameToolStripMenuItem.Name = "replyToDisplayNameToolStripMenuItem";
            this.replyToDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.replyToDisplayNameToolStripMenuItem.Text = "%ReplyTo.DisplayName";
            this.replyToDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // replyToAddressToolStripMenuItem
            // 
            this.replyToAddressToolStripMenuItem.Name = "replyToAddressToolStripMenuItem";
            this.replyToAddressToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.replyToAddressToolStripMenuItem.Text = "%ReplyTo.Address";
            this.replyToAddressToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // senderToolStripMenuItem
            // 
            this.senderToolStripMenuItem.Name = "senderToolStripMenuItem";
            this.senderToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.senderToolStripMenuItem.Text = "%Sender";
            this.senderToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // senderDisplayNameToolStripMenuItem
            // 
            this.senderDisplayNameToolStripMenuItem.Name = "senderDisplayNameToolStripMenuItem";
            this.senderDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.senderDisplayNameToolStripMenuItem.Text = "%Sender.DisplayName";
            this.senderDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // senderAddressToolStripMenuItem
            // 
            this.senderAddressToolStripMenuItem.Name = "senderAddressToolStripMenuItem";
            this.senderAddressToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.senderAddressToolStripMenuItem.Text = "%Sender.Address";
            this.senderAddressToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // toToolStripMenuItem
            // 
            this.toToolStripMenuItem.Name = "toToolStripMenuItem";
            this.toToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.toToolStripMenuItem.Text = "%To";
            this.toToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // toDisplayNameToolStripMenuItem
            // 
            this.toDisplayNameToolStripMenuItem.Name = "toDisplayNameToolStripMenuItem";
            this.toDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.toDisplayNameToolStripMenuItem.Text = "%To.DisplayName";
            this.toDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // toAddressToolStripMenuItem
            // 
            this.toAddressToolStripMenuItem.Name = "toAddressToolStripMenuItem";
            this.toAddressToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.toAddressToolStripMenuItem.Text = "%To.Address";
            this.toAddressToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // ccToolStripMenuItem
            // 
            this.ccToolStripMenuItem.Name = "ccToolStripMenuItem";
            this.ccToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.ccToolStripMenuItem.Text = "%Cc";
            this.ccToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // ccDisplayNameToolStripMenuItem
            // 
            this.ccDisplayNameToolStripMenuItem.Name = "ccDisplayNameToolStripMenuItem";
            this.ccDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.ccDisplayNameToolStripMenuItem.Text = "%Cc.DisplayName";
            this.ccDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // ccAddressToolStripMenuItem
            // 
            this.ccAddressToolStripMenuItem.Name = "ccAddressToolStripMenuItem";
            this.ccAddressToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.ccAddressToolStripMenuItem.Text = "%Cc.Address";
            this.ccAddressToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // subjectToolStripMenuItem
            // 
            this.subjectToolStripMenuItem.Name = "subjectToolStripMenuItem";
            this.subjectToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.subjectToolStripMenuItem.Text = "%Subject";
            this.subjectToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // bodyToolStripMenuItem
            // 
            this.bodyToolStripMenuItem.Name = "bodyToolStripMenuItem";
            this.bodyToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.bodyToolStripMenuItem.Text = "%Body";
            this.bodyToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // textBodyToolStripMenuItem
            // 
            this.textBodyToolStripMenuItem.Name = "textBodyToolStripMenuItem";
            this.textBodyToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.textBodyToolStripMenuItem.Text = "%TextBody";
            this.textBodyToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // htmlBodyToolStripMenuItem
            // 
            this.htmlBodyToolStripMenuItem.Name = "htmlBodyToolStripMenuItem";
            this.htmlBodyToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.htmlBodyToolStripMenuItem.Text = "%HtmlBody";
            this.htmlBodyToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // batchCreationDateTimeToolStripMenuItem
            // 
            this.batchCreationDateTimeToolStripMenuItem.Name = "batchCreationDateTimeToolStripMenuItem";
            this.batchCreationDateTimeToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.batchCreationDateTimeToolStripMenuItem.Text = "%BatchCreationDateTime";
            this.batchCreationDateTimeToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // dateSentToolStripMenuItem
            // 
            this.dateSentToolStripMenuItem.Name = "dateSentToolStripMenuItem";
            this.dateSentToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.dateSentToolStripMenuItem.Text = "%DateSent";
            this.dateSentToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // dateReceivedToolStripMenuItem
            // 
            this.dateReceivedToolStripMenuItem.Name = "dateReceivedToolStripMenuItem";
            this.dateReceivedToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.dateReceivedToolStripMenuItem.Text = "%DateReceived";
            this.dateReceivedToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // isSignedToolStripMenuItem
            // 
            this.isSignedToolStripMenuItem.Name = "isSignedToolStripMenuItem";
            this.isSignedToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.isSignedToolStripMenuItem.Text = "%IsSigned";
            this.isSignedToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // messageIDToolStripMenuItem
            // 
            this.messageIDToolStripMenuItem.Name = "messageIDToolStripMenuItem";
            this.messageIDToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.messageIDToolStripMenuItem.Text = "%MessageID";
            this.messageIDToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // priorityToolStripMenuItem
            // 
            this.priorityToolStripMenuItem.Name = "priorityToolStripMenuItem";
            this.priorityToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.priorityToolStripMenuItem.Text = "%Priority";
            this.priorityToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // sensitivityToolStripMenuItem
            // 
            this.sensitivityToolStripMenuItem.Name = "sensitivityToolStripMenuItem";
            this.sensitivityToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.sensitivityToolStripMenuItem.Text = "%Sensitivity";
            this.sensitivityToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // batchNumberToolStripMenuItem
            // 
            this.batchNumberToolStripMenuItem.Name = "batchNumberToolStripMenuItem";
            this.batchNumberToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.batchNumberToolStripMenuItem.Text = "%BatchNumber";
            this.batchNumberToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // emailIDToolStripMenuItem
            // 
            this.emailIDToolStripMenuItem.Name = "emailIDToolStripMenuItem";
            this.emailIDToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.emailIDToolStripMenuItem.Text = "%EmailID";
            this.emailIDToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // attachmentFileNameToolStripMenuItem
            // 
            this.attachmentFileNameToolStripMenuItem.Name = "attachmentFileNameToolStripMenuItem";
            this.attachmentFileNameToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.attachmentFileNameToolStripMenuItem.Text = "%AttachmentFileName";
            this.attachmentFileNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // headerHeaderNameToolStripMenuItem
            // 
            this.headerHeaderNameToolStripMenuItem.Name = "headerHeaderNameToolStripMenuItem";
            this.headerHeaderNameToolStripMenuItem.Size = new System.Drawing.Size(254, 26);
            this.headerHeaderNameToolStripMenuItem.Text = "%Header.[HeaderName]";
            this.headerHeaderNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // indexFieldsToolStripMenuItem
            // 
            this.indexFieldsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentSourceToolStripMenuItem,
            this.documentIsBodyToolStripMenuItem,
            this.documentExtensionToolStripMenuItem,
            this.documentAttachmentFileNameToolStripMenuItem,
            this.documentFailureReasonToolStripMenuItem});
            this.indexFieldsToolStripMenuItem.Name = "indexFieldsToolStripMenuItem";
            this.indexFieldsToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.indexFieldsToolStripMenuItem.Text = "Index Fields";
            // 
            // documentSourceToolStripMenuItem
            // 
            this.documentSourceToolStripMenuItem.Name = "documentSourceToolStripMenuItem";
            this.documentSourceToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.documentSourceToolStripMenuItem.Text = "%Document.Source";
            this.documentSourceToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // documentIsBodyToolStripMenuItem
            // 
            this.documentIsBodyToolStripMenuItem.Name = "documentIsBodyToolStripMenuItem";
            this.documentIsBodyToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.documentIsBodyToolStripMenuItem.Text = "%Document.IsBody";
            this.documentIsBodyToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // documentExtensionToolStripMenuItem
            // 
            this.documentExtensionToolStripMenuItem.Name = "documentExtensionToolStripMenuItem";
            this.documentExtensionToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.documentExtensionToolStripMenuItem.Text = "%Document.Extension";
            this.documentExtensionToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // documentAttachmentFileNameToolStripMenuItem
            // 
            this.documentAttachmentFileNameToolStripMenuItem.Name = "documentAttachmentFileNameToolStripMenuItem";
            this.documentAttachmentFileNameToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.documentAttachmentFileNameToolStripMenuItem.Text = "%Document.AttachmentFileName";
            this.documentAttachmentFileNameToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // documentFailureReasonToolStripMenuItem
            // 
            this.documentFailureReasonToolStripMenuItem.Name = "documentFailureReasonToolStripMenuItem";
            this.documentFailureReasonToolStripMenuItem.Size = new System.Drawing.Size(308, 26);
            this.documentFailureReasonToolStripMenuItem.Text = "%Document.FailureReason";
            this.documentFailureReasonToolStripMenuItem.Click += new System.EventHandler(this.predefinedValueToolStripMenuItem_Click);
            // 
            // copyFromToolStripMenuItem
            // 
            this.copyFromToolStripMenuItem.Name = "copyFromToolStripMenuItem";
            this.copyFromToolStripMenuItem.Size = new System.Drawing.Size(163, 26);
            this.copyFromToolStripMenuItem.Text = "Copy From";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(1187, 874);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(127, 28);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // tabPageFileTypes
            // 
            this.tabPageFileTypes.Controls.Add(this.dataGridViewFileTypes);
            this.tabPageFileTypes.Location = new System.Drawing.Point(4, 25);
            this.tabPageFileTypes.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageFileTypes.Name = "tabPageFileTypes";
            this.tabPageFileTypes.Size = new System.Drawing.Size(995, 829);
            this.tabPageFileTypes.TabIndex = 4;
            this.tabPageFileTypes.Text = "File Types";
            this.tabPageFileTypes.UseVisualStyleBackColor = true;
            // 
            // dataGridViewFileTypes
            // 
            this.dataGridViewFileTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFileTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFileTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FilenameRegexColumn,
            this.ProcessAsFileTypeColumn,
            this.IgnoreFileTypeColumn,
            this.PassthroughFileTypeColumn,
            this.AutoDeskewFileTypeColumn,
            this.AutoRotateFileTypeColumn,
            this.BitDepthFileTypeColumn,
            this.BinarisationAlgorithmFileTypeColumn,
            this.MinPixelsFileTypeColumn});
            this.dataGridViewFileTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewFileTypes.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewFileTypes.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewFileTypes.Name = "dataGridViewFileTypes";
            this.dataGridViewFileTypes.Size = new System.Drawing.Size(995, 829);
            this.dataGridViewFileTypes.TabIndex = 5;
            this.dataGridViewFileTypes.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFileTypes_CellValueChanged);
            this.dataGridViewFileTypes.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridViewFileTypes_EditingControlShowing);
            this.dataGridViewFileTypes.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewFileTypes_RowsRemoved);
            this.dataGridViewFileTypes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewFileTypes_MouseClick);
            // 
            // FilenameRegexColumn
            // 
            this.FilenameRegexColumn.DataPropertyName = "Pattern";
            this.FilenameRegexColumn.HeaderText = "File Name Regex";
            this.FilenameRegexColumn.Name = "FilenameRegexColumn";
            // 
            // ProcessAsFileTypeColumn
            // 
            this.ProcessAsFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ProcessAsFileTypeColumn.DataPropertyName = "ProcessAs";
            this.ProcessAsFileTypeColumn.HeaderText = "Process As";
            this.ProcessAsFileTypeColumn.MinimumWidth = 85;
            this.ProcessAsFileTypeColumn.Name = "ProcessAsFileTypeColumn";
            this.ProcessAsFileTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ProcessAsFileTypeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // IgnoreFileTypeColumn
            // 
            this.IgnoreFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.IgnoreFileTypeColumn.DataPropertyName = "Ignore";
            this.IgnoreFileTypeColumn.HeaderText = "Ignore";
            this.IgnoreFileTypeColumn.MinimumWidth = 49;
            this.IgnoreFileTypeColumn.Name = "IgnoreFileTypeColumn";
            this.IgnoreFileTypeColumn.ThreeState = true;
            this.IgnoreFileTypeColumn.Width = 54;
            // 
            // PassthroughFileTypeColumn
            // 
            this.PassthroughFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.PassthroughFileTypeColumn.DataPropertyName = "Passthrough";
            this.PassthroughFileTypeColumn.HeaderText = "Passthrough";
            this.PassthroughFileTypeColumn.MinimumWidth = 78;
            this.PassthroughFileTypeColumn.Name = "PassthroughFileTypeColumn";
            this.PassthroughFileTypeColumn.ThreeState = true;
            this.PassthroughFileTypeColumn.Width = 94;
            // 
            // AutoDeskewFileTypeColumn
            // 
            this.AutoDeskewFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.AutoDeskewFileTypeColumn.DataPropertyName = "AutoDeskew";
            this.AutoDeskewFileTypeColumn.HeaderText = "Auto Deskew";
            this.AutoDeskewFileTypeColumn.MinimumWidth = 83;
            this.AutoDeskewFileTypeColumn.Name = "AutoDeskewFileTypeColumn";
            this.AutoDeskewFileTypeColumn.ThreeState = true;
            this.AutoDeskewFileTypeColumn.Width = 86;
            // 
            // AutoRotateFileTypeColumn
            // 
            this.AutoRotateFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.AutoRotateFileTypeColumn.DataPropertyName = "AutoRotate";
            this.AutoRotateFileTypeColumn.HeaderText = "Auto Rotate";
            this.AutoRotateFileTypeColumn.MinimumWidth = 76;
            this.AutoRotateFileTypeColumn.Name = "AutoRotateFileTypeColumn";
            this.AutoRotateFileTypeColumn.ThreeState = true;
            this.AutoRotateFileTypeColumn.Width = 80;
            // 
            // BitDepthFileTypeColumn
            // 
            this.BitDepthFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BitDepthFileTypeColumn.DataPropertyName = "BitDepth";
            this.BitDepthFileTypeColumn.HeaderText = "Bit Depth";
            this.BitDepthFileTypeColumn.Name = "BitDepthFileTypeColumn";
            this.BitDepthFileTypeColumn.Width = 80;
            // 
            // BinarisationAlgorithmFileTypeColumn
            // 
            this.BinarisationAlgorithmFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BinarisationAlgorithmFileTypeColumn.DataPropertyName = "BinarisationAlgorithm";
            this.BinarisationAlgorithmFileTypeColumn.HeaderText = "Binarisation Algorithm";
            this.BinarisationAlgorithmFileTypeColumn.Name = "BinarisationAlgorithmFileTypeColumn";
            this.BinarisationAlgorithmFileTypeColumn.Width = 140;
            // 
            // MinPixelsFileTypeColumn
            // 
            this.MinPixelsFileTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MinPixelsFileTypeColumn.DataPropertyName = "MinPixels";
            this.MinPixelsFileTypeColumn.HeaderText = "Minimum Pixels";
            this.MinPixelsFileTypeColumn.Name = "MinPixelsFileTypeColumn";
            this.MinPixelsFileTypeColumn.Width = 120;
            // 
            // tabPageIndexFields
            // 
            this.tabPageIndexFields.Controls.Add(this.dataGridViewIndexFields);
            this.tabPageIndexFields.Location = new System.Drawing.Point(4, 25);
            this.tabPageIndexFields.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageIndexFields.Name = "tabPageIndexFields";
            this.tabPageIndexFields.Size = new System.Drawing.Size(995, 829);
            this.tabPageIndexFields.TabIndex = 2;
            this.tabPageIndexFields.Text = "Index Fields";
            this.tabPageIndexFields.UseVisualStyleBackColor = true;
            // 
            // dataGridViewIndexFields
            // 
            this.dataGridViewIndexFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewIndexFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIndexFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexFieldNameColumn,
            this.IndexFieldValueColumn,
            this.IndexFieldRegexColumn});
            this.dataGridViewIndexFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewIndexFields.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewIndexFields.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewIndexFields.Name = "dataGridViewIndexFields";
            this.dataGridViewIndexFields.Size = new System.Drawing.Size(995, 829);
            this.dataGridViewIndexFields.TabIndex = 4;
            this.dataGridViewIndexFields.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewIndexFields_CellValueChanged);
            this.dataGridViewIndexFields.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewIndexFields_RowsRemoved);
            this.dataGridViewIndexFields.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewIndexFields_MouseClick);
            // 
            // IndexFieldNameColumn
            // 
            this.IndexFieldNameColumn.DataPropertyName = "Name";
            this.IndexFieldNameColumn.FillWeight = 60F;
            this.IndexFieldNameColumn.HeaderText = "Index Field Name";
            this.IndexFieldNameColumn.Name = "IndexFieldNameColumn";
            // 
            // IndexFieldValueColumn
            // 
            this.IndexFieldValueColumn.DataPropertyName = "Value";
            this.IndexFieldValueColumn.FillWeight = 60F;
            this.IndexFieldValueColumn.HeaderText = "Index Field Value";
            this.IndexFieldValueColumn.Name = "IndexFieldValueColumn";
            // 
            // IndexFieldRegexColumn
            // 
            this.IndexFieldRegexColumn.DataPropertyName = "Regex";
            this.IndexFieldRegexColumn.HeaderText = "Regular Expression";
            this.IndexFieldRegexColumn.Name = "IndexFieldRegexColumn";
            this.IndexFieldRegexColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IndexFieldRegexColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPageBatchFields
            // 
            this.tabPageBatchFields.Controls.Add(this.dataGridViewBatchFields);
            this.tabPageBatchFields.Location = new System.Drawing.Point(4, 25);
            this.tabPageBatchFields.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageBatchFields.Name = "tabPageBatchFields";
            this.tabPageBatchFields.Size = new System.Drawing.Size(995, 829);
            this.tabPageBatchFields.TabIndex = 1;
            this.tabPageBatchFields.Text = "Batch Fields";
            this.tabPageBatchFields.UseVisualStyleBackColor = true;
            // 
            // dataGridViewBatchFields
            // 
            this.dataGridViewBatchFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBatchFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBatchFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BatchFieldNameColumn,
            this.BatchFieldValueColumn,
            this.BatchFieldRegexColumn});
            this.dataGridViewBatchFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBatchFields.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewBatchFields.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewBatchFields.Name = "dataGridViewBatchFields";
            this.dataGridViewBatchFields.Size = new System.Drawing.Size(995, 829);
            this.dataGridViewBatchFields.TabIndex = 3;
            this.dataGridViewBatchFields.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBatchFields_CellValueChanged);
            this.dataGridViewBatchFields.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewBatchFields_RowsRemoved);
            this.dataGridViewBatchFields.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewBatchFields_MouseClick);
            // 
            // BatchFieldNameColumn
            // 
            this.BatchFieldNameColumn.DataPropertyName = "Name";
            this.BatchFieldNameColumn.FillWeight = 60F;
            this.BatchFieldNameColumn.HeaderText = "Batch Field Name";
            this.BatchFieldNameColumn.Name = "BatchFieldNameColumn";
            // 
            // BatchFieldValueColumn
            // 
            this.BatchFieldValueColumn.DataPropertyName = "Value";
            this.BatchFieldValueColumn.FillWeight = 60F;
            this.BatchFieldValueColumn.HeaderText = "Batch Field Value";
            this.BatchFieldValueColumn.Name = "BatchFieldValueColumn";
            // 
            // BatchFieldRegexColumn
            // 
            this.BatchFieldRegexColumn.DataPropertyName = "Regex";
            this.BatchFieldRegexColumn.HeaderText = "Regular Expression";
            this.BatchFieldRegexColumn.Name = "BatchFieldRegexColumn";
            this.BatchFieldRegexColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BatchFieldRegexColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.groupBoxKofax);
            this.tabPageConfig.Controls.Add(this.groupBoxOutput);
            this.tabPageConfig.Controls.Add(this.groupBoxGeneral);
            this.tabPageConfig.Controls.Add(this.groupBoxConversion);
            this.tabPageConfig.Controls.Add(this.groupBoxCollection);
            this.tabPageConfig.Controls.Add(this.checkBoxEnabled);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 25);
            this.tabPageConfig.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageConfig.Size = new System.Drawing.Size(995, 829);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Configuration";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // groupBoxKofax
            // 
            this.groupBoxKofax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxKofax.Controls.Add(this.labelDocumentClass);
            this.groupBoxKofax.Controls.Add(this.textBoxDocumentClass);
            this.groupBoxKofax.Controls.Add(this.textBoxFormType);
            this.groupBoxKofax.Controls.Add(this.checkBoxAutoSeparation);
            this.groupBoxKofax.Controls.Add(this.labelFormType);
            this.groupBoxKofax.Controls.Add(this.checkBoxSingleDoc);
            this.groupBoxKofax.Controls.Add(this.textBoxFolderClass);
            this.groupBoxKofax.Controls.Add(this.labelBatchClass);
            this.groupBoxKofax.Controls.Add(this.labelFolderClass);
            this.groupBoxKofax.Controls.Add(this.textBoxBatchClass);
            this.groupBoxKofax.Location = new System.Drawing.Point(16, 463);
            this.groupBoxKofax.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxKofax.Name = "groupBoxKofax";
            this.groupBoxKofax.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxKofax.Size = new System.Drawing.Size(959, 160);
            this.groupBoxKofax.TabIndex = 36;
            this.groupBoxKofax.TabStop = false;
            this.groupBoxKofax.Text = "Kofax";
            // 
            // labelDocumentClass
            // 
            this.labelDocumentClass.AutoSize = true;
            this.labelDocumentClass.Location = new System.Drawing.Point(309, 91);
            this.labelDocumentClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDocumentClass.Name = "labelDocumentClass";
            this.labelDocumentClass.Size = new System.Drawing.Size(155, 17);
            this.labelDocumentClass.TabIndex = 28;
            this.labelDocumentClass.Text = "Document Class Name:";
            // 
            // textBoxDocumentClass
            // 
            this.textBoxDocumentClass.Location = new System.Drawing.Point(475, 87);
            this.textBoxDocumentClass.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDocumentClass.Name = "textBoxDocumentClass";
            this.textBoxDocumentClass.Size = new System.Drawing.Size(351, 22);
            this.textBoxDocumentClass.TabIndex = 27;
            this.textBoxDocumentClass.Leave += new System.EventHandler(this.textBoxDocumentClass_Leave);
            // 
            // textBoxFormType
            // 
            this.textBoxFormType.Location = new System.Drawing.Point(475, 119);
            this.textBoxFormType.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFormType.Name = "textBoxFormType";
            this.textBoxFormType.Size = new System.Drawing.Size(351, 22);
            this.textBoxFormType.TabIndex = 18;
            this.textBoxFormType.Leave += new System.EventHandler(this.textBoxFormType_Leave);
            // 
            // checkBoxAutoSeparation
            // 
            this.checkBoxAutoSeparation.AutoSize = true;
            this.checkBoxAutoSeparation.Location = new System.Drawing.Point(12, 26);
            this.checkBoxAutoSeparation.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAutoSeparation.Name = "checkBoxAutoSeparation";
            this.checkBoxAutoSeparation.Size = new System.Drawing.Size(246, 21);
            this.checkBoxAutoSeparation.TabIndex = 25;
            this.checkBoxAutoSeparation.Text = "Automatic Separation and Form ID";
            this.checkBoxAutoSeparation.UseVisualStyleBackColor = true;
            this.checkBoxAutoSeparation.CheckedChanged += new System.EventHandler(this.checkBoxAutoSeparation_CheckedChanged);
            // 
            // labelFormType
            // 
            this.labelFormType.AutoSize = true;
            this.labelFormType.Location = new System.Drawing.Point(309, 123);
            this.labelFormType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFormType.Name = "labelFormType";
            this.labelFormType.Size = new System.Drawing.Size(121, 17);
            this.labelFormType.TabIndex = 17;
            this.labelFormType.Text = "Form Type Name:";
            // 
            // checkBoxSingleDoc
            // 
            this.checkBoxSingleDoc.AutoSize = true;
            this.checkBoxSingleDoc.Location = new System.Drawing.Point(12, 58);
            this.checkBoxSingleDoc.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxSingleDoc.Name = "checkBoxSingleDoc";
            this.checkBoxSingleDoc.Size = new System.Drawing.Size(211, 21);
            this.checkBoxSingleDoc.TabIndex = 26;
            this.checkBoxSingleDoc.Text = "Single Document Processing";
            this.checkBoxSingleDoc.UseVisualStyleBackColor = true;
            this.checkBoxSingleDoc.CheckedChanged += new System.EventHandler(this.checkBoxSingleDoc_CheckedChanged);
            // 
            // textBoxFolderClass
            // 
            this.textBoxFolderClass.Location = new System.Drawing.Point(475, 55);
            this.textBoxFolderClass.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFolderClass.Name = "textBoxFolderClass";
            this.textBoxFolderClass.Size = new System.Drawing.Size(351, 22);
            this.textBoxFolderClass.TabIndex = 16;
            this.textBoxFolderClass.Leave += new System.EventHandler(this.textBoxFolderClass_Leave);
            // 
            // labelBatchClass
            // 
            this.labelBatchClass.AutoSize = true;
            this.labelBatchClass.Location = new System.Drawing.Point(309, 27);
            this.labelBatchClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBatchClass.Name = "labelBatchClass";
            this.labelBatchClass.Size = new System.Drawing.Size(127, 17);
            this.labelBatchClass.TabIndex = 13;
            this.labelBatchClass.Text = "Batch Class Name:";
            // 
            // labelFolderClass
            // 
            this.labelFolderClass.AutoSize = true;
            this.labelFolderClass.Location = new System.Drawing.Point(309, 59);
            this.labelFolderClass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFolderClass.Name = "labelFolderClass";
            this.labelFolderClass.Size = new System.Drawing.Size(131, 17);
            this.labelFolderClass.TabIndex = 15;
            this.labelFolderClass.Text = "Folder Class Name:";
            // 
            // textBoxBatchClass
            // 
            this.textBoxBatchClass.Location = new System.Drawing.Point(475, 23);
            this.textBoxBatchClass.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBatchClass.Name = "textBoxBatchClass";
            this.textBoxBatchClass.Size = new System.Drawing.Size(351, 22);
            this.textBoxBatchClass.TabIndex = 14;
            this.textBoxBatchClass.Leave += new System.EventHandler(this.textBoxBatchClass_Leave);
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOutput.Controls.Add(this.numericUpDownArchiveRetention);
            this.groupBoxOutput.Controls.Add(this.labelArchiveRetention);
            this.groupBoxOutput.Controls.Add(this.textBoxOutputFolderFormat);
            this.groupBoxOutput.Controls.Add(this.labelOutputFolderFormat);
            this.groupBoxOutput.Controls.Add(this.checkBoxBatchClassSubFolder);
            this.groupBoxOutput.Controls.Add(this.labelStorageRetention);
            this.groupBoxOutput.Controls.Add(this.numericUpDownStorageRetention);
            this.groupBoxOutput.Controls.Add(this.labelBatchNumberFormat);
            this.groupBoxOutput.Controls.Add(this.textBoxBatchNumberFormat);
            this.groupBoxOutput.Controls.Add(this.checkBoxZip);
            this.groupBoxOutput.Controls.Add(this.textBoxOutputPath);
            this.groupBoxOutput.Controls.Add(this.buttonStoragePath);
            this.groupBoxOutput.Controls.Add(this.labelOutputPath);
            this.groupBoxOutput.Controls.Add(this.labelArchivePath);
            this.groupBoxOutput.Controls.Add(this.textBoxStoragePath);
            this.groupBoxOutput.Controls.Add(this.buttonOutputPath);
            this.groupBoxOutput.Controls.Add(this.textBoxArchivePath);
            this.groupBoxOutput.Controls.Add(this.labelStoragePath);
            this.groupBoxOutput.Controls.Add(this.buttonArchivePath);
            this.groupBoxOutput.Location = new System.Drawing.Point(16, 294);
            this.groupBoxOutput.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxOutput.Size = new System.Drawing.Size(959, 161);
            this.groupBoxOutput.TabIndex = 35;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // numericUpDownArchiveRetention
            // 
            this.numericUpDownArchiveRetention.Location = new System.Drawing.Point(832, 92);
            this.numericUpDownArchiveRetention.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownArchiveRetention.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDownArchiveRetention.Name = "numericUpDownArchiveRetention";
            this.numericUpDownArchiveRetention.Size = new System.Drawing.Size(73, 22);
            this.numericUpDownArchiveRetention.TabIndex = 30;
            this.numericUpDownArchiveRetention.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDownArchiveRetention.ValueChanged += new System.EventHandler(this.numericUpDownArchiveRetention_ValueChanged);
            // 
            // labelArchiveRetention
            // 
            this.labelArchiveRetention.AutoSize = true;
            this.labelArchiveRetention.Location = new System.Drawing.Point(749, 95);
            this.labelArchiveRetention.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelArchiveRetention.Name = "labelArchiveRetention";
            this.labelArchiveRetention.Size = new System.Drawing.Size(73, 17);
            this.labelArchiveRetention.TabIndex = 29;
            this.labelArchiveRetention.Text = "Retention:";
            // 
            // textBoxOutputFolderFormat
            // 
            this.textBoxOutputFolderFormat.Location = new System.Drawing.Point(792, 23);
            this.textBoxOutputFolderFormat.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxOutputFolderFormat.Name = "textBoxOutputFolderFormat";
            this.textBoxOutputFolderFormat.Size = new System.Drawing.Size(143, 22);
            this.textBoxOutputFolderFormat.TabIndex = 28;
            this.textBoxOutputFolderFormat.Leave += new System.EventHandler(this.textBoxOutputFolderFormat_Leave);
            // 
            // labelOutputFolderFormat
            // 
            this.labelOutputFolderFormat.AutoSize = true;
            this.labelOutputFolderFormat.Location = new System.Drawing.Point(599, 27);
            this.labelOutputFolderFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOutputFolderFormat.Name = "labelOutputFolderFormat";
            this.labelOutputFolderFormat.Size = new System.Drawing.Size(188, 17);
            this.labelOutputFolderFormat.TabIndex = 27;
            this.labelOutputFolderFormat.Text = "Output Folder Format String:";
            // 
            // checkBoxBatchClassSubFolder
            // 
            this.checkBoxBatchClassSubFolder.AutoSize = true;
            this.checkBoxBatchClassSubFolder.Location = new System.Drawing.Point(768, 63);
            this.checkBoxBatchClassSubFolder.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxBatchClassSubFolder.Name = "checkBoxBatchClassSubFolder";
            this.checkBoxBatchClassSubFolder.Size = new System.Drawing.Size(169, 21);
            this.checkBoxBatchClassSubFolder.TabIndex = 26;
            this.checkBoxBatchClassSubFolder.Text = "Batch Class Subfolder";
            this.checkBoxBatchClassSubFolder.UseVisualStyleBackColor = true;
            this.checkBoxBatchClassSubFolder.CheckedChanged += new System.EventHandler(this.checkBoxBatchClassSubFolder_CheckedChanged);
            // 
            // labelStorageRetention
            // 
            this.labelStorageRetention.AutoSize = true;
            this.labelStorageRetention.Location = new System.Drawing.Point(749, 128);
            this.labelStorageRetention.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStorageRetention.Name = "labelStorageRetention";
            this.labelStorageRetention.Size = new System.Drawing.Size(73, 17);
            this.labelStorageRetention.TabIndex = 25;
            this.labelStorageRetention.Text = "Retention:";
            // 
            // numericUpDownStorageRetention
            // 
            this.numericUpDownStorageRetention.Location = new System.Drawing.Point(832, 126);
            this.numericUpDownStorageRetention.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownStorageRetention.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDownStorageRetention.Name = "numericUpDownStorageRetention";
            this.numericUpDownStorageRetention.Size = new System.Drawing.Size(73, 22);
            this.numericUpDownStorageRetention.TabIndex = 24;
            this.numericUpDownStorageRetention.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numericUpDownStorageRetention.ValueChanged += new System.EventHandler(this.numericUpDownStorageRetention_ValueChanged);
            // 
            // labelBatchNumberFormat
            // 
            this.labelBatchNumberFormat.AutoSize = true;
            this.labelBatchNumberFormat.Location = new System.Drawing.Point(215, 27);
            this.labelBatchNumberFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBatchNumberFormat.Name = "labelBatchNumberFormat";
            this.labelBatchNumberFormat.Size = new System.Drawing.Size(191, 17);
            this.labelBatchNumberFormat.TabIndex = 23;
            this.labelBatchNumberFormat.Text = "Batch Number Format String:";
            // 
            // textBoxBatchNumberFormat
            // 
            this.textBoxBatchNumberFormat.Location = new System.Drawing.Point(413, 23);
            this.textBoxBatchNumberFormat.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxBatchNumberFormat.Name = "textBoxBatchNumberFormat";
            this.textBoxBatchNumberFormat.Size = new System.Drawing.Size(143, 22);
            this.textBoxBatchNumberFormat.TabIndex = 19;
            this.textBoxBatchNumberFormat.Leave += new System.EventHandler(this.textBoxBatchNumberFormat_Leave);
            // 
            // checkBoxZip
            // 
            this.checkBoxZip.AutoSize = true;
            this.checkBoxZip.Location = new System.Drawing.Point(12, 26);
            this.checkBoxZip.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxZip.Name = "checkBoxZip";
            this.checkBoxZip.Size = new System.Drawing.Size(129, 21);
            this.checkBoxZip.TabIndex = 22;
            this.checkBoxZip.Text = "Create Zip Files";
            this.checkBoxZip.UseVisualStyleBackColor = true;
            this.checkBoxZip.CheckedChanged += new System.EventHandler(this.checkBoxZip_CheckedChanged);
            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Location = new System.Drawing.Point(112, 60);
            this.textBoxOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxOutputPath.MaxLength = 150;
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.Size = new System.Drawing.Size(579, 22);
            this.textBoxOutputPath.TabIndex = 9;
            this.textBoxOutputPath.Leave += new System.EventHandler(this.textBoxOutputPath_Leave);
            // 
            // buttonStoragePath
            // 
            this.buttonStoragePath.Location = new System.Drawing.Point(697, 124);
            this.buttonStoragePath.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStoragePath.Name = "buttonStoragePath";
            this.buttonStoragePath.Size = new System.Drawing.Size(32, 25);
            this.buttonStoragePath.TabIndex = 18;
            this.buttonStoragePath.Text = "...";
            this.buttonStoragePath.UseVisualStyleBackColor = true;
            this.buttonStoragePath.Click += new System.EventHandler(this.buttonStoragePath_Click);
            // 
            // labelOutputPath
            // 
            this.labelOutputPath.AutoSize = true;
            this.labelOutputPath.Location = new System.Drawing.Point(8, 64);
            this.labelOutputPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOutputPath.Name = "labelOutputPath";
            this.labelOutputPath.Size = new System.Drawing.Size(88, 17);
            this.labelOutputPath.TabIndex = 7;
            this.labelOutputPath.Text = "Output Path:";
            // 
            // labelArchivePath
            // 
            this.labelArchivePath.AutoSize = true;
            this.labelArchivePath.Location = new System.Drawing.Point(8, 97);
            this.labelArchivePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelArchivePath.Name = "labelArchivePath";
            this.labelArchivePath.Size = new System.Drawing.Size(92, 17);
            this.labelArchivePath.TabIndex = 8;
            this.labelArchivePath.Text = "Archive Path:";
            // 
            // textBoxStoragePath
            // 
            this.textBoxStoragePath.Location = new System.Drawing.Point(112, 124);
            this.textBoxStoragePath.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxStoragePath.MaxLength = 150;
            this.textBoxStoragePath.Name = "textBoxStoragePath";
            this.textBoxStoragePath.Size = new System.Drawing.Size(579, 22);
            this.textBoxStoragePath.TabIndex = 16;
            this.textBoxStoragePath.Leave += new System.EventHandler(this.textBoxStoragePath_Leave);
            // 
            // buttonOutputPath
            // 
            this.buttonOutputPath.Location = new System.Drawing.Point(697, 60);
            this.buttonOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOutputPath.Name = "buttonOutputPath";
            this.buttonOutputPath.Size = new System.Drawing.Size(32, 25);
            this.buttonOutputPath.TabIndex = 19;
            this.buttonOutputPath.Text = "...";
            this.buttonOutputPath.UseVisualStyleBackColor = true;
            this.buttonOutputPath.Click += new System.EventHandler(this.buttonOutputPath_Click);
            // 
            // textBoxArchivePath
            // 
            this.textBoxArchivePath.Location = new System.Drawing.Point(112, 92);
            this.textBoxArchivePath.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxArchivePath.MaxLength = 150;
            this.textBoxArchivePath.Name = "textBoxArchivePath";
            this.textBoxArchivePath.Size = new System.Drawing.Size(579, 22);
            this.textBoxArchivePath.TabIndex = 20;
            this.textBoxArchivePath.Leave += new System.EventHandler(this.textBoxArchivePath_Leave);
            // 
            // labelStoragePath
            // 
            this.labelStoragePath.AutoSize = true;
            this.labelStoragePath.Location = new System.Drawing.Point(8, 129);
            this.labelStoragePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStoragePath.Name = "labelStoragePath";
            this.labelStoragePath.Size = new System.Drawing.Size(95, 17);
            this.labelStoragePath.TabIndex = 12;
            this.labelStoragePath.Text = "Storage Path:";
            // 
            // buttonArchivePath
            // 
            this.buttonArchivePath.Location = new System.Drawing.Point(697, 92);
            this.buttonArchivePath.Margin = new System.Windows.Forms.Padding(4);
            this.buttonArchivePath.Name = "buttonArchivePath";
            this.buttonArchivePath.Size = new System.Drawing.Size(32, 25);
            this.buttonArchivePath.TabIndex = 21;
            this.buttonArchivePath.Text = "...";
            this.buttonArchivePath.UseVisualStyleBackColor = true;
            this.buttonArchivePath.Click += new System.EventHandler(this.buttonArchivePath_Click);
            // 
            // groupBoxGeneral
            // 
            this.groupBoxGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGeneral.Controls.Add(this.checkBoxLiteViewerEnabled);
            this.groupBoxGeneral.Controls.Add(this.textBoxDescription);
            this.groupBoxGeneral.Controls.Add(this.comboBoxGroup);
            this.groupBoxGeneral.Controls.Add(this.labelGroup);
            this.groupBoxGeneral.Controls.Add(this.labelDescription);
            this.groupBoxGeneral.Controls.Add(this.labelPriority);
            this.groupBoxGeneral.Controls.Add(this.numericUpDownPriority);
            this.groupBoxGeneral.Location = new System.Drawing.Point(16, 49);
            this.groupBoxGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxGeneral.Name = "groupBoxGeneral";
            this.groupBoxGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxGeneral.Size = new System.Drawing.Size(959, 102);
            this.groupBoxGeneral.TabIndex = 34;
            this.groupBoxGeneral.TabStop = false;
            this.groupBoxGeneral.Text = "General";
            // 
            // checkBoxLiteViewerEnabled
            // 
            this.checkBoxLiteViewerEnabled.AutoSize = true;
            this.checkBoxLiteViewerEnabled.Location = new System.Drawing.Point(12, 65);
            this.checkBoxLiteViewerEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxLiteViewerEnabled.Name = "checkBoxLiteViewerEnabled";
            this.checkBoxLiteViewerEnabled.Size = new System.Drawing.Size(155, 21);
            this.checkBoxLiteViewerEnabled.TabIndex = 25;
            this.checkBoxLiteViewerEnabled.Text = "Lite Viewer Enabled";
            this.checkBoxLiteViewerEnabled.UseVisualStyleBackColor = true;
            this.checkBoxLiteViewerEnabled.CheckedChanged += new System.EventHandler(this.checkBoxLiteViewerEnabled_CheckedChanged);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Location = new System.Drawing.Point(381, 27);
            this.textBoxDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.Size = new System.Drawing.Size(417, 22);
            this.textBoxDescription.TabIndex = 6;
            this.textBoxDescription.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDescription_KeyPress);
            this.textBoxDescription.Leave += new System.EventHandler(this.textBoxDescription_Leave);
            // 
            // comboBoxGroup
            // 
            this.comboBoxGroup.FormattingEnabled = true;
            this.comboBoxGroup.Location = new System.Drawing.Point(68, 27);
            this.comboBoxGroup.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxGroup.Name = "comboBoxGroup";
            this.comboBoxGroup.Size = new System.Drawing.Size(185, 24);
            this.comboBoxGroup.TabIndex = 3;
            this.comboBoxGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroup_SelectedIndexChanged);
            this.comboBoxGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxGroup_KeyDown);
            this.comboBoxGroup.Leave += new System.EventHandler(this.comboBoxGroup_Leave);
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(8, 31);
            this.labelGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(52, 17);
            this.labelGroup.TabIndex = 4;
            this.labelGroup.Text = "Group:";
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(289, 31);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(83, 17);
            this.labelDescription.TabIndex = 5;
            this.labelDescription.Text = "Description:";
            // 
            // labelPriority
            // 
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(827, 31);
            this.labelPriority.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(56, 17);
            this.labelPriority.TabIndex = 23;
            this.labelPriority.Text = "Priority:";
            // 
            // numericUpDownPriority
            // 
            this.numericUpDownPriority.Location = new System.Drawing.Point(889, 28);
            this.numericUpDownPriority.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownPriority.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownPriority.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPriority.Name = "numericUpDownPriority";
            this.numericUpDownPriority.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownPriority.TabIndex = 24;
            this.numericUpDownPriority.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownPriority.ValueChanged += new System.EventHandler(this.numericUpDownPriority_ValueChanged);
            // 
            // groupBoxConversion
            // 
            this.groupBoxConversion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConversion.Controls.Add(this.label2);
            this.groupBoxConversion.Controls.Add(this.comboPdfConversion);
            this.groupBoxConversion.Controls.Add(this.comboBoxBodyPosition);
            this.groupBoxConversion.Controls.Add(this.labelBodyPos);
            this.groupBoxConversion.Controls.Add(this.checkBoxRemoveFaxHeader);
            this.groupBoxConversion.Controls.Add(this.comboBoxBinarisationAlgorithm);
            this.groupBoxConversion.Controls.Add(this.labelBinarisationAlgorithm);
            this.groupBoxConversion.Controls.Add(this.comboBoxNativeFiles);
            this.groupBoxConversion.Controls.Add(this.labelNativeFiles);
            this.groupBoxConversion.Controls.Add(this.comboBoxResolution);
            this.groupBoxConversion.Controls.Add(this.checkBoxRemoveBlanks);
            this.groupBoxConversion.Controls.Add(this.comboBoxBodyConversion);
            this.groupBoxConversion.Controls.Add(this.comboBoxAttachmentConversion);
            this.groupBoxConversion.Controls.Add(this.comboBoxBatchStyle);
            this.groupBoxConversion.Controls.Add(this.labelBatchStyle);
            this.groupBoxConversion.Controls.Add(this.labelBodyConv);
            this.groupBoxConversion.Controls.Add(this.labelAttachmentConv);
            this.groupBoxConversion.Controls.Add(this.label1);
            this.groupBoxConversion.Controls.Add(this.comboBoxBitDepth);
            this.groupBoxConversion.Controls.Add(this.labelResolution);
            this.groupBoxConversion.Location = new System.Drawing.Point(16, 630);
            this.groupBoxConversion.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxConversion.Name = "groupBoxConversion";
            this.groupBoxConversion.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxConversion.Size = new System.Drawing.Size(959, 194);
            this.groupBoxConversion.TabIndex = 2;
            this.groupBoxConversion.TabStop = false;
            this.groupBoxConversion.Text = "Conversion";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 156);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Pdf Conversion";
            // 
            // comboPdfConversion
            // 
            this.comboPdfConversion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPdfConversion.FormattingEnabled = true;
            this.comboPdfConversion.Location = new System.Drawing.Point(347, 156);
            this.comboPdfConversion.Margin = new System.Windows.Forms.Padding(4);
            this.comboPdfConversion.Name = "comboPdfConversion";
            this.comboPdfConversion.Size = new System.Drawing.Size(181, 24);
            this.comboPdfConversion.TabIndex = 19;
            this.comboPdfConversion.SelectedIndexChanged += new System.EventHandler(this.comboPdfConversion_SelectedIndexChanged);
            // 
            // comboBoxBodyPosition
            // 
            this.comboBoxBodyPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBodyPosition.FormattingEnabled = true;
            this.comboBoxBodyPosition.Location = new System.Drawing.Point(347, 123);
            this.comboBoxBodyPosition.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBodyPosition.Name = "comboBoxBodyPosition";
            this.comboBoxBodyPosition.Size = new System.Drawing.Size(181, 24);
            this.comboBoxBodyPosition.TabIndex = 18;
            this.comboBoxBodyPosition.SelectedIndexChanged += new System.EventHandler(this.comboBoxBodyPosition_SelectedIndexChanged);
            // 
            // labelBodyPos
            // 
            this.labelBodyPos.AutoSize = true;
            this.labelBodyPos.Location = new System.Drawing.Point(219, 127);
            this.labelBodyPos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBodyPos.Name = "labelBodyPos";
            this.labelBodyPos.Size = new System.Drawing.Size(98, 17);
            this.labelBodyPos.TabIndex = 17;
            this.labelBodyPos.Text = "Body Position:";
            // 
            // checkBoxRemoveFaxHeader
            // 
            this.checkBoxRemoveFaxHeader.AutoSize = true;
            this.checkBoxRemoveFaxHeader.Location = new System.Drawing.Point(12, 59);
            this.checkBoxRemoveFaxHeader.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRemoveFaxHeader.Name = "checkBoxRemoveFaxHeader";
            this.checkBoxRemoveFaxHeader.Size = new System.Drawing.Size(159, 21);
            this.checkBoxRemoveFaxHeader.TabIndex = 16;
            this.checkBoxRemoveFaxHeader.Text = "Remove Fax Header";
            this.checkBoxRemoveFaxHeader.UseVisualStyleBackColor = true;
            this.checkBoxRemoveFaxHeader.CheckedChanged += new System.EventHandler(this.checkBoxRemoveFaxHeader_CheckedChanged);
            // 
            // comboBoxBinarisationAlgorithm
            // 
            this.comboBoxBinarisationAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBinarisationAlgorithm.FormattingEnabled = true;
            this.comboBoxBinarisationAlgorithm.Location = new System.Drawing.Point(753, 123);
            this.comboBoxBinarisationAlgorithm.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBinarisationAlgorithm.Name = "comboBoxBinarisationAlgorithm";
            this.comboBoxBinarisationAlgorithm.Size = new System.Drawing.Size(181, 24);
            this.comboBoxBinarisationAlgorithm.TabIndex = 15;
            this.comboBoxBinarisationAlgorithm.SelectedIndexChanged += new System.EventHandler(this.comboBoxBinarisationAlgorithm_SelectedIndexChanged);
            // 
            // labelBinarisationAlgorithm
            // 
            this.labelBinarisationAlgorithm.AutoSize = true;
            this.labelBinarisationAlgorithm.Location = new System.Drawing.Point(585, 127);
            this.labelBinarisationAlgorithm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBinarisationAlgorithm.Name = "labelBinarisationAlgorithm";
            this.labelBinarisationAlgorithm.Size = new System.Drawing.Size(149, 17);
            this.labelBinarisationAlgorithm.TabIndex = 14;
            this.labelBinarisationAlgorithm.Text = "Binarisation Algorithm:";
            // 
            // comboBoxNativeFiles
            // 
            this.comboBoxNativeFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNativeFiles.FormattingEnabled = true;
            this.comboBoxNativeFiles.Location = new System.Drawing.Point(753, 90);
            this.comboBoxNativeFiles.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxNativeFiles.Name = "comboBoxNativeFiles";
            this.comboBoxNativeFiles.Size = new System.Drawing.Size(181, 24);
            this.comboBoxNativeFiles.TabIndex = 13;
            this.comboBoxNativeFiles.SelectedIndexChanged += new System.EventHandler(this.comboBoxNativeFiles_SelectedIndexChanged);
            // 
            // labelNativeFiles
            // 
            this.labelNativeFiles.AutoSize = true;
            this.labelNativeFiles.Location = new System.Drawing.Point(585, 94);
            this.labelNativeFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNativeFiles.Name = "labelNativeFiles";
            this.labelNativeFiles.Size = new System.Drawing.Size(130, 17);
            this.labelNativeFiles.TabIndex = 12;
            this.labelNativeFiles.Text = "Retain Native Files:";
            // 
            // comboBoxResolution
            // 
            this.comboBoxResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxResolution.FormattingEnabled = true;
            this.comboBoxResolution.Location = new System.Drawing.Point(347, 57);
            this.comboBoxResolution.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxResolution.Name = "comboBoxResolution";
            this.comboBoxResolution.Size = new System.Drawing.Size(181, 24);
            this.comboBoxResolution.TabIndex = 11;
            this.comboBoxResolution.SelectedIndexChanged += new System.EventHandler(this.comboBoxResolution_SelectedIndexChanged);
            // 
            // checkBoxRemoveBlanks
            // 
            this.checkBoxRemoveBlanks.AutoSize = true;
            this.checkBoxRemoveBlanks.Location = new System.Drawing.Point(12, 26);
            this.checkBoxRemoveBlanks.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRemoveBlanks.Name = "checkBoxRemoveBlanks";
            this.checkBoxRemoveBlanks.Size = new System.Drawing.Size(165, 21);
            this.checkBoxRemoveBlanks.TabIndex = 10;
            this.checkBoxRemoveBlanks.Text = "Remove Blank Pages";
            this.checkBoxRemoveBlanks.UseVisualStyleBackColor = true;
            this.checkBoxRemoveBlanks.CheckedChanged += new System.EventHandler(this.checkBoxRemoveBlanks_CheckedChanged);
            // 
            // comboBoxBodyConversion
            // 
            this.comboBoxBodyConversion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBodyConversion.FormattingEnabled = true;
            this.comboBoxBodyConversion.Location = new System.Drawing.Point(753, 23);
            this.comboBoxBodyConversion.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBodyConversion.Name = "comboBoxBodyConversion";
            this.comboBoxBodyConversion.Size = new System.Drawing.Size(181, 24);
            this.comboBoxBodyConversion.TabIndex = 9;
            this.comboBoxBodyConversion.SelectedIndexChanged += new System.EventHandler(this.comboBoxBodyConversion_SelectedIndexChanged);
            // 
            // comboBoxAttachmentConversion
            // 
            this.comboBoxAttachmentConversion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAttachmentConversion.FormattingEnabled = true;
            this.comboBoxAttachmentConversion.Location = new System.Drawing.Point(753, 57);
            this.comboBoxAttachmentConversion.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxAttachmentConversion.Name = "comboBoxAttachmentConversion";
            this.comboBoxAttachmentConversion.Size = new System.Drawing.Size(181, 24);
            this.comboBoxAttachmentConversion.TabIndex = 8;
            this.comboBoxAttachmentConversion.SelectedIndexChanged += new System.EventHandler(this.comboBoxAttachmentConversion_SelectedIndexChanged);
            // 
            // comboBoxBatchStyle
            // 
            this.comboBoxBatchStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBatchStyle.FormattingEnabled = true;
            this.comboBoxBatchStyle.Location = new System.Drawing.Point(347, 90);
            this.comboBoxBatchStyle.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBatchStyle.Name = "comboBoxBatchStyle";
            this.comboBoxBatchStyle.Size = new System.Drawing.Size(181, 24);
            this.comboBoxBatchStyle.TabIndex = 7;
            this.comboBoxBatchStyle.SelectedIndexChanged += new System.EventHandler(this.comboBoxBatchStyle_SelectedIndexChanged);
            // 
            // labelBatchStyle
            // 
            this.labelBatchStyle.AutoSize = true;
            this.labelBatchStyle.Location = new System.Drawing.Point(219, 94);
            this.labelBatchStyle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBatchStyle.Name = "labelBatchStyle";
            this.labelBatchStyle.Size = new System.Drawing.Size(83, 17);
            this.labelBatchStyle.TabIndex = 6;
            this.labelBatchStyle.Text = "Batch Style:";
            // 
            // labelBodyConv
            // 
            this.labelBodyConv.AutoSize = true;
            this.labelBodyConv.Location = new System.Drawing.Point(585, 27);
            this.labelBodyConv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelBodyConv.Name = "labelBodyConv";
            this.labelBodyConv.Size = new System.Drawing.Size(119, 17);
            this.labelBodyConv.TabIndex = 5;
            this.labelBodyConv.Text = "Body Conversion:";
            // 
            // labelAttachmentConv
            // 
            this.labelAttachmentConv.AutoSize = true;
            this.labelAttachmentConv.Location = new System.Drawing.Point(585, 60);
            this.labelAttachmentConv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAttachmentConv.Name = "labelAttachmentConv";
            this.labelAttachmentConv.Size = new System.Drawing.Size(158, 17);
            this.labelAttachmentConv.TabIndex = 4;
            this.labelAttachmentConv.Text = "Attachment Conversion:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Bit Depth:";
            // 
            // comboBoxBitDepth
            // 
            this.comboBoxBitDepth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBitDepth.FormattingEnabled = true;
            this.comboBoxBitDepth.Location = new System.Drawing.Point(347, 23);
            this.comboBoxBitDepth.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxBitDepth.Name = "comboBoxBitDepth";
            this.comboBoxBitDepth.Size = new System.Drawing.Size(181, 24);
            this.comboBoxBitDepth.TabIndex = 2;
            this.comboBoxBitDepth.SelectedIndexChanged += new System.EventHandler(this.comboBoxBitDepth_SelectedIndexChanged);
            // 
            // labelResolution
            // 
            this.labelResolution.AutoSize = true;
            this.labelResolution.Location = new System.Drawing.Point(219, 60);
            this.labelResolution.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelResolution.Name = "labelResolution";
            this.labelResolution.Size = new System.Drawing.Size(79, 17);
            this.labelResolution.TabIndex = 0;
            this.labelResolution.Text = "Resolution:";
            // 
            // groupBoxCollection
            // 
            this.groupBoxCollection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCollection.Controls.Add(this.buttonImapFolder);
            this.groupBoxCollection.Controls.Add(this.buttonImapQuery);
            this.groupBoxCollection.Controls.Add(this.textBoxImapQuery);
            this.groupBoxCollection.Controls.Add(this.textBoxImapFolder);
            this.groupBoxCollection.Controls.Add(this.numericUpDownImapRetention);
            this.groupBoxCollection.Controls.Add(this.labelImapRetention);
            this.groupBoxCollection.Controls.Add(this.labelImapQuery);
            this.groupBoxCollection.Controls.Add(this.labelImapFolder);
            this.groupBoxCollection.Controls.Add(this.textBoxImapPassword);
            this.groupBoxCollection.Controls.Add(this.labelPassword);
            this.groupBoxCollection.Controls.Add(this.textBoxImapUserName);
            this.groupBoxCollection.Controls.Add(this.labelUserName);
            this.groupBoxCollection.Controls.Add(this.numericUpDownImapPort);
            this.groupBoxCollection.Controls.Add(this.labelImapPort);
            this.groupBoxCollection.Controls.Add(this.labelImapHost);
            this.groupBoxCollection.Controls.Add(this.textBoxImapHost);
            this.groupBoxCollection.Location = new System.Drawing.Point(16, 159);
            this.groupBoxCollection.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxCollection.Name = "groupBoxCollection";
            this.groupBoxCollection.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxCollection.Size = new System.Drawing.Size(959, 128);
            this.groupBoxCollection.TabIndex = 1;
            this.groupBoxCollection.TabStop = false;
            this.groupBoxCollection.Text = "Imap Collector";
            // 
            // buttonImapFolder
            // 
            this.buttonImapFolder.Location = new System.Drawing.Point(320, 58);
            this.buttonImapFolder.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImapFolder.Name = "buttonImapFolder";
            this.buttonImapFolder.Size = new System.Drawing.Size(32, 25);
            this.buttonImapFolder.TabIndex = 18;
            this.buttonImapFolder.Text = "...";
            this.buttonImapFolder.UseVisualStyleBackColor = true;
            this.buttonImapFolder.Click += new System.EventHandler(this.buttonImapFolder_Click);
            // 
            // buttonImapQuery
            // 
            this.buttonImapQuery.Location = new System.Drawing.Point(904, 90);
            this.buttonImapQuery.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImapQuery.Name = "buttonImapQuery";
            this.buttonImapQuery.Size = new System.Drawing.Size(32, 25);
            this.buttonImapQuery.TabIndex = 17;
            this.buttonImapQuery.Text = "...";
            this.buttonImapQuery.UseVisualStyleBackColor = true;
            this.buttonImapQuery.Click += new System.EventHandler(this.buttonImapQuery_Click);
            // 
            // textBoxImapQuery
            // 
            this.textBoxImapQuery.Location = new System.Drawing.Point(112, 90);
            this.textBoxImapQuery.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxImapQuery.Name = "textBoxImapQuery";
            this.textBoxImapQuery.Size = new System.Drawing.Size(785, 22);
            this.textBoxImapQuery.TabIndex = 15;
            this.textBoxImapQuery.Leave += new System.EventHandler(this.textBoxImapQuery_Leave);
            // 
            // textBoxImapFolder
            // 
            this.textBoxImapFolder.Location = new System.Drawing.Point(112, 58);
            this.textBoxImapFolder.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxImapFolder.Name = "textBoxImapFolder";
            this.textBoxImapFolder.Size = new System.Drawing.Size(201, 22);
            this.textBoxImapFolder.TabIndex = 14;
            this.textBoxImapFolder.Leave += new System.EventHandler(this.textBoxImapFolder_Leave);
            // 
            // numericUpDownImapRetention
            // 
            this.numericUpDownImapRetention.Location = new System.Drawing.Point(491, 57);
            this.numericUpDownImapRetention.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownImapRetention.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDownImapRetention.Name = "numericUpDownImapRetention";
            this.numericUpDownImapRetention.Size = new System.Drawing.Size(73, 22);
            this.numericUpDownImapRetention.TabIndex = 13;
            this.numericUpDownImapRetention.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numericUpDownImapRetention.ValueChanged += new System.EventHandler(this.numericUpDownImapRetention_ValueChanged);
            // 
            // labelImapRetention
            // 
            this.labelImapRetention.AutoSize = true;
            this.labelImapRetention.Location = new System.Drawing.Point(373, 59);
            this.labelImapRetention.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImapRetention.Name = "labelImapRetention";
            this.labelImapRetention.Size = new System.Drawing.Size(107, 17);
            this.labelImapRetention.TabIndex = 11;
            this.labelImapRetention.Text = "Imap Retention:";
            // 
            // labelImapQuery
            // 
            this.labelImapQuery.AutoSize = true;
            this.labelImapQuery.Location = new System.Drawing.Point(8, 94);
            this.labelImapQuery.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImapQuery.Name = "labelImapQuery";
            this.labelImapQuery.Size = new System.Drawing.Size(85, 17);
            this.labelImapQuery.TabIndex = 10;
            this.labelImapQuery.Text = "Imap Query:";
            // 
            // labelImapFolder
            // 
            this.labelImapFolder.AutoSize = true;
            this.labelImapFolder.Location = new System.Drawing.Point(8, 60);
            this.labelImapFolder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImapFolder.Name = "labelImapFolder";
            this.labelImapFolder.Size = new System.Drawing.Size(86, 17);
            this.labelImapFolder.TabIndex = 9;
            this.labelImapFolder.Text = "Imap Folder:";
            // 
            // textBoxImapPassword
            // 
            this.textBoxImapPassword.Location = new System.Drawing.Point(676, 57);
            this.textBoxImapPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxImapPassword.Name = "textBoxImapPassword";
            this.textBoxImapPassword.PasswordChar = '*';
            this.textBoxImapPassword.Size = new System.Drawing.Size(259, 22);
            this.textBoxImapPassword.TabIndex = 8;
            this.textBoxImapPassword.Leave += new System.EventHandler(this.textBoxImapPassword_Leave);
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(584, 60);
            this.labelPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(73, 17);
            this.labelPassword.TabIndex = 7;
            this.labelPassword.Text = "Password:";
            // 
            // textBoxImapUserName
            // 
            this.textBoxImapUserName.Location = new System.Drawing.Point(676, 26);
            this.textBoxImapUserName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxImapUserName.Name = "textBoxImapUserName";
            this.textBoxImapUserName.Size = new System.Drawing.Size(259, 22);
            this.textBoxImapUserName.TabIndex = 6;
            this.textBoxImapUserName.Leave += new System.EventHandler(this.textBoxImapUserName_Leave);
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Location = new System.Drawing.Point(584, 30);
            this.labelUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(83, 17);
            this.labelUserName.TabIndex = 5;
            this.labelUserName.Text = "User Name:";
            // 
            // numericUpDownImapPort
            // 
            this.numericUpDownImapPort.Location = new System.Drawing.Point(491, 26);
            this.numericUpDownImapPort.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownImapPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownImapPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImapPort.Name = "numericUpDownImapPort";
            this.numericUpDownImapPort.Size = new System.Drawing.Size(73, 22);
            this.numericUpDownImapPort.TabIndex = 4;
            this.numericUpDownImapPort.Value = new decimal(new int[] {
            143,
            0,
            0,
            0});
            this.numericUpDownImapPort.ValueChanged += new System.EventHandler(this.numericUpDownImapPort_ValueChanged);
            // 
            // labelImapPort
            // 
            this.labelImapPort.AutoSize = true;
            this.labelImapPort.Location = new System.Drawing.Point(373, 28);
            this.labelImapPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImapPort.Name = "labelImapPort";
            this.labelImapPort.Size = new System.Drawing.Size(72, 17);
            this.labelImapPort.TabIndex = 3;
            this.labelImapPort.Text = "Imap Port:";
            // 
            // labelImapHost
            // 
            this.labelImapHost.AutoSize = true;
            this.labelImapHost.Location = new System.Drawing.Point(8, 30);
            this.labelImapHost.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelImapHost.Name = "labelImapHost";
            this.labelImapHost.Size = new System.Drawing.Size(75, 17);
            this.labelImapHost.TabIndex = 2;
            this.labelImapHost.Text = "Imap Host:";
            // 
            // textBoxImapHost
            // 
            this.textBoxImapHost.Location = new System.Drawing.Point(112, 26);
            this.textBoxImapHost.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxImapHost.Name = "textBoxImapHost";
            this.textBoxImapHost.Size = new System.Drawing.Size(239, 22);
            this.textBoxImapHost.TabIndex = 1;
            this.textBoxImapHost.Leave += new System.EventHandler(this.textBoxImapHost_Leave);
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(16, 15);
            this.checkBoxEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(82, 21);
            this.checkBoxEnabled.TabIndex = 0;
            this.checkBoxEnabled.Text = "Enabled";
            this.toolTip.SetToolTip(this.checkBoxEnabled, "Check to set the current profile active.");
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabled.CheckedChanged += new System.EventHandler(this.checkBoxEnabled_CheckedChanged);
            // 
            // tabControlConfig
            // 
            this.tabControlConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlConfig.Controls.Add(this.tabPageConfig);
            this.tabControlConfig.Controls.Add(this.tabPageSchedule);
            this.tabControlConfig.Controls.Add(this.tabPageBatchFields);
            this.tabControlConfig.Controls.Add(this.tabPageIndexFields);
            this.tabControlConfig.Controls.Add(this.tabPageFileTypes);
            this.tabControlConfig.Controls.Add(this.tabPageScript);
            this.tabControlConfig.Controls.Add(this.tabPageErrorHandling);
            this.tabControlConfig.Controls.Add(this.tabPageTemplateDesigner);
            this.tabControlConfig.Location = new System.Drawing.Point(311, 10);
            this.tabControlConfig.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlConfig.Name = "tabControlConfig";
            this.tabControlConfig.SelectedIndex = 0;
            this.tabControlConfig.Size = new System.Drawing.Size(1003, 858);
            this.tabControlConfig.TabIndex = 3;
            // 
            // tabPageSchedule
            // 
            this.tabPageSchedule.Controls.Add(this.groupBoxSchedule);
            this.tabPageSchedule.Controls.Add(this.checkBoxEnableScheduler);
            this.tabPageSchedule.Controls.Add(this.checkBoxWeekdaysOnly);
            this.tabPageSchedule.Location = new System.Drawing.Point(4, 25);
            this.tabPageSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageSchedule.Name = "tabPageSchedule";
            this.tabPageSchedule.Size = new System.Drawing.Size(995, 829);
            this.tabPageSchedule.TabIndex = 6;
            this.tabPageSchedule.Text = "Schedule";
            this.tabPageSchedule.UseVisualStyleBackColor = true;
            // 
            // groupBoxSchedule
            // 
            this.groupBoxSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSchedule.Controls.Add(this.panelSchedule);
            this.groupBoxSchedule.Controls.Add(this.labelTimeSlot);
            this.groupBoxSchedule.Enabled = false;
            this.groupBoxSchedule.Location = new System.Drawing.Point(16, 49);
            this.groupBoxSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxSchedule.Name = "groupBoxSchedule";
            this.groupBoxSchedule.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxSchedule.Size = new System.Drawing.Size(1179, 103);
            this.groupBoxSchedule.TabIndex = 4;
            this.groupBoxSchedule.TabStop = false;
            this.groupBoxSchedule.Text = "Scheduler";
            // 
            // panelSchedule
            // 
            this.panelSchedule.Location = new System.Drawing.Point(20, 30);
            this.panelSchedule.Margin = new System.Windows.Forms.Padding(4);
            this.panelSchedule.Name = "panelSchedule";
            this.panelSchedule.Size = new System.Drawing.Size(769, 31);
            this.panelSchedule.TabIndex = 3;
            this.panelSchedule.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSchedule_Paint);
            this.panelSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelSchedule_MouseDown);
            this.panelSchedule.MouseLeave += new System.EventHandler(this.panelSchedule_MouseLeave);
            this.panelSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelSchedule_MouseMove);
            // 
            // labelTimeSlot
            // 
            this.labelTimeSlot.AutoSize = true;
            this.labelTimeSlot.Location = new System.Drawing.Point(16, 70);
            this.labelTimeSlot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimeSlot.Name = "labelTimeSlot";
            this.labelTimeSlot.Size = new System.Drawing.Size(93, 17);
            this.labelTimeSlot.TabIndex = 2;
            this.labelTimeSlot.Text = "labelTimeSlot";
            // 
            // checkBoxEnableScheduler
            // 
            this.checkBoxEnableScheduler.AutoSize = true;
            this.checkBoxEnableScheduler.Location = new System.Drawing.Point(16, 15);
            this.checkBoxEnableScheduler.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxEnableScheduler.Name = "checkBoxEnableScheduler";
            this.checkBoxEnableScheduler.Size = new System.Drawing.Size(142, 21);
            this.checkBoxEnableScheduler.TabIndex = 2;
            this.checkBoxEnableScheduler.Text = "Enable Scheduler";
            this.checkBoxEnableScheduler.UseVisualStyleBackColor = true;
            this.checkBoxEnableScheduler.CheckedChanged += new System.EventHandler(this.checkBoxEnableSchedule_CheckedChanged);
            // 
            // checkBoxWeekdaysOnly
            // 
            this.checkBoxWeekdaysOnly.AutoSize = true;
            this.checkBoxWeekdaysOnly.Enabled = false;
            this.checkBoxWeekdaysOnly.Location = new System.Drawing.Point(183, 15);
            this.checkBoxWeekdaysOnly.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxWeekdaysOnly.Name = "checkBoxWeekdaysOnly";
            this.checkBoxWeekdaysOnly.Size = new System.Drawing.Size(155, 21);
            this.checkBoxWeekdaysOnly.TabIndex = 0;
            this.checkBoxWeekdaysOnly.Text = "For weekdays only?";
            this.checkBoxWeekdaysOnly.UseVisualStyleBackColor = true;
            this.checkBoxWeekdaysOnly.CheckedChanged += new System.EventHandler(this.checkBoxWeekdaysOnly_CheckedChanged);
            // 
            // tabPageScript
            // 
            this.tabPageScript.Controls.Add(this.textBoxScript);
            this.tabPageScript.Controls.Add(this.toolStripScript);
            this.tabPageScript.Location = new System.Drawing.Point(4, 25);
            this.tabPageScript.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.Size = new System.Drawing.Size(995, 829);
            this.tabPageScript.TabIndex = 9;
            this.tabPageScript.Text = "Script Editor";
            this.tabPageScript.UseVisualStyleBackColor = true;
            // 
            // textBoxScript
            // 
            this.textBoxScript.AutoScrollMinSize = new System.Drawing.Size(2, 18);
            this.textBoxScript.BackBrush = null;
            this.textBoxScript.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxScript.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxScript.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.textBoxScript.IsReplaceMode = false;
            this.textBoxScript.Language = FastColoredTextBoxNS.Language.CSharp;
            this.textBoxScript.LeftBracket = '(';
            this.textBoxScript.Location = new System.Drawing.Point(0, 27);
            this.textBoxScript.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxScript.Name = "textBoxScript";
            this.textBoxScript.Paddings = new System.Windows.Forms.Padding(0);
            this.textBoxScript.RightBracket = ')';
            this.textBoxScript.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.textBoxScript.ShowFoldingLines = true;
            this.textBoxScript.Size = new System.Drawing.Size(995, 802);
            this.textBoxScript.TabIndex = 3;
            this.textBoxScript.Leave += new System.EventHandler(this.textBoxScript_Leave);
            // 
            // toolStripScript
            // 
            this.toolStripScript.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripScript.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCreateNewScript,
            this.toolStripDropDownButtonSnippets,
            this.toolStripSplitButtonTestScript});
            this.toolStripScript.Location = new System.Drawing.Point(0, 0);
            this.toolStripScript.Name = "toolStripScript";
            this.toolStripScript.Size = new System.Drawing.Size(995, 27);
            this.toolStripScript.TabIndex = 2;
            this.toolStripScript.Text = "toolStrip1";
            // 
            // toolStripButtonCreateNewScript
            // 
            this.toolStripButtonCreateNewScript.AutoToolTip = false;
            this.toolStripButtonCreateNewScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCreateNewScript.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreateNewScript.Image")));
            this.toolStripButtonCreateNewScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreateNewScript.Name = "toolStripButtonCreateNewScript";
            this.toolStripButtonCreateNewScript.Size = new System.Drawing.Size(132, 24);
            this.toolStripButtonCreateNewScript.Text = "Create New Script";
            this.toolStripButtonCreateNewScript.Click += new System.EventHandler(this.toolStripButtonCreateNewScript_Click);
            // 
            // toolStripDropDownButtonSnippets
            // 
            this.toolStripDropDownButtonSnippets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonSnippets.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSnippets.Image")));
            this.toolStripDropDownButtonSnippets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonSnippets.Name = "toolStripDropDownButtonSnippets";
            this.toolStripDropDownButtonSnippets.Size = new System.Drawing.Size(153, 24);
            this.toolStripDropDownButtonSnippets.Text = "Insert Code Snippet";
            // 
            // toolStripSplitButtonTestScript
            // 
            this.toolStripSplitButtonTestScript.AutoToolTip = false;
            this.toolStripSplitButtonTestScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButtonTestScript.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectMailMessageToolStripMenuItem,
            this.enableWindowsFormsToolStripMenuItem});
            this.toolStripSplitButtonTestScript.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonTestScript.Image")));
            this.toolStripSplitButtonTestScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonTestScript.Name = "toolStripSplitButtonTestScript";
            this.toolStripSplitButtonTestScript.Size = new System.Drawing.Size(54, 24);
            this.toolStripSplitButtonTestScript.Text = "Test";
            this.toolStripSplitButtonTestScript.ButtonClick += new System.EventHandler(this.toolStripSplitButtonTestScript_ButtonClick);
            // 
            // selectMailMessageToolStripMenuItem
            // 
            this.selectMailMessageToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.selectMailMessageToolStripMenuItem.Name = "selectMailMessageToolStripMenuItem";
            this.selectMailMessageToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.selectMailMessageToolStripMenuItem.Text = "Select Mail Message...";
            this.selectMailMessageToolStripMenuItem.Click += new System.EventHandler(this.selectMailMessageToolStripMenuItem_Click);
            // 
            // enableWindowsFormsToolStripMenuItem
            // 
            this.enableWindowsFormsToolStripMenuItem.CheckOnClick = true;
            this.enableWindowsFormsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.enableWindowsFormsToolStripMenuItem.Name = "enableWindowsFormsToolStripMenuItem";
            this.enableWindowsFormsToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.enableWindowsFormsToolStripMenuItem.Text = "Enable Windows Forms";
            // 
            // tabPageErrorHandling
            // 
            this.tabPageErrorHandling.Controls.Add(this.groupBoxErrorBatchFields);
            this.tabPageErrorHandling.Controls.Add(this.groupBoxErrorGeneral);
            this.tabPageErrorHandling.Controls.Add(this.groupBoxConversionErrorBatch);
            this.tabPageErrorHandling.Location = new System.Drawing.Point(4, 25);
            this.tabPageErrorHandling.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageErrorHandling.Name = "tabPageErrorHandling";
            this.tabPageErrorHandling.Size = new System.Drawing.Size(995, 829);
            this.tabPageErrorHandling.TabIndex = 7;
            this.tabPageErrorHandling.Text = "Error Handling";
            this.tabPageErrorHandling.UseVisualStyleBackColor = true;
            // 
            // groupBoxErrorBatchFields
            // 
            this.groupBoxErrorBatchFields.Controls.Add(this.dataGridViewErrorBatchFields);
            this.groupBoxErrorBatchFields.Location = new System.Drawing.Point(16, 326);
            this.groupBoxErrorBatchFields.Name = "groupBoxErrorBatchFields";
            this.groupBoxErrorBatchFields.Size = new System.Drawing.Size(1179, 246);
            this.groupBoxErrorBatchFields.TabIndex = 42;
            this.groupBoxErrorBatchFields.TabStop = false;
            // 
            // dataGridViewErrorBatchFields
            // 
            this.dataGridViewErrorBatchFields.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewErrorBatchFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewErrorBatchFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ErrorBatchFieldNameColumn,
            this.ErrorBatchFieldValueColumn});
            this.dataGridViewErrorBatchFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewErrorBatchFields.Location = new System.Drawing.Point(3, 18);
            this.dataGridViewErrorBatchFields.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewErrorBatchFields.Name = "dataGridViewErrorBatchFields";
            this.dataGridViewErrorBatchFields.Size = new System.Drawing.Size(1173, 225);
            this.dataGridViewErrorBatchFields.TabIndex = 40;
            this.dataGridViewErrorBatchFields.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewErrorBatchFields_CellValueChanged);
            this.dataGridViewErrorBatchFields.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridViewErrorBatchFields_RowsRemoved);
            this.dataGridViewErrorBatchFields.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridViewErrorBatchFields_MouseClick);
            // 
            // groupBoxConversionErrorBatch
            // 
            this.groupBoxConversionErrorBatch.Controls.Add(this.textBoxErrorOutputPath);
            this.groupBoxConversionErrorBatch.Controls.Add(this.btnErrorOutputPath);
            this.groupBoxConversionErrorBatch.Controls.Add(this.labelProcessErrorBatch);
            this.groupBoxConversionErrorBatch.Controls.Add(this.checkBoxProcessErrorBatch);
            this.groupBoxConversionErrorBatch.Controls.Add(this.labelErrorPath);
            this.groupBoxConversionErrorBatch.Location = new System.Drawing.Point(16, 214);
            this.groupBoxConversionErrorBatch.Name = "groupBoxConversionErrorBatch";
            this.groupBoxConversionErrorBatch.Size = new System.Drawing.Size(1179, 106);
            this.groupBoxConversionErrorBatch.TabIndex = 41;
            this.groupBoxConversionErrorBatch.TabStop = false;
            this.groupBoxConversionErrorBatch.Text = "Conversion Error Processing";
            // 
            // textBoxErrorOutputPath
            // 
            this.textBoxErrorOutputPath.Location = new System.Drawing.Point(179, 66);
            this.textBoxErrorOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxErrorOutputPath.MaxLength = 150;
            this.textBoxErrorOutputPath.Name = "textBoxErrorOutputPath";
            this.textBoxErrorOutputPath.Size = new System.Drawing.Size(531, 22);
            this.textBoxErrorOutputPath.TabIndex = 53;
            // 
            // btnErrorOutputPath
            // 
            this.btnErrorOutputPath.Location = new System.Drawing.Point(718, 66);
            this.btnErrorOutputPath.Margin = new System.Windows.Forms.Padding(4);
            this.btnErrorOutputPath.Name = "btnErrorOutputPath";
            this.btnErrorOutputPath.Size = new System.Drawing.Size(32, 25);
            this.btnErrorOutputPath.TabIndex = 54;
            this.btnErrorOutputPath.Text = "...";
            this.btnErrorOutputPath.UseVisualStyleBackColor = true;
            this.btnErrorOutputPath.Click += new System.EventHandler(this.btnErrorOutputPath_Click);
            // 
            // labelProcessErrorBatch
            // 
            this.labelProcessErrorBatch.AutoSize = true;
            this.labelProcessErrorBatch.Location = new System.Drawing.Point(36, 30);
            this.labelProcessErrorBatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelProcessErrorBatch.Name = "labelProcessErrorBatch";
            this.labelProcessErrorBatch.Size = new System.Drawing.Size(135, 17);
            this.labelProcessErrorBatch.TabIndex = 50;
            this.labelProcessErrorBatch.Text = "Process Error Batch";
            // 
            // checkBoxProcessErrorBatch
            // 
            this.checkBoxProcessErrorBatch.AutoSize = true;
            this.checkBoxProcessErrorBatch.Location = new System.Drawing.Point(12, 31);
            this.checkBoxProcessErrorBatch.Name = "checkBoxProcessErrorBatch";
            this.checkBoxProcessErrorBatch.Size = new System.Drawing.Size(18, 17);
            this.checkBoxProcessErrorBatch.TabIndex = 51;
            this.checkBoxProcessErrorBatch.UseVisualStyleBackColor = true;
            this.checkBoxProcessErrorBatch.CheckedChanged += new System.EventHandler(this.checkBoxProcessErrorBatch_CheckedChanged);
            // 
            // labelErrorPath
            // 
            this.labelErrorPath.AutoSize = true;
            this.labelErrorPath.Location = new System.Drawing.Point(7, 66);
            this.labelErrorPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelErrorPath.Name = "labelErrorPath";
            this.labelErrorPath.Size = new System.Drawing.Size(164, 17);
            this.labelErrorPath.TabIndex = 52;
            this.labelErrorPath.Text = "Error Batch Output Path:";
            // 
            // groupBoxErrorGeneral
            // 
            this.groupBoxErrorGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxErrorGeneral.Controls.Add(this.labelEscalationEmail);
            this.groupBoxErrorGeneral.Controls.Add(this.textBoxEscalationEmail);
            this.groupBoxErrorGeneral.Controls.Add(this.labelErrorUnprocessable);
            this.groupBoxErrorGeneral.Controls.Add(this.labelErrorUnknown);
            this.groupBoxErrorGeneral.Controls.Add(this.labelErrorUnsupported);
            this.groupBoxErrorGeneral.Controls.Add(this.comboBoxErrorUnprocessable);
            this.groupBoxErrorGeneral.Controls.Add(this.comboBoxErrorUnknown);
            this.groupBoxErrorGeneral.Controls.Add(this.comboBoxErrorUnsupported);
            this.groupBoxErrorGeneral.Controls.Add(this.radioButtonForwardAttachment);
            this.groupBoxErrorGeneral.Controls.Add(this.radioButtonForwardInline);
            this.groupBoxErrorGeneral.Controls.Add(this.textBoxTimeBetweenRetries);
            this.groupBoxErrorGeneral.Controls.Add(this.numericUpDownMaxRetries);
            this.groupBoxErrorGeneral.Controls.Add(this.labelMaxRetries);
            this.groupBoxErrorGeneral.Controls.Add(this.labelTimeBetweenRetries);
            this.groupBoxErrorGeneral.Location = new System.Drawing.Point(16, 15);
            this.groupBoxErrorGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxErrorGeneral.Name = "groupBoxErrorGeneral";
            this.groupBoxErrorGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxErrorGeneral.Size = new System.Drawing.Size(1179, 192);
            this.groupBoxErrorGeneral.TabIndex = 39;
            this.groupBoxErrorGeneral.TabStop = false;
            this.groupBoxErrorGeneral.Text = "General";
            // 
            // labelEscalationEmail
            // 
            this.labelEscalationEmail.AutoSize = true;
            this.labelEscalationEmail.Location = new System.Drawing.Point(8, 144);
            this.labelEscalationEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelEscalationEmail.Name = "labelEscalationEmail";
            this.labelEscalationEmail.Size = new System.Drawing.Size(115, 17);
            this.labelEscalationEmail.TabIndex = 49;
            this.labelEscalationEmail.Text = "Escalation Email:";
            // 
            // textBoxEscalationEmail
            // 
            this.textBoxEscalationEmail.Location = new System.Drawing.Point(132, 140);
            this.textBoxEscalationEmail.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEscalationEmail.Name = "textBoxEscalationEmail";
            this.textBoxEscalationEmail.Size = new System.Drawing.Size(719, 22);
            this.textBoxEscalationEmail.TabIndex = 48;
            this.textBoxEscalationEmail.Leave += new System.EventHandler(this.textBoxEscalationEmail_Leave);
            // 
            // labelErrorUnprocessable
            // 
            this.labelErrorUnprocessable.AutoSize = true;
            this.labelErrorUnprocessable.Location = new System.Drawing.Point(575, 62);
            this.labelErrorUnprocessable.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelErrorUnprocessable.Name = "labelErrorUnprocessable";
            this.labelErrorUnprocessable.Size = new System.Drawing.Size(107, 17);
            this.labelErrorUnprocessable.TabIndex = 47;
            this.labelErrorUnprocessable.Text = "Unprocessable:";
            // 
            // labelErrorUnknown
            // 
            this.labelErrorUnknown.AutoSize = true;
            this.labelErrorUnknown.Location = new System.Drawing.Point(575, 95);
            this.labelErrorUnknown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelErrorUnknown.Name = "labelErrorUnknown";
            this.labelErrorUnknown.Size = new System.Drawing.Size(106, 17);
            this.labelErrorUnknown.TabIndex = 46;
            this.labelErrorUnknown.Text = "Unknown Error:";
            // 
            // labelErrorUnsupported
            // 
            this.labelErrorUnsupported.AutoSize = true;
            this.labelErrorUnsupported.Location = new System.Drawing.Point(575, 28);
            this.labelErrorUnsupported.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelErrorUnsupported.Name = "labelErrorUnsupported";
            this.labelErrorUnsupported.Size = new System.Drawing.Size(94, 17);
            this.labelErrorUnsupported.TabIndex = 45;
            this.labelErrorUnsupported.Text = "Unsupported:";
            // 
            // comboBoxErrorUnprocessable
            // 
            this.comboBoxErrorUnprocessable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxErrorUnprocessable.FormattingEnabled = true;
            this.comboBoxErrorUnprocessable.Location = new System.Drawing.Point(691, 58);
            this.comboBoxErrorUnprocessable.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxErrorUnprocessable.Name = "comboBoxErrorUnprocessable";
            this.comboBoxErrorUnprocessable.Size = new System.Drawing.Size(160, 24);
            this.comboBoxErrorUnprocessable.TabIndex = 44;
            this.comboBoxErrorUnprocessable.SelectedIndexChanged += new System.EventHandler(this.comboBoxErrorUnprocessable_SelectedIndexChanged);
            // 
            // comboBoxErrorUnknown
            // 
            this.comboBoxErrorUnknown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxErrorUnknown.FormattingEnabled = true;
            this.comboBoxErrorUnknown.Location = new System.Drawing.Point(691, 91);
            this.comboBoxErrorUnknown.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxErrorUnknown.Name = "comboBoxErrorUnknown";
            this.comboBoxErrorUnknown.Size = new System.Drawing.Size(160, 24);
            this.comboBoxErrorUnknown.TabIndex = 43;
            this.comboBoxErrorUnknown.SelectedIndexChanged += new System.EventHandler(this.comboBoxErrorUnknown_SelectedIndexChanged);
            // 
            // comboBoxErrorUnsupported
            // 
            this.comboBoxErrorUnsupported.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxErrorUnsupported.FormattingEnabled = true;
            this.comboBoxErrorUnsupported.Location = new System.Drawing.Point(691, 25);
            this.comboBoxErrorUnsupported.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxErrorUnsupported.Name = "comboBoxErrorUnsupported";
            this.comboBoxErrorUnsupported.Size = new System.Drawing.Size(160, 24);
            this.comboBoxErrorUnsupported.TabIndex = 42;
            this.comboBoxErrorUnsupported.SelectedIndexChanged += new System.EventHandler(this.comboBoxErrorUnsupported_SelectedIndexChanged);
            // 
            // radioButtonForwardAttachment
            // 
            this.radioButtonForwardAttachment.AutoSize = true;
            this.radioButtonForwardAttachment.Location = new System.Drawing.Point(12, 54);
            this.radioButtonForwardAttachment.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonForwardAttachment.Name = "radioButtonForwardAttachment";
            this.radioButtonForwardAttachment.Size = new System.Drawing.Size(174, 21);
            this.radioButtonForwardAttachment.TabIndex = 41;
            this.radioButtonForwardAttachment.TabStop = true;
            this.radioButtonForwardAttachment.Text = "Forward as Attachment";
            this.radioButtonForwardAttachment.UseVisualStyleBackColor = true;
            this.radioButtonForwardAttachment.CheckedChanged += new System.EventHandler(this.radioButtonForwardAttachment_CheckedChanged);
            // 
            // radioButtonForwardInline
            // 
            this.radioButtonForwardInline.AutoSize = true;
            this.radioButtonForwardInline.Location = new System.Drawing.Point(12, 26);
            this.radioButtonForwardInline.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonForwardInline.Name = "radioButtonForwardInline";
            this.radioButtonForwardInline.Size = new System.Drawing.Size(117, 21);
            this.radioButtonForwardInline.TabIndex = 40;
            this.radioButtonForwardInline.TabStop = true;
            this.radioButtonForwardInline.Text = "Forward Inline";
            this.radioButtonForwardInline.UseVisualStyleBackColor = true;
            // 
            // textBoxTimeBetweenRetries
            // 
            this.textBoxTimeBetweenRetries.Location = new System.Drawing.Point(404, 25);
            this.textBoxTimeBetweenRetries.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTimeBetweenRetries.Name = "textBoxTimeBetweenRetries";
            this.textBoxTimeBetweenRetries.Size = new System.Drawing.Size(97, 22);
            this.textBoxTimeBetweenRetries.TabIndex = 39;
            this.textBoxTimeBetweenRetries.Leave += new System.EventHandler(this.textBoxTimeBetweenRetries_Leave);
            // 
            // numericUpDownMaxRetries
            // 
            this.numericUpDownMaxRetries.Location = new System.Drawing.Point(404, 58);
            this.numericUpDownMaxRetries.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownMaxRetries.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numericUpDownMaxRetries.Name = "numericUpDownMaxRetries";
            this.numericUpDownMaxRetries.Size = new System.Drawing.Size(99, 22);
            this.numericUpDownMaxRetries.TabIndex = 35;
            this.numericUpDownMaxRetries.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownMaxRetries.ValueChanged += new System.EventHandler(this.numericUpDownMaxRetries_ValueChanged);
            // 
            // labelMaxRetries
            // 
            this.labelMaxRetries.AutoSize = true;
            this.labelMaxRetries.Location = new System.Drawing.Point(244, 60);
            this.labelMaxRetries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMaxRetries.Name = "labelMaxRetries";
            this.labelMaxRetries.Size = new System.Drawing.Size(119, 17);
            this.labelMaxRetries.TabIndex = 34;
            this.labelMaxRetries.Text = "Maximum Retries:";
            // 
            // labelTimeBetweenRetries
            // 
            this.labelTimeBetweenRetries.AutoSize = true;
            this.labelTimeBetweenRetries.Location = new System.Drawing.Point(244, 28);
            this.labelTimeBetweenRetries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTimeBetweenRetries.Name = "labelTimeBetweenRetries";
            this.labelTimeBetweenRetries.Size = new System.Drawing.Size(150, 17);
            this.labelTimeBetweenRetries.TabIndex = 32;
            this.labelTimeBetweenRetries.Text = "Time Between Retries:";
            // 
            // tabPageTemplateDesigner
            // 
            this.tabPageTemplateDesigner.Controls.Add(this.linkLabelClear);
            this.tabPageTemplateDesigner.Controls.Add(this.linkLabelTemplateEditHtml);
            this.tabPageTemplateDesigner.Controls.Add(this.linkLabelTemplateDefault);
            this.tabPageTemplateDesigner.Controls.Add(this.textBoxTemplateFrom);
            this.tabPageTemplateDesigner.Controls.Add(this.htmlEditorTemplateBody);
            this.tabPageTemplateDesigner.Controls.Add(this.textBoxTemplateTo);
            this.tabPageTemplateDesigner.Controls.Add(this.labelTemplateSubject);
            this.tabPageTemplateDesigner.Controls.Add(this.textBoxTemplateCc);
            this.tabPageTemplateDesigner.Controls.Add(this.labelTemplateBcc);
            this.tabPageTemplateDesigner.Controls.Add(this.textBoxTemplateBcc);
            this.tabPageTemplateDesigner.Controls.Add(this.labelTemplateCc);
            this.tabPageTemplateDesigner.Controls.Add(this.textBoxTemplateSubject);
            this.tabPageTemplateDesigner.Controls.Add(this.labelTemplateTo);
            this.tabPageTemplateDesigner.Controls.Add(this.labelTemplateFrom);
            this.tabPageTemplateDesigner.Location = new System.Drawing.Point(4, 25);
            this.tabPageTemplateDesigner.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageTemplateDesigner.Name = "tabPageTemplateDesigner";
            this.tabPageTemplateDesigner.Size = new System.Drawing.Size(995, 829);
            this.tabPageTemplateDesigner.TabIndex = 8;
            this.tabPageTemplateDesigner.Text = "Template Designer";
            this.tabPageTemplateDesigner.UseVisualStyleBackColor = true;
            // 
            // linkLabelClear
            // 
            this.linkLabelClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelClear.AutoSize = true;
            this.linkLabelClear.Location = new System.Drawing.Point(1153, 753);
            this.linkLabelClear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelClear.Name = "linkLabelClear";
            this.linkLabelClear.Size = new System.Drawing.Size(41, 17);
            this.linkLabelClear.TabIndex = 13;
            this.linkLabelClear.TabStop = true;
            this.linkLabelClear.Text = "Clear";
            this.linkLabelClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelClear_LinkClicked);
            // 
            // linkLabelTemplateEditHtml
            // 
            this.linkLabelTemplateEditHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelTemplateEditHtml.AutoSize = true;
            this.linkLabelTemplateEditHtml.Location = new System.Drawing.Point(137, 753);
            this.linkLabelTemplateEditHtml.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelTemplateEditHtml.Name = "linkLabelTemplateEditHtml";
            this.linkLabelTemplateEditHtml.Size = new System.Drawing.Size(64, 17);
            this.linkLabelTemplateEditHtml.TabIndex = 12;
            this.linkLabelTemplateEditHtml.TabStop = true;
            this.linkLabelTemplateEditHtml.Text = "Edit Html";
            this.linkLabelTemplateEditHtml.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTemplateEditHtml_LinkClicked);
            // 
            // linkLabelTemplateDefault
            // 
            this.linkLabelTemplateDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelTemplateDefault.AutoSize = true;
            this.linkLabelTemplateDefault.Location = new System.Drawing.Point(12, 753);
            this.linkLabelTemplateDefault.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabelTemplateDefault.Name = "linkLabelTemplateDefault";
            this.linkLabelTemplateDefault.Size = new System.Drawing.Size(116, 17);
            this.linkLabelTemplateDefault.TabIndex = 11;
            this.linkLabelTemplateDefault.TabStop = true;
            this.linkLabelTemplateDefault.Text = "Default Template";
            this.linkLabelTemplateDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelTemplateDefault_LinkClicked);
            // 
            // textBoxTemplateFrom
            // 
            this.textBoxTemplateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateFrom.Location = new System.Drawing.Point(85, 15);
            this.textBoxTemplateFrom.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTemplateFrom.Name = "textBoxTemplateFrom";
            this.textBoxTemplateFrom.Size = new System.Drawing.Size(1108, 22);
            this.textBoxTemplateFrom.TabIndex = 0;
            this.textBoxTemplateFrom.Leave += new System.EventHandler(this.textBoxTemplateFrom_Leave);
            // 
            // htmlEditorTemplateBody
            // 
            this.htmlEditorTemplateBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlEditorTemplateBody.BorderSize = ((byte)(1));
            this.htmlEditorTemplateBody.InnerText = null;
            this.htmlEditorTemplateBody.Location = new System.Drawing.Point(16, 186);
            this.htmlEditorTemplateBody.Margin = new System.Windows.Forms.Padding(4);
            this.htmlEditorTemplateBody.Name = "htmlEditorTemplateBody";
            this.htmlEditorTemplateBody.Size = new System.Drawing.Size(1179, 558);
            this.htmlEditorTemplateBody.TabIndex = 10;
            this.htmlEditorTemplateBody.ToolbarDock = System.Windows.Forms.DockStyle.Top;
            this.htmlEditorTemplateBody.Leave += new System.EventHandler(this.htmlEditorTemplateBody_Leave);
            // 
            // textBoxTemplateTo
            // 
            this.textBoxTemplateTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateTo.Location = new System.Drawing.Point(85, 47);
            this.textBoxTemplateTo.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTemplateTo.Name = "textBoxTemplateTo";
            this.textBoxTemplateTo.Size = new System.Drawing.Size(1108, 22);
            this.textBoxTemplateTo.TabIndex = 1;
            this.textBoxTemplateTo.Leave += new System.EventHandler(this.textBoxTemplateTo_Leave);
            // 
            // labelTemplateSubject
            // 
            this.labelTemplateSubject.AutoSize = true;
            this.labelTemplateSubject.Location = new System.Drawing.Point(12, 146);
            this.labelTemplateSubject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTemplateSubject.Name = "labelTemplateSubject";
            this.labelTemplateSubject.Size = new System.Drawing.Size(59, 17);
            this.labelTemplateSubject.TabIndex = 9;
            this.labelTemplateSubject.Text = "Subject:";
            // 
            // textBoxTemplateCc
            // 
            this.textBoxTemplateCc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateCc.Location = new System.Drawing.Point(85, 79);
            this.textBoxTemplateCc.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTemplateCc.Name = "textBoxTemplateCc";
            this.textBoxTemplateCc.Size = new System.Drawing.Size(1108, 22);
            this.textBoxTemplateCc.TabIndex = 2;
            this.textBoxTemplateCc.Leave += new System.EventHandler(this.textBoxTemplateCc_Leave);
            // 
            // labelTemplateBcc
            // 
            this.labelTemplateBcc.AutoSize = true;
            this.labelTemplateBcc.Location = new System.Drawing.Point(12, 114);
            this.labelTemplateBcc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTemplateBcc.Name = "labelTemplateBcc";
            this.labelTemplateBcc.Size = new System.Drawing.Size(35, 17);
            this.labelTemplateBcc.TabIndex = 8;
            this.labelTemplateBcc.Text = "Bcc:";
            // 
            // textBoxTemplateBcc
            // 
            this.textBoxTemplateBcc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateBcc.Location = new System.Drawing.Point(85, 111);
            this.textBoxTemplateBcc.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTemplateBcc.Name = "textBoxTemplateBcc";
            this.textBoxTemplateBcc.Size = new System.Drawing.Size(1108, 22);
            this.textBoxTemplateBcc.TabIndex = 3;
            this.textBoxTemplateBcc.Leave += new System.EventHandler(this.textBoxTemplateBcc_Leave);
            // 
            // labelTemplateCc
            // 
            this.labelTemplateCc.AutoSize = true;
            this.labelTemplateCc.Location = new System.Drawing.Point(12, 82);
            this.labelTemplateCc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTemplateCc.Name = "labelTemplateCc";
            this.labelTemplateCc.Size = new System.Drawing.Size(28, 17);
            this.labelTemplateCc.TabIndex = 7;
            this.labelTemplateCc.Text = "Cc:";
            // 
            // textBoxTemplateSubject
            // 
            this.textBoxTemplateSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTemplateSubject.Location = new System.Drawing.Point(85, 143);
            this.textBoxTemplateSubject.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxTemplateSubject.Name = "textBoxTemplateSubject";
            this.textBoxTemplateSubject.Size = new System.Drawing.Size(1108, 22);
            this.textBoxTemplateSubject.TabIndex = 4;
            this.textBoxTemplateSubject.Leave += new System.EventHandler(this.textBoxTemplateSubject_Leave);
            // 
            // labelTemplateTo
            // 
            this.labelTemplateTo.AutoSize = true;
            this.labelTemplateTo.Location = new System.Drawing.Point(12, 50);
            this.labelTemplateTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTemplateTo.Name = "labelTemplateTo";
            this.labelTemplateTo.Size = new System.Drawing.Size(29, 17);
            this.labelTemplateTo.TabIndex = 6;
            this.labelTemplateTo.Text = "To:";
            // 
            // labelTemplateFrom
            // 
            this.labelTemplateFrom.AutoSize = true;
            this.labelTemplateFrom.Location = new System.Drawing.Point(12, 18);
            this.labelTemplateFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTemplateFrom.Name = "labelTemplateFrom";
            this.labelTemplateFrom.Size = new System.Drawing.Size(44, 17);
            this.labelTemplateFrom.TabIndex = 5;
            this.labelTemplateFrom.Text = "From:";
            // 
            // checkBoxReloadOnSave
            // 
            this.checkBoxReloadOnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxReloadOnSave.AutoSize = true;
            this.checkBoxReloadOnSave.Location = new System.Drawing.Point(899, 879);
            this.checkBoxReloadOnSave.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxReloadOnSave.Name = "checkBoxReloadOnSave";
            this.checkBoxReloadOnSave.Size = new System.Drawing.Size(279, 21);
            this.checkBoxReloadOnSave.TabIndex = 5;
            this.checkBoxReloadOnSave.Text = "Force service to reload profiles on save";
            this.checkBoxReloadOnSave.UseVisualStyleBackColor = true;
            // 
            // ErrorBatchFieldNameColumn
            // 
            this.ErrorBatchFieldNameColumn.DataPropertyName = "Name";
            this.ErrorBatchFieldNameColumn.FillWeight = 60F;
            this.ErrorBatchFieldNameColumn.HeaderText = "Error Batch Field Name";
            this.ErrorBatchFieldNameColumn.Name = "ErrorBatchFieldNameColumn";
            // 
            // ErrorBatchFieldValueColumn
            // 
            this.ErrorBatchFieldValueColumn.DataPropertyName = "Value";
            this.ErrorBatchFieldValueColumn.FillWeight = 60F;
            this.ErrorBatchFieldValueColumn.HeaderText = "Error Batch Field Value";
            this.ErrorBatchFieldValueColumn.Name = "ErrorBatchFieldValueColumn";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 911);
            this.Controls.Add(this.checkBoxReloadOnSave);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControlConfig);
            this.Controls.Add(this.listViewMailboxes);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1327, 838);
            this.Name = "MainForm";
            this.Text = "Email Import - Configuration";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuMailboxes.ResumeLayout(false);
            this.contextMenuDataGrid.ResumeLayout(false);
            this.tabPageFileTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFileTypes)).EndInit();
            this.tabPageIndexFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIndexFields)).EndInit();
            this.tabPageBatchFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBatchFields)).EndInit();
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.groupBoxKofax.ResumeLayout(false);
            this.groupBoxKofax.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArchiveRetention)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStorageRetention)).EndInit();
            this.groupBoxGeneral.ResumeLayout(false);
            this.groupBoxGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPriority)).EndInit();
            this.groupBoxConversion.ResumeLayout(false);
            this.groupBoxConversion.PerformLayout();
            this.groupBoxCollection.ResumeLayout(false);
            this.groupBoxCollection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImapRetention)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImapPort)).EndInit();
            this.tabControlConfig.ResumeLayout(false);
            this.tabPageSchedule.ResumeLayout(false);
            this.tabPageSchedule.PerformLayout();
            this.groupBoxSchedule.ResumeLayout(false);
            this.groupBoxSchedule.PerformLayout();
            this.tabPageScript.ResumeLayout(false);
            this.tabPageScript.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxScript)).EndInit();
            this.toolStripScript.ResumeLayout(false);
            this.toolStripScript.PerformLayout();
            this.tabPageErrorHandling.ResumeLayout(false);
            this.groupBoxErrorBatchFields.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewErrorBatchFields)).EndInit();
            this.groupBoxConversionErrorBatch.ResumeLayout(false);
            this.groupBoxConversionErrorBatch.PerformLayout();
            this.groupBoxErrorGeneral.ResumeLayout(false);
            this.groupBoxErrorGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxRetries)).EndInit();
            this.tabPageTemplateDesigner.ResumeLayout(false);
            this.tabPageTemplateDesigner.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewMailboxes;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ContextMenuStrip contextMenuDataGrid;
        private System.Windows.Forms.ToolStripMenuItem batchFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentIsBodyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentExtensionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentAttachmentFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentFailureReasonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromDisplayNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toDisplayNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ccDisplayNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ccAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bodyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateSentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateReceivedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem isSignedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem messageIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensitivityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem attachmentFileNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem headerHeaderNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFromToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageFileTypes;
        private System.Windows.Forms.DataGridView dataGridViewFileTypes;
        private System.Windows.Forms.TabPage tabPageIndexFields;
        private System.Windows.Forms.DataGridView dataGridViewIndexFields;
        private System.Windows.Forms.TabPage tabPageBatchFields;
        private System.Windows.Forms.DataGridView dataGridViewBatchFields;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabControl tabControlConfig;
        private System.Windows.Forms.ToolStripMenuItem textBodyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem htmlBodyToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuMailboxes;
        private System.Windows.Forms.ToolStripMenuItem reloadAllToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxConversion;
        private System.Windows.Forms.GroupBox groupBoxCollection;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private System.Windows.Forms.NumericUpDown numericUpDownImapPort;
        private System.Windows.Forms.Label labelImapPort;
        private System.Windows.Forms.Label labelImapHost;
        private System.Windows.Forms.TextBox textBoxImapHost;
        private System.Windows.Forms.TextBox textBoxImapPassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxImapUserName;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Button buttonStoragePath;
        private System.Windows.Forms.Button buttonImapQuery;
        private System.Windows.Forms.TextBox textBoxStoragePath;
        private System.Windows.Forms.TextBox textBoxImapQuery;
        private System.Windows.Forms.TextBox textBoxImapFolder;
        private System.Windows.Forms.NumericUpDown numericUpDownImapRetention;
        private System.Windows.Forms.Label labelStoragePath;
        private System.Windows.Forms.Label labelImapRetention;
        private System.Windows.Forms.Label labelImapQuery;
        private System.Windows.Forms.Label labelImapFolder;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.ComboBox comboBoxGroup;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Button buttonArchivePath;
        private System.Windows.Forms.TextBox textBoxArchivePath;
        private System.Windows.Forms.Button buttonOutputPath;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.Label labelArchivePath;
        private System.Windows.Forms.Label labelOutputPath;
        private System.Windows.Forms.CheckBox checkBoxZip;
        private System.Windows.Forms.NumericUpDown numericUpDownPriority;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.CheckBox checkBoxSingleDoc;
        private System.Windows.Forms.CheckBox checkBoxAutoSeparation;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxBitDepth;
        private System.Windows.Forms.Label labelBatchStyle;
        private System.Windows.Forms.Label labelBodyConv;
        private System.Windows.Forms.Label labelAttachmentConv;
        private System.Windows.Forms.ComboBox comboBoxBodyConversion;
        private System.Windows.Forms.ComboBox comboBoxAttachmentConversion;
        private System.Windows.Forms.ComboBox comboBoxBatchStyle;
        private System.Windows.Forms.CheckBox checkBoxRemoveBlanks;
        private System.Windows.Forms.Label labelBatchClass;
        private System.Windows.Forms.TextBox textBoxBatchNumberFormat;
        private System.Windows.Forms.TextBox textBoxFormType;
        private System.Windows.Forms.Label labelFormType;
        private System.Windows.Forms.TextBox textBoxFolderClass;
        private System.Windows.Forms.Label labelFolderClass;
        private System.Windows.Forms.TextBox textBoxBatchClass;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageSchedule;
        private System.Windows.Forms.CheckBox checkBoxEnableScheduler;
        private System.Windows.Forms.GroupBox groupBoxSchedule;
        private System.Windows.Forms.CheckBox checkBoxWeekdaysOnly;
        private System.Windows.Forms.Label labelTimeSlot;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchFieldNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchFieldValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchFieldRegexColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexFieldNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexFieldValueColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndexFieldRegexColumn;
        private System.Windows.Forms.Panel panelSchedule;
        private System.Windows.Forms.GroupBox groupBoxGeneral;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.Label labelBatchNumberFormat;
        private System.Windows.Forms.GroupBox groupBoxKofax;
        private System.Windows.Forms.TabPage tabPageErrorHandling;
        private System.Windows.Forms.NumericUpDown numericUpDownMaxRetries;
        private System.Windows.Forms.Label labelMaxRetries;
        private System.Windows.Forms.Label labelTimeBetweenRetries;
        private System.Windows.Forms.ComboBox comboBoxResolution;
        private System.Windows.Forms.Label labelNativeFiles;
        private System.Windows.Forms.ComboBox comboBoxNativeFiles;
        private System.Windows.Forms.GroupBox groupBoxErrorGeneral;
        private System.Windows.Forms.TextBox textBoxTemplateSubject;
        private System.Windows.Forms.TextBox textBoxTemplateBcc;
        private System.Windows.Forms.TextBox textBoxTemplateCc;
        private System.Windows.Forms.TextBox textBoxTemplateTo;
        private System.Windows.Forms.TextBox textBoxTemplateFrom;
        private System.Windows.Forms.Label labelTemplateFrom;
        private System.Windows.Forms.Label labelTemplateSubject;
        private System.Windows.Forms.Label labelTemplateBcc;
        private System.Windows.Forms.Label labelTemplateCc;
        private System.Windows.Forms.Label labelTemplateTo;
        private System.Windows.Forms.TextBox textBoxTimeBetweenRetries;
        private MSDN.Html.Editor.HtmlEditorControl htmlEditorTemplateBody;
        private System.Windows.Forms.LinkLabel linkLabelTemplateEditHtml;
        private System.Windows.Forms.LinkLabel linkLabelTemplateDefault;
        private System.Windows.Forms.Button buttonImapFolder;
        private System.Windows.Forms.RadioButton radioButtonForwardAttachment;
        private System.Windows.Forms.RadioButton radioButtonForwardInline;
        private System.Windows.Forms.CheckBox checkBoxReloadOnSave;
        private System.Windows.Forms.TabPage tabPageTemplateDesigner;
        private System.Windows.Forms.LinkLabel linkLabelClear;
        private System.Windows.Forms.ToolStripMenuItem fromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ccToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxBinarisationAlgorithm;
        private System.Windows.Forms.Label labelBinarisationAlgorithm;
        private System.Windows.Forms.Label labelDocumentClass;
        private System.Windows.Forms.TextBox textBoxDocumentClass;
        private System.Windows.Forms.Label labelErrorUnprocessable;
        private System.Windows.Forms.Label labelErrorUnknown;
        private System.Windows.Forms.Label labelErrorUnsupported;
        private System.Windows.Forms.ComboBox comboBoxErrorUnprocessable;
        private System.Windows.Forms.ComboBox comboBoxErrorUnknown;
        private System.Windows.Forms.ComboBox comboBoxErrorUnsupported;
        private System.Windows.Forms.TextBox textBoxEscalationEmail;
        private System.Windows.Forms.Label labelEscalationEmail;
        private System.Windows.Forms.ToolStripMenuItem replyToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replyToDisplayNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replyToAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchCreationDateTimeToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericUpDownStorageRetention;
        private System.Windows.Forms.Label labelStorageRetention;
        private System.Windows.Forms.TabPage tabPageScript;
        private System.Windows.Forms.ToolStrip toolStripScript;
        private FastColoredTextBoxNS.FastColoredTextBox textBoxScript;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreateNewScript;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonTestScript;
        private System.Windows.Forms.ToolStripMenuItem selectMailMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableWindowsFormsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSnippets;
        private System.Windows.Forms.ToolStripMenuItem senderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem senderDisplayNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem senderAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxRemoveFaxHeader;
        private System.Windows.Forms.NumericUpDown numericUpDownArchiveRetention;
        private System.Windows.Forms.Label labelArchiveRetention;
        private System.Windows.Forms.TextBox textBoxOutputFolderFormat;
        private System.Windows.Forms.Label labelOutputFolderFormat;
        private System.Windows.Forms.CheckBox checkBoxBatchClassSubFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilenameRegexColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn ProcessAsFileTypeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IgnoreFileTypeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PassthroughFileTypeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AutoDeskewFileTypeColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AutoRotateFileTypeColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn BitDepthFileTypeColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn BinarisationAlgorithmFileTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinPixelsFileTypeColumn;
        private System.Windows.Forms.CheckBox checkBoxLiteViewerEnabled;
        private System.Windows.Forms.ComboBox comboBoxBodyPosition;
        private System.Windows.Forms.Label labelBodyPos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboPdfConversion;
        private System.Windows.Forms.CheckBox checkBoxProcessErrorBatch;
        private System.Windows.Forms.Label labelProcessErrorBatch;
        private System.Windows.Forms.Label labelErrorPath;
        private System.Windows.Forms.Button btnErrorOutputPath;
        private System.Windows.Forms.TextBox textBoxErrorOutputPath;
        private System.Windows.Forms.GroupBox groupBoxErrorBatchFields;
        private System.Windows.Forms.DataGridView dataGridViewErrorBatchFields;
        private System.Windows.Forms.GroupBox groupBoxConversionErrorBatch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorBatchFieldNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ErrorBatchFieldValueColumn;
    }
}

