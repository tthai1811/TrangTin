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
	public class LoaiDichVuController : Controller
    {
        private readonly ThucTapDbContext _context;

        public LoaiDichVuController(ThucTapDbContext context)
        {
            _context = context;
        }

        // GET: LoaiDichVu
        public async Task<IActionResult> Index()
        {
            return _context.LoaiDichVu != null ?
                        View(await _context.LoaiDichVu.ToListAsync()) :
                        Problem("Entity set 'ThucTapDbContext.LoaiDichVu'  is null.");
        }

        // GET: LoaiDichVu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiDichVu == null)
            {
                return NotFound();
            }

            var loaiDichVu = await _context.LoaiDichVu
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loaiDichVu == null)
            {
                return NotFound();
            }

            return View(loaiDichVu);
        }

        // GET: LoaiDichVu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiDichVu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenLoai")] LoaiDichVu loaiDichVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiDichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiDichVu);
        }

        // GET: LoaiDichVu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiDichVu == null)
            {
                return NotFound();
            }

            var loaiDichVu = await _context.LoaiDichVu.FindAsync(id);
            if (loaiDichVu == null)
            {
                return NotFound();
            }
            return View(loaiDichVu);
        }

        // POST: LoaiDichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenLoai")] LoaiDichVu loaiDichVu)
        {
            if (id != loaiDichVu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiDichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiDichVuExists(loaiDichVu.ID))
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
            return View(loaiDichVu);
        }

        // GET: LoaiDichVu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiDichVu == null)
            {
                return NotFound();
            }

            var loaiDichVu = await _context.LoaiDichVu
                .FirstOrDefaultAsync(m => m.ID == id);
            if (loaiDichVu == null)
            {
                return NotFound();
            }

            return View(loaiDichVu);
        }

        // POST: LoaiDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiDichVu == null)
            {
                return Problem("Entity set 'ThucTapDbContext.LoaiDichVu'  is null.");
            }
            var loaiDichVu = await _context.LoaiDichVu.FindAsync(id);
            if (loaiDichVu != null)
            {
                _context.LoaiDichVu.Remove(loaiDichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiDichVuExists(int id)
        {
            return (_context.LoaiDichVu?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
