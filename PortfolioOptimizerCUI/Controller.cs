using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PortfolioOptimizerCUI.Services;
using POLib.SECScraper;

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
                        var capeService = _serviceProvider.GetService<CAPEService>();
                        var date = Convert.ToDateTime(args[1]);
                        var cape = capeService.GetCAPE(args[0], date);
                        _view.Show(cape + "\n");
                    }
                        break;

                    case "CPI":
                    {
                        var cpiService = _serviceProvider.GetService<ConsumerPriceIndexService>();
                        var date = Convert.ToDateTime(args[0]);
                        var cpi = cpiService.GetConsumerPriceIndex(date);
                        _view.Show(cpi.CPI + "\n");
                    }
                        break;

                    case "DilutedEPS":
                    {
                        var epsService = _serviceProvider.GetService<DilutedEPSService>();
                        var date = Convert.ToDateTime(args[1]);
                        var eps = epsService.GetDilutedEPS(args[0], date);
                        _view.Show(eps.EPS + "\n");
                    }
                        break;

                    case "SECDownload":
                    {
                        try
                        {
                            var sd = _serviceProvider.GetService<SECScraper>();
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

        private readonly IServiceProvider _serviceProvider;
        private readonly View _view;
    }
}
