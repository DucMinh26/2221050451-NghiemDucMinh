using Microsoft.AspNetCore.Mvc;

namespace BT_NET.Controllers
{
    public class DemoController : Controller
    {
        //Hien thi form
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Nhap du lieu tu form
        [HttpPost]
        public IActionResult Index(string Fullname, int msv)
        {
            ViewBag.name = Fullname;
            ViewBag.id = msv;
            return View();
        }


    }
}