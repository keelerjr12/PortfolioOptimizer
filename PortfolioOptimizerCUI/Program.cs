﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POLib.SECScraper;
using PortfolioOptimizerCUI.Services;
using System.Configuration;
using System.Net.Http;
using POLib.Http;
using POLib.SECScraper.EPS;

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
            collection.AddScoped<IHttpClient, RateLimitedHttpClient>();

            collection.AddTransient<CAPEService>();
            collection.AddTransient<ConsumerPriceIndexService>();
            collection.AddTransient<DilutedEPSService>();
            collection.AddTransient<DailyStockPriceService>();

            collection.AddTransient<EPSDownloader>();
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
