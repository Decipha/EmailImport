using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using EmailImport.Conversion;
using EmailImport.Conversion.Configuration;
using EmailImport.DataLayer;
using BitFactory.Logging;

namespace EmailImport
{
    static class Settings
    {
        #region Properties

        public static TimeSpan ImapCollectorInterval { get; private set; }
        public static TimeSpan EmailMonitorInterval { get; private set; }
        public static int ConcurrencyLevel { get; private set; }
        public static String DefaultStoragePath { get; private set; }
        public static String DefaultEscalationEmail { get; private set; }
        public static int SmtpSizeLimit { get; private set; }
        public static String LiteViewerAuthorisedPCs { get; private set; }

        public static Dictionary<Guid, MailboxProfile> MailboxProfiles { get; private set; }

        #endregion

        #region Constructor

        static Settings()
        {
            #region General Settings

            using (var ctx = new EmailImportDataContext())
            {
                try
                {
                    ImapCollectorInterval = TimeSpan.Parse(ctx.Settings.Single(s => s.Name == "Interval.ImapCollector").Value);
                }
                catch
                {
                    ImapCollectorInterval = TimeSpan.FromMinutes(5);
                }

                try
                {
                    EmailMonitorInterval = TimeSpan.Parse(ctx.Settings.Single(s => s.Name == "Interval.EmailMonitor").Value);
                }
                catch
                {
                    EmailMonitorInterval = TimeSpan.FromMinutes(1);
                }

                try
                {
                    ConcurrencyLevel = int.Parse(ctx.Settings.Single(s => s.Name == "ConcurrencyLevel").Value);
                }
                catch
                {
                    ConcurrencyLevel = Environment.ProcessorCount;
                }

                try
                {
                    SmtpSizeLimit = int.Parse(ctx.Settings.Single(s => s.Name == "SmtpSizeLimit").Value) * 1024 * 1024;
                }
                catch
                { }

                try
                {
                    LiteViewerAuthorisedPCs = ctx.Settings.Single(s => s.Name == "LiteViewerAuthorisedPCs").Value;
                }
                catch
                {
                    LiteViewerAuthorisedPCs = null;
                }

                if (SmtpSizeLimit <= 0)
                    SmtpSizeLimit = int.MaxValue;

                var setting = ctx.Settings.SingleOrDefault(s => s.Name == "DefaultStoragePath");
                DefaultStoragePath = (setting == null) ? null : setting.Value;
                FileSystemHelper.CreateDirectory(DefaultStoragePath);

                setting = ctx.Settings.SingleOrDefault(s => s.Name == "DefaultEscalationEmail");
                DefaultEscalationEmail = (setting == null) ? null : setting.Value;

                if (String.IsNullOrWhiteSpace(DefaultEscalationEmail))
                    throw new Exception("DefaultEscalationEmail setting must contain a valid email address.");
            }

            #endregion

            #region Mailboxes

            MailboxProfiles = new Dictionary<Guid, MailboxProfile>();
            LoadMailboxProfiles();

            #endregion
        }

        #endregion

        #region Load Mailbox Profiles

        public static void LoadMailboxProfiles()
        {
            MailboxProfiles.Clear();

            XmlSerializer serializer = new XmlSerializer(typeof(MailboxProfile));

            using (var ctx = new EmailImportDataContext())
            {
                foreach (var mailbox in ctx.Mailboxes)
                {
                    using (TextReader reader = new StringReader(mailbox.ProfileObject))
                    {
                        var profile = (MailboxProfile)serializer.Deserialize(reader);

                        if (profile.Enabled)
                        {
                            try
                            {
                                profile.ParseScript();
                            }
                            catch (Exception e)
                            {
                                ConfigLogger.Instance.LogError(e);
                            }

                            MailboxProfiles.Add(mailbox.MailboxGUID, profile);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
