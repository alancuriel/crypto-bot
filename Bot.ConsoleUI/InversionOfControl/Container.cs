
using System;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Bot.ConsoleUI.Services;
using Bot.Core.Clients;

namespace Bot.ConsoleUI.InversionOfControl
{
    public static class Container
    {
        private static IConfiguration Configuration;
        public static IServiceCollection AddConsoleServices(this IServiceCollection collection) =>
            collection
                .AddSingleton<CoinBaseClient>()
                .AddHostedService<CryptoCurrentPriceService>()
                .AddHttpClient(Configuration["CryptoSource:Name"], client =>
                {
                    client.BaseAddress = new Uri(Configuration["CryptoSource:BaseAddress"]);
                    client.DefaultRequestHeaders
                        .Add("CB-ACCESS-KEY", Configuration["CryptoSource:ApiKey"]);
                })
                .Services;


        public static void AddConsoleConfiguration(IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Sources.Clear();

            Configuration = configurationBuilder
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables().Build();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                    logging.ClearProviders().AddConsole())
                .ConfigureAppConfiguration((_, configuration) =>
                    AddConsoleConfiguration(configuration))
                .ConfigureServices((_, services) => services.AddConsoleServices());
    }
}