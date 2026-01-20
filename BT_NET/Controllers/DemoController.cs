using Microsoft.AspNetCore.Mvc;

namespace BT_NET.Controllers
{

    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}