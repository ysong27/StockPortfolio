﻿using System;
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
    public class PortfolioStockTransactionsController : Controller
    {
        private readonly PortfolioContext _context;

        public PortfolioStockTransactionsController(PortfolioContext context)
        {
            _context = context;
        }

        // GET: StockTransactions
        public async Task<IActionResult> Index()
        {
            var portfolioContext = _context.PortfolioStockTransactions
                .Include(s => s.PortfolioStock)
                .OrderByDescending(s => s.TransactionDateTime);
            return View(await portfolioContext.ToListAsync());
        }

        // GET: StockTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransaction = await _context.PortfolioStockTransactions
                .Include(s => s.PortfolioStock)
                .AsNoTracking()
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
            ViewData["StockID"] = new SelectList(_context.PortfolioStocks, "ID", "CompanyName");
            return View();
        }

        // POST: StockTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PortfolioStockID,TransactionDateTime,TransactionType,Price,Quantity")] PortfolioStockTransaction stockTransaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(stockTransaction);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            ViewData["StockID"] = new SelectList(_context.PortfolioStocks, "ID", "CompanyName", stockTransaction.PortfolioStockID);
            return View(stockTransaction);
        }


        // GET: StockTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockTransaction = await _context.PortfolioStockTransactions.FindAsync(id);
            if (stockTransaction == null)
            {
                return NotFound();
            }
            ViewData["StockID"] = new SelectList(_context.Stocks, "ID", "CompanyName", stockTransaction.PortfolioStockID);
            return View(stockTransaction);
        }


        // POST: StockTransactions/Edit/5
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
            var stockTransactionToUpdate = await _context.PortfolioStockTransactions
                .FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<PortfolioStockTransaction>(
                stockTransactionToUpdate, "", s => s.PortfolioStockID, s => s.TransactionDateTime, s => s.TransactionType, s => s.Price, s => s.Quantity))
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
            return View(stockTransactionToUpdate);
        }


        // GET: StockTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            var stockTransaction = await _context.PortfolioStockTransactions
                .AsNoTracking()
                .Include(s => s.PortfolioStock)
                .FirstOrDefaultAsync(s => s.ID == id);
            if (stockTransaction == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Delete failed.";
            }

            return View(stockTransaction);
        }


        // POST: StockTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockTransaction = await _context.PortfolioStockTransactions.FindAsync(id);
            if (stockTransaction == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.PortfolioStockTransactions.Remove(stockTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }



        private bool StockTransactionExists(int id)
        {
            return _context.PortfolioStockTransactions.Any(e => e.ID == id);
        }
    }
}
