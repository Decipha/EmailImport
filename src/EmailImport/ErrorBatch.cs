using Aspose.Email.Mail;
using BitFactory.Logging;
using Decipha.Net.Mail;
using EmailImport.Conversion;
using EmailImport.Conversion.Configuration;
using EmailImport.DataLayer;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;


namespace EmailImport
{
    class ErrorBatch
    {
        #region Fields
        private long emailID;
        private String attachmentPath, attachmentFile, batchNumber;
        private Dictionary<String, String> fields;
        private MailboxProfile profile;
        private string errorreason;

        #endregion

        #region Public Properties
        public String OutputPath { get; private set; }
        public List<Document> Documents { get; private set; }
        #endregion
        #region Constructor

        public ErrorBatch(Email email, MailMessage message, MailboxProfile profile)
        {
            Documents = new List<Document>();

            this.emailID = email.EmailID;
            this.batchNumber = email.BatchNumber;
            this.profile = profile;
            this.errorreason = email.Status == "Rejected" ?string.Join(",", email.Errors.Descendants("Error")
                                 .Select(e => (string)e.Attribute("fileName") +" - "+(string)e.Attribute("reason"))
                                 .ToList()) : "Unsupported File Type";


            var folder = String.Format(String.IsNullOrWhiteSpace(profile.OutputFolderFormat) ? "{0:00000000}" : profile.OutputFolderFormat, emailID);

            if (profile.BatchStyle == BatchStyle.ImagesOnly || !profile.BatchClassSubfolder)
                OutputPath = Path.Combine(profile.ErrorOutputPath, folder);
            else
                OutputPath = Path.Combine(profile.ErrorOutputPath, String.Format(@"{0}\{1}", profile.BatchClassName, folder));

            FileSystemHelper.CleanDirectory(OutputPath, true);

            SaveMessage(email);
            InitialiseFields(message);
            //ExtractDocuments(message, false);

            FileSystemHelper.DeleteDirectory(Path.Combine(OutputPath, "temp"), true);
        }

        #endregion
        #region Public
        public void CreateOutput()
        {
            CreateBatchXml();
     
        }
        #endregion
        #region Create Output
        public void CreateBatchXml()
        {
            // Add BatchCreationDateTime field
            fields.Add("%BatchCreationDateTime", DateTime.Now.ToString("s"));

            // Create the xml batch
            var xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                      new XElement("Batch",
                         GetBatchAttributes(),
                         GetFields("BatchField", profile.ErrorBatchFields, null)
                         ));

            var tmp = Path.Combine(OutputPath, String.Format("{0}.tmp", Path.GetFileName(OutputPath)));

            xml.Save(tmp);

            if (profile.RetainNativeFiles == RetainNativeFileOptions.None)
                Directory.Delete(attachmentPath, true);
            
            
            File.Move(tmp, Path.ChangeExtension(tmp, ".xml"));
   
        }
        private List<XAttribute> GetBatchAttributes()
        {
            List<XAttribute> attributes = new List<XAttribute>();

            attributes.Add(new XAttribute("batchClassName", profile.BatchClassName));
            attributes.Add(new XAttribute("name", batchNumber));

            if (profile.Priority >= 1 && profile.Priority <= 10)
                attributes.Add(new XAttribute("priority", profile.Priority));

            if (!String.IsNullOrEmpty(profile.ArchivePath))
                attributes.Add(new XAttribute("archivePath", profile.ArchivePath));

            attributes.Add(GetBatchAttachment());

            if (profile.AutomaticSeparationAndFormID)
                attributes.Add(new XAttribute("enableAutomaticSeparationAndFormID", "true"));

            if (profile.SingleDocumentProcessing)
                attributes.Add(new XAttribute("enableSingleDocumentProcessing", "true"));

            return attributes;
        }
        private XAttribute GetBatchAttachment()
        {
            if (profile.RetainNativeFiles == RetainNativeFileOptions.MailMessage || profile.RetainNativeFiles == RetainNativeFileOptions.All)
                return new XAttribute("originalmessage", Path.Combine(Path.GetFileName(attachmentPath), Path.GetFileName(attachmentFile)));

            File.Delete(attachmentFile);
            return null;
        }
        private XElement GetFields(String elementName, List<Field> fields, Dictionary<String, String> documentFields)
        {
            List<XElement> elements = new List<XElement>();

            foreach (var field in fields)
            {
                if (String.IsNullOrWhiteSpace(field.Name))
                    continue;

                StringBuilder sb = new StringBuilder();

                if (field.Value != null)
                {
                    foreach (var split in field.Value.Split('|'))
                    {
                        if (sb.Length > 0)
                            sb.AppendLine();

                        if (this.fields.ContainsKey(split))
                            sb.Append(this.fields[split]);
                        else if (documentFields != null && documentFields.ContainsKey(split))
                            sb.Append(documentFields[split]);
                        else
                            sb.Append(split);
                    }
                }

                String value = sb.ToString();

                var element = new XElement(elementName,
                                  new XAttribute("name", field.Name),
                                  new XAttribute("value", (value == null) ? String.Empty : value));

                elements.Add(element);
            }

            return new XElement(elementName + "s", elements);
        }
       
