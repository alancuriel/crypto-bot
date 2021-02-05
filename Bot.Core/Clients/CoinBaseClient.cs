using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

using Bot.Core.Models.Coinbase;
using Bot.Core.Helpers;
using System.Security.Cryptography;
using System.Text;

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
            HttpClient client = _httpClientFactory.CreateClient(_configuration["CryptoSource:Name"]);

            var path = $"/v2/prices/{_configuration["CryptoSource:CurrencyPair"]}/spot";
            var timestamp = TimeStamp.Now;
            var method = "GET";

            string signature = CreateSignature(timestamp, method, path, "");

            client.DefaultRequestHeaders
                .Add("CB-ACCESS-SIGN", signature);

            return await client.GetFromJsonAsync<CryptoPriceModel>(path);
        }

        private string CreateSignature(string timestamp, string method,
            string requestPath, string body)
        {

            var key = Encoding.Default.GetBytes( 
                _configuration["CryptoSource:AppSecret"]);

            var message = Encoding.Default.GetBytes(
                $"{timestamp}{method}{requestPath}{body}");          

            var hash = new HMACSHA256(key);

            return hash.ComputeHash(message).ToString();
        }
    }
}