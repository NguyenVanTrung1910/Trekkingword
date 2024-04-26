using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly TourWebContext _context;
        public HomeController(TourWebContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Request.Cookies["WishList"] != null)
            {
                HttpContext.Session.SetInt32("quantityWish", JsonConvert.DeserializeObject<List<Product>>(HttpContext.Request.Cookies["WishList"]).Count);
            }
            else
            {
                HttpContext.Session.SetInt32("quantityWish", 0);
            }
            if (HttpContext.Request.Cookies["CartDetail"] != null)
            {
                HttpContext.Session.SetInt32("quantityCart", JsonConvert.DeserializeObject<List<Product>>(HttpContext.Request.Cookies["CartDetail"]).Count);
            }
            else
            {
                HttpContext.Session.SetInt32("quantityCart", 0);
            }
            if (HttpContext.Session.GetString("name") != null)
            {
                HttpContext.Session.SetInt32("quantityCart", _context.Cart.Select(p=>p.Id).Distinct().Count()); ;
                return View();
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