        #endregion
        #region Other Methods

        private void SaveMessage(Email email)
        {
            attachmentPath = Path.Combine(OutputPath, "OriginalMessage");
            attachmentFile = GetAttachmentFileName("MailMessage.eml");

            FileSystemHelper.CreateDirectory(attachmentPath, true);
            File.Copy(email.MessageFilePath, attachmentFile, true);
        }
        private void InitialiseFields(MailMessage message)
        {
            fields = new Dictionary<String, String>();

            fields.Add("%From", (message.From == null) ? String.Empty : message.From.ToStringEx());
            fields.Add("%From.DisplayName", (message.From == null) ? String.Empty : message.From.DisplayName);
            fields.Add("%From.Address", message.From.GetAddressOrDisplayName());
            fields.Add("%ReplyTo", message.ReplyToList.ToStringEx());
            fields.Add("%ReplyTo.DisplayName", String.Join("; ", message.ReplyToList.Select(a => a.DisplayName)));
            fields.Add("%ReplyTo.Address", String.Join("; ", message.ReplyToList.Select(a => a.GetAddressOrDisplayName())));
            fields.Add("%Sender", (message.Sender == null) ? String.Empty : message.Sender.ToStringEx());
            fields.Add("%Sender.DisplayName", (message.Sender == null) ? String.Empty : message.Sender.DisplayName);
            fields.Add("%Sender.Address", message.Sender.GetAddressOrDisplayName());
            fields.Add("%To", message.To.ToStringEx());
            fields.Add("%To.DisplayName", String.Join("; ", message.To.Select(a => a.DisplayName)));
            fields.Add("%To.Address", String.Join("; ", message.To.Select(a => a.GetAddressOrDisplayName())));
            fields.Add("%Cc", message.CC.ToStringEx());
            fields.Add("%Cc.DisplayName", String.Join("; ", message.CC.Select(a => a.DisplayName)));
            fields.Add("%Cc.Address", String.Join("; ", message.CC.Select(a => a.GetAddressOrDisplayName())));
            fields.Add("%Subject", message.Subject);
            fields.Add("%Body", message.Body);
            fields.Add("%TextBody", message.TextBody);
            fields.Add("%HtmlBody", message.HtmlBody);
            fields.Add("%DateSent", NullableDateTimeToString(message.DateSent(), "o"));
            fields.Add("%DateReceived", NullableDateTimeToString(message.DateReceived(), "o"));
            fields.Add("%IsSigned", message.IsSigned.ToString());
            fields.Add("%MessageID", message.MessageId);
            fields.Add("%Priority", message.Priority.ToString());
            fields.Add("%Sensitivity", message.Sensitivity.ToString());
            fields.Add("%BatchNumber", batchNumber);
            fields.Add("%EmailID", emailID.ToString());
            fields.Add("%AttachmentFileName", Path.GetFileName(attachmentFile));
            fields.Add("%ErrorReason", errorreason);
            
            if (profile.ExtractMessageHeaders)
            {
                // Add Message Headers            
                foreach (var key in message.Headers.AllKeys)
                {
                    var k = String.Format("%Header.{0}", key);

                    if (fields.ContainsKey(k))
                    {
                        fields[k] += "|" + message.Headers[key];
                    }
                    else
                    {
                        fields.Add(k, message.Headers[key]);
                    }
                }
            }
        }
        private String NullableDateTimeToString(DateTime? value, String format)
        {
            if (value.HasValue)
                return value.Value.ToString(format);
            else
                return String.Empty;
        }
        private String GetAttachmentFileName(String filePart)
        {
            var file = Path.GetFileNameWithoutExtension(filePart);
            var ext = Path.GetExtension(filePart);

            for (int i = 1; ; i++)
            {
                var suffix = (i == 1) ? String.Empty : String.Format(" ({0})", i);
                var max = 259 - (attachmentPath.Length + suffix.Length + ext.Length + 1);
                var fileName = Path.Combine(attachmentPath, String.Format("{0}{1}{2}", (file.Length > max) ? file.Substring(0, max) : file, suffix, ext));

                if (!File.Exists(fileName))
                    return fileName;
            }
        }
        #endregion
    }
}
