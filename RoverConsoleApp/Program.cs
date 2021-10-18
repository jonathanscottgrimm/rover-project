using Microsoft.Extensions.DependencyInjection;
using RoverConsoleApp.ConsoleHelper;
using RoverConsoleApp.RoverService;

namespace RoverConsoleApp
{
    class Program
    {
        private static void Main()
        {
            var serviceProvider = ConfigureServices();

            var consoleHelper = serviceProvider.GetService<IConsoleHelper>();
            consoleHelper.Execute();
        }

        private static ServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConsoleHelper, ConsoleHelper.ConsoleHelper>()
                .AddSingleton<IRoverService, RoverService.RoverService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
