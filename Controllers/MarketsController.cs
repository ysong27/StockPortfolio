using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockPortfolio.Data;

namespace StockPortfolio.Controllers
{
    public class MarketsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var marketList = await YHFinanceAPIClient.ProcessMarkets();
            return View(marketList);
        }
    }
}
