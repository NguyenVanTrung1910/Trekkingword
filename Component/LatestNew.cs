using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourWeb.Models;

namespace TourWeb.Component
{
    public class LatestNew : ViewComponent
    {
        private readonly TourWebContext _context;
        public LatestNew(TourWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.New.Include(a=>a.Accounts).ToList());
        }
    }
}
