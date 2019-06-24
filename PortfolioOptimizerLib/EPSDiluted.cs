using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioOptimizerLib
{
    [Table("EPS_Diluted")]
    public class EPSDiluted
    {
        [Key, Column(Order = 0)]
        public string Ticker { get; set; }
        [Key, Column(Order = 1)]
        public DateTime QuarterEnd { get; set; }
        public decimal EPS { get; set; }
    }
}
