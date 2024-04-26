using Microsoft.AspNetCore.Mvc;
using TourWeb.Models;

namespace TourWeb.Component
{
    public class TourGuider : ViewComponent
    {
        private readonly TourWebContext _context;
        public TourGuider(TourWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Account.ToList());
        }
    }
}
