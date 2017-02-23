namespace MailboxClient
{
    partial class MailboxClient
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
            this.comboBoxMailbox = new System.Windows.Forms.ComboBox();
            this.comboBoxFolder = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.listViewMessages = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.tabControlSearch = new System.Windows.Forms.TabControl();
            this.tabPageCriteria = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxBody = new System.Windows.Forms.TextBox();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.textBoxSender = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabelDate = new System.Windows.Forms.LinkLabel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBoxMessageID = new System.Windows.Forms.GroupBox();
            this.textBoxMessageID = new System.Windows.Forms.TextBox();
            this.tabPageIMAP = new System.Windows.Forms.TabPage();
            this.textBoxIMAP = new System.Windows.Forms.TextBox();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.buttonClearCriteria = new System.Windows.Forms.Button();
            this.groupBoxConn = new System.Windows.Forms.GroupBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.buttonDeDuplicate = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.textBoxEmailAddress = new System.Windows.Forms.TextBox();
            this.buttonMove = new System.Windows.Forms.Button();
            this.comboBoxMove = new System.Windows.Forms.ComboBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonDeselectAll = new System.Windows.Forms.Button();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.groupBoxService = new System.Windows.Forms.GroupBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.checkBoxExhaustive = new System.Windows.Forms.CheckBox();
            this.tabControlSearch.SuspendLayout();
            this.tabPageCriteria.SuspendLayout();
            this.groupBoxOther.SuspendLayout();
            this.groupBoxMessageID.SuspendLayout();
            this.tabPageIMAP.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.groupBoxConn.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            this.groupBoxService.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxMailbox
            // 
            this.comboBoxMailbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMailbox.FormattingEnabled = true;
            this.comboBoxMailbox.Location = new System.Drawing.Point(62, 23);
            this.comboBoxMailbox.Name = "comboBoxMailbox";
            this.comboBoxMailbox.Size = new System.Drawing.Size(196, 21);
            this.comboBoxMailbox.TabIndex = 1;
            this.comboBoxMailbox.SelectedIndexChanged += new System.EventHandler(this.comboBoxMailbox_SelectedIndexChanged);
            // 
            // comboBoxFolder
            // 
            this.comboBoxFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFolder.Enabled = false;
            this.comboBoxFolder.FormattingEnabled = true;
            this.comboBoxFolder.Location = new System.Drawing.Point(320, 23);
            this.comboBoxFolder.Name = "comboBoxFolder";
            this.comboBoxFolder.Size = new System.Drawing.Size(250, 21);
            this.comboBoxFolder.TabIndex = 2;
            this.comboBoxFolder.SelectedIndexChanged += new System.EventHandler(this.comboBoxFolder_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(275, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Folder:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Mailbox:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(915, 12);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 11;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // listViewMessages
            // 
            this.listViewMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMessages.CheckBoxes = true;
            this.listViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.listViewMessages.FullRowSelect = true;
            this.listViewMessages.HideSelection = false;
            this.listViewMessages.Location = new System.Drawing.Point(6, 48);
            this.listViewMessages.MultiSelect = false;
            this.listViewMessages.Name = "listViewMessages";
            this.listViewMessages.Size = new System.Drawing.Size(1038, 261);
            this.listViewMessages.TabIndex = 12;
            this.listViewMessages.UseCompatibleStateImageBehavior = false;
            this.listViewMessages.View = System.Windows.Forms.View.Details;
            this.listViewMessages.DoubleClick += new System.EventHandler(this.listViewMessages_DoubleClick);
            this.listViewMessages.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewMessages_ItemCheck);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "";
            this.columnHeader2.Width = 30;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "From";
            this.columnHeader1.Width = 35;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Subject";
            this.columnHeader4.Width = 48;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Sent";
            this.columnHeader5.Width = 34;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Received";
            this.columnHeader6.Width = 58;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Size";
            this.columnHeader7.Width = 32;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Has Attachments";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 95;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "UID";
            this.columnHeader9.Width = 31;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Message ID";
            this.columnHeader10.Width = 395;
            // 
            // tabControlSearch
            // 
            this.tabControlSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSearch.Controls.Add(this.tabPageCriteria);
            this.tabControlSearch.Controls.Add(this.tabPageIMAP);
            this.tabControlSearch.Location = new System.Drawing.Point(6, 19);
            this.tabControlSearch.Name = "tabControlSearch";
            this.tabControlSearch.SelectedIndex = 0;
            this.tabControlSearch.Size = new System.Drawing.Size(986, 165);
            this.tabControlSearch.TabIndex = 13;
            // 
            // tabPageCriteria
            // 
            this.tabPageCriteria.Controls.Add(this.label2);
            this.tabPageCriteria.Controls.Add(this.groupBoxOther);
            this.tabPageCriteria.Controls.Add(this.groupBoxMessageID);
            this.tabPageCriteria.Location = new System.Drawing.Point(4, 22);
            this.tabPageCriteria.Name = "tabPageCriteria";
            this.tabPageCriteria.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCriteria.Size = new System.Drawing.Size(978, 139);
            this.tabPageCriteria.TabIndex = 0;
            this.tabPageCriteria.Text = "Criteria";
            this.tabPageCriteria.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(547, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "OR";
            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Controls.Add(this.label7);
            this.groupBoxOther.Controls.Add(this.label6);
            this.groupBoxOther.Controls.Add(this.textBoxBody);
            this.groupBoxOther.Controls.Add(this.textBoxSubject);
            this.groupBoxOther.Controls.Add(this.textBoxSender);
            this.groupBoxOther.Controls.Add(this.label5);
            this.groupBoxOther.Controls.Add(this.linkLabelDate);
            this.groupBoxOther.Controls.Add(this.dtpTo);
            this.groupBoxOther.Controls.Add(this.label4);
            this.groupBoxOther.Controls.Add(this.dtpFrom);
            this.groupBoxOther.Location = new System.Drawing.Point(576, 6);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(396, 127);
            this.groupBoxOther.TabIndex = 3;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "Date | Sender | Subject | Body";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Subject:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Body:";
            // 
            // textBoxBody
            // 
            this.textBoxBody.Location = new System.Drawing.Point(63, 99);
            this.textBoxBody.Name = "textBoxBody";
            this.textBoxBody.Size = new System.Drawing.Size(322, 20);
            this.textBoxBody.TabIndex = 7;
            this.textBoxBody.TextChanged += new System.EventHandler(this.textBoxBody_TextChanged);
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Location = new System.Drawing.Point(63, 73);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(322, 20);
            this.textBoxSubject.TabIndex = 6;
            this.textBoxSubject.TextChanged += new System.EventHandler(this.textBoxSubject_TextChanged);
            // 
            // textBoxSender
            // 
            this.textBoxSender.Location = new System.Drawing.Point(63, 47);
            this.textBoxSender.Name = "textBoxSender";
            this.textBoxSender.Size = new System.Drawing.Size(322, 20);
            this.textBoxSender.TabIndex = 5;
            this.textBoxSender.TextChanged += new System.EventHandler(this.textBoxSender_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Sender:";
            // 
            // linkLabelDate
            // 
            this.linkLabelDate.AutoSize = true;
            this.linkLabelDate.Location = new System.Drawing.Point(11, 24);
            this.linkLabelDate.Name = "linkLabelDate";
            this.linkLabelDate.Size = new System.Drawing.Size(58, 13);
            this.linkLabelDate.TabIndex = 3;
            this.linkLabelDate.TabStop = true;
            this.linkLabelDate.Text = "Sent Date:";
            this.linkLabelDate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDate_LinkClicked);
            // 
            // dtpTo
            // 
            this.dtpTo.Checked = false;
            this.dtpTo.CustomFormat = "dd-MMM-yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(260, 21);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.ShowCheckBox = true;
            this.dtpTo.Size = new System.Drawing.Size(125, 20);
            this.dtpTo.TabIndex = 2;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "TO";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Checked = false;
            this.dtpFrom.CustomFormat = "dd-MMM-yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(101, 21);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.ShowCheckBox = true;
            this.dtpFrom.Size = new System.Drawing.Size(125, 20);
            this.dtpFrom.TabIndex = 0;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // groupBoxMessageID
            // 
            this.groupBoxMessageID.Controls.Add(this.textBoxMessageID);
            this.groupBoxMessageID.Location = new System.Drawing.Point(6, 6);
            this.groupBoxMessageID.Name = "groupBoxMessageID";
            this.groupBoxMessageID.Size = new System.Drawing.Size(535, 127);
            this.groupBoxMessageID.TabIndex = 2;
            this.groupBoxMessageID.TabStop = false;
            this.groupBoxMessageID.Text = "Message-ID";
            // 
            // textBoxMessageID
            // 
            this.textBoxMessageID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMessageID.Location = new System.Drawing.Point(3, 16);
            this.textBoxMessageID.Multiline = true;
            this.textBoxMessageID.Name = "textBoxMessageID";
            this.textBoxMessageID.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessageID.Size = new System.Drawing.Size(529, 108);
            this.textBoxMessageID.TabIndex = 0;
            this.textBoxMessageID.WordWrap = false;
            this.textBoxMessageID.TextChanged += new System.EventHandler(this.textBoxMessageID_TextChanged);
            // 
            // tabPageIMAP
            // 
            this.tabPageIMAP.Controls.Add(this.textBoxIMAP);
            this.tabPageIMAP.Location = new System.Drawing.Point(4, 22);
            this.tabPageIMAP.Name = "tabPageIMAP";
            this.tabPageIMAP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIMAP.Size = new System.Drawing.Size(978, 139);
            this.tabPageIMAP.TabIndex = 1;
            this.tabPageIMAP.Text = "IMAP4 Expression";
            this.tabPageIMAP.UseVisualStyleBackColor = true;
            // 
            // textBoxIMAP
            // 
            this.textBoxIMAP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxIMAP.Location = new System.Drawing.Point(3, 3);
            this.textBoxIMAP.Multiline = true;
            this.textBoxIMAP.Name = "textBoxIMAP";
            this.textBoxIMAP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxIMAP.Size = new System.Drawing.Size(972, 133);
            this.textBoxIMAP.TabIndex = 0;
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Controls.Add(this.checkBoxExhaustive);
            this.groupBoxSearch.Controls.Add(this.buttonClearCriteria);
            this.groupBoxSearch.Controls.Add(this.buttonSearch);
            this.groupBoxSearch.Controls.Add(this.tabControlSearch);
            this.groupBoxSearch.Location = new System.Drawing.Point(12, 79);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(998, 190);
            this.groupBoxSearch.TabIndex = 14;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Search";
            // 
            // buttonClearCriteria
            // 
            this.buttonClearCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearCriteria.Location = new System.Drawing.Point(834, 12);
            this.buttonClearCriteria.Name = "buttonClearCriteria";
            this.buttonClearCriteria.Size = new System.Drawing.Size(75, 23);
            this.buttonClearCriteria.TabIndex = 17;
            this.buttonClearCriteria.Text = "Clear";
            this.buttonClearCriteria.UseVisualStyleBackColor = true;
            this.buttonClearCriteria.Click += new System.EventHandler(this.buttonClearCriteria_Click);
            // 
            // groupBoxConn
            // 
            this.groupBoxConn.Controls.Add(this.buttonDelete);
            this.groupBoxConn.Controls.Add(this.comboBoxMailbox);
            this.groupBoxConn.Controls.Add(this.comboBoxFolder);
            this.groupBoxConn.Controls.Add(this.label1);
            this.groupBoxConn.Controls.Add(this.label3);
            this.groupBoxConn.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConn.Name = "groupBoxConn";
            this.groupBoxConn.Size = new System.Drawing.Size(705, 61);
            this.groupBoxConn.TabIndex = 15;
            this.groupBoxConn.TabStop = false;
            this.groupBoxConn.Text = "Connection";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(596, 22);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(91, 23);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete Folder";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxResults.Controls.Add(this.buttonDeDuplicate);
            this.groupBoxResults.Controls.Add(this.buttonSend);
            this.groupBoxResults.Controls.Add(this.textBoxEmailAddress);
            this.groupBoxResults.Controls.Add(this.buttonMove);
            this.groupBoxResults.Controls.Add(this.comboBoxMove);
            this.groupBoxResults.Controls.Add(this.buttonSave);
            this.groupBoxResults.Controls.Add(this.buttonClear);
            this.groupBoxResults.Controls.Add(this.buttonDeselectAll);
            this.groupBoxResults.Controls.Add(this.buttonSelectAll);
            this.groupBoxResults.Controls.Add(this.listViewMessages);
            this.groupBoxResults.Location = new System.Drawing.Point(12, 275);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(1050, 315);
            this.groupBoxResults.TabIndex = 16;
            this.groupBoxResults.TabStop = false;
            this.groupBoxResults.Text = "Results";
            // 
            // buttonDeDuplicate
            // 
            this.buttonDeDuplicate.Location = new System.Drawing.Point(330, 19);
            this.buttonDeDuplicate.Name = "buttonDeDuplicate";
            this.buttonDeDuplicate.Size = new System.Drawing.Size(80, 23);
            this.buttonDeDuplicate.TabIndex = 22;
            this.buttonDeDuplicate.Text = "De-Duplicate";
            this.buttonDeDuplicate.UseVisualStyleBackColor = true;
            this.buttonDeDuplicate.Click += new System.EventHandler(this.buttonDeDuplicate_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(786, 19);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 21;
            this.buttonSend.Text = "Send To";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // textBoxEmailAddress
            // 
            this.textBoxEmailAddress.AutoCompleteCustomSource.AddRange(new string[] {
            "trent.cromer@decipha.com.au",
            "karen.adlam@decipha.com.au",
            "vishal.sharma@decipha.com.au",
            "towsib.ali@decipha.com.au",
            "felix.kang@decipha.com.au",
            "chamila.perera@decipha.com.au",
            "stephen.johnson@decipha.com.au",
            "alan.to@decipha.com.au"});
            this.textBoxEmailAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.textBoxEmailAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxEmailAddress.Location = new System.Drawing.Point(867, 21);
            this.textBoxEmailAddress.Name = "textBoxEmailAddress";
            this.textBoxEmailAddress.Size = new System.Drawing.Size(177, 20);
            this.textBoxEmailAddress.TabIndex = 20;
            // 
            // buttonMove
            // 
            this.buttonMove.Location = new System.Drawing.Point(430, 19);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(75, 23);
            this.buttonMove.TabIndex = 19;
            this.buttonMove.Text = "Move To";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // comboBoxMove
            // 
            this.comboBoxMove.FormattingEnabled = true;
            this.comboBoxMove.Location = new System.Drawing.Point(511, 21);
            this.comboBoxMove.Name = "comboBoxMove";
            this.comboBoxMove.Size = new System.Drawing.Size(250, 21);
            this.comboBoxMove.TabIndex = 18;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(249, 19);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 17;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(168, 19);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(75, 23);
            this.buttonClear.TabIndex = 15;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonDeselectAll
            // 
            this.buttonDeselectAll.Location = new System.Drawing.Point(87, 19);
            this.buttonDeselectAll.Name = "buttonDeselectAll";
            this.buttonDeselectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonDeselectAll.TabIndex = 14;
            this.buttonDeselectAll.Text = "Deselect All";
            this.buttonDeselectAll.UseVisualStyleBackColor = true;
            this.buttonDeselectAll.Click += new System.EventHandler(this.buttonDeselectAll_Click);
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(6, 19);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectAll.TabIndex = 13;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.buttonSelectAll_Click);
            // 
            // groupBoxService
            // 
            this.groupBoxService.Controls.Add(this.buttonStart);
            this.groupBoxService.Controls.Add(this.buttonStop);
            this.groupBoxService.Controls.Add(this.labelStatus);
            this.groupBoxService.Location = new System.Drawing.Point(726, 12);
            this.groupBoxService.Name = "groupBoxService";
            this.groupBoxService.Size = new System.Drawing.Size(284, 61);
            this.groupBoxService.TabIndex = 17;
            this.groupBoxService.TabStop = false;
            this.groupBoxService.Text = "Email Import Service";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(120, 22);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(201, 22);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(6, 27);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(40, 13);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Status:";
            this.labelStatus.DoubleClick += new System.EventHandler(this.labelStatus_DoubleClick);
            // 
            // checkBoxExhaustive
            // 
            this.checkBoxExhaustive.AutoSize = true;
            this.checkBoxExhaustive.Location = new System.Drawing.Point(705, 15);
            this.checkBoxExhaustive.Name = "checkBoxExhaustive";
            this.checkBoxExhaustive.Size = new System.Drawing.Size(115, 17);
            this.checkBoxExhaustive.TabIndex = 18;
            this.checkBoxExhaustive.Text = "Exhaustive Search";
            this.checkBoxExhaustive.UseVisualStyleBackColor = true;
            // 
            // MailboxClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 602);
            this.Controls.Add(this.groupBoxService);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBoxConn);
            this.Controls.Add(this.groupBoxSearch);
            this.MinimumSize = new System.Drawing.Size(933, 500);
            this.Name = "MailboxClient";
            this.Text = "Email Import - Mailbox Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MailboxClient_Load);
            this.tabControlSearch.ResumeLayout(false);
            this.tabPageCriteria.ResumeLayout(false);
            this.tabPageCriteria.PerformLayout();
            this.groupBoxOther.ResumeLayout(false);
            this.groupBoxOther.PerformLayout();
            this.groupBoxMessageID.ResumeLayout(false);
            this.groupBoxMessageID.PerformLayout();
            this.tabPageIMAP.ResumeLayout(false);
            this.tabPageIMAP.PerformLayout();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.groupBoxConn.ResumeLayout(false);
            this.groupBoxConn.PerformLayout();
            this.groupBoxResults.ResumeLayout(false);
            this.groupBoxResults.PerformLayout();
            this.groupBoxService.ResumeLayout(false);
            this.groupBoxService.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMailbox;
        private System.Windows.Forms.ComboBox comboBoxFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ListView listViewMessages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabControl tabControlSearch;
        private System.Windows.Forms.TabPage tabPageCriteria;
        private System.Windows.Forms.TabPage tabPageIMAP;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.GroupBox groupBoxConn;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonDeselectAll;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxMessageID;
        private System.Windows.Forms.TextBox textBoxIMAP;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.ComboBox comboBoxMove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxOther;
        private System.Windows.Forms.GroupBox groupBoxMessageID;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.LinkLabel linkLabelDate;
        private System.Windows.Forms.TextBox textBoxBody;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.TextBox textBoxSender;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonClearCriteria;
        private System.Windows.Forms.TextBox textBoxEmailAddress;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.GroupBox groupBoxService;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonDeDuplicate;
        private System.Windows.Forms.CheckBox checkBoxExhaustive;
    }
}

