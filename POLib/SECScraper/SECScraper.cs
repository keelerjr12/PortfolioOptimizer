using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using POLib.SECScraper.EPS;

namespace POLib.SECScraper
{
    public class SECScraper
    {
        public event EventHandler<ProgressChangedEventArgs> ProgressChangedEvent;

        public SECScraper(EPSDownloader downloader, FinanceContext financeContext)
        {
            _downloader = downloader;
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
            var epsDataPoints = await _downloader.GetEPSData(cik);

            foreach (var eps in epsDataPoints)
            {
                Console.WriteLine($"{cik} :: {eps.DateInterval} :: {eps.EPS}");
            }

            Console.WriteLine($"Completed: {cik}");
            IncrementNumDownloadedAndNotify();
        }

        private void IncrementNumDownloadedAndNotify()
        {
            _numDownloaded++;

            if (_numDownloaded % _interval != 0)
                return;

            var percentDownloaded = _numDownloaded / _interval;
            OnProgressChanged(new ProgressChangedEventArgs(percentDownloaded, this));
        }

        private readonly EPSDownloader _downloader;
        private readonly FinanceContext _financeContext;

        private int _numDownloaded;
        private int _interval; }
}
