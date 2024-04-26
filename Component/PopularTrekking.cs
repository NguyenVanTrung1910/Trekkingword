using Microsoft.AspNetCore.Mvc;
using TourWeb.Models;

namespace TourWeb.Component
{
    public class PopularTrekking : ViewComponent
    {
        private readonly TourWebContext _context;
        public PopularTrekking(TourWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Trekking.ToArray());
        }
    }
}
