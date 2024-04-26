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
    public class ItineraryController : Controller
    {
        private readonly TourWebContext _context;

        public ItineraryController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/Itinerary
        public async Task<IActionResult> Index()
        {
            ViewBag.Itinerary = "active";
            var tourWebContext = _context.Itinerary.Include(i => i.Tours).Include(i => i.Trekkings);
            return View(await tourWebContext.ToListAsync());
        }

        // GET: Admin/Itinerary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Itinerary == null)
            {
                return NotFound();
            }

            var itinerary = await _context.Itinerary
                .Include(i => i.Tours)
                .Include(i => i.Trekkings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // GET: Admin/Itinerary/Create
        public IActionResult Create()
        {
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Name");
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Name");
            return View();
        }

        // POST: Admin/Itinerary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Day,Detail,Accomodation,Meals,TourId,TrekkingId")] Itinerary itinerary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itinerary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Name", itinerary.TourId);
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Name", itinerary.TrekkingId);
            return View(itinerary);
        }

        // GET: Admin/Itinerary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Itinerary == null)
            {
                return NotFound();
            }

            var itinerary = await _context.Itinerary.FindAsync(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Name", itinerary.TourId);
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Name", itinerary.TrekkingId);
            return View(itinerary);
        }

        // POST: Admin/Itinerary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Day,Detail,Accomodation,Meals,TourId,TrekkingId")] Itinerary itinerary)
        {
            if (id != itinerary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itinerary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItineraryExists(itinerary.Id))
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
            ViewData["TourId"] = new SelectList(_context.Tour, "Id", "Name", itinerary.TourId);
            ViewData["TrekkingId"] = new SelectList(_context.Trekking, "Id", "Name", itinerary.TrekkingId);
            return View(itinerary);
        }

        // GET: Admin/Itinerary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Itinerary == null)
            {
                return NotFound();
            }

            var itinerary = await _context.Itinerary
                .Include(i => i.Tours)
                .Include(i => i.Trekkings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // POST: Admin/Itinerary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Itinerary == null)
            {
                return Problem("Entity set 'TourWebContext.Itinerary'  is null.");
            }
            var itinerary = await _context.Itinerary.FindAsync(id);
            if (itinerary != null)
            {
                _context.Itinerary.Remove(itinerary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryExists(int id)
        {
          return (_context.Itinerary?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
