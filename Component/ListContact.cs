using Microsoft.AspNetCore.Mvc;
using TourWeb.Models;

namespace TourWeb.Component 
{
    public class ListContact: ViewComponent
    {
        private readonly TourWebContext _context;
        public ListContact(TourWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Contact.ToList());
        }
    }
}
