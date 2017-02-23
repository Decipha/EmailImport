using System;
using System.Windows.Forms;
using System.Threading;
using EmailImport.Conversion;

namespace WebConverter
{
    class WebPrinter
    {
        private String url;
        private TiffImagePrinter.PrintSession printSession;

        public Boolean Spooled { get; private set; }

        public WebPrinter(String url, TiffImagePrinter.PrintSession printSession)
        {
            Spooled = false;

            this.url = url;
            this.printSession = printSession;
        }

        public Boolean Print(int timeout)
        {
            Thread t = new Thread(new ParameterizedThreadStart(Print));
            t.SetApartmentState(ApartmentState.STA);
            t.Start(timeout);
            t.Join(timeout * 2);

            return Spooled;
        }

        private void Print(Object timeout)
        {
            using (WebBrowser browser = new WebBrowser())
            {
                browser.ScriptErrorsSuppressed = true;
                browser.Navigate(url);

                while (browser.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    Thread.Sleep(10);
                }

                if (browser.ReadyState == WebBrowserReadyState.Complete)
                {
                    browser.Print();
                    Spooled = printSession.WaitForJobsSpooled((int)timeout);
                }
                else
                {
                    browser.Stop();
                }
            }
        }
    }
}
