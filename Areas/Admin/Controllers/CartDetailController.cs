using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourWeb.Models;

namespace TourWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CartDetailController : Controller
    {
        private readonly TourWebContext _context;

        public CartDetailController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/CartDetail
        public async Task<IActionResult> Index()
        {
            ViewBag.CartDetail = "active";
            var tourWebContext = _context.CartDetail.Include(c => c.Carts).Include(c => c.Products);
            return View(await tourWebContext.ToListAsync());
        }

        // GET: Admin/CartDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CartDetail == null)
            {
                return NotFound();
            }

            var cartDetail = await _context.CartDetail
                .Include(c => c.Carts)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartDetail == null)
            {
                return NotFound();
            }

            return View(cartDetail);
        }

        // GET: Admin/CartDetail/Create
        public IActionResult Create()
        {
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: Admin/CartDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CartId,Quantity")] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartDetail.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", cartDetail.ProductId);
            return View(cartDetail);
        }

        // GET: Admin/CartDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartDetail == null)
            {
                return NotFound();
            }

            var cartDetail = await _context.CartDetail.FindAsync(id);
            if (cartDetail == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartDetail.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", cartDetail.ProductId);
            return View(cartDetail);
        }

        // POST: Admin/CartDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,CartId,Quantity")] CartDetail cartDetail)
        {
            if (id != cartDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartDetailExists(cartDetail.Id))
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
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", cartDetail.CartId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", cartDetail.ProductId);
            return View(cartDetail);
        }

        // GET: Admin/CartDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CartDetail == null)
            {
                return NotFound();
            }

            var cartDetail = await _context.CartDetail
                .Include(c => c.Carts)
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartDetail == null)
            {
                return NotFound();
            }

            return View(cartDetail);
        }

        // POST: Admin/CartDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartDetail == null)
            {
                return Problem("Entity set 'TourWebContext.CartDetail'  is null.");
            }
            var cartDetail = await _context.CartDetail.FindAsync(id);
            if (cartDetail != null)
            {
                _context.CartDetail.Remove(cartDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartDetailExists(int id)
        {
          return (_context.CartDetail?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
