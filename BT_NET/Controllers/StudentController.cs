using BT_NET.Data;
using BT_NET.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create(Student std)
        {
            _context.Students.Add(std);
            _context.SaveChanges();
            return RedirectToAction("Index");
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
            _context.Students.Update(std);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}