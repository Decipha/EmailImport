namespace EmailImport.Configuration
{
    partial class QueryBuilderDialog
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.textBoxQuery = new System.Windows.Forms.TextBox();
            this.comboBoxFields = new System.Windows.Forms.ComboBox();
            this.labelField = new System.Windows.Forms.Label();
            this.comboBoxOperand = new System.Windows.Forms.ComboBox();
            this.labelOperand = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.labelValue = new System.Windows.Forms.Label();
            this.linkLabelAdd = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(397, 178);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReset.Location = new System.Drawing.Point(12, 178);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 1;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // textBoxQuery
            // 
            this.textBoxQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQuery.Location = new System.Drawing.Point(12, 66);
            this.textBoxQuery.Multiline = true;
            this.textBoxQuery.Name = "textBoxQuery";
            this.textBoxQuery.ReadOnly = true;
            this.textBoxQuery.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxQuery.Size = new System.Drawing.Size(460, 100);
            this.textBoxQuery.TabIndex = 2;
            // 
            // comboBoxFields
            // 
            this.comboBoxFields.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFields.FormattingEnabled = true;
            this.comboBoxFields.Items.AddRange(new object[] {
            "Bcc",
            "Body",
            "Cc",
            "From",
            "Internal Date",
            "Message ID",
            "Message Size",
            "Sent Date",
            "Subject",
            "Text",
            "To"});
            this.comboBoxFields.Location = new System.Drawing.Point(13, 29);
            this.comboBoxFields.Name = "comboBoxFields";
            this.comboBoxFields.Size = new System.Drawing.Size(95, 21);
            this.comboBoxFields.TabIndex = 3;
            this.comboBoxFields.SelectedIndexChanged += new System.EventHandler(this.comboBoxFields_SelectedIndexChanged);
            // 
            // labelField
            // 
            this.labelField.AutoSize = true;
            this.labelField.Location = new System.Drawing.Point(12, 13);
            this.labelField.Name = "labelField";
            this.labelField.Size = new System.Drawing.Size(32, 13);
            this.labelField.TabIndex = 4;
            this.labelField.Text = "Field:";
            // 
            // comboBoxOperand
            // 
            this.comboBoxOperand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOperand.FormattingEnabled = true;
            this.comboBoxOperand.Location = new System.Drawing.Point(114, 29);
            this.comboBoxOperand.Name = "comboBoxOperand";
            this.comboBoxOperand.Size = new System.Drawing.Size(110, 21);
            this.comboBoxOperand.TabIndex = 5;
            // 
            // labelOperand
            // 
            this.labelOperand.AutoSize = true;
            this.labelOperand.Location = new System.Drawing.Point(111, 13);
            this.labelOperand.Name = "labelOperand";
            this.labelOperand.Size = new System.Drawing.Size(51, 13);
            this.labelOperand.TabIndex = 6;
            this.labelOperand.Text = "Operand:";
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(230, 30);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(210, 20);
            this.textBoxValue.TabIndex = 7;
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(227, 13);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(37, 13);
            this.labelValue.TabIndex = 8;
            this.labelValue.Text = "Value:";
            // 
            // linkLabelAdd
            // 
            this.linkLabelAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabelAdd.AutoSize = true;
            this.linkLabelAdd.Location = new System.Drawing.Point(446, 33);
            this.linkLabelAdd.Name = "linkLabelAdd";
            this.linkLabelAdd.Size = new System.Drawing.Size(26, 13);
            this.linkLabelAdd.TabIndex = 9;
            this.linkLabelAdd.TabStop = true;
            this.linkLabelAdd.Text = "Add";
            this.linkLabelAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAdd_LinkClicked);
            // 
            // QueryBuilderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 213);
            this.Controls.Add(this.linkLabelAdd);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.labelOperand);
            this.Controls.Add(this.comboBoxOperand);
            this.Controls.Add(this.labelField);
            this.Controls.Add(this.comboBoxFields);
            this.Controls.Add(this.textBoxQuery);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueryBuilderDialog";
            this.ShowInTaskbar = false;
            this.Text = "Imap Query Builder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TextBox textBoxQuery;
        private System.Windows.Forms.ComboBox comboBoxFields;
        private System.Windows.Forms.Label labelField;
        private System.Windows.Forms.ComboBox comboBoxOperand;
        private System.Windows.Forms.Label labelOperand;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.LinkLabel linkLabelAdd;
    }
}