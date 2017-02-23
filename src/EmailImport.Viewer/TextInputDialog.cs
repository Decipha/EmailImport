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
    public partial class TextInputDialog : Form
    {
        public string Title
        {
            set
            {
                this.Text = value;
            }
        }
        public string Value
        {
            get
            {
                return textBoxValue.Text;
            }
        }

        public TextInputDialog()
        {
            InitializeComponent();
        }

        private void TextInputDialog_Load(object sender, EventArgs e)
        {
            textBoxValue.Focus();
        }
    }
}
