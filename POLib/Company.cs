using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POLib.SECScraper
{
    [Table("Company")]
    public class Company
    {
        public Guid Id { get; }

        public string Ticker { get; }

        public string Name { get; }

        public int SectorId { get; }

        public int CIK { get; }

        public Company(Guid id, string ticker, string name, int sectorId, int cik)
        {
            Id = id;
            Ticker = ticker;
            Name = name;
            SectorId = sectorId;
            CIK = cik;
        }
    }
}
