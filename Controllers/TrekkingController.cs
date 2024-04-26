using Microsoft.AspNetCore.Mvc;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class TrekkingController : Controller
    {
        private readonly TourWebContext _context;
        public TrekkingController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Trekking = "current";
            ViewBag.Index = "Trekking";
            return View(_context.Trekking.ToList());
        }
    }
}
