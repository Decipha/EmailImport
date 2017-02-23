namespace EmailImport.Conversion.Configuration
{
    public class ErrorHandling
    {
        public ErrorHandlingActions Unsupported { get; set; }
        public ErrorHandlingActions Unprocessable { get; set; }
        public ErrorHandlingActions Unknown { get; set; }

        public ErrorHandling()
        {
            Unsupported = ErrorHandlingActions.Reject;
            Unprocessable = ErrorHandlingActions.Reject;
            Unknown = ErrorHandlingActions.Escalate;
        }
    }
}
