using System;
using System.Collections.Generic;

namespace EmailImport.Conversion
{
    public interface IConverter
    {
        List<PageInfo> Convert(String inputFile, ImageConversionOptions options);
    }
}
