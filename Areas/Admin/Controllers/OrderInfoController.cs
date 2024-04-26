using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourWeb.Models;

namespace TourWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderInfoController : Controller
    {
        private readonly TourWebContext _context;
        public OrderInfoController(TourWebContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        { 
            ViewBag.OrderInfo = "active";
            var tourWebContext = _context.OrderInfo;
            return View(await tourWebContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderInfo == null)
            {
                return NotFound();
            }

            var account = await _context.OrderInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Company,StreetAddress,City,Country,Zip,Email,Phone")] OrderInfo orderInfo)
        {
            if (ModelState.IsValid )
            {
                _context.Add(orderInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderInfo);
        }

        // GET: Admin/Account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderInfo == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfo.FindAsync(id);
            if (orderInfo == null)
            {
                return NotFound();
            }
            return View(orderInfo);
        }

        // POST: Admin/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Company,StreetAddress,City,Country,Zip,Email,Phone")] OrderInfo orderInfo)
        {
            if (id != orderInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(orderInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orderInfo);
        }

        // GET: Admin/Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderInfo == null)
            {
                return NotFound();
            }

            var orderInfo = await _context.OrderInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderInfo == null)
            {
                return NotFound();
            }

            return View(orderInfo);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderInfo == null)
            {
                return Problem("Entity set 'TourWebContext.Account'  is null.");
            }
            var orderInfo = await _context.OrderInfo.FindAsync(id);
            if (orderInfo != null)
            {
                _context.OrderInfo.Remove(orderInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return (_context.Account?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

