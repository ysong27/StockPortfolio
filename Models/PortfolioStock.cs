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

        [Display(Name = "Name")]
        public string CompanyName { get; set; }

        public Currency Currency { get; set; }

        [Display(Name = "Average Price")]
        public double AveragePrice { get; set; }

        public int Volume { get; set; }

        [Display(Name = "Cost")]
        public double StockCost { get; set; }

        [Display(Name = "Initial Purchase Time")]
        public DateTime InitialPurchaseDateTime { get; set; }



        public PortfolioStock()
        {
            AveragePrice = 0;
            Volume = 0;
            StockCost = 0;
            InitialPurchaseDateTime = DateTime.Now;
        }

    }
}
