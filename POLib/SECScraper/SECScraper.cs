﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace POLib.SECScraper
{
    public class SECScraper
    {
        public SECScraper(HttpClient client, FinanceContext financeContext)
        {
            _client = client;
            _financeContext = financeContext;
        }

        public void Download()
        {
            _numDownloaded = 0;
            _interval = _financeContext.Companies.Count() / 10;

            Parallel.ForEach(_financeContext.Companies, new ParallelOptions {MaxDegreeOfParallelism = 6},
                company =>
                {
                    RetrieveSECData(company.CIK);
                    Task.Delay(175);
                });
        }

        private void RetrieveSECData(int cik)
        {
            // move this url elsewhere
            var url = "https://www.sec.gov/cgi-bin/browse-edgar?action=getcompany&CIK=" + cik +
                      "&type=10-q&dateb=&owner=include&count=100";

            var srBody = ReadHTML(url).Result; // consider moving this to srPage
            var srPage = new SearchResultsPage(srBody);

            var reportLinks = srPage.GetAllReportLinks();

            foreach (var link in reportLinks)
            {
                url = SEC_HOSTNAME + link;
                //Console.WriteLine(url);

                var fdBody = ReadHTML(url).Result;
                var fdPage = new FilingDetailsPage(fdBody);

                var xbrlLink = fdPage.GetInstanceDocumentLink();
                Console.WriteLine(xbrlLink);

                var xbrlBody = ReadHTML(SEC_HOSTNAME + xbrlLink).Result;
                var xbrlDoc = new XBRLDocument(xbrlBody);
            }

            _numDownloaded++;
            DisplayLoadingText();
        }

        private async Task<string> ReadHTML(string url)
        {
            using var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        private void DisplayLoadingText()
        {
            if (_numDownloaded % _interval == 0)
            {
                Console.WriteLine(_numDownloaded / _interval * 10 + "% downloaded");
            }
        }

        private const string SEC_HOSTNAME = "https://www.sec.gov";

        private readonly HttpClient _client;
        private readonly FinanceContext _financeContext;

        private int _numDownloaded;
        private int _interval;
    }
}
