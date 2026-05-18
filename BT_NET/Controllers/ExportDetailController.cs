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
    public class ExportDetailController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExportDetailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExportDetail
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExportDetails.Include(e => e.Device).Include(e => e.ExportTicket);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExportDetail/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportDetail = await _context.ExportDetails
                .Include(e => e.Device)
                .Include(e => e.ExportTicket)
                .FirstOrDefaultAsync(m => m.ExportDetailID == id);
            if (exportDetail == null)
            {
                return NotFound();
            }

            return View(exportDetail);
        }

        // GET: ExportDetail/Create
        public IActionResult Create()
        {
            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName");
            ViewData["ExportTicketID"] = new SelectList(_context.ExportTickets, "ExportTicketID", "ExportTicketID");
            return View();
        }

        // POST: ExportDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExportDetailID,ExportTicketID,DeviceID,Quantity,UnitPrice")] ExportDetail exportDetail)
        {
            if (ModelState.IsValid)
            {
                var device = await _context.Devices.FindAsync(exportDetail.DeviceID);
                if (device == null)
                {
                    ModelState.AddModelError(string.Empty, "Thiết bị không tồn tại.");
                }
                else if (device.StockQuantity < exportDetail.Quantity)
                {
                    ModelState.AddModelError(string.Empty, "Số lượng tồn kho không đủ để xuất.");
                }
                else
                {
                    device.StockQuantity -= exportDetail.Quantity;
                    _context.Update(device);
                    _context.Add(exportDetail);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName", exportDetail.DeviceID);
            ViewData["ExportTicketID"] = new SelectList(_context.ExportTickets, "ExportTicketID", "ExportTicketID", exportDetail.ExportTicketID);
            return View(exportDetail);
        }

        // GET: ExportDetail/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportDetail = await _context.ExportDetails.FindAsync(id);
            if (exportDetail == null)
            {
                return NotFound();
            }
            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName", exportDetail.DeviceID);
            ViewData["ExportTicketID"] = new SelectList(_context.ExportTickets, "ExportTicketID", "ExportTicketID", exportDetail.ExportTicketID);
            return View(exportDetail);
        }

        // POST: ExportDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExportDetailID,ExportTicketID,DeviceID,Quantity,UnitPrice")] ExportDetail exportDetail)
        {
            if (id != exportDetail.ExportDetailID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldDetail = await _context.ExportDetails.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.ExportDetailID == id);

                    if (oldDetail != null)
                    {
                        var oldDevice = await _context.Devices.FindAsync(oldDetail.DeviceID);
                        if (oldDevice != null)
                        {
                            oldDevice.StockQuantity += oldDetail.Quantity;
                            _context.Update(oldDevice);
                        }
                    }

                    var newDevice = await _context.Devices.FindAsync(exportDetail.DeviceID);
                    if (newDevice == null)
                    {
                        ModelState.AddModelError(string.Empty, "Thiết bị không tồn tại.");
                    }
                    else if (newDevice.StockQuantity < exportDetail.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, "Số lượng tồn kho không đủ để xuất.");
                    }
                    else
                    {
                        newDevice.StockQuantity -= exportDetail.Quantity;
                        _context.Update(newDevice);

                        _context.Update(exportDetail);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExportDetailExists(exportDetail.ExportDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["DeviceID"] = new SelectList(_context.Devices, "DeviceID", "DeviceName", exportDetail.DeviceID);
            ViewData["ExportTicketID"] = new SelectList(_context.ExportTickets, "ExportTicketID", "ExportTicketID", exportDetail.ExportTicketID);
            return View(exportDetail);
        }

        // GET: ExportDetail/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exportDetail = await _context.ExportDetails
                .Include(e => e.Device)
                .Include(e => e.ExportTicket)
                .FirstOrDefaultAsync(m => m.ExportDetailID == id);
            if (exportDetail == null)
            {
                return NotFound();
            }

            return View(exportDetail);
        }

        // POST: ExportDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var exportDetail = await _context.ExportDetails.FindAsync(id);
            if (exportDetail != null)
            {
                var device = await _context.Devices.FindAsync(exportDetail.DeviceID);
                if (device != null)
                {
                    device.StockQuantity += exportDetail.Quantity;
                    _context.Update(device);
                }

                _context.ExportDetails.Remove(exportDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExportDetailExists(string id)
        {
            return _context.ExportDetails.Any(e => e.ExportDetailID == id);
        }
    }
}
