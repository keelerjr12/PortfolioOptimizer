using POLib.SECScraper;
using System;

namespace PortfolioOptimizerCUI.Services
{
    class DailyStockPriceService
    {
        public DailyStockPriceService(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }
        public decimal GetDailyPrice(string ticker, DateTime date)
        {
            var stockQuote = _financeContext?.DailyStockPrice?.Find(ticker, date);
            if (stockQuote == null)
            {
                stockQuote = new DailyStockPrice("TEST", DateTime.Now, 10);
            }

            return stockQuote.Price;
        }

        private readonly FinanceContext _financeContext;
    }
}
