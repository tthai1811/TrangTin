
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Drawing.Printing;
using Slugify;
using ThucTap.Models;

namespace ThucTap.Controllers
{
	public class HomeController : Controller
	{
		private readonly ThucTapDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HomeController(ThucTapDbContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

        public IActionResult Index(int? trang)
        {
            var danhSach = LayDanhSachBaiViet(trang ?? 1);
            if (danhSach.BaiViet.Count == 0)
                return NotFound();
            else
                return View(danhSach);
        }

        // GET: Login
        [AllowAnonymous]
		public IActionResult Login(string? ReturnUrl)
		{
			if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
			{
				// Nếu đã đăng nhập thì chuyển đến trang chủ
				return LocalRedirect(ReturnUrl ?? "/");
			}
			else
			{
				
		    // Nếu chưa đăng nhập thì chuyển đến trang đăng nhập
 ViewBag.LienKetChuyenTrang = ReturnUrl ?? "/";
				return View();
			}
		}

		// POST: Login
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login([Bind] DangNhap dangNhap)
		{
			if (ModelState.IsValid)
			{
				var nguoiDung = _context.NguoiDung.Where(r => r.TenDangNhap == dangNhap.TenDangNhap).SingleOrDefault();

				if (nguoiDung == null || !BC.Verify(dangNhap.MatKhau, nguoiDung.MatKhau))
				{
					TempData["ThongBaoLoi"] = "Tài khoản không tồn tại trong hệ thống.";
					return View(dangNhap);
				}
				else
				{
					var claims = new List<Claim>
 {
 new Claim("ID", nguoiDung.ID.ToString()),
 new Claim(ClaimTypes.Name, nguoiDung.TenDangNhap),
new Claim("HoVaTen", nguoiDung.HoVaTen),
 new Claim(ClaimTypes.Role, nguoiDung.Quyen ? "Admin" : "User")
 };
					var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var authProperties = new AuthenticationProperties
					{
						IsPersistent = dangNhap.DuyTriDangNhap
					};
					// Đăng nhập hệ thống
					await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity),
				   authProperties);

					return LocalRedirect(dangNhap.LienKetChuyenTrang ?? (nguoiDung.Quyen ? "/Admin" : "/"));
				}
			}

			return View(dangNhap);
		}

		// GET: Register
		[AllowAnonymous]
		public IActionResult Register(string? successMessage)
		{
			if (!string.IsNullOrEmpty(successMessage))
				TempData["ThongBao"] = successMessage;
			return View();
		}
		// POST: Register
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Register([Bind("ID,HoVaTen,Email,DienThoai,GioiTinh,DiaChi,TenDangNhap,MatKhau,XacNhanMatKhau")] NguoiDung nguoiDung)
		{
			if (ModelState.IsValid)
			{
				var kiemTra = _context.NguoiDung.Where(r => r.TenDangNhap == nguoiDung.TenDangNhap).SingleOrDefault();
				if (kiemTra == null)
				{
					nguoiDung.MatKhau = BC.HashPassword(nguoiDung.MatKhau);
					nguoiDung.XacNhanMatKhau = BC.HashPassword(nguoiDung.MatKhau);
					nguoiDung.Quyen = false; // Khách hàng
					nguoiDung.NgayTao = DateTime.Now;
					_context.Add(nguoiDung);
					await _context.SaveChangesAsync();
					return RedirectToAction("Login", "Home", new { Area = "", successMessage = "Đăng ký tài khoản thành công." });
				}
				else
				{
					TempData["ThongBaoLoi"] = "Tên đăng nhập này đã được sử dụng cho một tài khoản khác.";
					return View(nguoiDung);
				}
			}
			return View(nguoiDung);
		}

		// GET: DangXuat
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home", new { Area = "" });
		}

		// GET: Forbidden
		public IActionResult Forbidden()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		// Thêm action cho trang giới thiệu
		public IActionResult GioiThieu()
		{
			return View();
		}

		// GET: Index
		
		private PhanTrangBaiViet LayDanhSachBaiViet(int trangHienTai)
		{
			int maxRows = 12;
			PhanTrangBaiViet phanTrang = new PhanTrangBaiViet();
			phanTrang.BaiViet = _context.BaiViet
			.Include(s => s.NguoiDung)
			.Include(s => s.ChuDe)
			.Where(r => r.KiemDuyet == true && r.HienThi == true)
			.OrderByDescending(r => r.NgayDang)
			.Skip((trangHienTai - 1) * maxRows)
			.Take(maxRows).ToList();
			decimal tongSoTrang = Convert.ToDecimal(_context.BaiViet.Count()) / Convert.ToDecimal(maxRows);
			phanTrang.TongSoTrang = (int)Math.Ceiling(tongSoTrang);
			phanTrang.TrangHienTai = trangHienTai;
			return phanTrang;
		}



    }

	

}
