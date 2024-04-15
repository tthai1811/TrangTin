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
	public class DichVuController : Controller
    {
        private readonly ThucTapDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DichVuController(ThucTapDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: DichVu
        public async Task<IActionResult> Index()
        {
            var iTShopDbContext = _context.DichVu.Include(b => b.LoaiDichVu);
            return View(await iTShopDbContext.ToListAsync());
        }


        // GET: DichVu/Create
        public IActionResult Create()
        {
            ViewData["LoaiDichVuID"] = new SelectList(_context.LoaiDichVu, "ID", "TenLoai");
            return View();
           
        }

        // POST: DichVu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LoaiDichVuID,TenDV,TieuDe,NoiDung")] DichVu dichVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, cần lấy lại danh sách loại dịch vụ và gửi nó đến view
            ViewData["LoaiDichVuID"] = new SelectList(_context.LoaiDichVu, "ID", "TenLoai");
            return View();
        }


        // GET: DichVu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DichVu == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVu.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }
            // Nếu ModelState không hợp lệ, cần lấy lại danh sách loại dịch vụ và gửi nó đến view
            ViewData["LoaiDichVuID"] = new SelectList(_context.LoaiDichVu, "ID", "TenLoai");
            return View();
        }

        // POST: DichVu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LoaiDichVuID,TenDV,TieuDe,NoiDung")] DichVu dichVu)
        {
            if (id != dichVu.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichVuExists(dichVu.ID))
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
            ViewData["LoaiDichVuID"] = new SelectList(_context.LoaiDichVu, "ID", "TenLoai");
            return View();
        }

        // GET: DichVu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DichVu == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVu
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        // POST: DichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DichVu == null)
            {
                return Problem("Entity set 'ThucTapDbContext.DichVu'  is null.");
            }
            var dichVu = await _context.DichVu.FindAsync(id);
            if (dichVu != null)
            {
                _context.DichVu.Remove(dichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DichVuExists(int id)
        {
            return (_context.DichVu?.Any(e => e.ID == id)).GetValueOrDefault();
        }


        // GET: DichVu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DichVu == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVu
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }
    }
}
