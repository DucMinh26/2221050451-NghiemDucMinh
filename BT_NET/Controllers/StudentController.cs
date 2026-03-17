using BT_NET.Data;
using BT_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BT_NET.Controllers
{
    public class StudentController(ApplicationDbContext context) : Controller
    {

        private readonly ApplicationDbContext _context = context;

        //1. Hiển thị form nhập liệu
        [HttpGet]
        public IActionResult Index()
        {
            //Lấy ds sinh viên từ cơ sở dữ liệu
            var students = _context.Students.ToList();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        //2. Nhận dữ liệu từ form và lưu vào SQlite
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student std)
        {
            // 1. Kiểm tra xem mã này đã có trong DB chưa
            var existingStudent = _context.Students.Find(std.StudentCode);

            if (existingStudent != null)
            {
                // Nếu đã tồn tại, báo lỗi ra màn hình (hoặc trả về View kèm thông báo)
                ModelState.AddModelError("StudentCode", "Mã sinh viên này đã tồn tại rồi!");
                return View(std);
            }
            //kiểm tra dữ liệu hợp lệ mới lưu
            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(std);
        }

        //1. Hiển thị form với dữ liệu cũ
        public IActionResult Edit(string id)
        {
            var student = _context.Students.Find(id);
            if (student == null) return NotFound();
            return View(student);
        }

        //2. Nhận dữ liệu mới và cập nhật
        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(std);
        }

        public IActionResult Delete(string id)
        {
            var student = _context.Students.Find(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}