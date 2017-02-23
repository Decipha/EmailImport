using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EmailImport.Viewer
{
    public partial class XmlViewForm : Form
    {
        string xmlFilePath;

        public XmlViewForm()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void XmlViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty(xmlFilePath) && File.Exists(xmlFilePath))
            {
                File.Delete(xmlFilePath);
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            File.Delete(webBrowser.Url.AbsolutePath);
        }

        public void SetXml(string xml)
        {
            xmlFilePath = Path.Combine(Path.GetTempPath(), "v.xml");

            using (StreamWriter sw = new StreamWriter(xmlFilePath))
            {
                sw.WriteLine(xml);
            }

            webBrowser.Navigate(xmlFilePath);
        }
    }
}
