using Microsoft.AspNetCore.Mvc;
using TourWeb.Models;

namespace TourWeb.Component
{
    public class ListProduct : ViewComponent
    {
        private readonly TourWebContext _context;
        public ListProduct(TourWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Product.Take(8).ToList());
        }
    }
}
