using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Text;

namespace EmailImport
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            if (Program.EnableCollect && !Program.EnableProcess)
            {
                serviceInstaller1.ServiceName += " - Collect";
                serviceInstaller1.DisplayName += " - Collect";
            }
            else if (Program.EnableProcess && !Program.EnableCollect)
            {
                serviceInstaller1.ServiceName += " - Process";
                serviceInstaller1.DisplayName += " - Process";
            }
        }

        public override void Install(IDictionary stateSaver)
        {
            if (Program.EnableCollect == false || Program.EnableProcess == false)
            {
                var path = new StringBuilder(Context.Parameters["assemblypath"]);

                if (path[0] != '"')
                {
                    path.Insert(0, '"');
                    path.Append('"');
                }

                path.Append(Program.EnableCollect ? " -collect" : " -process");

                Context.Parameters["assemblypath"] = path.ToString();
            }

            base.Install(stateSaver);
        }
    }
}
