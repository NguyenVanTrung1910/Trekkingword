using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class PageController : Controller
    {
        private readonly TourWebContext _context;
        public PageController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult TourDetail(int id) {
            var tour = _context.Tour.Include(p => p.Itineraries).FirstOrDefault(p => p.Id == id);
            ViewBag.Img = _context.Image.Where(p=>p.TourId == id ).ToList();
            if (tour != null)
            {
                return View(tour);
            }
            else { return RedirectToAction("index", "home"); }
        }
        public IActionResult TrekkingDetail(int id)
        {
            var trekking = _context.Trekking.Include(p => p.Itineraries).FirstOrDefault(p => p.Id == id);
            if (trekking != null)
            {
                return View(trekking);
            }
            else { return RedirectToAction("index","home"); }
            
        }
        [HttpGet]
        
        public IActionResult Booking(int id)
        {
            ViewBag.TrekkingId = id;
            ViewBag.TourId = id;
            return View();
        }
        [HttpPost]
        public IActionResult Booking([Bind("Id,Country,Time,Date,GuestNumber,ChildrenNumber,TotalMoney,TourId,TrekkingId,AccountId")] BookTour bookTour)
        {
            if(bookTour.TourId != 0)
            {
                bookTour.AccountId = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                var tour = _context.Tour.FirstOrDefault(p => p.Id == bookTour.TourId);
                bookTour.TotalMoney = (bookTour.GuestNumber + bookTour.ChildrenNumber) * tour.Price;
            }
            else
            {
                bookTour.AccountId = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                var trekking  = _context.Trekking.FirstOrDefault(p=>p.Id == bookTour.TrekkingId);
                bookTour.TotalMoney = (bookTour.GuestNumber + bookTour.ChildrenNumber) * trekking.Price;
            }
            _context.Add(bookTour);
            _context.SaveChanges();
            return RedirectToAction("index", "home");
        }
        public IActionResult TermPage()
        {
            ViewBag.Index = "TermPage";
            ViewBag.Page = "current";
            return View();
        }
        public IActionResult ErrorPage()
        {
            ViewBag.Page = "current";
            return View();
        }
		public IActionResult Activity()
		{
            ViewBag.Page = "current";
            ViewBag.Index = "Activity";
            return View(_context.Tour.ToList());
		}
	}
}
