using System;

namespace POLib.SECScraper
{
    public class DailyStockPrice
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }
    }
}
