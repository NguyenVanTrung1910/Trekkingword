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
    public class BookTourController : Controller
    {
        private readonly TourWebContext _context;

        public BookTourController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/BookTour
        public async Task<IActionResult> Index()
        {
            ViewBag.BookTour = "active";
            var tourWebContext = _context.BookTour.Include(b => b.Accounts).Include(b => b.Tour).Include(b => b.Trekkings);
            return View(await tourWebContext.ToListAsync());
        }

        // GET: Admin/BookTour/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookTour == null)
            {
                return NotFound();
            }

            var bookTour = await _context.BookTour
                .Include(b => b.Accounts)
                .Include(b => b.Tour)
                .Include(b => b.Trekkings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookTour == null)
            {
                return NotFound();
            }

            return View(bookTour);
        }

        // GET: Admin/BookTour/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name");
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Id");
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Id");
            return View();
        }

        // POST: Admin/BookTour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Country,Time,Date,GuestNumber,ChildrenNumber,TotalMoney,TourId,TrekkingId,AccountId")] BookTour bookTour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookTour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", bookTour.AccountId);
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Id", bookTour.TourId);
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Id", bookTour.TrekkingId);
            return View(bookTour);
        }

        // GET: Admin/BookTour/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookTour == null)
            {
                return NotFound();
            }

            var bookTour = await _context.BookTour.FindAsync(id);
            if (bookTour == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", bookTour.AccountId);
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Id", bookTour.TourId);
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Id", bookTour.TrekkingId);
            return View(bookTour);
        }

        // POST: Admin/BookTour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country,Time,Date,GuestNumber,ChildrenNumber,TotalMoney,TourId,TrekkingId,AccountId")] BookTour bookTour)
        {
            if (id != bookTour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookTour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookTourExists(bookTour.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", bookTour.AccountId);
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Id", bookTour.TourId);
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Id", bookTour.TrekkingId);
            return View(bookTour);
        }

        // GET: Admin/BookTour/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookTour == null)
            {
                return NotFound();
            }

            var bookTour = await _context.BookTour
                .Include(b => b.Accounts)
                .Include(b => b.Tour)
                .Include(b => b.Trekkings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookTour == null)
            {
                return NotFound();
            }

            return View(bookTour);
        }

        // POST: Admin/BookTour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookTour == null)
            {
                return Problem("Entity set 'TourWebContext.BookTour'  is null.");
            }
            var bookTour = await _context.BookTour.FindAsync(id);
            if (bookTour != null)
            {
                _context.BookTour.Remove(bookTour);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookTourExists(int id)
        {
          return (_context.BookTour?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
