namespace PortfolioOptimizerCUI
{
    class PortfolioOptimizerApp
    {
        public PortfolioOptimizerApp(Controller controller)
        {
            _controller = controller;
        }
        
        public void Run()
        {
            _controller.Run();
        }

        private readonly Controller _controller;
    }
}
