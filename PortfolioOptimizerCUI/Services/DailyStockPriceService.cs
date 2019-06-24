﻿using System;
using POLib.SECScraper;

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
            var stockQuote = _financeContext.DailyStockPrice.Find(ticker, date);
            if (stockQuote == null)
            {
                //download here
            }

            return stockQuote.Price;
        }

        private readonly FinanceContext _financeContext;
    }
}