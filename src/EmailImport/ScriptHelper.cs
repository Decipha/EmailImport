using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;
using Aspose.Email.Mail;
using EmailImport.Conversion;
using EmailImport.Conversion.Configuration;
using BitFactory.Logging;

namespace EmailImport
{
    public sealed class ScriptHelper
    {
        private static readonly Lazy<ScriptHelper> lazy = new Lazy<ScriptHelper>(() => new ScriptHelper());

        public static ScriptHelper Instance { get { return lazy.Value; } }

        private ScriptEngine engine = null;

        private ScriptHelper()
        {
            engine = new ScriptEngine();

            engine.AddReference("System");
            engine.AddReference("System.Core");
            engine.AddReference(typeof(MailMessage).Assembly);
            engine.AddReference(typeof(ScriptContext).Assembly);
        }

        public Session CreateSession(ScriptContext context)
        {
            lock (engine)
            {
                return engine.CreateSession(context);
            }
        }

        public Object Execute(Session session, MailboxProfile profile, String methodName)
        {
            if (session == null || String.IsNullOrWhiteSpace(methodName))
                return null;

            if (profile.ScriptEntryPoints == null || !profile.ScriptEntryPoints.Contains(methodName))
                return null;

            return session.Execute(String.Format("{0}();", methodName));
        }

        public Boolean ContainsMethod(Session session, MailboxProfile profile, String methodName)
        {
            if (session == null || String.IsNullOrWhiteSpace(methodName))
                return false;

            if (profile.ScriptEntryPoints == null || !profile.ScriptEntryPoints.Contains(methodName))
                return false;

            return true;
        }
    }
}
