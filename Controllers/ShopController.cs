using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Immutable;
using TourWeb.Models;

namespace TourWeb.Controllers
{
    public class ShopController : Controller
    {
        private readonly TourWebContext _context;
        public ShopController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Shop = "current";
            ViewBag.Index = "Shop";
            return View(_context.Product.Take(12).ToList());
        }
        [HttpPost]
        public IActionResult GetProductWithType(string index)
        {
            ViewBag.Shop = "current";
            ViewBag.Index = "Shop";
            return View(_context.Product.Take(12).ToList());
        }
        public IActionResult ProductDetail(int id)
        {
            ViewBag.Index = "ProductDetail";
            ViewBag.Image = _context.Image.Where(x => x.ProductId == id);
            ViewBag.Feature = _context.Product.FirstOrDefault(p => p.Id == id);
            ViewBag.Product = _context.Product.OrderByDescending(d => d.Id).Take(8).ToList();
            var productDetail = _context.Product.FirstOrDefault(p => p.Id == id);
            if(productDetail != null) { return View(productDetail); }
            else { return View(); }
            
        }
        public IActionResult WishList()
        {
            ViewBag.Index = "WishList";
            if (HttpContext.Request.Cookies["WishList"] != null){
                return View(JsonConvert.DeserializeObject<List<Product>> (HttpContext.Request.Cookies["WishList"]));
            }
            else
            {
                return View();
            }
            
        }
        public IActionResult AddWish(int id)
        {
            var product = _context.Product.FirstOrDefault(p => p.Id == id);
            var newWish = new Product
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                ImagesSpecial = product.ImagesSpecial,
            };
            DateTime expirationDate = DateTime.Now.AddDays(7);
            var wishlist = HttpContext.Request.Cookies["WishList"];
            var wishListItems = new List<Product>();

            if (!string.IsNullOrEmpty(wishlist))
            {
                wishListItems = JsonConvert.DeserializeObject<List<Product>>(wishlist);
            }

            // Thêm sản phẩm vào danh sách mong muốn
            wishListItems.Add(newWish);

            // Lưu lại danh sách mong muốn vào cookie
            HttpContext.Response.Cookies.Append("WishList", JsonConvert.SerializeObject(wishListItems), new CookieOptions
            {
                Expires = expirationDate
            });

