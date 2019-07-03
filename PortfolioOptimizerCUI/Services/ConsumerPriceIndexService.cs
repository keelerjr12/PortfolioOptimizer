using POLib.SECScraper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioOptimizerCUI.Services
{
    class ConsumerPriceIndexService
    {
        public ConsumerPriceIndexService(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }
        public ConsumerPriceIndex GetConsumerPriceIndex(DateTime date)
        {
            var monthAndYear = new DateTime(date.Year, date.Month, 1);
            var cpi = _financeContext?.ConsumerPriceIndex?.Find(monthAndYear);

            if (cpi == null)
                throw new Exception($"CPI for {date} does not exist");

            return cpi;
        }

        public IList<ConsumerPriceIndex> GetConsumerPriceIndex(DateTime dateFrom, DateTime dateTo)
        {
            var dateToStartOfMonth = new DateTime(dateTo.Year, dateTo.Month, 1);
            var dateFromStartOfMonth = new DateTime(dateFrom.Year, dateFrom.Month, 1);

            return _financeContext.ConsumerPriceIndex.Where(cpi => cpi.Date >= dateFromStartOfMonth && cpi.Date <= dateToStartOfMonth).ToList();
        }

        private readonly FinanceContext _financeContext;
    }
}
