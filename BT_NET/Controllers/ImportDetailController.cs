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
    public class ImportDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImportDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ImportDetail
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ImportDetails.Include(i => i.Device).Include(i => i.ImportTicket);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ImportDetail/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importDetail = await _context.ImportDetails
                .Include(i => i.Device)
                .Include(i => i.ImportTicket)
                .FirstOrDefaultAsync(m => m.ImportDetailID == id);
            if (importDetail == null)
            {
                return NotFound();
            }

            return View(importDetail);
        }

        // GET: ImportDetail/Create
        public IActionResult Create(string importTicketId)
        {
            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName");
            ViewData["ImportTicketID"] = new SelectList(_context.ImportTickets, "ImportTicketID", "ImportTicketID", importTicketId);
            return View(new ImportDetail
            {
                ImportDetailID = Guid.NewGuid().ToString(),
                ImportTicketID = importTicketId
            });
        }

        // POST: ImportDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImportDetailID,ImportTicketID,DeviceID,Quantity,UnitPrice")] ImportDetail importDetail)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(importDetail.ImportDetailID))
                {
                    importDetail.ImportDetailID = Guid.NewGuid().ToString();
                }

                _context.Add(importDetail);
                var device = await _context.Devices.FindAsync(importDetail.DeviceID);
                if (device != null)
                {
                    device.StockQuantity += importDetail.Quantity;
                    _context.Update(device);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "ImportTicket", new { id = importDetail.ImportTicketID });
            }
            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName", importDetail.DeviceID);
            ViewData["ImportTicketID"] = new SelectList(_context.ImportTickets, "ImportTicketID", "ImportTicketID", importDetail.ImportTicketID);
            return View(importDetail);
        }

        // GET: ImportDetail/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importDetail = await _context.ImportDetails.FindAsync(id);
            if (importDetail == null)
            {
                return NotFound();
            }
            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName", importDetail.DeviceID);
            ViewData["ImportTicketID"] = new SelectList(_context.ImportTickets, "ImportTicketID", "ImportTicketID", importDetail.ImportTicketID);
            return View(importDetail);
        }

        // POST: ImportDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ImportDetailID,ImportTicketID,DeviceID,Quantity,UnitPrice")] ImportDetail importDetail)
        {
            if (id != importDetail.ImportDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldDetail = await _context.ImportDetails.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.ImportDetailID == id);

                    if (oldDetail != null)
                    {
                        var oldDevice = await _context.Devices.FindAsync(oldDetail.DeviceID);
                        if (oldDevice != null)
                        {
                            oldDevice.StockQuantity -= oldDetail.Quantity;
                            _context.Update(oldDevice);
                        }
                    }

                    var newDevice = await _context.Devices.FindAsync(importDetail.DeviceID);
                    if (newDevice != null)
                    {
                        newDevice.StockQuantity += importDetail.Quantity;
                        _context.Update(newDevice);
                    }

                    _context.Update(importDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportDetailExists(importDetail.ImportDetailID))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName", importDetail.DeviceID);
            ViewData["ImportTicketID"] = new SelectList(_context.ImportTickets, "ImportTicketID", "ImportTicketID", importDetail.ImportTicketID);
            return View(importDetail);
        }

        private bool ImportDetailExists(string id)
        {
            return _context.ImportDetails.Any(e => e.ImportDetailID == id);
        }

        // GET: ImportDetail/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var importDetail = await _context.ImportDetails
                .Include(i => i.Device)
                .Include(i => i.ImportTicket)
                .FirstOrDefaultAsync(m => m.ImportDetailID == id);
            if (importDetail == null)
            {
                return NotFound();
            }

            return View(importDetail);
        }

        // POST: ImportDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var importDetail = await _context.ImportDetails.FindAsync(id);
            if (importDetail != null)
            {
                var device = await _context.Devices.FindAsync(importDetail.DeviceID);
                if (device != null)
                {
                    device.StockQuantity -= importDetail.Quantity;
                    _context.Update(device);
                }

                _context.ImportDetails.Remove(importDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}