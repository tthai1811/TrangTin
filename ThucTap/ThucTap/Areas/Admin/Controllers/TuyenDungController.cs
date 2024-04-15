using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThucTap.Models;

namespace ThucTap.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TuyenDungController : Controller
    {
        private readonly ThucTapDbContext _context;

        public TuyenDungController(ThucTapDbContext context)
        {
            _context = context;
        }

        // GET: TuyenDung
        public async Task<IActionResult> Index()
        {
            return _context.TuyenDung != null ?
                        View(await _context.TuyenDung.ToListAsync()) :
                        Problem("Entity set 'ThucTapDbContext.TuyenDung'  is null.");
        }

        // GET: TuyenDung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TuyenDung == null)
            {
                return NotFound();
            }

            var tuyenDung = await _context.TuyenDung
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tuyenDung == null)
            {
                return NotFound();
            }

            return View(tuyenDung);
        }

        // GET: TuyenDung/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TuyenDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NoiDung")] TuyenDung tuyenDung)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tuyenDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tuyenDung);
        }

        // GET: TuyenDung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TuyenDung == null)
            {
                return NotFound();
            }

            var tuyenDung = await _context.TuyenDung.FindAsync(id);
            if (tuyenDung == null)
            {
                return NotFound();
            }
            return View(tuyenDung);
        }

        // POST: TuyenDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NoiDung")] TuyenDung tuyenDung)
        {
            if (id != tuyenDung.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tuyenDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TuyenDungExists(tuyenDung.ID))
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
            return View(tuyenDung);
        }

        // GET: TuyenDung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TuyenDung == null)
            {
                return NotFound();
            }

            var tuyenDung = await _context.TuyenDung
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tuyenDung == null)
            {
                return NotFound();
            }

            return View(tuyenDung);
        }

        // POST: TuyenDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TuyenDung == null)
            {
                return Problem("Entity set 'ThucTapDbContext.TuyenDung'  is null.");
            }
            var tuyenDung = await _context.TuyenDung.FindAsync(id);
            if (tuyenDung != null)
            {
                _context.TuyenDung.Remove(tuyenDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TuyenDungExists(int id)
        {
            return (_context.TuyenDung?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
