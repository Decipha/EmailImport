using System;
using Inlite.ClearImageNet;

namespace EmailImport.Conversion
{
    public class PageInfo
    {
        public String FileName { get; set; }
        public String RelativePath { get; set; }
        public Boolean IsBlank { get; set; }
        public Double Skew { get; set; }
        public PageRotation Rotation { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Area
        {
            get { return Width * Height; }
        }
    }
}
