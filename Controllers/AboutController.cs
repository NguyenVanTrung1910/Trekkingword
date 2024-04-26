using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class AboutController : Controller
    {
        private readonly TourWebContext _context;
        public AboutController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.About = "current";
            ViewBag.Index = "About";
            return View();
        }
        public IActionResult Team()
        {
            ViewBag.Index = "Out Team";
            return View(_context.Account.Where(a=>a.Role=="tourguider").ToList());
        }
        
        public IActionResult TeamMember (int id)
        {
            ViewBag.Index = "Team Member";
            return View(_context.Account.Where(a => a.Role == "tourguider").FirstOrDefault(a=>a.Id==id));
        }
        public IActionResult Faq()
        {
            ViewBag.Index = "Faq";
            return View(_context.Contact.Take(8).ToList());
        }
        public IActionResult Gallery ()
        {
            ViewBag.Index = "Galerry";
            ViewBag.Tour = _context.Tour.ToList();
            return View(_context.Trekking.ToList());
        }
    }
}
