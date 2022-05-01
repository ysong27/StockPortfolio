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
    public class StocksController : Controller
    {
        private readonly PortfolioContext _context;

        public StocksController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var stocks = await _context.Stocks.OrderBy(m => m.Symbol).ToListAsync();
            return View(stocks);
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Symbol,CompanyName")] Stock stock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(stock);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return View(stock);
        }

        // POST: Stocks/Edit/5
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

            var stockToUpdate = await _context.Stocks
                .FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Stock>(stockToUpdate, "", s => s.Symbol, s => s.CompanyName))
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

            return View(stockToUpdate);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stock == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed.";
            }

            return View(stock);
        }


        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }


        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.ID == id);
        }
    }
}
