using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bot.ConsoleUI.Services
{
    public class CryptoCurrentPriceService : BackgroundService
    {
        private readonly ILogger _logger;
        private int _count = 1;

        public CryptoCurrentPriceService(ILogger<CryptoCurrentPriceService> logger) =>
            _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Hello World #{_count}");
                _count++;
                
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}