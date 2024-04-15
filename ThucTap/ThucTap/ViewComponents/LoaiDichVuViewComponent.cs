using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ThucTap.Models;

namespace ThucTap.ViewComponents
{
    public class LoaiDichVuViewComponent : ViewComponent
    {
        private readonly ThucTapDbContext _context;

        public LoaiDichVuViewComponent(ThucTapDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var loaiDichVu = _context.LoaiDichVu
                             .Include(ldv => ldv.DichVu) // Kết hợp thông tin từ bảng DichVu
                             .OrderBy(ldv => ldv.TenLoai)
                             .ToList();
            return View(loaiDichVu);
        }
    }
}
