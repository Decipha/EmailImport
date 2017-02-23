using System;
using System.ComponentModel.Composition;
using System.IO;
using EmailImport.Conversion;

namespace WebConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "XML", IsMultiple = true)]
    class WebConverter : IConverter
    {
        public void Convert(String inputFile, ConversionOptions options)
        {
            using (var printSession = new TiffImagePrinter.PrintSession(options))
            {
                if (printSession.SetDefaultPrinter())
                {
                    var url = Path.ChangeExtension(inputFile, options.Extension);

                    try
                    {
                        File.Copy(inputFile, url, true);

                        WebPrinter printer = new WebPrinter(url, printSession);

                        if (printer.Print(TimeoutValues.JobSpooled))
                        {
                            // Now that its started spooling, wait for the job to complete
                            // Don't need to do this as the Web Browser can be closed, this is to ensure the job
                            // completes successfully before returning control to the main thread
                            if (!printSession.WaitForJobsCompleted())
                            {
                                printSession.Cancel();
                                throw new TimeoutException("Timeout waiting for print job to complete.");
                            }

                        }
                        else
                        {
                            printSession.Cancel();
                            throw new TimeoutException("Timeout waiting for print job to spool.");
                        }
                    }
                    finally
                    {
                        File.Delete(url);
                    }
                }
                else
                {
                    printSession.Cancel();
                    throw new Exception("Unable to set the system default printer.");
                }
            }
        }
    }   
}
