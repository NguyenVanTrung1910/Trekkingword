using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TourWeb.Models;

namespace TourWeb.Component
{
    public class ListCart :ViewComponent
    {
        private readonly TourWebContext _context;
        public ListCart(TourWebContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Request.Cookies["CartDetail"];
            var cartList = new List<CartDetail>();

            if (!string.IsNullOrEmpty(cart))
            {
                cartList = JsonConvert.DeserializeObject<List<CartDetail>>(cart);
            }
            ViewBag.Cart = cartList;

            ViewBag.Product = _context.Product.ToList();
            var cart1 = _context.Cart.FirstOrDefault(p => p.AccountId == Convert.ToInt32(HttpContext.Session.GetInt32("id")));
            var cartid = 1;
            if (cart1 != null) { cartid = cart1.Id; }
            return View(_context.CartDetail.Where(p => p.CartId == cartid).ToList());
        }
    }
}
