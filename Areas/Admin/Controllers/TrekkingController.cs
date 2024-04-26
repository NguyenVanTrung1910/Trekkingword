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
    public class TrekkingController : Controller
    {
        private readonly TourWebContext _context;

        public TrekkingController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/Trekking
        public async Task<IActionResult> Index()
        {
            ViewBag.Trekking = "active";
              return _context.Trekking != null ? 
                          View(await _context.Trekking.ToListAsync()) :
                          Problem("Entity set 'TourWebContext.Trekking'  is null.");
        }

        // GET: Admin/Trekking/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Trekking == null)
            {
                return NotFound();
            }

            var trekking = await _context.Trekking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trekking == null)
            {
                return NotFound();
            }

            return View(trekking);
        }

        // GET: Admin/Trekking/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trekking/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Detail,Price,Review,Address,Duration,Feature,Height,Difficulty,ImageSpecial,Area")] Trekking trekking, IFormFile imageSpecial)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(imageSpecial.FileName);
                if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                {
                    fileName = "Trekking-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageSpecial.CopyToAsync(fileStream);
                    }
                }
                trekking.ImageSpecial = fileName;
                _context.Add(trekking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trekking);
        }

        // GET: Admin/Trekking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Trekking == null)
            {
                return NotFound();
            }

            var trekking = await _context.Trekking.FindAsync(id);
            if (trekking == null)
            {
                return NotFound();
            }
            return View(trekking);
        }

        // POST: Admin/Trekking/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Detail,Price,Review,Address,Duration,Feature,Height,Difficulty,ImageSpecial,Area")] Trekking trekking, IFormFile imageSpecial,List<IFormFile> ManyImage)
        {
            if (id != trekking.Id)
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
                            fileName = "Trekking-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageSpecial.CopyToAsync(fileStream);
                            }
                        }
                        var newImage = new Image
                        {
                            ImagePath = fileName,
                            TrekkingId = trekking.Id
                        };
                        _context.Image.Add(newImage);
                        await _context.SaveChangesAsync();
                    }
                    if (imageSpecial != null)
                    {
                        var fileName = Path.GetFileName(imageSpecial.FileName);
                        if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                        {
                            fileName = "Trekking-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageSpecial.CopyToAsync(fileStream);
                            }
                        }
                        trekking.ImageSpecial = fileName;
                    }
                    _context.Update(trekking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrekkingExists(trekking.Id))
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
            return View(trekking);
        }

        // GET: Admin/Trekking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Trekking == null)
            {
                return NotFound();
            }

            var trekking = await _context.Trekking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trekking == null)
            {
                return NotFound();
            }

            return View(trekking);
        }

        // POST: Admin/Trekking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Trekking == null)
            {
                return Problem("Entity set 'TourWebContext.Trekking'  is null.");
            }
            var trekking = await _context.Trekking.FindAsync(id);
            if (trekking != null)
            {
                _context.Trekking.Remove(trekking);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrekkingExists(int id)
        {
          return (_context.Trekking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
