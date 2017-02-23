using System;


namespace EmailImport.Conversion
{
    public class ImageConversionOptions
    {
        public int PageIndex { get; set; }

        public String SaveToPath { get; private set; }
        public String RelativePath { get; private set; }
        public String ProcessAs { get; set; }

        public BinarisationAlgorithm BinarisationAlgorithm { get; set; }
        public int Resolution { get; set; }
        public int BitDepth { get; set; }
        public Boolean? AutoRotate { get; set; }
        public Boolean? AutoDeskew { get; set; }
        public Boolean RemoveFaxHeader { get; set; }
        public PdfConversion PdfConversion { get; set; }

        public ImageConversionOptions(String saveToPath, String relativePath)
        {
            BinarisationAlgorithm = BinarisationAlgorithm.Default;
            Resolution = 200;
            BitDepth = 1;
            PageIndex = 0;

            SaveToPath = saveToPath;
            RelativePath = relativePath ?? String.Empty;
            PdfConversion = PdfConversion.Apose;
        }
    }
}
