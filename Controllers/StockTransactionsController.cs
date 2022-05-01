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
    public class StockTransactionsController : Controller
    {
        private readonly PortfolioContext _context;

        public StockTransactionsController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: StockTransactions
        public async Task<IActionResult> Index()
        {
            var portfolioContext = _context.StockTransactions.Include(s => s.Stock);
            return View(await portfolioContext.ToListAsync());
        }

        // GET: StockTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransaction = await _context.StockTransactions
                .Include(s => s.Stock)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stockTransaction == null)
            {
                return NotFound();
            }

            return View(stockTransaction);
        }

        // GET: StockTransactions/Create
        public IActionResult Create()
        {
            ViewData["StockID"] = new SelectList(_context.Stocks, "ID", "ID");
            return View();
        }

        // POST: StockTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StockID,TransactionDateTime,TransactionType,Price,Quantity")] StockTransaction stockTransaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StockID"] = new SelectList(_context.Stocks, "ID", "ID", stockTransaction.StockID);
            return View(stockTransaction);
        }

        // GET: StockTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransaction = await _context.StockTransactions.FindAsync(id);
            if (stockTransaction == null)
            {
                return NotFound();
            }
            ViewData["StockID"] = new SelectList(_context.Stocks, "ID", "ID", stockTransaction.StockID);
            return View(stockTransaction);
        }

        // POST: StockTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StockID,TransactionDateTime,TransactionType,Price,Quantity")] StockTransaction stockTransaction)
        {
            if (id != stockTransaction.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTransactionExists(stockTransaction.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StockID"] = new SelectList(_context.Stocks, "ID", "ID", stockTransaction.StockID);
            return View(stockTransaction);
        }

        // GET: StockTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransaction = await _context.StockTransactions
                .Include(s => s.Stock)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (stockTransaction == null)
            {
                return NotFound();
            }

            return View(stockTransaction);
        }

        // POST: StockTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockTransaction = await _context.StockTransactions.FindAsync(id);
            _context.StockTransactions.Remove(stockTransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockTransactionExists(int id)
        {
            return _context.StockTransactions.Any(e => e.ID == id);
        }
    }
}
