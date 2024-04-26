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
    public class ProductController : Controller
    {
        private readonly TourWebContext _context;

        public ProductController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index()
        {
            ViewBag.Product = "active";
            var tourWebContext = _context.Product.Include(p => p.Categorys);
            return View(await tourWebContext.ToListAsync());
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Categorys)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Detail,Price,Quantity,ImagesSpecial,CategoryId,Description,Review,Feature")] Product product,IFormFile imageSpecial)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(imageSpecial.FileName);
                if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                {
                    fileName = "Product-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageSpecial.CopyToAsync(fileStream);
                    }
                }
                product.ImagesSpecial = fileName;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Detail,Price,Quantity,ImagesSpecial,CategoryId,Description,Review,Feature")] Product product, IFormFile imageSpecial, List<IFormFile> ManyImage)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach(var item in ManyImage)
                    {
                        if(item!=null && item.Length > 0)
                        {
                            var fileNameItem = Path.GetFileName(imageSpecial.FileName);
                            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                            {
                                fileNameItem = "Product-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileNameItem);
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await imageSpecial.CopyToAsync(fileStream);
                                }
                            }
                            var image = new Image
                            {
                                ImagePath = fileNameItem,
                                ProductId = product.Id
                            };
                            _context.Image.Add(image);
                            await _context.SaveChangesAsync();
                        }
                    }
                    var fileName = Path.GetFileName(imageSpecial.FileName);
                    if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imageSpecial.FileName))))
                    {
                        fileName = "Product-" + Path.GetFileNameWithoutExtension(imageSpecial.FileName) + Path.GetExtension(imageSpecial.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageSpecial.CopyToAsync(fileStream);
                        }
                    }
                    product.ImagesSpecial = fileName;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Categorys)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'TourWebContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
