﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlugGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThucTap.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ThucTap.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ChuDeController : Controller
    {
        private readonly ThucTapDbContext _context;

        public ChuDeController(ThucTapDbContext context)
        {
            _context = context;
        }

        // GET: ChuDe
        public async Task<IActionResult> Index()
        {
            return _context.ChuDe != null ?
                        View(await _context.ChuDe.ToListAsync()) :
                        Problem("Entity set 'ThucTapDbContext.ChuDe'  is null.");
        }

        // GET: ChuDe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChuDe == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDe
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // GET: ChuDe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChuDe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TenChuDe,TenChuDeKhongDau")] ChuDe chuDe)
        {

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(chuDe.TenChuDeKhongDau))
                {
                    chuDe.TenChuDeKhongDau = chuDe.TenChuDe.GenerateSlug();
                }
                _context.Add(chuDe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuDe);
        }

        // GET: ChuDe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChuDe == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDe.FindAsync(id);
            if (chuDe == null)
            {
                return NotFound();
            }
            return View(chuDe);
        }

        // POST: ChuDe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TenChuDe,TenChuDeKhongDau")] ChuDe chuDe)
        {
            if (id != chuDe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(chuDe.TenChuDeKhongDau))
                    {
                        chuDe.TenChuDeKhongDau = chuDe.TenChuDe.GenerateSlug();
                    }
                    _context.Update(chuDe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuDeExists(chuDe.ID))
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
            return View(chuDe);
        }

        // GET: ChuDe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChuDe == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDe
                .FirstOrDefaultAsync(m => m.ID == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // POST: ChuDe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChuDe == null)
            {
                return Problem("Entity set 'ThucTapDbContext.ChuDe'  is null.");
            }
            var chuDe = await _context.ChuDe.FindAsync(id);
            if (chuDe != null)
            {
                _context.ChuDe.Remove(chuDe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChuDeExists(int id)
        {
            return (_context.ChuDe?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
