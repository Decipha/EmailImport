using BitFactory.Logging;
using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace EmailImport
{
    static class Program
    {
        public static Boolean EnableCollect;
        public static Boolean EnableProcess;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(String[] args)
        {
            ParseStartArguments(args);

            var service = new EmailImport();

            if (Environment.UserInteractive)
            {
                ConfigureConsoleLogger(args);

                try
                {
                    switch (args.FirstOrDefault())
                    {
                        case "-i":
                        case "-install":
                            ManagedInstallerClass.InstallHelper(new String[] { Assembly.GetExecutingAssembly().Location });
                            break;

                        case "-u":
                        case "-uninstall":
                            ManagedInstallerClass.InstallHelper(new String[] { "/u", Assembly.GetExecutingAssembly().Location });
                            break;

                        case "-c":
                        case "-console":
                            service.GetType().InvokeMember("OnStart", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, service, new Object[] { null });
                            Console.WriteLine("Press any key to stop service...");
                            Console.ReadKey(true);
                            service.GetType().InvokeMember("OnStop", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, service, null);
                            break;
                    }
                }
                catch (Exception e)
                {
                    ConfigLogger.Instance.LogError(e);
                }
            }
            else
            {
                ServiceBase.Run(service);
            }
        }

        private static void ParseStartArguments(String[] args)
        {
            if (args != null)
            {
                foreach (var arg in args)
                {
                    switch (arg.ToLower())
                    {
                        case "-collect":
                            EnableCollect = true;
                            break;

                        case "-process":
                            EnableProcess = true;
                            break;
                    }
                }
            }

            if (EnableCollect == false && EnableProcess == false)
            {
                EnableCollect = true;
                EnableProcess = true;
            }
        }

        private static void ConfigureConsoleLogger(String[] args)
        {
            var logger = TextWriterLogger.NewConsoleLogger();
            logger.Formatter = new LogEntryFormatStringFormatter("[{category}] - {message}", null);
            logger.SeverityThreshold = (args.Contains("-d") || args.Contains("-debug")) ? LogSeverity.Debug : LogSeverity.Info;
            
            ConfigLogger.Instance.AddLogger("ConsoleLogger", logger);
        }
    }
}
