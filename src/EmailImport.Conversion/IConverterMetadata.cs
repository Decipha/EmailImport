using System;
using System.Collections.Generic;

namespace EmailImport.Conversion
{
    public interface IConverterMetadata
    {
        IList<String> Format { get; }
    }
}
