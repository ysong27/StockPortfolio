using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StockPortfolio.Models
{
    public class Stock
    {
        public int ID { get; set; }

        public string Symbol { get; set; }
        
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }
    }
}
