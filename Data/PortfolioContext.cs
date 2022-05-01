using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockPortfolio.Models;

namespace StockPortfolio.Data
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<PortfolioStock> PortfolioStocks { get; set; }
        public DbSet<PortfolioStockTransaction> StockTransactions { get; set; }
        
    }
}
