using Microsoft.AspNetCore.Mvc;

namespace BT_NET.Controllers
{
    public class TestController : Controller
    {
        public IActionResult test1()
        {
            return View();
        }
    }
}