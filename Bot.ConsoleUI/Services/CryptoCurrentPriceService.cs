using System;
using System.Threading;
using System.Threading.Tasks;
using Bot.Core.Clients;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bot.ConsoleUI.Services
{
    public class CryptoCurrentPriceService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly CoinBaseClient _coinBaseClient;

        public CryptoCurrentPriceService(ILogger<CryptoCurrentPriceService> logger,
                                        CoinBaseClient coinBaseClient) =>
            (_logger , _coinBaseClient) = (logger, coinBaseClient);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                try
                {
                    var coinData = (await _coinBaseClient.GetSpotPrice()).Data;
                    _logger
                        .LogInformation($"The price of {coinData.Currency} is {coinData.Amount}");
                }    
                catch (System.Exception ex)
                {
                    
                    _logger.LogError(ex,ex.Message);
                }
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}