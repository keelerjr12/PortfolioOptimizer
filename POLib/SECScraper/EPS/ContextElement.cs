using System.Linq;
using System.Xml.Linq;
using NodaTime;
using NodaTime.Text;

namespace POLib.SECScraper.EPS
{
    internal class ContextElement
    {
        internal ContextElement(XElement ctxEl)
        {
            _ctxEl = ctxEl;
        }

        public bool IsQuarterly
        {
            get
            {
                var numDays = CalculateNumOfDays();
                return numDays >= MinNumberOfDaysInQtr && numDays <= MaxNumberOfDaysInQtr;
            }
        }

        internal LocalDate GetStartDate()
        {
            var date = _ctxEl.Descendants().First(e => e.Name.LocalName == "startDate").Value;
            return LocalDatePattern.Iso.Parse(date).Value;
        }

        internal LocalDate GetEndDate()
        {
            var date = _ctxEl.Descendants().First(e => e.Name.LocalName == "endDate").Value;
            return LocalDatePattern.Iso.Parse(date).Value;
        }

        private int CalculateNumOfDays()
        {
            return new DateInterval(GetStartDate(), GetEndDate()).Length;
        }

        private readonly XElement _ctxEl;

        private const int MinNumberOfDaysInQtr = 80;
        private const int MaxNumberOfDaysInQtr = 100;
    }
}
