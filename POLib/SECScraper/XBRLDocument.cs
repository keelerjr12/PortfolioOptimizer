using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace POLib.SECScraper
{
    public class XBRLDocument
    {
        public XBRLDocument(string page)
        {
            _doc = new XmlDocument();
            //_nsmgr = new XmlNamespaceManager(_doc.NameTable);
            //_nsmgr.AddNamespace("xbrli", "http://www.xbrl.org/2003/instance");
            //_nsmgr.AddNamespace("us-gaap", "http://xbrl.us/us-gaap/2009-01-31");

            _doc.LoadXml(page);
            _test = XElement.Parse(page);
        }
        public IList<EPSDataPoint> GetAllEPSData()
        {
            var epsNodes = _test.Descendants().Where(d => d.Name.LocalName == "EarningsPerShareDiluted").ToList();
            var eps = epsNodes?[0].Value;
            var ctxRef = epsNodes?[0].Attribute("contextRef")?.Value;

            return new List<EPSDataPoint>();
        }


        private readonly XmlDocument _doc;

        private readonly XElement _test;
        // private readonly XmlNamespaceManager _nsmgr;
    }
}
