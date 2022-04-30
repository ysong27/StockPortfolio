using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockPortfolio.Models;

namespace StockPortfolio.Data
{
    public class DbInitializer
    {
        public static void Initialize(PortfolioContext context)
        {
            context.Database.EnsureCreated();

            if (context.Stocks.Any())
            {
                return;
            }

            var stocks = new Stock[]
            {
                new Stock {Symbol="MSFT", CompanyName="Microsoft Corporation"},
                new Stock {Symbol="NVDA", CompanyName="NVIDIA Corporation"},
                new Stock {Symbol="SHOP", CompanyName="Shopify Inc."}
            };

            foreach (var stock in stocks)
            {
                context.Stocks.Add(stock);
            }

            context.SaveChanges();
        }
    }
}
