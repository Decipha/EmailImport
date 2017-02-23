namespace EmailImport.Viewer
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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButtonFilterBy = new System.Windows.Forms.ToolStripDropDownButton();
            this.dateSentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateReceivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inProgressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.completeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoredToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rejectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.escalatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messageIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxMaximumEmails = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFindEMailID = new System.Windows.Forms.ToolStripButton();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.listViewProfiles = new System.Windows.Forms.ListView();
            this.splitContainerDisplay = new System.Windows.Forms.SplitContainer();
            this.labelLiteViewError = new System.Windows.Forms.Label();
            this.listViewEmails = new System.Windows.Forms.ListView();
            this.columnHeaderFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSubject = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDateSent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDateReceived = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderProcessedCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderStartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderEndTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderErrors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuEmails = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprocessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewAttachments = new System.Windows.Forms.ListView();
            this.contextMenuAttachments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveAttachmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAttachmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.labelSubject = new System.Windows.Forms.Label();
            this.labelCC = new System.Windows.Forms.Label();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentFilterToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDisplay)).BeginInit();
            this.splitContainerDisplay.Panel1.SuspendLayout();
            this.splitContainerDisplay.Panel2.SuspendLayout();
            this.splitContainerDisplay.SuspendLayout();
            this.contextMenuEmails.SuspendLayout();
            this.contextMenuAttachments.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonFilterBy,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripTextBoxMaximumEmails,
            this.toolStripSeparator2,
            this.toolStripButtonRefresh,
            this.toolStripButtonFindEMailID});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1075, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripDropDownButtonFilterBy
            // 
            this.toolStripDropDownButtonFilterBy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateSentToolStripMenuItem,
            this.dateReceivedToolStripMenuItem,
            this.statusToolStripMenuItem,
            this.batchNumberToolStripMenuItem,
            this.fromToolStripMenuItem,
            this.subjectToolStripMenuItem,
            this.messageIDToolStripMenuItem,
            this.emailIDToolStripMenuItem});
            this.toolStripDropDownButtonFilterBy.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonFilterBy.Image")));
            this.toolStripDropDownButtonFilterBy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonFilterBy.Name = "toolStripDropDownButtonFilterBy";
            this.toolStripDropDownButtonFilterBy.Size = new System.Drawing.Size(75, 22);
            this.toolStripDropDownButtonFilterBy.Text = "Filter By";
            // 
            // dateSentToolStripMenuItem
            // 
            this.dateSentToolStripMenuItem.Name = "dateSentToolStripMenuItem";
            this.dateSentToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dateSentToolStripMenuItem.Text = "Date Sent";
            this.dateSentToolStripMenuItem.Click += new System.EventHandler(this.dateSentToolStripMenuItem_Click);
            // 
            // dateReceivedToolStripMenuItem
            // 
            this.dateReceivedToolStripMenuItem.Name = "dateReceivedToolStripMenuItem";
            this.dateReceivedToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.dateReceivedToolStripMenuItem.Text = "Date Received";
            this.dateReceivedToolStripMenuItem.Click += new System.EventHandler(this.dateReceivedToolStripMenuItem_Click);
            // 
            // statusToolStripMenuItem
            // 
            this.statusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem,
            this.inProgressToolStripMenuItem,
            this.completeToolStripMenuItem,
            this.errorToolStripMenuItem,
            this.emptyToolStripMenuItem,
            this.ignoredToolStripMenuItem,
            this.rejectedToolStripMenuItem,
            this.escalatedToolStripMenuItem,
            this.purgedToolStripMenuItem});
            this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
            this.statusToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.statusToolStripMenuItem.Text = "Status";
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.noneToolStripMenuItem.Text = "<None>";
            this.noneToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // inProgressToolStripMenuItem
            // 
            this.inProgressToolStripMenuItem.Name = "inProgressToolStripMenuItem";
            this.inProgressToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.inProgressToolStripMenuItem.Tag = "InProgress";
            this.inProgressToolStripMenuItem.Text = "In Progress";
            this.inProgressToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // completeToolStripMenuItem
            // 
            this.completeToolStripMenuItem.Name = "completeToolStripMenuItem";
            this.completeToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.completeToolStripMenuItem.Tag = "Complete";
            this.completeToolStripMenuItem.Text = "Complete";
            this.completeToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // errorToolStripMenuItem
            // 
            this.errorToolStripMenuItem.Name = "errorToolStripMenuItem";
            this.errorToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.errorToolStripMenuItem.Tag = "Error";
            this.errorToolStripMenuItem.Text = "Error";
            this.errorToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // emptyToolStripMenuItem
            // 
            this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
            this.emptyToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.emptyToolStripMenuItem.Tag = "Empty";
            this.emptyToolStripMenuItem.Text = "Empty";
            this.emptyToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // ignoredToolStripMenuItem
            // 
            this.ignoredToolStripMenuItem.Name = "ignoredToolStripMenuItem";
            this.ignoredToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.ignoredToolStripMenuItem.Tag = "Ignored";
            this.ignoredToolStripMenuItem.Text = "Ignored";
            this.ignoredToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // rejectedToolStripMenuItem
            // 
            this.rejectedToolStripMenuItem.Name = "rejectedToolStripMenuItem";
            this.rejectedToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.rejectedToolStripMenuItem.Tag = "Rejected";
            this.rejectedToolStripMenuItem.Text = "Rejected";
            this.rejectedToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // escalatedToolStripMenuItem
            // 
            this.escalatedToolStripMenuItem.Name = "escalatedToolStripMenuItem";
            this.escalatedToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.escalatedToolStripMenuItem.Tag = "Escalated";
            this.escalatedToolStripMenuItem.Text = "Escalated";
            this.escalatedToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // purgedToolStripMenuItem
            // 
            this.purgedToolStripMenuItem.Name = "purgedToolStripMenuItem";
            this.purgedToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.purgedToolStripMenuItem.Tag = "Purged";
            this.purgedToolStripMenuItem.Text = "Purged";
            this.purgedToolStripMenuItem.Click += new System.EventHandler(this.filterByStatusToolStripMenuItem_Click);
            // 
            // batchNumberToolStripMenuItem
            // 
            this.batchNumberToolStripMenuItem.Name = "batchNumberToolStripMenuItem";
            this.batchNumberToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.batchNumberToolStripMenuItem.Text = "Batch Number";
            this.batchNumberToolStripMenuItem.Click += new System.EventHandler(this.batchNumberToolStripMenuItem_Click);
            // 
            // fromToolStripMenuItem
            // 
            this.fromToolStripMenuItem.Name = "fromToolStripMenuItem";
            this.fromToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.fromToolStripMenuItem.Text = "From";
            this.fromToolStripMenuItem.Click += new System.EventHandler(this.fromToolStripMenuItem_Click);
            // 
            // subjectToolStripMenuItem
            // 
            this.subjectToolStripMenuItem.Name = "subjectToolStripMenuItem";
            this.subjectToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.subjectToolStripMenuItem.Text = "Subject";
            this.subjectToolStripMenuItem.Click += new System.EventHandler(this.subjectToolStripMenuItem_Click);
            // 
            // messageIDToolStripMenuItem
            // 
            this.messageIDToolStripMenuItem.Name = "messageIDToolStripMenuItem";
            this.messageIDToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.messageIDToolStripMenuItem.Text = "Message ID";
            this.messageIDToolStripMenuItem.Click += new System.EventHandler(this.messageIDToolStripMenuItem_Click);
            // 
            // emailIDToolStripMenuItem
            // 
            this.emailIDToolStripMenuItem.Name = "emailIDToolStripMenuItem";
            this.emailIDToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.emailIDToolStripMenuItem.Text = "Email ID";
            this.emailIDToolStripMenuItem.Click += new System.EventHandler(this.emailIDToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(111, 22);
            this.toolStripLabel1.Text = "Maximum # of Emails:";
            // 
            // toolStripTextBoxMaximumEmails
            // 
            this.toolStripTextBoxMaximumEmails.Name = "toolStripTextBoxMaximumEmails";
            this.toolStripTextBoxMaximumEmails.Size = new System.Drawing.Size(60, 25);
            this.toolStripTextBoxMaximumEmails.Text = "500";
            this.toolStripTextBoxMaximumEmails.Leave += new System.EventHandler(this.toolStripTextBoxMaximumEmails_Leave);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(49, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            this.toolStripButtonRefresh.Click += new System.EventHandler(this.toolStripButtonRefresh_Click);
            // 
            // toolStripButtonFindEMailID
            // 
            this.toolStripButtonFindEMailID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonFindEMailID.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonFindEMailID.Image")));
            this.toolStripButtonFindEMailID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFindEMailID.Name = "toolStripButtonFindEMailID";
            this.toolStripButtonFindEMailID.Size = new System.Drawing.Size(127, 22);
            this.toolStripButtonFindEMailID.Text = "Find Email using Email ID";
            this.toolStripButtonFindEMailID.Click += new System.EventHandler(this.toolStripButtonFindEMailID_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(0, 25);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.listViewProfiles);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerDisplay);
            this.splitContainerMain.Size = new System.Drawing.Size(1075, 665);
            this.splitContainerMain.SplitterDistance = 181;
            this.splitContainerMain.TabIndex = 1;
            // 
            // listViewProfiles
            // 
            this.listViewProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewProfiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewProfiles.HideSelection = false;
            this.listViewProfiles.Location = new System.Drawing.Point(0, 0);
            this.listViewProfiles.Name = "listViewProfiles";
            this.listViewProfiles.Size = new System.Drawing.Size(181, 665);
            this.listViewProfiles.TabIndex = 0;
            this.listViewProfiles.UseCompatibleStateImageBehavior = false;
            this.listViewProfiles.View = System.Windows.Forms.View.Details;
            this.listViewProfiles.SelectedIndexChanged += new System.EventHandler(this.listViewProfiles_SelectedIndexChanged);
            // 
            // splitContainerDisplay
            // 
            this.splitContainerDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerDisplay.Location = new System.Drawing.Point(0, 0);
            this.splitContainerDisplay.Name = "splitContainerDisplay";
            this.splitContainerDisplay.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerDisplay.Panel1
            // 
            this.splitContainerDisplay.Panel1.Controls.Add(this.labelLiteViewError);
            this.splitContainerDisplay.Panel1.Controls.Add(this.listViewEmails);
            // 
            // splitContainerDisplay.Panel2
            // 
            this.splitContainerDisplay.Panel2.Controls.Add(this.listViewAttachments);
            this.splitContainerDisplay.Panel2.Controls.Add(this.label5);
            this.splitContainerDisplay.Panel2.Controls.Add(this.labelSubject);
            this.splitContainerDisplay.Panel2.Controls.Add(this.labelCC);
            this.splitContainerDisplay.Panel2.Controls.Add(this.labelTo);
            this.splitContainerDisplay.Panel2.Controls.Add(this.labelFrom);
            this.splitContainerDisplay.Panel2.Controls.Add(this.label4);
            this.splitContainerDisplay.Panel2.Controls.Add(this.label3);
            this.splitContainerDisplay.Panel2.Controls.Add(this.label2);
            this.splitContainerDisplay.Panel2.Controls.Add(this.label1);
            this.splitContainerDisplay.Panel2.Controls.Add(this.webBrowser);
            this.splitContainerDisplay.Size = new System.Drawing.Size(890, 665);
            this.splitContainerDisplay.SplitterDistance = 176;
            this.splitContainerDisplay.TabIndex = 0;
            // 
            // labelLiteViewError
            // 
            this.labelLiteViewError.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLiteViewError.AutoSize = true;
            this.labelLiteViewError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLiteViewError.ForeColor = System.Drawing.Color.Red;
            this.labelLiteViewError.Location = new System.Drawing.Point(78, 78);
            this.labelLiteViewError.Name = "labelLiteViewError";
            this.labelLiteViewError.Size = new System.Drawing.Size(199, 20);
            this.labelLiteViewError.TabIndex = 2;
            this.labelLiteViewError.Text = "LiteView Error Message";
            this.labelLiteViewError.Visible = false;
            // 
            // listViewEmails
            // 
            this.listViewEmails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderFrom,
            this.columnHeaderSubject,
            this.columnHeaderDateSent,
            this.columnHeaderDateReceived,
            this.columnHeaderProcessedCount,
            this.columnHeaderStatus,
            this.columnHeaderStartTime,
            this.columnHeaderEndTime,
            this.columnHeaderErrors});
            this.listViewEmails.ContextMenuStrip = this.contextMenuEmails;
            this.listViewEmails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewEmails.FullRowSelect = true;
            this.listViewEmails.HideSelection = false;
            this.listViewEmails.Location = new System.Drawing.Point(0, 0);
            this.listViewEmails.Name = "listViewEmails";
            this.listViewEmails.Size = new System.Drawing.Size(890, 176);
            this.listViewEmails.TabIndex = 0;
            this.listViewEmails.UseCompatibleStateImageBehavior = false;
            this.listViewEmails.View = System.Windows.Forms.View.Details;
            this.listViewEmails.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewEmails_ColumnClick);
            this.listViewEmails.SelectedIndexChanged += new System.EventHandler(this.listViewEmails_SelectedIndexChanged);
            this.listViewEmails.SizeChanged += new System.EventHandler(this.listViewEmails_SizeChanged);
            this.listViewEmails.DoubleClick += new System.EventHandler(this.listViewEmails_DoubleClick);
            this.listViewEmails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewEmails_KeyDown);
            // 
            // columnHeaderFrom
            // 
            this.columnHeaderFrom.Tag = "2";
            this.columnHeaderFrom.Text = "From";
            // 
            // columnHeaderSubject
            // 
            this.columnHeaderSubject.Tag = "4";
            this.columnHeaderSubject.Text = "Subject";
            // 
            // columnHeaderDateSent
            // 
            this.columnHeaderDateSent.Tag = "2";
            this.columnHeaderDateSent.Text = "Date Sent";
            // 
            // columnHeaderDateReceived
            // 
            this.columnHeaderDateReceived.Tag = "2";
            this.columnHeaderDateReceived.Text = "Date Received";
            // 
            // columnHeaderProcessedCount
            // 
            this.columnHeaderProcessedCount.Tag = "1";
            this.columnHeaderProcessedCount.Text = "Processed Count";
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Tag = "1";
            this.columnHeaderStatus.Text = "Status";
            // 
            // columnHeaderStartTime
            // 
            this.columnHeaderStartTime.Tag = "2";
            this.columnHeaderStartTime.Text = "Start Time";
            // 
            // columnHeaderEndTime
            // 
            this.columnHeaderEndTime.Tag = "2";
            this.columnHeaderEndTime.Text = "End Time";
            // 
            // columnHeaderErrors
            // 
            this.columnHeaderErrors.Tag = "1";
            this.columnHeaderErrors.Text = "Errors";
            // 
            // contextMenuEmails
            // 
            this.contextMenuEmails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveEmailToolStripMenuItem,
            this.forwardToolStripMenuItem,
            this.reprocessToolStripMenuItem});
            this.contextMenuEmails.Name = "contextMenuEmails";
            this.contextMenuEmails.Size = new System.Drawing.Size(127, 70);
            this.contextMenuEmails.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuEmails_Opening);
            // 
            // saveEmailToolStripMenuItem
            // 
            this.saveEmailToolStripMenuItem.Name = "saveEmailToolStripMenuItem";
            this.saveEmailToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.saveEmailToolStripMenuItem.Text = "Save...";
            this.saveEmailToolStripMenuItem.Click += new System.EventHandler(this.saveEmailToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem
            // 
            this.forwardToolStripMenuItem.Name = "forwardToolStripMenuItem";
            this.forwardToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.forwardToolStripMenuItem.Text = "Forward...";
            this.forwardToolStripMenuItem.Click += new System.EventHandler(this.forwardToolStripMenuItem_Click);
            // 
            // reprocessToolStripMenuItem
            // 
            this.reprocessToolStripMenuItem.Name = "reprocessToolStripMenuItem";
            this.reprocessToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.reprocessToolStripMenuItem.Text = "Reprocess";
            this.reprocessToolStripMenuItem.Click += new System.EventHandler(this.reprocessToolStripMenuItem_Click);
            // 
            // listViewAttachments
            // 
            this.listViewAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewAttachments.ContextMenuStrip = this.contextMenuAttachments;
            this.listViewAttachments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewAttachments.Location = new System.Drawing.Point(478, 22);
            this.listViewAttachments.Name = "listViewAttachments";
            this.listViewAttachments.Size = new System.Drawing.Size(400, 71);
            this.listViewAttachments.SmallImageList = this.imageListIcons;
            this.listViewAttachments.TabIndex = 11;
            this.listViewAttachments.UseCompatibleStateImageBehavior = false;
            this.listViewAttachments.View = System.Windows.Forms.View.Details;
            this.listViewAttachments.DoubleClick += new System.EventHandler(this.listViewAttachments_DoubleClick);
            // 
            // contextMenuAttachments
            // 
            this.contextMenuAttachments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAttachmentToolStripMenuItem,
            this.saveAttachmentsToolStripMenuItem});
            this.contextMenuAttachments.Name = "contextMenuAttachments";
            this.contextMenuAttachments.Size = new System.Drawing.Size(125, 48);
            this.contextMenuAttachments.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuAttachments_Opening);
            // 
            // saveAttachmentToolStripMenuItem
            // 
            this.saveAttachmentToolStripMenuItem.Name = "saveAttachmentToolStripMenuItem";
            this.saveAttachmentToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveAttachmentToolStripMenuItem.Text = "Save...";
            this.saveAttachmentToolStripMenuItem.Click += new System.EventHandler(this.saveAttachmentToolStripMenuItem_Click);
            // 
            // saveAttachmentsToolStripMenuItem
            // 
            this.saveAttachmentsToolStripMenuItem.Name = "saveAttachmentsToolStripMenuItem";
            this.saveAttachmentsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveAttachmentsToolStripMenuItem.Text = "Save All...";
            this.saveAttachmentsToolStripMenuItem.Click += new System.EventHandler(this.saveAttachmentsToolStripMenuItem_Click);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(475, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Attachments:";
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(55, 75);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(19, 13);
            this.labelSubject.TabIndex = 8;
            this.labelSubject.Text = "<>";
            // 
            // labelCC
            // 
            this.labelCC.AutoSize = true;
            this.labelCC.Location = new System.Drawing.Point(55, 53);
            this.labelCC.Name = "labelCC";
            this.labelCC.Size = new System.Drawing.Size(19, 13);
            this.labelCC.TabIndex = 7;
            this.labelCC.Text = "<>";
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(55, 31);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(19, 13);
            this.labelTo.TabIndex = 6;
            this.labelTo.Text = "<>";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(55, 9);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(19, 13);
            this.labelFrom.TabIndex = 5;
            this.labelFrom.Text = "<>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Subject:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cc:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "From:";
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(0, 99);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(890, 386);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.currentFilterToolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 691);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1075, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(75, 17);
            this.toolStripStatusLabel1.Text = "Current Filter:";
            // 
            // currentFilterToolStripStatusLabel
            // 
            this.currentFilterToolStripStatusLabel.Name = "currentFilterToolStripStatusLabel";
            this.currentFilterToolStripStatusLabel.Size = new System.Drawing.Size(48, 17);
            this.currentFilterToolStripStatusLabel.Text = "<None>";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 713);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStrip);
            this.Name = "MainForm";
            this.Text = "Email Import - Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerDisplay.Panel1.ResumeLayout(false);
            this.splitContainerDisplay.Panel1.PerformLayout();
            this.splitContainerDisplay.Panel2.ResumeLayout(false);
            this.splitContainerDisplay.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerDisplay)).EndInit();
            this.splitContainerDisplay.ResumeLayout(false);
            this.contextMenuEmails.ResumeLayout(false);
            this.contextMenuAttachments.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ListView listViewProfiles;
        private System.Windows.Forms.SplitContainer splitContainerDisplay;
        private System.Windows.Forms.ListView listViewEmails;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ColumnHeader columnHeaderFrom;
        private System.Windows.Forms.ColumnHeader columnHeaderSubject;
        private System.Windows.Forms.ColumnHeader columnHeaderDateSent;
        private System.Windows.Forms.ColumnHeader columnHeaderProcessedCount;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderStartTime;
        private System.Windows.Forms.ColumnHeader columnHeaderEndTime;
        private System.Windows.Forms.ColumnHeader columnHeaderErrors;
        private System.Windows.Forms.ContextMenuStrip contextMenuEmails;
        private System.Windows.Forms.ContextMenuStrip contextMenuAttachments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label labelCC;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listViewAttachments;
        private System.Windows.Forms.ImageList imageListIcons;
        private System.Windows.Forms.ToolStripMenuItem saveEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAttachmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAttachmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonFilterBy;
        private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inProgressToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem completeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoredToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rejectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem escalatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem purgedToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMaximumEmails;
        private System.Windows.Forms.ToolStripMenuItem dateSentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem messageIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel currentFilterToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reprocessToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.ColumnHeader columnHeaderDateReceived;
        private System.Windows.Forms.ToolStripMenuItem dateReceivedToolStripMenuItem;
        private System.Windows.Forms.Label labelLiteViewError;
        private System.Windows.Forms.ToolStripButton toolStripButtonFindEMailID;
    }
}

