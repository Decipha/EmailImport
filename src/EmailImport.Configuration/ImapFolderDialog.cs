using System;
using System.Windows.Forms;
using Aspose.Email.Imap;
using Decipha.Net.Mail;

namespace EmailImport.Configuration
{
    public partial class ImapFolderDialog : Form
    {
        public String SelectedFolder { get; private set; }

        public String HostName { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public int Port { get; set; }

        public ImapFolderDialog()
        {
            InitializeComponent();

            Port = 143;
            DialogResult = DialogResult.Cancel;
            SelectedFolder = null;
        }

        private void AddFolders(Imap imap, TreeNode parent, ImapFolderInfoCollection folders)
        {
            var delimiter = imap.Delimiter;

            foreach (ImapFolderInfo info in folders)
            {
                var index = info.Name.LastIndexOf(delimiter);

                TreeNode node = new TreeNode((index > 0) ? info.Name.Substring(index + 1) : info.Name);
                node.Tag = info.Name;

                if (parent == null)
                    treeViewFolders.Nodes.Add(node);
                else
                    parent.Nodes.Add(node);

                AddFolders(imap, node, imap.ListFolders(info.Name, (parent != null)));
            }
        }

        private void treeViewFolders_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            SelectedFolder = (String)e.Node.Tag;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ImapFolderDialog_Shown(object sender, EventArgs e)
        {
            using (var imap = new Imap(HostName, Port, UserName, Password))
            {
                AddFolders(imap, null, imap.ListFolders());
            }
        }
    }
}
