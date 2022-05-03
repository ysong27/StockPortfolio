using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StockPortfolio.Data;
using StockPortfolio.Models;

namespace StockPortfolio.Controllers
{
    public class PortfolioStocksController : Controller
    {
        private readonly PortfolioContext _context;

        public PortfolioStocksController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: PortfolioStocks
        public async Task<IActionResult> Index()
        {
            var portfolioStocks = await _context.PortfolioStocks.OrderBy(m => m.Symbol).ToListAsync();
            var symbolsStr = string.Join(",", portfolioStocks.Select(p => p.Symbol.ToString()));
            ViewBag.quoteList = await YHFinanceAPIClient.ProcessQuotes(symbolsStr);
            return View(portfolioStocks);
        }

        // GET: PortfolioStocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var portfolioStock = await _context.PortfolioStocks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portfolioStock == null)
            {
                return NotFound();
            }

            return View(portfolioStock);
        }

        // GET: PortfolioStocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioStocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Symbol,CompanyName")] PortfolioStock portfolioStock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(portfolioStock);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(portfolioStock);
        }


        // GET: PortfolioStocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioStock = await _context.PortfolioStocks.FindAsync(id);
            if (portfolioStock == null)
            {
                return NotFound();
            }
            return View(portfolioStock);
        }

        // POST: PortfolioStocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var portfolioStockToUpdate = await _context.PortfolioStocks
                .FirstOrDefaultAsync(p => p.ID == id);
            if (await TryUpdateModelAsync<PortfolioStock>(portfolioStockToUpdate, "", p => p.Symbol, p => p.CompanyName, p => p.Currency))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            return View(portfolioStockToUpdate);
        }


        // GET: PortfolioStocks/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioStock = await _context.PortfolioStocks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (portfolioStock == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed.";
            }

            return View(portfolioStock);
        }

        // POST: PortfolioStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioStock = await _context.PortfolioStocks.FindAsync(id);
            if (portfolioStock == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.PortfolioStocks.Remove(portfolioStock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }


        private bool PortfolioStockExists(int id)
        {
            return _context.PortfolioStocks.Any(e => e.ID == id);
        }
    }
}
