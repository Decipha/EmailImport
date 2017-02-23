using Aspose.Email;
using Aspose.Email.Mail;
using BitFactory.Logging;
using System;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using EmailImport.Conversion.Configuration;
using EmailImport.DataLayer;
using System.Xml.Linq;
using EmailImport.Conversion;
using Ionic.Zip;

namespace EmailImport
{
    [Export]
    public class ErrorEmailConverter
    {
        #region Static Methods

        static int count = 0;
        static ErrorEmailConverter()
        {
            License license = new License();
            license.SetLicense("Aspose.Total.lic");
        }
        #endregion

        #region Fields

        private long emailID;
        private String attachmentPath, attachmentFile, batchNumber;
        private Dictionary<String, String> fields;
        private MailboxProfile profile;
        private MailMessage message;
        private Email email;

        #endregion
       
        #region Public Properties

        public String OutputPath { get; private set; }
        public List<Document> Documents { get; private set; }

        #endregion

        
       
        #region Public Methods

        public void BeginAsync(Email email, MailboxProfile profile)
        {
            Initialise(email, profile);
            ManualResetEvent waiter = new ManualResetEvent(false);
            //var folder = String.Format(String.IsNullOrWhiteSpace(profile.OutputFolderFormat) ? "{0:00000000}" : profile.OutputFolderFormat, emailID);
            //OutputPath = Path.Combine(profile.OutputPath, folder);
            System.Threading.Tasks.Task.Run(() =>
            {
                Worker(waiter);
            });

            waiter.WaitOne();
        }
        #endregion
        #region Private Methods
        private void Initialise(Email email, MailboxProfile profile)
        {
            // Assign parameters to class members
            this.email = email;
            this.profile = profile;

            // Load the Mail Message
            var options = new MailMessageLoadOptions();
            options.FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking;
            options.MessageFormat = email.MessageFilePath.ToLower().EndsWith("msg") ? MessageFormat.Msg : MessageFormat.Eml;

            this.message = MailMessage.Load(email.MessageFilePath, options);
        }
        private void Worker(Object waiter)
        {
            ErrorBatch batch = null;

            Interlocked.Increment(ref count);

            ((ManualResetEvent)waiter).Set();
            batch = new ErrorBatch(email, message, profile);
            batch.CreateOutput();
        }
        #endregion
       
    }
}
