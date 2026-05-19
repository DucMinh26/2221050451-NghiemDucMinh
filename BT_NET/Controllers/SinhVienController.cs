using BT_NET.Data;
using BT_NET.Models.Entities;
using BT_NET.Models.ViewModels;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BT_NET.Controllers
{
    public class SinhVienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinhVienController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetSinhVien(int page = 1, int pageSize = 10)
        {
            var query = _context.sinhViens.AsNoTracking().OrderBy(x=>x.MaSV);

            var totalItems = await query.CountAsync();

            var sinhViens = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            var result = new PagedResult<SinhVien>
            {
                Items = sinhViens,
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };

            return PartialView("_SinhVienTable", result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_Create", new SinhVien());
        }

        [HttpPost]
        public async Task<IActionResult> Create( SinhVien sinhVien)
        {
            if (ModelState.IsValid)
            {
                _context.sinhViens.Add(sinhVien);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message="Them sinh vien thanh cong"
                });
            }

            return PartialView("_Create", sinhVien);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sinhVien = await _context.sinhViens.FindAsync(id);
            if(sinhVien == null)
            {
                return NotFound("Khong tim thay sinh vien");
            }

            return PartialView("_Edit",sinhVien);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SinhVien sinhVien)
        {
            if(id != sinhVien.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(sinhVien);
                await _context.SaveChangesAsync();

                return Json(new
                {
                    success = true,
                    message = "Cap nhat thanh cong"
                });
            }

            return PartialView("_Edit",sinhVien);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var sinhVien = await _context.sinhViens.FindAsync(id);

            if(sinhVien == null)
            {
                return Json(new
                {
                    success = false,
                    message= "khong tim thay sinh vien"
                });
            }

            _context.sinhViens.Remove(sinhVien);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message =" xoa thanh cong"
            });
        }
    }
}