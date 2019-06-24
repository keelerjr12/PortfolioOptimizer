using System;
using System.Linq;

namespace PortfolioOptimizerCUI.Services
{
    class CAPEService
    {
        public CAPEService(DilutedEPSService dilutedEPSService, ConsumerPriceIndexService cpiService, DailyStockPriceService dailyStockPriceService)
        {
            _dilutedEPSService = dilutedEPSService;
            _cpiService = cpiService;
            _dailyStockPriceService = dailyStockPriceService;
        }

        public decimal GetCAPE(string ticker, DateTime dateTo)
        {
            var dateFrom = new DateTime(dateTo.Year - 10, dateTo.Month, dateTo.Day);
            var epsList = _dilutedEPSService.GetDilutedEPS(ticker, dateFrom, dateTo);
            var cpiList = _cpiService.GetConsumerPriceIndex(dateFrom, dateTo);

            var lastEPSDate = epsList.Last().QuarterEnd;
            var currCPI = cpiList.First(c => c.Date == new DateTime(lastEPSDate.Year, lastEPSDate.Month, 1)).CPI;
            Console.WriteLine("Current CPI {0}", currCPI);

            var e10Sum = 0.0;

            foreach (var eps in epsList)
            {
                var epsStartOfMonth = new DateTime(eps.QuarterEnd.Year, eps.QuarterEnd.Month, 1);
                var cpi = cpiList.First(c => c.Date == epsStartOfMonth).CPI;
                var adjEPS = decimal.ToDouble(eps.EPS) / cpi * currCPI;
                e10Sum += adjEPS;
                Console.WriteLine("DATE: {0}, EPS: {1}, CPI: {2}, ADJ_EPS: {3}",  epsStartOfMonth, eps.EPS, cpi, adjEPS);
            }

            var e10 = e10Sum / 10;
            Console.WriteLine("E10: " + e10);

            var price = _dailyStockPriceService.GetDailyPrice(ticker, dateTo);
            Console.WriteLine("Price: " + price);

            return 0;
        }

        private readonly DilutedEPSService _dilutedEPSService;
        private readonly ConsumerPriceIndexService _cpiService;
        private readonly DailyStockPriceService _dailyStockPriceService;
    }
}
