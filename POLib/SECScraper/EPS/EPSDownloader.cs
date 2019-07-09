using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using POLib.Http;

namespace POLib.SECScraper.EPS
{
    public class EPSDownloader
    {
        public EPSDownloader(IHttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<EPSDataPoint>> GetEPSData(int cik)
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

            return epsDataPoints.OrderBy(d => d.Key.End).Select(e => e.Value);
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

        private readonly IHttpClient _client;
        private const string SEC_HOSTNAME = "https://www.sec.gov";
    }
}