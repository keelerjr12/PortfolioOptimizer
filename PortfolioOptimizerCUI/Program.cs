using System.Configuration;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortfolioOptimizerCUI.Services;
using POLib.SECScraper;

namespace PortfolioOptimizerCUI
{
    class Program
    {
        static void Main()
        {
            var collection = new ServiceCollection();

            collection.AddScoped<PortfolioOptimizerApp>();
            collection.AddScoped<Controller>();
            collection.AddScoped<View>();

            collection.AddScoped<HttpClient>();

            collection.AddTransient<CAPEService>();
            collection.AddTransient<ConsumerPriceIndexService>();
            collection.AddTransient<DilutedEPSService>();
            collection.AddTransient<DailyStockPriceService>();

            collection.AddTransient<SECScraper>();
            collection.AddTransient<StockDownloader>();

            collection.AddDbContext<FinanceContext>(options =>
                options.UseSqlServer(ConfigurationManager.ConnectionStrings["FinanceContext"].ConnectionString));

            var serviceProvider = collection.BuildServiceProvider();
            var app = serviceProvider.GetService<PortfolioOptimizerApp>();

            app.Run();
        }
    }
}
