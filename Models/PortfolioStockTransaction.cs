using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Models
{
    public enum TransactionType
    {
        Buy,
        Sell
    }

    public class PortfolioStockTransaction
    {
        public int ID { get; set; }

        public int PortfolioStockID { get; set; }        // foreign key

        [Display(Name = "Transaction")]
        public DateTime TransactionDateTime { get; set; }

        [Display(Name = "Type")]
        public TransactionType TransactionType { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Value")]
        public double TransactionValue { get; set; }


        // Navigation Property
        public PortfolioStock PortfolioStock { get; set; }        



        public PortfolioStockTransaction()
        {
            TransactionDateTime = DateTime.Now;
        }

    }
}
