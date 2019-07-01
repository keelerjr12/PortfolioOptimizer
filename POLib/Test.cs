using System;
using CsvHelper.Configuration.Attributes;

namespace POLib.SECScraper
{
    class Test
    {
        [Name("Quarter end")]
        public DateTime QuarterEnd { get; set; }

        [Name("Split factor")]
        public double? SplitFactor { get; set; }

        [Name("EPS diluted")]
        public string? EPS_Diluted { get; set; }
    }
}
