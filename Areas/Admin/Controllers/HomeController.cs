using Microsoft.AspNetCore.Mvc;

namespace TourWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Home = "active";
            return View();
        }
    }
}
