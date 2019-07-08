using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace POLib.SECScraper
{
    public class XBRLDocument
    {
        public XBRLDocument(string page)
        {
            _doc = XElement.Parse(page);
        }

        /*
         * TODO: Add Quarterly Report Check
         */
        public IList<EPSDataPoint> GetAllQuarterlyEPSData()
        {
            var epsDataPoints = new List<EPSDataPoint>();
            var epsElements = GetAllEPSElements();

            foreach (var epsElement in epsElements)
            {
                var ctxRef = epsElement.Attribute("contextRef")?.Value;

                if (ctxRef == null)
                    continue;

                var ctxRefEl = GetContextRefElement(ctxRef);

                var startDate = GetStartDate(ctxRefEl);
                var endDate = GetEndDate(ctxRefEl);
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

        private XElement GetContextRefElement(string contextRef)
        {
            return _doc.Descendants().First(d => d.Name.LocalName == "context" && d.Attribute("id")?.Value == contextRef);
        }

        private static DateTime GetStartDate(XElement ctxRefEl)
        {
            var date = ctxRefEl.Descendants().First(e => e.Name.LocalName == "startDate").Value;
            return DateTime.Parse(date);
        }

        private static DateTime GetEndDate(XElement ctxRefEl)
        {
            var date = ctxRefEl.Descendants().First(e => e.Name.LocalName == "endDate").Value;
            return DateTime.Parse(date);
        }

        private object ParseEPSDataPoints(XElement ctxRefEl)
        {
            throw new NotImplementedException();
        }

        private readonly XElement _doc;
    }
}
