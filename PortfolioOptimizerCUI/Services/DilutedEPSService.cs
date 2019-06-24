using System;
using System.Collections.Generic;
using System.Linq;
using POLib.SECScraper;

namespace PortfolioOptimizerCUI.Services
{
    class DilutedEPSService
    {
        public DilutedEPSService(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }

        public EPSDiluted GetDilutedEPS(string ticker, DateTime date)
        {
            return _financeContext.EPSDiluted.Find(ticker, date);
        }

        public IList<EPSDiluted> GetDilutedEPS(string ticker, DateTime dateFrom, DateTime dateTo)
        {
            var dateFromStartOfMonth = new DateTime(dateFrom.Year, dateFrom.Month, 1);
            return _financeContext.EPSDiluted.Where(eps => eps.Ticker == ticker && eps.QuarterEnd >= dateFrom && eps.QuarterEnd <= dateTo).ToList();
        }

        private readonly FinanceContext _financeContext;
    }
}
