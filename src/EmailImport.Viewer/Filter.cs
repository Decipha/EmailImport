using System;

namespace EmailImport.Viewer
{
    public enum FilterType
    {
        None,
        DateSent,
        DateReceived,
        Status,
        BatchNumber,
        From,
        Subject,
        MessageID,
        EmailID
    }

    public class Filter
    {
        public FilterType Type { get; set; }

        public DateTime Date1 { get; set; }
        public DateTime Date2 { get; set; }
        public string StringValue { get; set; }
        public long LongValue { get; set; }

        public Filter() : this(FilterType.None)
        { }

        public Filter(FilterType type)
        {
            Type = type;
        }
    }
}
