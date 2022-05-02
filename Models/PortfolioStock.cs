using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Models
{

    public enum Currency
    {
        USD,
        CAD,
        EUR,
        CHF,
        JPY,
        GBP
    }




    public class PortfolioStock
    {
        public int ID { get; set; }

        public string Symbol { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public Currency Currency { get; set; }

        [Display(Name = "Initial Price")]
        public double InitialPrice { get; set; }

        [Display(Name = "Average Purchase Price")]
        public double AveragePrice { get; set; }

        public int Volume { get; set; }

        public double StockValue { get; set; }

        [Display(Name = "Initial Purchase Datetime")]
        public DateTime InitialPurchaseDateTime { get; set; }



        public PortfolioStock()
        {
            AveragePrice = 0;
            Volume = 0;
            StockValue = 0;
            InitialPrice = 0;
            InitialPurchaseDateTime = DateTime.Now;
        }

    }
}
