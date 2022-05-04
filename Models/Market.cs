using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockPortfolio.Models
{
    public class Market
    {
        [JsonProperty("shortName")]
        public string Name { get; set; }

        public string QuoteType { get; set; }

        public string TimeZone { get; set; }

        public string MarketChange { get; set; }

        public string PercentageMarketChange { get; set; }

        public string MarketPrice { get; set; }

        public string ExchangeDataDelay { get; set; }

    }
}
