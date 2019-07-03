using POLib.SECScraper;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var eps = _financeContext?.EPSDiluted?.Find(ticker, date);
            if (eps == null)
                throw new Exception($"EPS for {ticker} and {date} does not exist");
            return eps;
        }

        public IList<EPSDiluted> GetDilutedEPS(string ticker, DateTime dateFrom, DateTime dateTo)
        {
            var dateFromStartOfMonth = new DateTime(dateFrom.Year, dateFrom.Month, 1);
            var epsList = _financeContext?.EPSDiluted.Where(eps => eps.Ticker == ticker && eps.QuarterEnd >= dateFrom && eps.QuarterEnd <= dateTo).ToList();
            return epsList ?? new List<EPSDiluted>();
        }

        private readonly FinanceContext _financeContext;
    }
}
