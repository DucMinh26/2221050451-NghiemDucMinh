using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BT_NET.Data;
using BT_NET.Models;

namespace BT_NET.Controllers
{
    public class StudentNewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentNewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentNew
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentNews.ToListAsync());
        }

        // GET: StudentNew/Details/5
        // GET: StudentNew/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return View("NotFound");

            var studentNew = await _context.StudentNews
                .FirstOrDefaultAsync(m => m.StudentCode == id);
                
            if (studentNew == null) return View("NotFound");

            return View(studentNew);
        }

        // GET: StudentNew/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentNew/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentCode,FullName,Age,Email")] StudentNew studentNew)
        {
            if (ModelState.IsValid)
            {
                try 
                {
                    _context.Add(studentNew);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    // Nếu trùng mã hoặc lỗi DB, văng sang trang NotFound ngay
                    return View("NotFound");
                }
            }
            return View(studentNew);
        }

        // GET: StudentNew/Edit/5
        // GET: StudentNew/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) // Kiểm tra cả trường hợp chuỗi rỗng
            {
                return View("NotFound");
            }

            var studentNew = await _context.StudentNews.FindAsync(id);
            if (studentNew == null)
            {
                return View("NotFound");
            }
            return View(studentNew);
        }

        // POST: StudentNew/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentCode,FullName,Age,Email")] StudentNew studentNew)
        {
            // 1. Kiểm tra nếu ID trên URL không khớp với dữ liệu trong Form
            if (id != studentNew.StudentCode)
            {
                return View("NotFound"); // Thay vì return NotFound() mặc định
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentNew);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Lỗi khi dữ liệu đã bị xóa bởi người khác trong lúc mình đang sửa
                    if (!StudentNewExists(studentNew.StudentCode))
                    {
                        return View("NotFound"); 
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception) 
                {
                    // 2. Tóm tất cả các lỗi Database khác (như trùng mã UNIQUE)
                    // Khi có bất kỳ lỗi nào xảy ra lúc Save, nó sẽ nhảy vào đây
                    return View("NotFound");
                }
            }
            
            // Nếu dữ liệu không hợp lệ (như sai định dạng Email, Age...) thì hiện lại Form Edit
            return View(studentNew);
        }

        // GET: StudentNew/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return View("NotFound");

            var studentNew = await _context.StudentNews
                .FirstOrDefaultAsync(m => m.StudentCode == id);
                
            if (studentNew == null) return View("NotFound");

            return View(studentNew);
        }

        // POST: StudentNew/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var studentNew = await _context.StudentNews.FindAsync(id);
            if (studentNew != null)
            {
                _context.StudentNews.Remove(studentNew);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentNewExists(string id)
        {
            return _context.StudentNews.Any(e => e.StudentCode == id);
        }
    }
}
