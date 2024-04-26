using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TourWeb.Models;

namespace TourWeb.Component
{
    public class TourTredding : ViewComponent
    {
        private readonly TourWebContext _context;
        public TourTredding(TourWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke() { 
            return View(_context.Tour.ToList());
        }
    }
}
