namespace POLib.SECScraper
{
    class TickerMetadata
    {
        public string Ticker { get; set; }
        public string Url { get; set; }

        public TickerMetadata(string ticker, string url)
        {
            Ticker = ticker;
            Url = url;
        }
    }
}
