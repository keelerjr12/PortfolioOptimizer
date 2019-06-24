using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POLib.SECScraper
{
    [Table("Company")]
    public class Company
    {
        public Guid Id { get; set; }

        public string Ticker { get; set; }

        public string Name { get; set; }

        public int SectorId { get; set; }

        public int CIK { get; set; }
    }
}
