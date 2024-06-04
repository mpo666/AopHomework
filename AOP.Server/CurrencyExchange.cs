using AOP.Server.Models;
using Newtonsoft.Json;

namespace AOP.Server
{
    public class CurrencyExchange
    {
        /// <summary>
        /// load from api.frankfurter.app current exchange values
        /// </summary>
        /// <returns>exchange rates</returns>
        public static async Task<Models.ExchangeRates> GetExchangeRates()
        {
            Models.ExchangeRates exchangeRates = new ExchangeRates();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "https://api.frankfurter.app/latest?base=USD";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        exchangeRates = JsonConvert.DeserializeObject<Models.ExchangeRates>(responseBody);
                    }
                }
                catch (HttpRequestException e)
                {

                }
            }
            return exchangeRates;
        }


    }
}
