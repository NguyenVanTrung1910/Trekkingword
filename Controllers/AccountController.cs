using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourWeb.Models;
using Newtonsoft.Json;
using System.Net;
using System.Security.Principal;

namespace TourWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly TourWebContext _context;
        public AccountController(TourWebContext context)
        {
            _context = context;
        }
        public IActionResult LogIn()
        {
            ViewBag.Index = "Login";
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var account = _context.Account?.SingleOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
            if (account != null)
            {
                HttpContext.Session.SetString("name", account.Name);//Gán các giá trị vào các session
                HttpContext.Session.SetString("pass", password);
                HttpContext.Session.SetString("role", account.Role);
                HttpContext.Session.SetInt32("id", account.Id);
                var claims = new List<Claim>//tạo ra claim để chứa các thuộc tính của client
                {
                    new Claim(ClaimTypes.Email, account.Email ),
                    new Claim(ClaimTypes.Role, account.Role ),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);// tạo claimIdentity để có các phương thức và thuộc tính cần dùng

                HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)); //phương thức SignInasync để tạo ra phiên đăng nhập

                if(account.Role == "Admin")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                if (HttpContext.Request.Cookies["CartDetail"] != null)
                {
                    var cart = HttpContext.Request.Cookies["CartDetail"];
                    var cartList = new List<CartDetail>();
                    
                    if (!string.IsNullOrEmpty(cart))
                    {
                        cartList = JsonConvert.DeserializeObject<List<CartDetail>>(cart);
                    }

                    // Thêm sản phẩm vào giỏ hàng
                    foreach (var item in cartList)
                    {
                        if (_context.CartDetail.Where(x => x.ProductId == item.ProductId && x.Quantity == item.Quantity&& x.CartId ==item.CartId) == null)
                        {
                            _context.CartDetail.Add(item);
                            _context.SaveChangesAsync();
                        }

                    }
                    HttpContext.Response.Cookies.Append("CartDetail", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
                }
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Alert = "Email or password is incorrect";
            return View();
        }
        public IActionResult LoginFacebook(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/account/ExternalLoginCallback" }, "Facebook");
        }
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                return RedirectToAction("Login");
            }
            var userPrincipal = authenticateResult.Principal;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, "user")
            };
            var identity = new ClaimsIdentity(userPrincipal.Identity, claims);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            // Get user information from the claims
            var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
            var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);
            var newAccount = new Account
            {
                Email = email,
                Name = name,
                Password = "1",
                Role = "user"
            };
            if (_context.Account.FirstOrDefault(p=>p.Email ==email&&p.Name == name)== null)
            {
                _context.Account.Add(newAccount);
                await _context.SaveChangesAsync();
            }
            
            HttpContext.Session.SetString("name", name);
            HttpContext.Session.SetString("role","user");
            HttpContext.Session.SetInt32("id", newAccount.Id);

            if (HttpContext.Request.Cookies["CartDetail"] != null)
            {
                var cart = HttpContext.Request.Cookies["CartDetail"];
                var cartList = new List<CartDetail>();

                if (!string.IsNullOrEmpty(cart))
                {
                    cartList = JsonConvert.DeserializeObject<List<CartDetail>>(cart);
                }

                // Thêm sản phẩm vào giỏ hàng
                foreach (var item in cartList)
                {
                    if (_context.CartDetail.Where(x => x.ProductId == item.ProductId && x.Quantity == item.Quantity) == null)
                    {
                        _context.CartDetail.Add(item);
                        await _context.SaveChangesAsync();
                    }

                }
                HttpContext.Response.Cookies.Append("CartDetail", "", new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            }

            return LocalRedirect(returnUrl);
        }


        public IActionResult Logout()
        {
            //Xóa các Session để gán các giá trị mới
            HttpContext.Session.Remove("name");
            HttpContext.Session.Remove("pass");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("id");
            HttpContext.SignOutAsync();
            return View("Login");
        }
        public IActionResult SignUp() {
            ViewBag.Index = "Signup";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(string name, string email, string password, string repassword)
        {
            var newAccount = new Account { Name = name, Email = email, Password = password };
            if (password.Length < 8 || repassword.Length < 8)
            {
                ViewBag.Alert = "Password Must Be Between 8 and 30 Characters.";
                return View(newAccount);
            }
            if (password != repassword && _context.Account.FirstOrDefault(p => p.Name == name && p.Password == password && p.Email != email) == null)
            {
                ViewBag.Alert = "Passwords do not match and account already exists";
                return View(newAccount);
            }
            if(password != repassword)
            {
                ViewBag.Alert = "Passwords do not match";
                return View(newAccount);
            }
            if(_context.Account.FirstOrDefault(p => p.Name == name && p.Password == password && p.Email == email) != null)
            {
                ViewBag.Alert = "Account already exists";
                return View(newAccount);
            }
            if (ModelState.IsValid && password == repassword && _context.Account.FirstOrDefault(p=>p.Name==name&& p.Password == password && p.Email ==email)==null)
            {              
                _context.Account.Add(newAccount);
                 await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(newAccount);
        }
        public IActionResult ResetPassword() {
            ViewBag.Index = "Resetpassword";
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string email,string oldpassword, string password, string repassword)
        {
            if (password.Length < 8 || repassword.Length < 8|| oldpassword.Length<8)
            {
                ViewBag.Alert = "Password Must Be Between 8 and 30 Characters.";
                return View();
            }
            if (password != repassword && _context.Account.FirstOrDefault(p =>  p.Password == oldpassword && p.Email != email) == null)
            {
                ViewBag.Alert = "Passwords do not match and account already exists";
                return View();
            }
            if (password != repassword)
            {
                ViewBag.Alert = "Passwords do not match";
                return View();
            }
            if (ModelState.IsValid && password == repassword)
            {
                var oldAccount = _context.Account.FirstOrDefault(p => p.Email == email && p.Password == oldpassword);
                if(oldAccount!=null) { 
                    oldAccount.Password = password;
                    _context.Account.Update(oldAccount);
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
    }
}
