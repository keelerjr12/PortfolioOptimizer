using System;

namespace POLib.SECScraper
{
    public class DailyStockPrice
    {
        public string Ticker { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public DailyStockPrice(string ticker, DateTime date, decimal price)
        {
            Ticker = ticker;
            Date = date;
            Price = price;
        }
    }
}
