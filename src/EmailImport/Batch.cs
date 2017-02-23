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
    class Batch
    {
        #region Fields

        private long emailID;
        private String attachmentPath, attachmentFile, batchNumber;
        private Dictionary<String, String> fields;
        private MailboxProfile profile;

        #endregion

        #region Public Properties

        public String OutputPath { get; private set; }
        public List<Document> Documents { get; private set; }

        #endregion

        #region Private Properties

        private int NextIndex
        {
            get { return Documents.Count() + 1; }
        }

        private String TempPath
        {
            get
            {
                String path = Path.Combine(Path.Combine(OutputPath, "temp"), NextIndex.ToString());

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }

        private String SaveToPath
        {
            get
            {
                String path = Path.Combine(OutputPath, String.Format("{0:000}", NextIndex));

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                return path;
            }
        }

        #endregion

        #region Constructor

        public Batch(Email email, MailMessage message, MailboxProfile profile)
        {
            Documents = new List<Document>();

            this.emailID = email.EmailID;
            this.batchNumber = email.BatchNumber;
            this.profile = profile;

            var folder = String.Format(String.IsNullOrWhiteSpace(profile.OutputFolderFormat) ? "{0:00000000}" : profile.OutputFolderFormat, emailID);

            if (profile.BatchStyle == BatchStyle.ImagesOnly || !profile.BatchClassSubfolder)
                OutputPath = Path.Combine(profile.OutputPath, folder);
            else
                OutputPath = Path.Combine(profile.OutputPath, String.Format(@"{0}\{1}", profile.BatchClassName, folder));

            FileSystemHelper.CleanDirectory(OutputPath, true);

            SaveMessage(email);
            InitialiseFields(message);
            ExtractDocuments(message, false);

            FileSystemHelper.DeleteDirectory(Path.Combine(OutputPath, "temp"), true);
        }

        #endregion

        #region Public Methods

        public void WaitOnComplete()
        {
            while (Documents.Any(d => d.Complete == false))
            {
                Thread.Sleep(250);
            }
        }

        private void RepositionBody()
        {
            Document attachment = null;

            // Nothing to do if there is only 1 document or BodyPosition is set to First
            if (profile.BodyPosition == BodyPosition.First || Documents.Count <= 1)
                return;

            // Get the body document
            var body = Documents.FirstOrDefault(d => d.Source == DocumentSource.Body);

            // If there is no body, return
            if (body == null)
                return;
            
            // Otherwise, do the repositioning
            switch (profile.BodyPosition)
            {
                case BodyPosition.Last:
                    Documents.Remove(body);
                    Documents.Add(body);

                    break;

                case BodyPosition.FirstAttachmentAppend:
                    attachment = Documents.FirstOrDefault(d => d.Source != DocumentSource.Body);

                    if (attachment != null)
                    {
                        Documents.Remove(body);
                        attachment.Pages.AddRange(body.Pages);
                    }

                    break;

                case BodyPosition.FirstAttachmentPrepend:
                    attachment = Documents.FirstOrDefault(d => d.Source != DocumentSource.Body);

                    if (attachment != null)
                    {
                        Documents.Remove(body);
                        attachment.Pages.InsertRange(0, body.Pages);
                    }

                    break;

                case BodyPosition.LastAttachmentAppend:
                    attachment = Documents.LastOrDefault(d => d.Source != DocumentSource.Body);

                    if (attachment != null)
                    {
                        Documents.Remove(body);
                        attachment.Pages.AddRange(body.Pages);
                    }

                    break;

                case BodyPosition.LastAttachmentPrepend:
                    attachment = Documents.LastOrDefault(d => d.Source != DocumentSource.Body);

                    if (attachment != null)
                    {
                        Documents.Remove(body);
                        attachment.Pages.InsertRange(0, body.Pages);
                    }

                    break;

                case BodyPosition.AllAttachmentsAppend:
                    Documents.Remove(body);

                    foreach (var att in Documents)
                    {
                        att.Pages.AddRange(body.Pages);
                    }

                    break;

                case BodyPosition.AllAttachmentsPrepend:
                    Documents.Remove(body);

                    foreach (var att in Documents)
                    {
                        att.Pages.InsertRange(0, body.Pages);
                    }

                    break;
            }
        }

        public void CreateOutput()
        {
            RepositionBody();

            switch (profile.BatchStyle)
            {
                case BatchStyle.ImagesOnly:
                    CreateImagesOnly();
                    break;

                case BatchStyle.LoosePages:
                case BatchStyle.MultiDocument:
                case BatchStyle.SingleDocument:
                    CreateBatchXml();
                    break;

                default:
                    throw new NotImplementedException(String.Format("{0} batch style not implemented.", profile.BatchStyle));
            }
        }

        #endregion

        #region Private Methods

        #region Other Methods

        private void SaveMessage(Email email)
        {
            attachmentPath = Path.Combine(OutputPath, "Attachments");
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

        #endregion

        #region Extract Documents

        private void ExtractDocuments(MailMessage message, Boolean isEmbedded)
        {
            // If the message is digitally signed, remove the signature attachment
            if (message.IsSigned)
            {
                for (int i = message.Attachments.Count - 1; i >= 0; i--)
                {
                    if (message.Attachments[i].ContentType.MediaType.ToLower() == "application/pkcs7-signature")
                    {
                        message.Attachments.RemoveAt(i);
                    }
                }
            }

            // Add the message body (except if config say not to)
            if (profile.BodyConversion == BodyConversion.Always ||
                profile.BodyConversion == BodyConversion.OnBatchEmpty ||
                (profile.BodyConversion == BodyConversion.EmbeddedOnly && isEmbedded))
            {
                Documents.Add(new Document(GetBody(message), "Body.mht", SaveToPath, batchNumber, emailID, isEmbedded ? DocumentSource.EmbeddedBody : DocumentSource.Body));
            }

            // Extract all of the attachments if AttachmentConversion is not Never
            // (inline attachments are not included in this collection)
            if (profile.AttachmentConversion != AttachmentConversion.Never)
            {
                ExtractAttachments(message.Attachments);
            }
        }

        private void ExtractAttachments(AttachmentCollection attachments)
        {
            // Add all of the attachments           
            foreach (Attachment attachment in attachments)
            {
                // If the attachment is an embedded MailMessage, process it
                if (attachment.IsEmbeddedMessage())
                {
                    var options = new MailMessageLoadOptions()
                    {
                        FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking,
                        MessageFormat = MessageFormat.Eml
                    };

                    ExtractDocuments(attachment.GetEmbeddedMessage(options), true);

                    continue;
                }

                // Get the safe filename
                var safeFile = attachment.GetSafeFileName();

                // Get the filetype (if exists)
                var fileType = profile.FileTypes.FirstOrDefault(f => Regex.IsMatch(safeFile, f.Pattern??string.Empty, RegexOptions.IgnoreCase));

                // Ignore the attachment if specified
                if (fileType != null && fileType.Ignore.GetValueOrDefault())
                    continue;

                // Extract archives if not set to passthrough
                if (fileType == null || fileType.Passthrough.GetValueOrDefault() == false)
                {
                    // Get the extension (either actual or process as extension)
                    var extension = Path.GetExtension(safeFile).TrimStart('.').ToLower();

                    // If the attachment is a zip file then extract each of the attachments
                    if (extension == "zip")
                    {
                        ExtractZip(attachment.ContentStream, safeFile);
                        continue;
                    }

                    // TODO: ADD SUPPORT FOR RAR & 7ZIP

                    // If the attachment is an embedded email, load and extract it
                    if (extension == "eml" || extension == "msg")
                    {
                        ExtractMessage(attachment.ContentStream, safeFile, extension);
                        continue;
                    }
                }

                // Otherwise, save the attachment
                String file = GetAttachmentFileName(safeFile);
                attachment.Save(file);
                File.SetAttributes(file, FileAttributes.Normal);
                Documents.Add(new Document(file, safeFile, SaveToPath, batchNumber, emailID, DocumentSource.Attachment));
            }
        }

        private String GetZipTempPath(String basePath)
        {
            for (int i = 1; ; i++)
            {
                var temp = Path.Combine(basePath, String.Format("Zip_{0:00}", i));

                if (!Directory.Exists(temp))
                    return temp;
            }
        }

        private void CleanZipExtract(String path)
        {
            // Check for __MACOSX folder and delete it
            if (Directory.Exists(Path.Combine(path, "__MACOSX")))
                Directory.Delete(Path.Combine(path, "__MACOSX"), true);

            // Check for Thumbs.db and delete them
            foreach (var file in Directory.GetFiles(path, "Thumbs*.db", SearchOption.AllDirectories))
            {
                if (FileSignatures.IsThumbsDb(file))
                    File.Delete(file);
            }
        }

        private void ExtractZip(Stream stream, String safeFile)
        {
            var zipTemp = GetZipTempPath(TempPath);

            try
            {
                using (ZipFile zip = ZipFile.Read(stream))
                {
                    zip.ExtractAll(zipTemp, ExtractExistingFileAction.OverwriteSilently);
                }
            }
            catch (Exception e)
            {
                String file = GetAttachmentFileName(safeFile);

                using (var fs = File.Create(file))
                {
                    Byte[] buffer = new Byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, buffer.Length);
                }

                var document = new Document(file, safeFile, SaveToPath, batchNumber, emailID, DocumentSource.Attachment);

                document.ErrorMessage = e.ToString();
                document.FailureReason = (e is BadPasswordException) ? "Zip file is password protected." : "Unable to extract zip file.";
                document.ActionToTake = profile.ErrorHandling.Unprocessable;

                Documents.Add(document);

                return;
            }

            // Clean-up the zip extract path of unwanted files
            CleanZipExtract(zipTemp);

            // Process the extracted files
            foreach (String fileName in Directory.GetFiles(zipTemp, "*.*", SearchOption.AllDirectories))
            {
                String filePart = Path.GetFileName(fileName);

                File.SetAttributes(fileName, FileAttributes.Normal);

                // Get the filetype (if exists)
                var fileType = profile.FileTypes.FirstOrDefault(f => Regex.IsMatch(filePart, f.Pattern??string.Empty, RegexOptions.IgnoreCase));

                // Ignore the attachment if specified
                if (fileType != null && fileType.Ignore.GetValueOrDefault())
                    continue;

                // Get the filenames extension
                var extension = Path.GetExtension(filePart).TrimStart('.').ToLower();

                // If it's another zip file, extract it
                if (extension == "zip")
                {
                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(fileName)))
                    {
                        ExtractZip(ms, Path.GetFileName(fileName));
                    }

                    continue;
                }

                // TODO: ADD SUPPORT FOR RAR & 7ZIP

                // If the file is an email, load and extract it
                if (extension == "eml" || extension == "msg")
                {
                    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(fileName)))
                    {
                        ExtractMessage(ms, Path.GetFileName(fileName), extension);
                    }

                    continue;
                }

                String file = GetAttachmentFileName(filePart);
                File.Copy(fileName, file);
                Documents.Add(new Document(file, filePart, SaveToPath, batchNumber, emailID, DocumentSource.Attachment));
            }
        }

        private void ExtractMessage(Stream stream, String safeFile, String format)
        {
            MailMessage message = null;

            try
            {
                var options = new MailMessageLoadOptions();
                options.FileCompatibilityMode = FileCompatibilityMode.SkipValidityChecking;

                switch (format)
                {
                    case "eml":
                        options.MessageFormat = MessageFormat.Eml;
                        break;

                    case "msg":
                        options.MessageFormat = MessageFormat.Msg;
                        break;
                }

                message = MailMessage.Load(stream, options);
            }
            catch
            {
                String file = GetAttachmentFileName(safeFile);

                using (var fs = File.Create(file))
                {
                    Byte[] buffer = new Byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    fs.Write(buffer, 0, buffer.Length);
                }

                Documents.Add(new Document(file, safeFile, SaveToPath, batchNumber, emailID, DocumentSource.Attachment));

                return;
            }

            ExtractDocuments(message, true);
        }

        private String GetBody(MailMessage message)
        {
            // Get the attachment filename, accounting for duplicate files
            String file = GetAttachmentFileName("Body.mht");

            // Remove all MIME headers starting with x-, many of these cause issues with conversion
            // Issue to be raised to Aspose, but for now this will resolve the immediate problem
            foreach (var header in message.Headers.AllKeys)
            {
                if (header.StartsWith("x-", StringComparison.OrdinalIgnoreCase))
                    message.Headers.Remove(header);
            }

            // Get the message in MHT format and save it
            File.WriteAllText(file, message.GetMht(true));

            // Return the file path
            return file;
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

        #region Create Output

        private void CreateBatchXml()
        {
            // Add BatchCreationDateTime field
            fields.Add("%BatchCreationDateTime", DateTime.Now.ToString("s"));

            // Create the xml batch
            var xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                      new XElement("Batch",
                         GetBatchAttributes(),
                         GetFields("BatchField", profile.BatchFields, null),
                         GetContent()
                         ));

            var tmp = Path.Combine(OutputPath, String.Format("{0}.tmp", Path.GetFileName(OutputPath)));

            xml.Save(tmp);

            if (profile.RetainNativeFiles == RetainNativeFileOptions.None)
                Directory.Delete(attachmentPath, true);

            if (profile.Zip)
            {
                File.Delete(OutputPath + ".zip");

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncoding = Encoding.UTF8;
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.ParallelDeflateThreshold = -1;

                    zip.AddDirectory(OutputPath);

                    var filePart = Path.GetFileName(tmp);
                    var entry = zip.Single(z => z.FileName == filePart);
                    entry.FileName = Path.ChangeExtension(entry.FileName, "xml");

                    zip.Save(OutputPath + ".tmp");
                }

                Directory.Delete(OutputPath, true);
                File.Move(OutputPath + ".tmp", OutputPath + ".zip");
            }
            else
            {
                File.Move(tmp, Path.ChangeExtension(tmp, ".xml"));
            }
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
                return new XAttribute("attachment", Path.Combine(Path.GetFileName(attachmentPath), Path.GetFileName(attachmentFile)));

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

                if (!String.IsNullOrEmpty(field.Regex))
                {
                    value = DoRegex(field.Regex, value);
                }

                var element = new XElement(elementName,
                                  new XAttribute("name", field.Name),
                                  new XAttribute("value", (value == null) ? String.Empty : value));

                elements.Add(element);
            }

            return new XElement(elementName + "s", elements);
        }

        private String DoRegex(String regex, String value)
        {
            try
            {
                int index = regex.IndexOf('(');

                if (index == -1)
                    return null;

                var operation = regex.Substring(0, index).ToUpper();
                var pattern = regex.Substring(index + 1, regex.Length - index - 2);

                Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
                var matches = r.Matches(value).Cast<Match>().Select(m => m.Value);

                switch (operation)
                {
                    case "FIRST":
                        return matches.FirstOrDefault();

                    case "ALL":
                        return String.Join("|", matches.ToArray());

                    case "DISTINCT":
                        return String.Join("|", matches.Distinct().ToArray());

                    case "SINGLE":
                        return (matches.Count() == 1) ? matches.Single() : null;

                    case "ISMATCH":
                        return matches.Any().ToString();

                    case "!ISMATCH":
                        return (!matches.Any()).ToString();

                    default:
                        return null;
                }
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogWarning(emailID, e);
                return null;
            }
        }

        private XElement GetContent()
        {
            switch (profile.BatchStyle)
            {
                case BatchStyle.LoosePages:
                    return GetLoosePages();

                case BatchStyle.MultiDocument:
                case BatchStyle.SingleDocument:
                    return String.IsNullOrWhiteSpace(profile.FolderClassName) ? GetDocuments() : GetFolders();

                default:
                    return null;
            }
        }

        private XElement GetLoosePages()
        {
            return new XElement("Pages",
                       from document in Documents
                       from page in document.Pages
                       select new XElement("Page",
                           new XAttribute("fileName", page.RelativePath)
                           )
                       );
        }

        private XElement GetFolders()
        {
            return new XElement("Folders",
                       new XElement("Folder",
                           new XAttribute("folderClassName", profile.FolderClassName),
                           GetDocuments()
                       )
                   );
        }

        private XElement GetDocuments()
        {
            switch (profile.BatchStyle)
            {
                case BatchStyle.SingleDocument:
                    return new XElement("Documents",
                               new XElement("Document",
                                   String.IsNullOrWhiteSpace(profile.DocumentClassName) ? null : new XAttribute("documentClassName", profile.DocumentClassName),
                                   String.IsNullOrWhiteSpace(profile.FormTypeName) ? null : new XAttribute("formTypeName", profile.FormTypeName),
                                   new XAttribute("pageCount", Documents.Sum(d => d.Pages.Count)),
                                   new XElement("ImageFiles",
                                       new XElement("Pages",
                                           from document in Documents
                                           from page in document.Pages
                                           select new XElement("Page",
                                               new XAttribute("fileName", page.RelativePath)
                                               )
                                           )
                                       ),
                                   GetFields("IndexField", profile.IndexFields, null)
                                   )
                               );

                default:
                    return new XElement("Documents",
                               from document in Documents
                               select new XElement("Document",
                                   String.IsNullOrWhiteSpace(profile.DocumentClassName) ? null : new XAttribute("documentClassName", profile.DocumentClassName),
                                   String.IsNullOrWhiteSpace(profile.FormTypeName) ? null : new XAttribute("formTypeName", profile.FormTypeName),
                                   new XAttribute("pageCount", document.Pages.Count),
                                   GetDocumentAttachment(document.AttachmentFile),
                                   new XElement("ImageFiles",
                                       new XElement("Pages",
                                           from page in document.Pages
                                           select new XElement("Page",
                                               new XAttribute("fileName", page.RelativePath)
                                               )
                                           )
                                       ),
                                   GetFields("IndexField", profile.IndexFields, document.Fields)
                                   )
                               );
            }
        }

        private XAttribute GetDocumentAttachment(String fileName)
        {
            if (profile.RetainNativeFiles == RetainNativeFileOptions.Attachments || profile.RetainNativeFiles == RetainNativeFileOptions.All)
                return new XAttribute("attachment", Path.Combine(Path.GetFileName(attachmentPath), Path.GetFileName(fileName)));

            File.Delete(fileName);
            return null;
        }

        private void CreateImagesOnly()
        {
            foreach (var document in Documents)
            {
                var dest = Path.Combine(profile.OutputPath, String.Format("{0:00000000}_{1}.tif", emailID, document.RelativePath));
                var temp = Path.ChangeExtension(dest, "tmp");

                File.Delete(dest);

                if (document.Pages.Count == 1)
                {
                    File.Copy(document.Pages.Single().FileName, temp, true);
                }
                else
                {
                    File.Delete(temp);

                    ImageProcessingEngine.Instance.Concat(temp, document.Pages);
                }

                File.Move(temp, dest);
            }

            try
            {
                FileSystemHelper.DeleteDirectory(OutputPath);
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogWarning(emailID, e);
            }
        }

        #endregion

        #endregion
    }
}
