using System.Linq;
using HtmlAgilityPack;

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
            var tableNode = _doc.DocumentNode.SelectNodes("//table[@summary='Data Files']/tr");
            var rows = tableNode.Skip(1);

            foreach (var row in rows)
            {
                var type = row.ChildNodes[7].InnerText;

                if (type != "XML" && type != "EX-101.INS")
                    continue;

                var link = row.ChildNodes[5].Element("a").GetAttributeValue("href", "");
                return link;
            }

            return "";
        }

        private readonly HtmlDocument _doc;
    }
}
