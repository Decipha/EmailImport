namespace EmailImport.Viewer
{
    partial class DateRangeDialog
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
            this.dtpDateReceivedFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateReceivedTo = new System.Windows.Forms.DateTimePicker();
            this.lTo = new System.Windows.Forms.Label();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpDateReceivedFrom
            // 
            this.dtpDateReceivedFrom.Location = new System.Drawing.Point(12, 12);
            this.dtpDateReceivedFrom.Name = "dtpDateReceivedFrom";
            this.dtpDateReceivedFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpDateReceivedFrom.TabIndex = 4;
            this.dtpDateReceivedFrom.ValueChanged += new System.EventHandler(this.dtpDateReceivedFrom_ValueChanged);
            // 
            // dtpDateReceivedTo
            // 
            this.dtpDateReceivedTo.Location = new System.Drawing.Point(240, 12);
            this.dtpDateReceivedTo.Name = "dtpDateReceivedTo";
            this.dtpDateReceivedTo.Size = new System.Drawing.Size(200, 20);
            this.dtpDateReceivedTo.TabIndex = 6;
            this.dtpDateReceivedTo.ValueChanged += new System.EventHandler(this.dtpDateReceivedTo_ValueChanged);
            // 
            // lTo
            // 
            this.lTo.AutoSize = true;
            this.lTo.Location = new System.Drawing.Point(218, 18);
            this.lTo.Name = "lTo";
            this.lTo.Size = new System.Drawing.Size(16, 13);
            this.lTo.TabIndex = 5;
            this.lTo.Text = "to";
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(284, 49);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(75, 23);
            this.bOk.TabIndex = 7;
            this.bOk.Text = "OK";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bCancel
            // 
            this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCancel.Location = new System.Drawing.Point(365, 49);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 8;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            // 
            // DateRangeDialog
            // 
            this.AcceptButton = this.bOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 85);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.lTo);
            this.Controls.Add(this.dtpDateReceivedTo);
            this.Controls.Add(this.dtpDateReceivedFrom);
            this.Name = "DateRangeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Date Range";
            this.Load += new System.EventHandler(this.DateRangeDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDateReceivedFrom;
        private System.Windows.Forms.DateTimePicker dtpDateReceivedTo;
        private System.Windows.Forms.Label lTo;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
    }
}