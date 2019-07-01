using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CsvHelper;

namespace POLib.SECScraper
{
    public class StockDownloader
    {
        public StockDownloader(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }

        public void Download()
        {
            var metadataList = RetrieveTickerMetadata();

            var currentCount = 0;
            var interval = metadataList.Count / 10;

            foreach (var metadata in metadataList)
            {
                if (currentCount % interval == 0)
                {
                    Console.WriteLine((currentCount / interval) * 10 + "% completed.");
                }

                var req = WebRequest.Create(metadata.Url);
                var resp = req.GetResponse();

                var sr = new StreamReader(resp.GetResponseStream() ?? throw new InvalidOperationException());

                using (var csvReader = new CsvReader(sr))
                {
                    var records = csvReader.GetRecords<Test>().ToList();
                    var epsList = new List<EPSDiluted>();
                    foreach (var record in records)
                    {
                        if (record.EPS_Diluted == "None")
                            continue;

                        var eps = new EPSDiluted()
                        {
                            Ticker = metadata.Ticker,
                            QuarterEnd = record.QuarterEnd,
                            EPS = decimal.Parse(record.EPS_Diluted) / Convert.ToDecimal(record.SplitFactor)
                        };

                        epsList.Add(eps);

                    }

                    _financeContext?.EPSDiluted?.AddRange(epsList);
                    _financeContext?.SaveChanges();
                }

                sr.Close();
                currentCount++;
            }
        }

        private IList<TickerMetadata> RetrieveTickerMetadata()
        {
            var metadataList = new List<TickerMetadata>();

            var line = "";
            var file =
                new System.IO.StreamReader(@"C:\Users\Joshua\source\repos\PortfolioOptimizer\PortfolioOptimizer\bin\Debug\netcoreapp2.1\stocks.txt");
            while ((line = file.ReadLine()) != null)
            {
                var tokens = line.Split();
                var metadata = new TickerMetadata(tokens[0], tokens[1]);

                metadataList.Add(metadata);
            }

            return metadataList;
        }

        private FinanceContext _financeContext;
    }
}
