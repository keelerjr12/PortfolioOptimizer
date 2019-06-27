using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace POLib.SECScraper
{
    public class SearchResultsPage
    {
        public SearchResultsPage(string html)
        {
            _doc = new HtmlDocument();
            _doc.LoadHtml(html);
        }

        public IList<string> GetAllReportLinks()
        {
            var node = _doc.DocumentNode.SelectSingleNode("//table[@class='tableFile2']");
            var rows = node?.Elements("tr").Skip(1);

            var reportLinks = new List<string>();

            if (rows == null)
                return reportLinks;

            foreach (var row in rows)
            {
                var links = row.ChildNodes.Descendants("a").ToList();
                if (links.Count() < 3)
                    continue;

                var documentLink = links[0].GetAttributeValue("href", "");
                reportLinks.Add(documentLink);
            }

            return reportLinks;
        }

        private readonly HtmlDocument _doc;
    }
}
