using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThucTap.Models;

namespace ThucTap.Controllers
{
    public class DichVuController : Controller
    {
        private readonly ThucTapDbContext _context;
        private readonly HttpContext _httpContext = new HttpContextAccessor().HttpContext;
        public DichVuController(ThucTapDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // Action để load danh sách dịch vụ dựa trên tên loại
        public IActionResult LoadDanhSachDichVu(string tenloai)
        {
            // Truy vấn cơ sở dữ liệu để lấy danh sách dịch vụ cho tên loại đã chọn
            var danhSachDichVu = _context.DichVu.Where(dv => dv.LoaiDichVu.TenLoai == tenloai).ToList();

            // Trả về dữ liệu dưới dạng JSON
            return Json(danhSachDichVu);
        }
    }
}
