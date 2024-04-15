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
using BC = BCrypt.Net.BCrypt;
namespace ThucTap.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class NguoiDungController : Controller
    {
        private readonly ThucTapDbContext _context;

        public NguoiDungController(ThucTapDbContext context)
        {
            _context = context;
        }

        // GET: NguoiDung
        public async Task<IActionResult> Index()
        {
            return _context.NguoiDung != null ?
                        View(await _context.NguoiDung.ToListAsync()) :
                        Problem("Entity set 'ThucTapDbContext.NguoiDung'  is null.");
        }

        // GET: NguoiDung/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NguoiDung == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // GET: NguoiDung/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NguoiDung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,HoVaTen,Email,DienThoai,GioiTinh,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau,Quyen")] NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {

                nguoiDung.MatKhau = BC.HashPassword(nguoiDung.MatKhau);
                nguoiDung.XacNhanMatKhau = BC.HashPassword(nguoiDung.MatKhau);
                nguoiDung.NgayTao = DateTime.Now;
                _context.Add(nguoiDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(nguoiDung);
        }


        // GET: NguoiDung/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NguoiDung == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung == null)
            {
                return NotFound();
            }
            return View(new NguoiDung_ChinhSua(nguoiDung));
        }

        // POST: NguoiDung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,HoVaTen,Email,DienThoai,GioiTinh,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau,Quyen")] NguoiDung_ChinhSua nguoiDung)
        {
            if (id != nguoiDung.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var n = await _context.NguoiDung.FindAsync(id);

                    // Kiểm tra mật khẩu mới không được rỗng
                    if (nguoiDung.MatKhau != null && nguoiDung.XacNhanMatKhau != null)
                    {
                        n.ID = nguoiDung.ID;
                        n.HoVaTen = nguoiDung.HoVaTen;
                        n.Email = nguoiDung.Email;
                        n.DienThoai = nguoiDung.DienThoai;
                        n.GioiTinh = nguoiDung.GioiTinh;
                        n.DiaChi = nguoiDung.DiaChi;
                        n.TenDangNhap = nguoiDung.TenDangNhap;
                        n.MatKhau = BC.HashPassword(nguoiDung.MatKhau);
                        n.XacNhanMatKhau = nguoiDung.XacNhanMatKhau;
                        n.Quyen = nguoiDung.Quyen;
                    }
                    else // Giữ nguyên mật khẩu cũ
                    {
                        n.ID = nguoiDung.ID;
                        n.HoVaTen = nguoiDung.HoVaTen;
                        n.Email = nguoiDung.Email;
                        n.DienThoai = nguoiDung.DienThoai;
                        n.GioiTinh = nguoiDung.GioiTinh;
                        n.DiaChi = nguoiDung.DiaChi;
                        n.TenDangNhap = nguoiDung.TenDangNhap;
                        n.XacNhanMatKhau = n.MatKhau;
                        n.Quyen = nguoiDung.Quyen;
                    }

                    _context.Update(n);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NguoiDungExists(nguoiDung.ID))
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
            return View(nguoiDung);
        }

        // GET: NguoiDung/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NguoiDung == null)
            {
                return NotFound();
            }

            var nguoiDung = await _context.NguoiDung
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nguoiDung == null)
            {
                return NotFound();
            }

            return View(nguoiDung);
        }

        // POST: NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NguoiDung == null)
            {
                return Problem("Entity set 'ThucTapDbContext.NguoiDung'  is null.");
            }
            var nguoiDung = await _context.NguoiDung.FindAsync(id);
            if (nguoiDung != null)
            {
                _context.NguoiDung.Remove(nguoiDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NguoiDungExists(int id)
        {
            return (_context.NguoiDung?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
