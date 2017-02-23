using System;
using EmailImport.Conversion.Configuration;

namespace MailboxClient
{
    class MailboxItem
    {
        public MailboxElement Mailbox { get; private set; }

        public MailboxItem(MailboxElement mailbox)
        {
            Mailbox = mailbox;
        }

        public override string ToString()
        {
            return Mailbox.Description;
        }
    }
}
