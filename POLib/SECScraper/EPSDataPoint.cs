using NodaTime;

namespace POLib.SECScraper
{
    public class EPSDataPoint
    {
        public DateInterval DateInterval { get; }

        public decimal EPS { get; }

        public EPSDataPoint(LocalDate startDate, LocalDate endDate, decimal eps)
        {
            DateInterval = new DateInterval(startDate, endDate);
            EPS = eps;
        }
    }
}
