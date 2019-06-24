using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PortfolioOptimizerLib
{
    public class SECDownloader
    {
        private int _numDownloaded;
        private int _interval;
        private readonly List<Task> _tasks = new List<Task>();
        public SECDownloader(FinanceContext financeContext)
        {
            _financeContext = financeContext;
        }

        public void Download()
        {
            _numDownloaded = 0;
            _interval = _financeContext.Companies.Count() / 10;

            foreach (var company in _financeContext.Companies)
            {
                // Console.WriteLine(company.Ticker + " " + company.Name);
                //Task.Run(() => RetrieveSECData(company.CIK));
                _tasks.Add(Task.Factory.StartNew(() => RetrieveSECData(company.CIK)));
            }

            Task.WaitAll(_tasks.ToArray());
        }

        private async Task RetrieveSECData(int cik)
        {
//            Console.WriteLine(cik);
            var url = "https://www.sec.gov/cgi-bin/browse-edgar?action=getcompany&CIK=" + cik +
                      "&type=10-q&dateb=&owner=include&count=100";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var body = await response.Content.ReadAsStringAsync();

                // TODO: Retrieve quarterly reports here
                //Console.WriteLine(body);

                _numDownloaded++;
                DisplayLoadingText();
            }
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
