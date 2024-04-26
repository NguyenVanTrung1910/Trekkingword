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
    public class ImageController : Controller
    {
        private readonly TourWebContext _context;

        public ImageController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/Image
        public async Task<IActionResult> Index()
        {
            ViewBag.Image = "active";
            var tourWebContext = _context.Image.Include(i => i.Itinerarys).Include(i => i.News).Include(i => i.Products).Include(i => i.Tours).Include(i => i.Trekkings);
            return View(await tourWebContext.ToListAsync());
        }

        // GET: Admin/Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.Itinerarys)
                .Include(i => i.News)
                .Include(i => i.Products)
                .Include(i => i.Tours)
                .Include(i => i.Trekkings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Admin/Image/Create
        public IActionResult Create()
        {
			var Itinerary = _context.Itinerary.ToList(); Itinerary.Insert(0, new Itinerary { Id = 0, Name = "null" });
			var New = _context.New.ToList(); New.Insert(0, new New { Id = 0, Title = "null" });
			var Product = _context.Product.ToList(); Product.Insert(0, new Product { Id = 0, Name = "null" });
			var Tour = _context.Tour.ToList(); Tour.Insert(0, new Tour { Id = 0, Name = "null" });
			var Trekking = _context.Trekking.ToList(); Trekking.Insert(0, new Trekking { Id = 0, Name = "null" });
			ViewData["ItineraryId"] = new SelectList(Itinerary, "Id", "Name");
			ViewData["NewId"] = new SelectList(New, "Id", "Title");
			ViewData["ProductId"] = new SelectList(Product, "Id", "Name");
			ViewData["TourId"] = new SelectList(Tour, "Id", "Name");
			ViewData["TrekkingId"] = new SelectList(Trekking, "Id", "Name");
			return View();
        }

        // POST: Admin/Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImagePath,NewId,ProductId,TourId,TrekkingId,ItineraryId")] Image image)
        {
            if (ModelState.IsValid)
            {
                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			var Itinerary = _context.Itinerary.ToList(); Itinerary.Insert(0, new Itinerary { Id = 0, Name = "null" });
			var New = _context.New.ToList(); New.Insert(0, new New { Id = 0, Title = "null" });
			var Product = _context.Product.ToList(); Product.Insert(0, new Product { Id = 0, Name = "null" });
			var Tour = _context.Tour.ToList(); Tour.Insert(0, new Tour { Id = 0, Name = "null" });
			var Trekking = _context.Trekking.ToList(); Trekking.Insert(0, new Trekking { Id = 0, Name = "null" });
			ViewData["ItineraryId"] = new SelectList(Itinerary, "Id", "Name");
			ViewData["NewId"] = new SelectList(New, "Id", "Title");
			ViewData["ProductId"] = new SelectList(Product, "Id", "Name");
			ViewData["TourId"] = new SelectList(Tour, "Id", "Name");
			ViewData["TrekkingId"] = new SelectList(Trekking, "Id", "Name");
			return View(image);
        }

        // GET: Admin/Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
			var Itinerary = _context.Itinerary.ToList(); Itinerary.Insert(0,new Itinerary { Id = 0, Name = "null" });
			var New = _context.New.ToList(); New.Insert(0,new New { Id = 0, Title = "null" });
			var Product = _context.Product.ToList(); Product.Insert(0,new Product { Id = 0, Name = "null" });
			var Tour = _context.Tour.ToList(); Tour.Insert(0, new Tour { Id = 0, Name = "null" });
			var Trekking = _context.Trekking.ToList(); Trekking.Insert(0, new Trekking { Id = 0, Name = "null" });
			ViewData["ItineraryId"] = new SelectList(Itinerary, "Id", "Name");
			ViewData["NewId"] = new SelectList(New, "Id", "Title");
			ViewData["ProductId"] = new SelectList(Product, "Id", "Name");
			ViewData["TourId"] = new SelectList(Tour, "Id", "Name");
			ViewData["TrekkingId"] = new SelectList(Trekking, "Id", "Name");
			return View(image);
        }

        // POST: Admin/Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImagePath,NewId,ProductId,TourId,TrekkingId,ItineraryId")] Image image)
        {
            if (id != image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageExists(image.Id))
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
			var Itinerary = _context.Itinerary.ToList(); Itinerary.Insert(0, new Itinerary { Id = 0, Name = "null" });
			var New = _context.New.ToList(); New.Insert(0, new New { Id = 0, Title = "null" });
			var Product = _context.Product.ToList(); Product.Insert(0, new Product { Id = 0, Name = "null" });
			var Tour = _context.Tour.ToList(); Tour.Insert(0, new Tour { Id = 0, Name = "null" });
			var Trekking = _context.Trekking.ToList(); Trekking.Insert(0, new Trekking { Id = 0, Name = "null" });
			ViewData["ItineraryId"] = new SelectList(Itinerary, "Id", "Name");
			ViewData["NewId"] = new SelectList(New, "Id", "Title");
			ViewData["ProductId"] = new SelectList(Product, "Id", "Name");
			ViewData["TourId"] = new SelectList(Tour, "Id", "Name");
			ViewData["TrekkingId"] = new SelectList(Trekking, "Id", "Name");
			return View(image);
        }

        // GET: Admin/Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Image == null)
            {
                return NotFound();
            }

            var image = await _context.Image
                .Include(i => i.Itinerarys)
                .Include(i => i.News)
                .Include(i => i.Products)
                .Include(i => i.Tours)
                .Include(i => i.Trekkings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Admin/Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Image == null)
            {
                return Problem("Entity set 'TourWebContext.Image'  is null.");
            }
            var image = await _context.Image.FindAsync(id);
            if (image != null)
            {
                _context.Image.Remove(image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
          return (_context.Image?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
