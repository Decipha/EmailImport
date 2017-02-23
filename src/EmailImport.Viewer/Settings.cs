using System;
using System.Linq;

namespace EmailImport.Viewer
{
    static class Settings
    {
        #region Properties
        public static String LiteViewerAuthorisedPCs { get; private set; }
        #endregion

        #region Constructor

        static Settings()
        {
            using (var ctx = new EmailImportDataContext())
            {
                try
                {
                    LiteViewerAuthorisedPCs = ctx.Settings.Single(s => s.Name == "LiteViewerAuthorisedPCs").Value;
                }
                catch
                {
                    LiteViewerAuthorisedPCs = null;
                }
            }
        }

        #endregion

    }
}
