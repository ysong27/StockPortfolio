using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPortfolio.Models
{
    public enum TransactionType
    {
        Buy,
        Sell
    }

    public class StockTransaction
    {
        public int ID { get; set; }
        public int StockID { get; set; }        // foreign key
        public DateTime TransactionDateTime { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }


        public Stock Stock { get; set; }        // navigation property
    }
}
