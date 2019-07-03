using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;

namespace POLib.SECScraper
{
    public class FilingDetailsPage
    {
        public FilingDetailsPage(string html)
        {
            _doc = new HtmlDocument();
            _doc.LoadHtml(html);
        }

        public string GetInstanceDocumentLink()
        {
            var link = "";
            var tableRows = GetTableRows();

            foreach (var row in tableRows)
            {
                var fdpTableRow = new FilingDetailsPageTableRow(row);

                if (fdpTableRow.IsInstanceDocument())
                {
                    link = fdpTableRow.GetLink();
                    break;
                }
            }

            return link;
        }

        private IEnumerable<HtmlNode> GetTableRows()
        {
            var tableNode = _doc.DocumentNode.SelectNodes("//table[@summary='Data Files']/tr");
            return tableNode.Skip(1);
        }

        private readonly HtmlDocument _doc;
    }
}
