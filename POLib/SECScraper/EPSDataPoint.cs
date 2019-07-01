using System;

namespace POLib.SECScraper
{
    public class EPSDataPoint
    {
        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public decimal EPS { get; }

        public EPSDataPoint(DateTime startDate, DateTime endDate, decimal eps)
        {
            StartDate = startDate;
            EndDate = endDate;
            EPS = eps;
        }
    }
}
