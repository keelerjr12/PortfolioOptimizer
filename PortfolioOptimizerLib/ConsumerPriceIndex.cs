using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioOptimizerLib
{
    [Table("ConsumerPriceIndex")]
    public class ConsumerPriceIndex
    {
        [Key]
        public DateTime Date { get; set; }
        public double CPI { get; set; }
    }
}
