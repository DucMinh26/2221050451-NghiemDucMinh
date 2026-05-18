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
    }
}