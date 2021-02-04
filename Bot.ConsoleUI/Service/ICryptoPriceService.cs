namespace Bot.ConsoleUI.Service
{
    public interface ICryptoPriceService
    {
        string CurrencyPair { get; }

        double GetSpotPrice();
    }
}