            return RedirectToAction("WishList");

        }
        public IActionResult ShopCart()
        {
            ViewBag.Index = "ShopCart";
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
            if(cart1 != null) {  cartid = cart1.Id; }
            return View(_context.CartDetail.Where(p=>p.CartId == cartid).ToList());
        }
        [HttpPost]
        public IActionResult CheckOutTotal([FromBody] string total)
        {
            return Json(new { total = 1 }); 
        }
        public IActionResult CheckOut()
        {
            ViewBag.Index = "CheckOut";
            var cart = _context.Cart.FirstOrDefault(p => p.AccountId == Convert.ToInt32(HttpContext.Session.GetInt32("id")));
            if (cart != null) {  
                var cartDetails = _context.CartDetail.Include(p=>p.Products).Where(p=>p.CartId==cart.Id).ToList();
                decimal cartTotal = 0;
                foreach (var cartdetail in cartDetails)
                {
                    if(cartdetail.Products != null) {cartTotal += cartdetail.Quantity *cartdetail.Products.Price; }
                }
                ViewBag.Total = cartTotal;
                cart.Total = cartTotal;
                _context.SaveChanges();
            }
            var user = _context.Account.FirstOrDefault(a => a.Id == Convert.ToInt32(HttpContext.Session.GetInt32("id")));
            return View(user);
        }
        [HttpPost]
        public IActionResult AddOrderInfo(OrderInfo orderInfo)
        {
            _context.OrderInfo.Add(orderInfo);
            _context.SaveChanges();
            var cart = _context.Cart.FirstOrDefault(p => p.AccountId == Convert.ToInt32(HttpContext.Session.GetInt32("id")));
            if(cart != null)
            {
                var order = new Order
                {
                    OrderInfoId = orderInfo.Id,
                    Total = cart.Total,
                    AccountId = cart.AccountId,
                    Accounts = cart.Accounts,
                    Date = DateTime.Now,
                };
                _context.Add(order);
                _context.SaveChanges();
                var cartDetails = _context.CartDetail.Where(p => p.CartId == cart.Id).ToList();
                foreach (var cartdetail in cartDetails)
                {
                    var orderDetail = new OrderDetail
                    {
                        Quantity = cartdetail.Quantity,
                        ProductId = cartdetail.ProductId,
                        OrderId = order.Id
                    };
                    _context.Add(orderDetail);
                    _context.SaveChanges();
                }
            }
            return View("ShopCart");
        }
        
        public async Task<IActionResult> AddCart(int id)
        {
            DateTime expirationDate = DateTime.Now.AddDays(7);
            if (HttpContext.Session.GetString("id") != null)
            {
                var CartId = _context.Cart.FirstOrDefault(p => p.AccountId == Convert.ToInt32(HttpContext.Session.GetString("id")));
                var newCartDetail = new CartDetail
                {
                    CartId = CartId.Id,
                    ProductId = id,
                    Quantity = 1
                };
                _context.CartDetail.Add(newCartDetail);
                await _context.SaveChangesAsync();
                return View("ShopCart");

            }
            else
            {
                var newCartDetail = new CartDetail
                {
                    ProductId = id,
                    Quantity = 1
                };

                var cart = HttpContext.Request.Cookies["CartDetail"];
                var cartList = new List<CartDetail>();

                if (!string.IsNullOrEmpty(cart))
                {
                    cartList = JsonConvert.DeserializeObject<List<CartDetail>>(cart);
                }

                // Thêm sản phẩm vào giỏ hàng
                cartList.Add(newCartDetail);

                // Lưu lại giỏ hàng vào cookie
                var jsonCart = JsonConvert.SerializeObject(cartList);
                HttpContext.Response.Cookies.Append("CartDetail", jsonCart, new CookieOptions
                {
                    Expires = expirationDate
                });

                return View("ShopCart");

            }
        }
        public async Task<IActionResult> removeCart(int id)
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                var CartRemove = _context.Cart.FirstOrDefault(p => p.AccountId == Convert.ToInt32(HttpContext.Session.GetString("id")));
                if(CartRemove != null)
                {
                    var cartDetail = _context.CartDetail.FirstOrDefault(x => x.Id == id && x.CartId == CartRemove.Id);
                    if(cartDetail != null)
                    {
                        _context.CartDetail.Remove(cartDetail);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                
                return View("ShopCart");

            }
            else
            {
                
                var newCartDetail = new CartDetail
                {
                    ProductId = id,
                    Quantity = 1
                };

                var cart = HttpContext.Request.Cookies["CartDetail"];
                if (cart == null) { return View("ShopCart"); }
                var cartList = new List<CartDetail>();

                if (!string.IsNullOrEmpty(cart))
                {
                    cartList = JsonConvert.DeserializeObject<List<CartDetail>>(cart);
                }

                // Thêm sản phẩm vào giỏ hàng
                var itemToRemove = cartList.SingleOrDefault(item => item.ProductId == newCartDetail.ProductId);
                if (itemToRemove != null)
                {
                    cartList.Remove(itemToRemove);
                }
                // Lưu lại giỏ hàng vào cookie
                var jsonCart = JsonConvert.SerializeObject(cartList);
                HttpContext.Response.Cookies.Append("CartDetail", jsonCart);
                return View("ShopCart");

            }
        }
        [HttpGet]
        public IActionResult FilterProduct(string type)
        {
            ViewBag.Shop = "current";
            ViewBag.Index = "Shop";
            var product = _context.Product.Where(p => p.Categorys.Name == type).ToList();
            if (type == "All")
            {
                return Json(_context.Product.ToList());
            }
            return Json(product);
        }


    }
}
