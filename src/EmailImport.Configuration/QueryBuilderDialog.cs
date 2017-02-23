using System;
using System.Windows.Forms;
using Aspose.Email;
using Aspose.Email.Imap;

namespace EmailImport.Configuration
{
    public partial class QueryBuilderDialog : Form
    {
        public String ImapQuery { get; private set; }

        ImapQueryBuilder query = new ImapQueryBuilder();
        
        public QueryBuilderDialog()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxQuery.Text))
                ImapQuery = textBoxQuery.Text;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            query = new ImapQueryBuilder();
            textBoxQuery.Text = String.Empty;
        }

        private void linkLabelAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBoxFields.SelectedItem == null || comboBoxOperand.SelectedItem == null)
                return;

            if (String.IsNullOrWhiteSpace(textBoxValue.Text) && !(comboBoxOperand.Text == "Empty" || comboBoxOperand.Text == "Not Empty"))
                return;

            switch ((String)comboBoxFields.SelectedItem)
            {
                case "Bcc":
                    ProcessStringComparisonField(query.Bcc);
                    break;

                case "Body":
                    ProcessStringComparisonField(query.Body);
                    break;

                case "Cc":
                    ProcessStringComparisonField(query.Cc);
                    break;

                case "From":
                    ProcessStringComparisonField(query.From);
                    break;

                case "Message ID":
                    ProcessStringComparisonField(query.MessageId);
                    break;

                case "Subject":
                    ProcessStringComparisonField(query.Subject);
                    break;

                case "Text":
                    ProcessStringComparisonField(query.Text);
                    break;

                case "To":
                    ProcessStringComparisonField(query.To);
                    break;

                case "Internal Date":
                    ProcessDateComparisonField(query.InternalDate);
                    break;

                case "Sent Date":
                    ProcessDateComparisonField(query.SentDate);
                    break;

                case "Message Size":
                    ProcessIntComparisonField(query.MessageSize);
                    break;
            }

            textBoxQuery.Text = query.GetQuery().ToString();

            comboBoxFields.SelectedItem = null;
            comboBoxOperand.SelectedItem = null;
            textBoxValue.Text = null;
        }

        private void ProcessStringComparisonField(StringComparisonField field)
        {
            switch ((String)comboBoxOperand.SelectedItem)
            {
                case "Contains":
                    field.Contains(textBoxValue.Text);
                    break;

                case "Empty":
                    field.Empty();
                    break;

                case "Equals":
                    field.Equals(textBoxValue.Text);
                    break;

                case "Not Contains":
                    field.NotContains(textBoxValue.Text);
                    break;

                case "Not Empty":
                    field.NotEmpty();
                    break;

                case "Not Equals":
                    field.NotEquals(textBoxValue.Text);
                    break;
            }
        }

        private void ProcessDateComparisonField(DateComparisonField field)
        {
            DateTime value;

            if (!DateTime.TryParse(textBoxValue.Text, out value))
            {
                MessageBox.Show(this, "Invalid DateTime value.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch ((String)comboBoxOperand.SelectedItem)
            {
                case "Before":
                    field.Before(value);
                    break;
                
                case "Not On":
                    field.NotOn(value);
                    break;

                case "On":
                    field.On(value);
                    break;

                case "Since":
                    field.Since(value);
                    break;
            }
        }

        private void ProcessIntComparisonField(IntComparisonField field)
        {
            int value;

            if (!int.TryParse(textBoxValue.Text, out value))
            {
                MessageBox.Show(this, "Invalid integer value.", "Invalid Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch ((String)comboBoxOperand.SelectedItem)
            {
                case "Equals":
                    field.Equals(value);
                    break;

                case "Greater":
                    field.Greater(value);
                    break;

                case "Greater Or Equal":
                    field.GreaterOrEqual(value);
                    break;

                case "Less":
                    field.Less(value);
                    break;

                case "Less Or Equal":
                    field.LessOrEqual(value);
                    break;

                case "Not Equals":
                    field.NotEquals(value);
                    break;
            }
        }

        private void comboBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFields.SelectedItem == null)
                return;

            comboBoxOperand.Items.Clear();

            switch ((String)comboBoxFields.SelectedItem)
            {
                case "Bcc":
                case "Body":
                case "Cc":
                case "From":
                case "Message ID":
                case "Subject":
                case "Text":
                case "To":
                    comboBoxOperand.Items.AddRange(new String[] { "Contains", "Empty", "Equals", "Not Contains", "Not Empty", "Not Equals" });
                    break;

                case "Internal Date":
                case "Sent Date":
                    comboBoxOperand.Items.AddRange(new String[] { "Before", "Not On", "On", "Since" });
                    break;

                case "Message Size":
                    comboBoxOperand.Items.AddRange(new String[] { "Equals", "Greater", "Greater Or Equal", "Less", "Less Or Equal", "Not Equals" });
                    break;
            }
        }
    }
}
