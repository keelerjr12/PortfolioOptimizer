using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace POLib.SECScraper.EPS
{
    public class XBRLDocument
    {
        public XBRLDocument(string page)
        {
            _doc = XElement.Parse(page);
        }

        public IList<EPSDataPoint> GetAllQuarterlyEPSData()
        {
            var epsDataPoints = new List<EPSDataPoint>();
            var epsElements = GetAllEPSElements();

            foreach (var epsElement in epsElements)
            {
                var ctxRef = epsElement.Attribute("contextRef")?.Value;

                if (ctxRef == null)
                    continue;

                var ctxEl = GetContextElement(ctxRef);

                if (!ctxEl.IsQuarterly)
                    continue;
                
                var startDate = ctxEl.GetStartDate();
                var endDate = ctxEl.GetEndDate();
                var eps = decimal.Parse(epsElement.Value);

                var epsDP = new EPSDataPoint(startDate, endDate, eps);
                epsDataPoints.Add(epsDP);
            }

            return epsDataPoints;
        }

        private IEnumerable<XElement> GetAllEPSElements()
        {
            return _doc.Descendants().Where(d => d.Name.LocalName == "EarningsPerShareDiluted" || 
                                                 d.Name.LocalName == "EarningsPerShareBasicAndDiluted");
        }

        private ContextElement GetContextElement(string contextRef)
        {
            var ctxEl = _doc.Descendants().First(d => d.Name.LocalName == "context" && d.Attribute("id")?.Value == contextRef);
            return new ContextElement(ctxEl);
        }

        private readonly XElement _doc;
    }
}
