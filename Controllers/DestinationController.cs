using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class DestinationController : Controller
    {
        private readonly TourWebContext _context;
        public DestinationController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Index = "Destinations";
            ViewBag.Destinations = "current";
            ViewBag.CountTour = _context.Tour
                .GroupBy(t => t.Address)
                .Select(g => new { Address = g.Key, TourCount = g.Count(), Id = g.Select(t => t.Id).FirstOrDefault() })
                .ToList();
            ViewBag.ImageTour = _context.Tour.ToList();
            ViewBag.CountTrekking = _context.Trekking.GroupBy(a => a.Address).Select(g => new { Address = g.Key, TourCount = g.Count() }).ToList();
            ViewBag.ImageTrekking = _context.Trekking.ToList();
            return View(_context.Tour.Include(a=>a.ImageGarelly).ToList());
        }
    }
}
