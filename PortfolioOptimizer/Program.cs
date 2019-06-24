using System;
using System.Configuration;
using System.IO;
using PortfolioOptimizerLib;

namespace PortfolioOptimizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var financeCtx = new FinanceContext();

            Console.Write("> ");
            var input = "";

            while ((input = Console.ReadLine()) != "q")
            {
                var tokens = input.Split();
                switch (tokens[0])
                {
                    case "download":
                        var sd = new StockDownloader();
                        sd.Download();
                        break;

                    case "cpi":
                        var cpiRepo = new ConsumerPriceIndexRepo(financeCtx);
                        var date = Convert.ToDateTime(tokens[1]);

                        var cpi = cpiRepo.GetByDate(date);
                        Console.WriteLine(cpi.CPI);
                        break;
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }

                Console.Write("> ");
            }
        }
    }
}
