using System;
using System.Linq;
using System.Windows.Forms;

namespace EmailImport.Viewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            switch (args.FirstOrDefault())
            {
                case "-f":
                case "-full":
                    Application.Run(new MainForm(false));
                    break;

                default:
                    Application.Run(new MainForm(true));
                    break;
            }
        }
    }
}
