using HtmlAgilityPack;

namespace POLib.SECScraper
{
    internal class FilingDetailsPageTableRow
    { 
        public FilingDetailsPageTableRow(HtmlNode row)
        {
            _row = row;
        }
        public string GetLink()
        {
            return _row.ChildNodes[5].Element("a").GetAttributeValue("href", "");
        }
        public bool IsInstanceDocument()
        {
            var docType = GetDocumentType();
            return docType == "XML" || docType == "EX-101.INS";
        }

        private string GetDocumentType()
        {
            return _row.ChildNodes[7].InnerText;
        }

        private readonly HtmlNode _row;
    }
}
