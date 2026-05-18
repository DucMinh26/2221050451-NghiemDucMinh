using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BT_NET.Data;
using BT_NET.Models.Entities;

namespace BT_NET.Controllers
{
    public class ImportTicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportTicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImportTicket
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ImportTickets.Include(i => i.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ImportTicket/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importTicket = await _context.ImportTickets
                .Include(i => i.Supplier)
                .Include(i => i.ImportDetails)
                .ThenInclude(d => d.Device)
                .FirstOrDefaultAsync(m => m.ImportTicketID == id);
            if (importTicket == null)
            {
                return NotFound();
            }

            return View(importTicket);
        }

        // GET: ImportTicket/Create
        public IActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(_context.Suppliers, "SupplierID", "SupplierName");
            return View();
        }

        // POST: ImportTicket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportTicketID,ImportTicketDate,SupplierID")] ImportTicket importTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(importTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = importTicket.ImportTicketID });
            }
            ViewBag.SupplierID = new SelectList(_context.Suppliers, "SupplierID", "SupplierName", importTicket.SupplierID);
            return View(importTicket);
        }

        // GET: ImportTicket/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importTicket = await _context.ImportTickets.FindAsync(id);
            if (importTicket == null)
            {
                return NotFound();
            }
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierName", importTicket.SupplierID);
            return View(importTicket);
        }

        // POST: ImportTicket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ImportTicketID,ImportTicketDate,SupplierID")] ImportTicket importTicket)
        {
            if (id != importTicket.ImportTicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(importTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportTicketExists(importTicket.ImportTicketID))
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
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierName", importTicket.SupplierID);
            return View(importTicket);
        }

        // GET: ImportTicket/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importTicket = await _context.ImportTickets
                .Include(i => i.Supplier)
                .Include(i => i.ImportDetails)
                .ThenInclude(d => d.Device)
                .FirstOrDefaultAsync(m => m.ImportTicketID == id);
            if (importTicket == null)
            {
                return NotFound();
            }

            return View(importTicket);
        }

        // POST: ImportTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var importTicket = await _context.ImportTickets.FindAsync(id);
            if (importTicket != null)
            {
                _context.ImportTickets.Remove(importTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImportTicketExists(string id)
        {
            return _context.ImportTickets.Any(e => e.ImportTicketID == id);
        }
    }
}
