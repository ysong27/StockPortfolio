using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Models
{
    public class Stock
    {
        public int ID { get; set; }
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
    }
}
