using System;
using System.Collections.Generic;

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
            return tokens;
        }
    }
}
