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
    public class ExportTicketController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportTicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExportTicket
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExportTickets.ToListAsync());
        }

        // GET: ExportTicket/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportTicket = await _context.ExportTickets
                .Include(e => e.ExportDetails)
                .ThenInclude(d => d.Device)
                .FirstOrDefaultAsync(m => m.ExportTicketID == id);
            if (exportTicket == null)
            {
                return NotFound();
            }

            return View(exportTicket);
        }

        // GET: ExportTicket/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExportTicket/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExportTicketID,ExportDate,CustomerName")] ExportTicket exportTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exportTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exportTicket);
        }

        // GET: ExportTicket/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportTicket = await _context.ExportTickets.FindAsync(id);
            if (exportTicket == null)
            {
                return NotFound();
            }
            return View(exportTicket);
        }

        // POST: ExportTicket/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExportTicketID,ExportDate,CustomerName")] ExportTicket exportTicket)
        {
            if (id != exportTicket.ExportTicketID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exportTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportTicketExists(exportTicket.ExportTicketID))
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
            return View(exportTicket);
        }

        // GET: ExportTicket/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportTicket = await _context.ExportTickets
                .Include(e => e.ExportDetails)
                .FirstOrDefaultAsync(m => m.ExportTicketID == id);
            if (exportTicket == null)
            {
                return NotFound();
            }

            return View(exportTicket);
        }

        // POST: ExportTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var exportTicket = await _context.ExportTickets.FindAsync(id);
            if (exportTicket != null)
            {
                _context.ExportTickets.Remove(exportTicket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportTicketExists(string id)
        {
            return _context.ExportTickets.Any(e => e.ExportTicketID == id);
        }
    }
}
