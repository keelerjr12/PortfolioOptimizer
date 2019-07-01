using System;

namespace POLib.SECScraper
{
    public class DailyStockPrice
    {
        public string Ticker { get; }

        public DateTime Date { get; }

        public decimal Price { get; }

        public DailyStockPrice(string ticker, DateTime date, decimal price)
        {
            Ticker = ticker;
            Date = date;
            Price = price;
        }
    }
}
