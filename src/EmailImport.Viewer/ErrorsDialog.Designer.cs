namespace EmailImport.Viewer
{
    partial class ErrorsDialog
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxReason = new System.Windows.Forms.TextBox();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.linkLabelFileName = new System.Windows.Forms.LinkLabel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAction = new System.Windows.Forms.TextBox();
            this.comboBoxOverride = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonNextError = new System.Windows.Forms.Button();
            this.buttonPreviousError = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxProcessAs = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "File Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Reason:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Message:";
            // 
            // textBoxReason
            // 
            this.textBoxReason.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReason.Location = new System.Drawing.Point(81, 41);
            this.textBoxReason.Name = "textBoxReason";
            this.textBoxReason.ReadOnly = true;
            this.textBoxReason.Size = new System.Drawing.Size(795, 20);
            this.textBoxReason.TabIndex = 4;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.Location = new System.Drawing.Point(81, 67);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxMessage.Size = new System.Drawing.Size(795, 190);
            this.textBoxMessage.TabIndex = 6;
            // 
            // linkLabelFileName
            // 
            this.linkLabelFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelFileName.AutoSize = true;
            this.linkLabelFileName.Location = new System.Drawing.Point(78, 17);
            this.linkLabelFileName.Name = "linkLabelFileName";
            this.linkLabelFileName.Size = new System.Drawing.Size(45, 13);
            this.linkLabelFileName.TabIndex = 1;
            this.linkLabelFileName.TabStop = true;
            this.linkLabelFileName.Text = "<None>";
            this.linkLabelFileName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFileName_LinkClicked);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(801, 12);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 266);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Action:";
            // 
            // textBoxAction
            // 
            this.textBoxAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAction.Location = new System.Drawing.Point(81, 263);
            this.textBoxAction.Name = "textBoxAction";
            this.textBoxAction.ReadOnly = true;
            this.textBoxAction.Size = new System.Drawing.Size(795, 20);
            this.textBoxAction.TabIndex = 8;
            // 
            // comboBoxOverride
            // 
            this.comboBoxOverride.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxOverride.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOverride.Enabled = false;
            this.comboBoxOverride.FormattingEnabled = true;
            this.comboBoxOverride.Items.AddRange(new object[] {
            "",
            "Ignore",
            "Substitute",
            "Reject",
            "Escalate"});
            this.comboBoxOverride.Location = new System.Drawing.Point(81, 289);
            this.comboBoxOverride.Name = "comboBoxOverride";
            this.comboBoxOverride.Size = new System.Drawing.Size(795, 21);
            this.comboBoxOverride.TabIndex = 10;
            this.comboBoxOverride.SelectedIndexChanged += new System.EventHandler(this.comboBoxOverride_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Override:";
            // 
            // buttonNextError
            // 
            this.buttonNextError.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonNextError.Enabled = false;
            this.buttonNextError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNextError.Location = new System.Drawing.Point(447, 364);
            this.buttonNextError.Name = "buttonNextError";
            this.buttonNextError.Size = new System.Drawing.Size(153, 39);
            this.buttonNextError.TabIndex = 14;
            this.buttonNextError.Text = "Next Error >";
            this.buttonNextError.UseVisualStyleBackColor = true;
            this.buttonNextError.Click += new System.EventHandler(this.buttonNextError_Click);
            // 
            // buttonPreviousError
            // 
            this.buttonPreviousError.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPreviousError.Enabled = false;
            this.buttonPreviousError.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPreviousError.Location = new System.Drawing.Point(288, 364);
            this.buttonPreviousError.Name = "buttonPreviousError";
            this.buttonPreviousError.Size = new System.Drawing.Size(153, 39);
            this.buttonPreviousError.TabIndex = 13;
            this.buttonPreviousError.Text = "< Previous Error";
            this.buttonPreviousError.UseVisualStyleBackColor = true;
            this.buttonPreviousError.Click += new System.EventHandler(this.buttonPreviousError_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 319);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Process As:";
            // 
            // comboBoxProcessAs
            // 
            this.comboBoxProcessAs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProcessAs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProcessAs.Enabled = false;
            this.comboBoxProcessAs.FormattingEnabled = true;
            this.comboBoxProcessAs.Location = new System.Drawing.Point(81, 316);
            this.comboBoxProcessAs.Name = "comboBoxProcessAs";
            this.comboBoxProcessAs.Size = new System.Drawing.Size(795, 21);
            this.comboBoxProcessAs.TabIndex = 16;
            this.comboBoxProcessAs.SelectedIndexChanged += new System.EventHandler(this.comboBoxProcessAs_SelectedIndexChanged);
            // 
            // ErrorsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 429);
            this.Controls.Add(this.comboBoxProcessAs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonNextError);
            this.Controls.Add(this.buttonPreviousError);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxOverride);
            this.Controls.Add(this.textBoxAction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.linkLabelFileName);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.textBoxReason);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.Name = "ErrorsDialog";
            this.Text = "Errors";
            this.Load += new System.EventHandler(this.ErrorsDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ErrorsDialog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxReason;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.LinkLabel linkLabelFileName;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAction;
        private System.Windows.Forms.ComboBox comboBoxOverride;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonNextError;
        private System.Windows.Forms.Button buttonPreviousError;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxProcessAs;
    }
}