using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using EmailImport.Conversion;

namespace MdiConverter
{
    [Export(typeof(IConverter))]
    [ExportMetadata("Format", "MDI", IsMultiple = true)]
    class MdiConverter : IConverter
    {
        public void Convert(String inputFile, ConversionOptions options)
        {
            Object oMissing = Type.Missing;
            MODI.Document oMdiDoc = null;

            try
            {
                // Create an instance on a MODI document
                oMdiDoc = new MODI.Document();

                // Open the input file
                oMdiDoc.Create(inputFile);

                // For some unknown reason I need to do this otherwise pages 2+
                // will print tiny, shrunk pages in the top left corner, its either
                // this or calling SaveAs, this is quicker and easier...
                for (int i = 0; i < oMdiDoc.Images.Count; i++)
                {
                    var image = (MODI.Image)oMdiDoc.Images[i];
                    int tmp = image.PixelHeight;
                    Marshal.ReleaseComObject(image);
                }

                using (var printSession = new TiffImagePrinter.PrintSession(options))
                {
                    try
                    {
                        oMdiDoc.PrintOut(0, -1, 1, printSession.PrinterName, "", false, MODI.MiPRINT_FITMODES.miPRINT_PAGE);
                    }
                    catch
                    {
                        printSession.Cancel();
                        throw;
                    }

                    // Wait for the jobs to start spooling to the printer
                    // TODO: make timeout parameterised
                    if (printSession.WaitForJobsSpooling())
                    {
                        // Now that its started spooling, wait for the job to complete
                        // Don't need to do this as MODI can be closed, this is to ensure the job
                        // completes successfully before returning control to the main thread
                        if (!printSession.WaitForJobsCompleted())
                        {
                            printSession.Cancel();
                            throw new TimeoutException("Timeout waiting for print job to complete.");
                        }
                    }
                    else
                    {
                        // The document did not enter the print queue so we need to cancel this 
                        // printSession to allow the printer to return to the printer pool.
                        printSession.Cancel();

                        // Throw a TimeoutException
                        throw new TimeoutException("Timeout waiting for print job to spool.");
                    }
                }
            }
            finally
            {
                try
                {
                    if (oMdiDoc != null)
                    {
                        oMdiDoc.Close(false);
                        oMdiDoc = null;
                    }
                }
                catch 
                {
                    // TODO: Log the error here...
                }
            }

            // Force a Garbage Collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
