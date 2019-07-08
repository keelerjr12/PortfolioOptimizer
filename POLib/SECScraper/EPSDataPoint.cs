using System;
using NodaTime;

namespace POLib.SECScraper
{
    public class EPSDataPoint
    {
        public DateInterval DateInterval { get; }

        public decimal EPS { get; }

        public EPSDataPoint(DateTime startDate, DateTime endDate, decimal eps)
        {
            var startDateLD = new LocalDate(startDate.Year, startDate.Month, startDate.Day);
            var endDateLD = new LocalDate(endDate.Year, endDate.Month, endDate.Day);

            DateInterval = new DateInterval(startDateLD, endDateLD);
            EPS = eps;
        }
    }
}
