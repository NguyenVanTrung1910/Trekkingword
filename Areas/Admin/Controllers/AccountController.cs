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
    public class AccountController : Controller
    {
        private readonly TourWebContext _context;

        public AccountController(TourWebContext context)
        {
            _context = context;
        }

        // GET: Admin/Account
        public async Task<IActionResult> Index()
        {
            ViewBag.Account = "active";
              return _context.Account != null ? 
                          View(await _context.Account.ToListAsync()) :
                          Problem("Entity set 'TourWebContext.Account'  is null.");
        }

        // GET: Admin/Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
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
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,PhoneNumber,Address,Role,Detail,BankName,BankNumber,Monney,Sex,ImagePath")] Account account,IFormFile imagePath)
        {
            if (ModelState.IsValid )
            {
                if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imagePath.FileName))))
                {
                    var fileName = "Account-" + Path.GetFileNameWithoutExtension(imagePath.FileName) + Path.GetExtension(imagePath.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                    account.ImagePath = fileName;
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imagePath.CopyToAsync(fileStream);
                    }
                }
                else
                {
                    account.ImagePath = Path.GetFileName(imagePath.FileName);
                }
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/Account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Admin/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,PhoneNumber,Address,Role,Detail,BankName,BankNumber,Monney,Sex,ImagePath")] Account account, IFormFile imagePath)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", Path.GetFileName(imagePath.FileName))))
                    {
                        var fileName = "Account-" + Path.GetFileNameWithoutExtension(imagePath.FileName) + Path.GetExtension(imagePath.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);
                        account.ImagePath = fileName;
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imagePath.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        account.ImagePath = Path.GetFileName(imagePath.FileName);
                    }
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            return View(account);
        }

        // GET: Admin/Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Account == null)
            {
                return Problem("Entity set 'TourWebContext.Account'  is null.");
            }
            var account = await _context.Account.FindAsync(id);
            if (account != null)
            {
                _context.Account.Remove(account);
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
