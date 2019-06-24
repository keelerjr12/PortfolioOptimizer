using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace POLib.SECScraper
{
    public class SECScraper
    {
        private int _numDownloaded;
        private int _interval;
        private readonly List<Task> _tasks = new List<Task>();
        public SECScraper(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }

        public void Download()
        {
            _numDownloaded = 0;
            _interval = _financeContext.Companies.Count() / 10;

            /*foreach (var company in _financeContext.Companies)
            {
                // Console.WriteLine(company.Ticker + " " + company.Name);
                //Task.Run(() => RetrieveSECData(company.CIK));
                _tasks.Add(Task.Factory.StartNew(() => RetrieveSECData(company.CIK)));
            }*/

            Parallel.ForEach(_financeContext.Companies, new ParallelOptions() {MaxDegreeOfParallelism = 6},
                async company =>
                {
                    await RetrieveSECData(company.CIK);
                    await Task.Delay(250);
                });
        }

        private async Task RetrieveSECData(int cik)
        {
            var url = "https://www.sec.gov/cgi-bin/browse-edgar?action=getcompany&CIK=" + cik +
                      "&type=10-q&dateb=&owner=include&count=100";

            var body = await ReadHTMLAsync(url);
            var srPage = new SearchResultsPage(body);

            var reportLinks = srPage.RetrieveAllReportLinks();

            /*foreach (var link in reportLinks)
            {
                Console.WriteLine(link);
            }*/

            _numDownloaded++;
            DisplayLoadingText();
        }

        private async Task<string> ReadHTMLAsync(string url)
        {
            string body;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                body = await response.Content.ReadAsStringAsync();
            }

            return body;
        }

        private void DisplayLoadingText()
        {
            if (_numDownloaded % _interval == 0)
            {
                Console.WriteLine(_numDownloaded / _interval * 10 + "% downloaded");
            }
        }

        private readonly FinanceContext _financeContext;
    }
}
