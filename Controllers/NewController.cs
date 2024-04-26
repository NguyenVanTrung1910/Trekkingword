using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class NewController : Controller
    {
        private readonly TourWebContext _context;
        public NewController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.New = "current";
            ViewBag.Index = "New";
            return View(_context.New.Include(a => a.Accounts).ToList());
        }
        [HttpGet]
        public IActionResult BlogDetail(int id)
        {
            ViewBag.TopRelatedPost = _context.New.Include(a => a.Accounts).Take(3).ToList();
            ViewBag.Index = "BlogDetail";
            return View(_context.New.Include(a=>a.Accounts).Include(i=>i.Images).Where(n=>n.Id==id).FirstOrDefault());
        }
    }
}
