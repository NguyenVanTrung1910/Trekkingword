using Microsoft.AspNetCore.Mvc;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class ContactController : Controller
    {
        private readonly TourWebContext _context;
        public ContactController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Index = "Contact";
            ViewBag.Contact = "current";
            return View();
        }
        [HttpPost]
        public IActionResult AddContact(string name, string email, int phone, string subject, string message)
        {
            Contact add = new Contact
            {
                NameUser = name,
                Email = email,
                PhoneNumber = phone,
                Subject = subject,
                Message = message
            };
            _context.Contact.Add(add);
            _context.SaveChanges();
            return View("Index");
        }
    }
}
