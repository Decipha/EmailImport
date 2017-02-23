using System;
using Aspose.Email.Mail;

namespace EmailImport.Conversion
{
    public class ScriptContext
    {
        public MailMessage Message { get; set; }
        public Boolean IgnoreMessage { get; set; }
    }
}
