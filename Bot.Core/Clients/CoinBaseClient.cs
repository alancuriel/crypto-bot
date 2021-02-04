using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bot.Core.Models.Coinbase;
using Microsoft.Extensions.Configuration;

namespace Bot.Core.Clients
{
    public class CoinBaseClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CoinBaseClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<CryptoPriceModel> GetSpotPrice()
        {
            var client = _httpClientFactory.CreateClient(_configuration["CryptoSource:Name"]);


            return await client
                .GetFromJsonAsync<CryptoPriceModel>($"prices/{_configuration["CryptoSource:CurrencyPair"]}/spot");
        }
    }
}