using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace POLib.SECScraper
{
    public class SearchResultsPage
    {
        public SearchResultsPage(string html)
        {
            _doc = new HtmlDocument();
            _doc.LoadHtml(html);

            _reportLinks = new List<string>();
        }

        public IList<string> GetAllReportLinks()
        {
            var tableRows = GetTableRows();

            foreach (var row in tableRows)
                GetReportLinkAndAddToCollection(row);

            return _reportLinks;
        }

        private IEnumerable<HtmlNode> GetTableRows()
        {
            var node = _doc.DocumentNode.SelectSingleNode("//table[@class='tableFile2']");
            return node == null ? new List<HtmlNode>() : node.Elements("tr").Skip(1);
        }

        private void GetReportLinkAndAddToCollection(HtmlNode row)
        {
            var links = row.ChildNodes.Descendants("a").ToList();
            if (links.Count() < 3)
                return;

            var documentLink = links[0].GetAttributeValue("href", "");
            _reportLinks.Add(documentLink);
        }

        private readonly HtmlDocument _doc;
        private readonly List<string> _reportLinks;
    }
}
