using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Roslyn.Compilers.CSharp;

namespace EmailImport.Conversion.Configuration
{
    [Serializable]
    public class MailboxProfile
    {
        #region Public Properties

        [XmlIgnore]
        public Boolean IsActive
        {
            get
            {
                if (!EnableScheduler)
                    return Enabled;

                var now = DateTime.Now;

                if (WeekdayScheduleOnly && (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday))
                    return Enabled;

                int index = (now.Hour * 2) + (now.Minute >= 30 ? 1 : 0);

                return Enabled && Schedule[index];
            }
        }

        [XmlIgnore]
        public Boolean ExtractMessageHeaders
        {
            get
            {
                if (BatchFields != null && BatchFields.Any(f => f.Value != null && f.Value.StartsWith("%Header.")))
                    return true;

                if (IndexFields != null && IndexFields.Any(f => f.Value != null && f.Value.StartsWith("%Header.")))
                    return true;

                return false;
            }
        }

        [XmlIgnore]
        public String OriginalSerializedObject { get; set; }
        
        #endregion

        #region Constructor

        public MailboxProfile()
        {
            MailboxGUID = Guid.NewGuid();
            Priority = 5;

            ImapPort = 143;
            ImapFolder = "Inbox";
            ImapRetention = 14;

            Schedule = new Boolean[48];

            RemoveBlankPages = true;
            BatchClassSubfolder = true;

            StorageRetention = 64;
            ArchiveRetention = 64;

            BitDepth = 1;
            Resolution = 200;
            BatchStyle = BatchStyle.MultiDocument;
            BodyConversion = BodyConversion.Always;
            BodyPosition = BodyPosition.First;
            AttachmentConversion = AttachmentConversion.Always;
            RetainNativeFiles = RetainNativeFileOptions.All;
            BinarisationAlgorithm = BinarisationAlgorithm.Default;

            TimeBetweenRetries = TimeSpan.FromMinutes(10);
            MaximumRetries = 2;
            ErrorHandling = new ErrorHandling();
            ForwardAsAttachment = true;

            BatchFields = new List<Field>();
            IndexFields = new List<Field>();
            FileTypes = new List<FileType>();
            PdfConversion = PdfConversion.Apose;
            ErrorBatchFields = new List<Field>();
        }

        #endregion

        #region General Settings

        public Guid MailboxGUID { get; set; }
        public Boolean Enabled { get; set; }
        public String Group { get; set; }
        public String Description { get; set; }
        public int Priority { get; set; }
        public Boolean LiteViewerEnabled { get; set; }
        #endregion

        #region IMAP Collector

        public String ImapHost { get; set; }
        public int ImapPort { get; set; }
        public String ImapUserName { get; set; }
        public String ImapPassword { get; set; }
        public String ImapFolder { get; set; }
        public String ImapQuery { get; set; }
        public int ImapRetention { get; set; }

        #endregion

        #region Output Settings

        public Boolean Zip { get; set; }
        public Boolean BatchClassSubfolder { get; set; }
        public String BatchNumberFormat { get; set; }
        public String OutputFolderFormat { get; set; }
        public String OutputPath { get; set; }
        public String ArchivePath { get; set; }
        public String StoragePath { get; set; }
        public int StorageRetention { get; set; }
        public int ArchiveRetention { get; set; }
       
        #endregion

        #region Schedule

        public Boolean[] Schedule { get; set; }
        public Boolean EnableScheduler { get; set; }
        public Boolean WeekdayScheduleOnly { get; set; }

        #endregion

        #region Kofax Specific

        public Boolean AutomaticSeparationAndFormID { get; set; }
        public Boolean SingleDocumentProcessing { get; set; }
        public String BatchClassName { get; set; }
        public String FolderClassName { get; set; }
        public String DocumentClassName { get; set; }
        public String FormTypeName { get; set; }

        #endregion

        #region Conversion Options

        public Boolean RemoveBlankPages { get; set; }
        public Boolean RemoveFaxHeader { get; set; }
        public int BitDepth { get; set; }
        public int Resolution { get; set; }
        public BatchStyle BatchStyle { get; set; }
        public BodyConversion BodyConversion { get; set; }
        public BodyPosition BodyPosition { get; set; }
        public AttachmentConversion AttachmentConversion { get; set; }
        public RetainNativeFileOptions RetainNativeFiles { get; set; }
        public BinarisationAlgorithm BinarisationAlgorithm { get; set; }

        #endregion

        #region Pdf Conversion Options

        public PdfConversion PdfConversion { get; set; }

        #endregion

        #region Error Handling & Notifications

        private TimeSpan timeBetweenRetries;

        [XmlIgnore]
        public TimeSpan TimeBetweenRetries 
        { 
            get { return timeBetweenRetries; }
            set { timeBetweenRetries = value; }
        }

        [XmlElement(ElementName = "TimeBetweenRetries")]
        public long TimeBetweenRetriesTicks
        {
            get { return timeBetweenRetries.Ticks; }
            set { timeBetweenRetries = new TimeSpan(value); }
        }
        
        public int MaximumRetries { get; set; }
        public ErrorHandling ErrorHandling { get; set; }
        public Boolean ForwardAsAttachment { get; set; }
        public String EscalationEmail { get; set; }

        public String TemplateFrom { get; set; }
        public String TemplateTo { get; set; }
        public String TemplateCc { get; set; }
        public String TemplateBcc { get; set; }
        public String TemplateSubject { get; set; }
        public String TemplateBodyHtml { get; set; }
        public Boolean ProcessErrorBatch { get; set; }
        public String ErrorOutputPath { get; set; }
        #endregion

        #region Scripting

        public String Script { get; set; }

        [XmlIgnore]
        public IEnumerable<String> ScriptEntryPoints { get; private set; }

        public void ParseScript()
        {
            if (String.IsNullOrWhiteSpace(Script))
                return;

            SyntaxTree tree = SyntaxTree.ParseText(Script);

            ScriptEntryPoints = tree.GetRoot().Members.OfType<MethodDeclarationSyntax>().Select(m => m.Identifier.ValueText);
        }

        #endregion

        #region Collections

        public List<Field> BatchFields { get; set; }
        public List<Field> IndexFields { get; set; }
        public List<FileType> FileTypes { get; set; }
        public List<Field> ErrorBatchFields { get; set; }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return String.Format("{0} - {1}", Group, Description);
        }

        public Boolean HasChanged()
        {
            if (String.IsNullOrWhiteSpace(OriginalSerializedObject))
                return true;

            XmlSerializer serializer = new XmlSerializer(typeof(MailboxProfile));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.NewLineHandling = NewLineHandling.Entitize;

            using (TextWriter textWriter = new StringWriter())
            using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
            {
                serializer.Serialize(xmlWriter, this);
                return (textWriter.ToString() != OriginalSerializedObject);
            }
        }

        #endregion
    }
}
