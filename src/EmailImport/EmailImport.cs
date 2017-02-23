using BitFactory.Logging;
using EmailImport.Conversion;
using System;
using System.ServiceProcess;

namespace EmailImport
{
    partial class EmailImport : ServiceBase
    {
        ImapCollector collector = null;
        EmailMonitor monitor = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailImport"/> class.
        /// </summary>
        public EmailImport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                // Subscribe to the AppDomain UnhandledException handler to allow us
                // to perform any cleanup and logging before stopping the service
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                              
                // Set the ConcurrencyLevel for the ImageProcessingEngine
                ImageProcessingEngine.ConcurrencyLevel = Settings.ConcurrencyLevel;

                // Create an instance of ImapCollector for downloading of mail messages
                if (Program.EnableCollect)
                    collector = new ImapCollector();

                // Create an instance of EmailMonitor for processing of downloaded emails
                if (Program.EnableProcess)
                    monitor = new EmailMonitor();

                // Log that we have started successfully
                ConfigLogger.Instance.LogInfo(String.Format("{0} Started.", GetServiceName()));
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogFatal(e);

                throw;
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            if (collector != null)
            {
                collector.Dispose();
                collector = null;
            }

            if (monitor != null)
            {
                monitor.Dispose();
                monitor = null;
            }

            EmailConverter.WaitOnComplete();
            ImageProcessingEngine.Complete();

            ConfigLogger.Instance.LogInfo(String.Format("{0} Stopped.", GetServiceName()));
        }

        /// <summary>
        /// Gets the service name based on the start-up parameters
        /// </summary>
        /// <returns></returns>
        private String GetServiceName()
        {
            if (Program.EnableCollect && !Program.EnableProcess)
                return "Email Import - Collect";
            else if (Program.EnableProcess && !Program.EnableCollect)
                return "Email Import - Process";
            else
                return "Email Import";
        }

        /// <summary>
        /// Occurs when a custom command is issued via the SCM
        /// </summary>
        /// <param name="command"></param>
        protected override void OnCustomCommand(int command)
        {
            try
            {
                ConfigLogger.Instance.LogDebug(String.Format("Received custom command: {0}", (EmailImportServiceCommand)command));

                switch ((EmailImportServiceCommand)command)
                {
                    case EmailImportServiceCommand.ReloadMailboxProfiles:
                        try
                        {
                            // Pause the Imap Collector and Email Monitor
                            if (collector != null)
                                collector.Pause();

                            if (monitor != null)
                                monitor.Pause();

                            // Wait for all in progress conversions to complete
                            EmailConverter.WaitOnComplete();

                            // Refresh the Mailbox Profiles
                            Settings.LoadMailboxProfiles();
                        }
                        finally
                        {
                            // Resume the Imap Collector and Email Monitor
                            if (collector != null)
                                collector.Resume();

                            if (monitor != null)
                                monitor.Resume();
                        }

                        ConfigLogger.Instance.LogInfo("Mailbox Profiles Reloaded.");

                        break;
                }
            }
            catch (Exception e)
            {
                ConfigLogger.Instance.LogError(e);
            }
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the unhandled exception
            ConfigLogger.Instance.LogFatal(e.ExceptionObject);
        }
    }
}