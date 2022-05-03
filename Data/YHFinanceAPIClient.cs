using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using StockPortfolio.Models;
using Newtonsoft.Json.Linq;


namespace StockPortfolio.Data
{
    public class YHFinanceAPIClient
    {
        private static readonly HttpClient client;
        private static readonly string apiKey;
        private static readonly string baseUrl;


        static YHFinanceAPIClient()
        {
            client = new HttpClient();
            apiKey = "replace string with API key";
            baseUrl = "https://yfapi.net";
            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }
        

        public static async Task<List<Quote>> ProcessQuotes(string symbols)
        {
            var quoteList = new List<Quote>();
            if (string.IsNullOrEmpty(symbols))
            {
                return quoteList;
            }
            var url = $"{baseUrl}/v6/finance/quote?region=US&lang=en&symbols={symbols}";
            var stringTask = await client.GetStringAsync(url);
            var quoteResponse = JObject.Parse(stringTask);
            var quoteResult = quoteResponse["quoteResponse"]["result"].Children().AsEnumerable();
            foreach (var quote in quoteResult)
            {
                quoteList.Add(quote.ToObject<Quote>());
            }
            return quoteList;
        }





    }
}
