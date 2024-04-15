
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThucTap.Models;
using Microsoft.AspNetCore.Mvc;

namespace ThucTap.Controllers
{
    public class TuyenDungController : Controller
    {
        private readonly ThucTapDbContext _context;

        public TuyenDungController(ThucTapDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tuyenDungs = await _context.TuyenDung.ToListAsync();
            return View(tuyenDungs);
        }
    }
}