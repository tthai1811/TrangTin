using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThucTap.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ThucTap.Controllers
{
	public class BinhLuanBaiVietController : Controller
	{
		private readonly ThucTapDbContext _context; // Thay YourDbContext bằng tên của lớp DbContext trong ứng dụng của bạn
		private readonly IHttpContextAccessor _httpContextAccessor;

		public BinhLuanBaiVietController(ThucTapDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("BaiVietID, NoiDungBinhLuan")] BinhLuanBaiViet binhLuanBaiViet)
		{
			if (ModelState.IsValid)
			{
				// Lấy ID người dùng hiện tại từ Claims
				int maNguoiDung = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "ID")?.Value);

				// Gán thông tin bình luận
				binhLuanBaiViet.NguoiDungID = maNguoiDung;
				binhLuanBaiViet.NgayDang = DateTime.Now;
				binhLuanBaiViet.LuotXem = 0;
				binhLuanBaiViet.KiemDuyet = true; // Mặc định là chưa được kiểm duyệt

				// Thêm bình luận vào cơ sở dữ liệu
				_context.BinhLuanBaiViet.Add(binhLuanBaiViet);
				await _context.SaveChangesAsync();

				// Lấy thông tin bài viết để truyền khi chuyển hướng
				var baiViet = await _context.BaiViet
					.Include(b => b.ChuDe)
					.FirstOrDefaultAsync(b => b.ID == binhLuanBaiViet.BaiVietID);

				// Chuyển hướng người dùng đến trang chi tiết bài viết
				return RedirectToAction("ChiTiet", "BaiViet", new { tieuDe = baiViet.TieuDeKhongDau, tenChuDe = baiViet.ChuDe.TenChuDe });
			}

			// Nếu ModelState không hợp lệ, chuyển hướng về trang trước đó
			return RedirectToAction("Login", "Home");
		}
	
	}
}