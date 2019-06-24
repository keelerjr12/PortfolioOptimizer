using System;
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

        public IList<string> RetrieveAllReportLinks()
        {
            Console.WriteLine(_doc.Text);
            var node = _doc.DocumentNode.SelectSingleNode("//table[@class='tableFile2']");
            if (node == null)
                Console.WriteLine("NULL ALERT");
            var rows = node.Elements("tr").Skip(1);

            var reportLinks = new List<string>();
            
            foreach (var row in rows)
            {
                var interactiveDataBtn = row.SelectSingleNode("//a[@id='interactiveDataBtn']");
                if (interactiveDataBtn == null)
                    continue;

                var link = row.ChildNodes.FindFirst("a").GetAttributeValue("href", "");

                //Console.WriteLine(link);
                reportLinks.Add(link);
            }

            return reportLinks;
        }

        private readonly HtmlDocument _doc;
    }
}
