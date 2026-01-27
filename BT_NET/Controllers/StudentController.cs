using BT_NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace BT_NET.Controllers{
    public class StudentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(String Fullname, String StudentCode)
        {
            ViewBag.ThongBao = "Fullname: " + Fullname + ", StudentCode: " + StudentCode;
            return View();
        }
    }
}