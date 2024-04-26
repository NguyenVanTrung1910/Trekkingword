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
    public class TourController : Controller
    {
        private readonly TourWebContext _context;

        public TourController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/Tour
        public async Task<IActionResult> Index()
        {
            ViewBag.Tour = "active";
            return _context.Tour != null ? 
                          View(await _context.Tour.ToListAsync()) :
                          Problem("Entity set 'TourWebContext.Tour'  is null.");
        }

        // GET: Admin/Tour/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tour == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: Admin/Tour/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Tour/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Detail,Price,Review,Address,Duration,GroupSize,Feature,ImageSpecial,Area")] Tour tour, IFormFile imageSpecial)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(imageSpecial.FileName);
                if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                {
                    fileName = "Tour-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageSpecial.CopyToAsync(fileStream);
                    }
                }
                tour.ImageSpecial = fileName;
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Admin/Tour/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tour == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tour/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Detail,Price,Review,Address,Duration,GroupSize,Feature,ImageSpecial,Area")] Tour tour, IFormFile imageSpecial, List<IFormFile> ManyImage)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var file in ManyImage)
                    {
                        var fileName = Path.GetFileName(imageSpecial.FileName);
                        if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                        {
                            fileName = "Tour-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageSpecial.CopyToAsync(fileStream);
                            }
                        }
                        var newImage = new Image
                        {
                            ImagePath = fileName,
                            TourId = tour.Id
                        };
                        _context.Image.Add(newImage);
                        await _context.SaveChangesAsync();
                    }
                    if(imageSpecial != null)
                    {
                        var fileName = Path.GetFileName(imageSpecial.FileName);
                        if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                        {
                            fileName = "Tour-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageSpecial.CopyToAsync(fileStream);
                            }
                        }
                        tour.ImageSpecial = fileName;
                    }
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.Id))
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
            return View(tour);
        }

        // GET: Admin/Tour/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tour == null)
            {
                return NotFound();
            }

            var tour = await _context.Tour
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Admin/Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tour == null)
            {
                return Problem("Entity set 'TourWebContext.Tour'  is null.");
            }
            var tour = await _context.Tour.FindAsync(id);
            if (tour != null)
            {
                _context.Tour.Remove(tour);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
          return (_context.Tour?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
