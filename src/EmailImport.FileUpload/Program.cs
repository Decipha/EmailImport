using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace EmailImport.FileUpload
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Boolean owned;

            using (var mutex = new Mutex(true, Assembly.GetExecutingAssembly().GetName().Name, out owned))
            {
                if (owned)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
                else
                {
                    MessageBox.Show("Another instance is already running.", "Email Import - File Upload", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
    }
}
