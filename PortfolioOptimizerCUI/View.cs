using System;
using System.Collections.Generic;
using System.Linq;

namespace PortfolioOptimizerCUI
{
    class View
    {
        public void Show(string output)
        {
            Console.Write(output);
        }

        public IList<string> GetInput()
        {
            var input = Console.ReadLine();
            var tokens = input?.Split();
            return tokens == null ? new List<string>() : tokens.ToList();
        }
    }
}
