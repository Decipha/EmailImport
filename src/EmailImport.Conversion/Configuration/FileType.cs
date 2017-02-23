using System;

namespace EmailImport.Conversion.Configuration
{
    public class FileType
    {
        public String Pattern { get; set; }
        public String ProcessAs { get; set; }
        public Boolean? Ignore { get; set; }
        public Boolean? Passthrough { get; set; }
        public Boolean? AutoDeskew { get; set; }
        public Boolean? AutoRotate { get; set; }
        public BinarisationAlgorithm? BinarisationAlgorithm { get; set; }
        public int? BitDepth { get; set; }
        public int? MinPixels { get; set; }
    }
}
