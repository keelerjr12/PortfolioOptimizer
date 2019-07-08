using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using POLib.Http;

namespace POLib.SECScraper
{
    public class SECScraper
    {
        public event EventHandler<ProgressChangedEventArgs> ProgressChangedEvent;

        public SECScraper(IHttpClient client, FinanceContext financeContext)
        {
            _client = client;
            _financeContext = financeContext;
        }

        public void Download()
        {
            _numDownloaded = 0;

            var companies = _financeContext.Companies.OrderBy(c => c.Name);
            _interval = companies.Count() / 100;

            var tasks = companies.Select(c => ScrapeSEC(c.CIK) ).ToList();
            Task.WhenAll(tasks);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChangedEvent?.Invoke(this, e);
        }

        private async Task ScrapeSEC(int cik)
        {
            var epsDataPoints = await DownloadEPSData(cik);

            Console.WriteLine($"Completed: {cik}");
            IncrementNumDownloadedAndNotify();
        }

        private async Task<IEnumerable<EPSDataPoint>> DownloadEPSData(int cik)
        {
            var epsDataPoints = new Dictionary<DateInterval, EPSDataPoint>();

            var reportLinks = await GetReportLinks(cik);

            foreach (var reportLink in reportLinks)
            {
                var xbrlLink = await GetXBRLLink(reportLink);
                var epsData = await GetEPSData(xbrlLink);

                foreach (var eps in epsData)
                {
                    if (!epsDataPoints.ContainsKey(eps.DateInterval))
                        epsDataPoints.Add(eps.DateInterval, eps);
                }
            }

            /* TODO: 1) Compute quarterly reports only! */

            return epsDataPoints.OrderBy(d => d.Key.End).Select(e => e.Value);
        }

        private void IncrementNumDownloadedAndNotify()
        {
            _numDownloaded++;

            if (_numDownloaded % _interval != 0)
                return;

            var percentDownloaded = _numDownloaded / _interval;
            OnProgressChanged(new ProgressChangedEventArgs(percentDownloaded, this));
        }

        private async Task<IList<string>> GetReportLinks(int cik)
        {
            // move this url elsewhere
            var url = "https://www.sec.gov/cgi-bin/browse-edgar?action=getcompany&CIK=" + cik +
                      "&type=10-q&dateb=&owner=include&count=100";

            var srBody = await _client.ReadAsync(url); // consider moving this to srPage
            var srPage = new SearchResultsPage(srBody);

            return srPage.GetAllReportLinks();
        }

        private async Task<string> GetXBRLLink(string link)
        {
            var url = SEC_HOSTNAME + link;

            var fdBody = await _client.ReadAsync(url);
            var fdPage = new FilingDetailsPage(fdBody);

            return fdPage.GetInstanceDocumentLink();
        }

        private async Task<IList<EPSDataPoint>> GetEPSData(string xbrlLink)
        {
            var xbrlBody = await _client.ReadAsync(SEC_HOSTNAME + xbrlLink);
            var xbrlDoc = new XBRLDocument(xbrlBody);

            return xbrlDoc.GetAllQuarterlyEPSData();
        }

        private const string SEC_HOSTNAME = "https://www.sec.gov";

        private readonly IHttpClient _client;
        private readonly FinanceContext _financeContext;

        private int _numDownloaded;
        private int _interval; }
}
