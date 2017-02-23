using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EmailImport.Viewer
{
    public partial class DateRangeDialog : Form
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public DateRangeDialog()
        {
            InitializeComponent();
        }

        private void DateRangeDialog_Load(object sender, EventArgs e)
        {
            // initialise from and to dates
            dtpDateReceivedTo.Value = DateTime.Now.Date;
            dtpDateReceivedFrom.Value = DateTime.Now.Date;
        }

        private void dtpDateReceivedFrom_ValueChanged(object sender, EventArgs e)
        {
            dtpDateReceivedTo.MinDate = dtpDateReceivedFrom.Value;
        }

        private void dtpDateReceivedTo_ValueChanged(object sender, EventArgs e)
        {
            dtpDateReceivedFrom.MaxDate = dtpDateReceivedTo.Value;
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (dtpDateReceivedFrom.Value > dtpDateReceivedTo.Value)
            {
                MessageBox.Show(this, "From date must be less than or equal to the To date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                dtpDateReceivedFrom.Focus();
                
                return;
            }

            From = dtpDateReceivedFrom.Value.Date;
            To = dtpDateReceivedTo.Value.Date;

            DialogResult = DialogResult.OK;
        }
    }
}
