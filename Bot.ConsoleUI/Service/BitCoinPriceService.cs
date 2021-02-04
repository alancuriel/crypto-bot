using Microsoft.Extensions.Logging;

namespace Bot.ConsoleUI.Service
{
    public class BitCoinPriceService : ICryptoPriceService
    {
        public string CurrencyPair { get; } = "BTC-USD";

        public double GetSpotPrice()
        {
            return 1000.50;
        }
    }
}