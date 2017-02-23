namespace EmailImport.FileUpload
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
            this.comboBoxProfile = new System.Windows.Forms.ComboBox();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonCollect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.radioButtonMSG = new System.Windows.Forms.RadioButton();
            this.radioButtonPST = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // comboBoxProfile
            // 
            this.comboBoxProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfile.FormattingEnabled = true;
            this.comboBoxProfile.Location = new System.Drawing.Point(99, 15);
            this.comboBoxProfile.Name = "comboBoxProfile";
            this.comboBoxProfile.Size = new System.Drawing.Size(292, 21);
            this.comboBoxProfile.TabIndex = 0;
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(99, 47);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(292, 20);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonCollect
            // 
            this.buttonCollect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCollect.Location = new System.Drawing.Point(166, 116);
            this.buttonCollect.Name = "buttonCollect";
            this.buttonCollect.Size = new System.Drawing.Size(119, 41);
            this.buttonCollect.TabIndex = 2;
            this.buttonCollect.Text = "Upload";
            this.buttonCollect.UseVisualStyleBackColor = true;
            this.buttonCollect.Click += new System.EventHandler(this.buttonCollect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mailbox Profile:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "File Path:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(392, 47);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(24, 20);
            this.buttonBrowse.TabIndex = 5;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 163);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(432, 15);
            this.progressBar.TabIndex = 6;
            this.progressBar.Visible = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // radioButtonMSG
            // 
            this.radioButtonMSG.AutoSize = true;
            this.radioButtonMSG.Checked = true;
            this.radioButtonMSG.Location = new System.Drawing.Point(174, 73);
            this.radioButtonMSG.Name = "radioButtonMSG";
            this.radioButtonMSG.Size = new System.Drawing.Size(49, 17);
            this.radioButtonMSG.TabIndex = 7;
            this.radioButtonMSG.TabStop = true;
            this.radioButtonMSG.Text = "MSG";
            this.radioButtonMSG.UseVisualStyleBackColor = true;
            // 
            // radioButtonPST
            // 
            this.radioButtonPST.AutoSize = true;
            this.radioButtonPST.Location = new System.Drawing.Point(229, 73);
            this.radioButtonPST.Name = "radioButtonPST";
            this.radioButtonPST.Size = new System.Drawing.Size(46, 17);
            this.radioButtonPST.TabIndex = 8;
            this.radioButtonPST.Text = "PST";
            this.radioButtonPST.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 178);
            this.Controls.Add(this.radioButtonPST);
            this.Controls.Add(this.radioButtonMSG);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCollect);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.comboBoxProfile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Email Import - File Upload";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProfile;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonCollect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.RadioButton radioButtonMSG;
        private System.Windows.Forms.RadioButton radioButtonPST;
    }
}

