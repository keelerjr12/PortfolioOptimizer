using Microsoft.Extensions.DependencyInjection;
using POLib.SECScraper;
using PortfolioOptimizerCUI.Services;
using System;
using System.ComponentModel;
using System.Linq;

namespace PortfolioOptimizerCUI
{
    class Controller
    {
        public Controller(IServiceProvider serviceProvider, View view)
        {
            _serviceProvider = serviceProvider;
            _view = view;
        }

        public void Run()
        {
            while (true)
            {
                _view.Show("> ");

                var input = _view.GetInput();
                var strCommand = input?[0];
                var args = input?.Skip(1).ToList();

                switch (strCommand)
                {
                    case "CAPE":
                        {
                            if (args != null)
                            {
                                var capeService = _serviceProvider.GetService<CAPEService>();
                                var date = Convert.ToDateTime(args[1]);
                                var cape = capeService.GetCAPE(args[0], date);
                                _view.Show(cape + "\n");
                            }
                        }
                        break;

                    case "CPI":
                        {
                            if (args != null)
                            {
                                var cpiService = _serviceProvider.GetService<ConsumerPriceIndexService>();
                                var date = Convert.ToDateTime(args[0]);
                                var cpi = cpiService.GetConsumerPriceIndex(date);
                                _view.Show(cpi.CPI + "\n");
                            }
                        }
                        break;

                    case "DilutedEPS":
                        {
                            if (args != null)
                            {
                                var epsService = _serviceProvider.GetService<DilutedEPSService>();
                                var date = Convert.ToDateTime(args[1]);
                                var eps = epsService.GetDilutedEPS(args[0], date);
                                _view.Show(eps.EPS + "\n");
                            }
                        }
                        break;

                    case "SECDownload":
                        {
                            try
                            {
                                var sd = _serviceProvider.GetService<SECScraper>();
                                sd.ProgressChangedEvent += SECScraper_ProgressChanged;
                                sd.Download();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                                return;
                            }
                        }
                        break;

                    case "StockDownload":
                        {
                            var sd = _serviceProvider.GetService<StockDownloader>();
                            sd.Download();
                        }
                        break;
                }
            }
        }

        // This event handler updates the progress bar.
        private void SECScraper_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _view.Show($"{e.ProgressPercentage}% downloaded\n");
        }

        private readonly IServiceProvider _serviceProvider;
        private readonly View _view;
    }
}
