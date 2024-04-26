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
    public class NewController : Controller
    {
        private readonly TourWebContext _context;

        public NewController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/New
        public async Task<IActionResult> Index()
        {
            var tourWebContext = _context.New.Include(a => a.Accounts);
            return View(await tourWebContext.ToListAsync());
        }

        // GET: Admin/New/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.New = "active";
            if (id == null || _context.New == null)
            {
                return NotFound();
            }

            var @new = await _context.New
                .Include(a => a.Accounts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@new == null)
            {
                return NotFound();
            }

            return View(@new);
        }

        // GET: Admin/New/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name");
            return View();
        }

        // POST: Admin/New/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Date,ImageSpecail,AccountId")] New @new,IFormFile imageSpecial)
        {
            if (ModelState.IsValid)
            {
                if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                {
                    var fileName = "New-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    @new.ImageSpecail = fileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageSpecial.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    @new.ImageSpecail = Path.GetFileName(imageSpecial.FileName);
                }
                _context.New.Add(@new);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", @new.AccountId);
            return View(@new);
        }

        // GET: Admin/New/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.New == null)
            {
                return NotFound();
            }

            var @new = await _context.New.FindAsync(id);
            if (@new == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", @new.AccountId);
            return View(@new);
        }

        // POST: Admin/New/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Date,ImageSpecail,AccountId")] New @new, IFormFile imageSpecial, List<IFormFile> ManyImage)
        {
            if (id != @new.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var image in ManyImage)
                    {
                        if (image!=null &&image.Length > 0)
                        {
                            var fileName = Path.GetFileName(imageSpecial.FileName);
                            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                            {
                                fileName = "New-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await imageSpecial.CopyToAsync(fileStream);
                                }
                            }
                            var imageNew = new Image
                            {
                                ImagePath = fileName,
                                NewId = @new.Id,
                            };
                             _context.Image.Add(imageNew);
                            await _context.SaveChangesAsync();
                        }
                    }
                    if (imageSpecial!=null &&imageSpecial.Length > 0)
                    {
                        if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                        {
                            var fileName = "New-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                            @new.ImageSpecail = fileName;
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageSpecial.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            @new.ImageSpecail = Path.GetFileName(imageSpecial.FileName);
                        }
                    }
                    _context.Update(@new);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewExists(@new.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Name", @new.AccountId);
            return View(@new);
        }

        // GET: Admin/New/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.New == null)
            {
                return NotFound();
            }

            var @new = await _context.New
                .Include(a => a.Accounts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@new == null)
            {
                return NotFound();
            }

            return View(@new);
        }

        // POST: Admin/New/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.New == null)
            {
                return Problem("Entity set 'TourWebContext.New'  is null.");
            }
            var @new = await _context.New.FindAsync(id);
            if (@new != null)
            {
                _context.New.Remove(@new);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewExists(int id)
        {
          return (_context.New?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
