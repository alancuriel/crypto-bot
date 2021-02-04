
using Bot.ConsoleUI.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bot.ConsoleUI.InversionOfControl
{
    public static class Container
    {
        public static IServiceCollection AddConsoleServices(this IServiceCollection collection) =>
            collection.AddSingleton<ICryptoPriceService, BitCoinPriceService>();

        public static void AddConsoleConfiguration(HostBuilderContext context,
            IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Sources.Clear();

            IHostEnvironment env = context.HostingEnvironment;

            configurationBuilder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                    logging.ClearProviders().AddConsole())
                .ConfigureAppConfiguration((hostingContext, configuration) =>
                    AddConsoleConfiguration(hostingContext, configuration))
                .ConfigureServices((_, services) => services.AddConsoleServices());
    }
}