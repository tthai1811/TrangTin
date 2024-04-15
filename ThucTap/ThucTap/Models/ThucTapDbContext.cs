using Microsoft.EntityFrameworkCore;
using ThucTap.Models;

namespace ThucTap.Models
{
	public class ThucTapDbContext : DbContext
	{
		public ThucTapDbContext(DbContextOptions<ThucTapDbContext> options) : base(options) { }
		public DbSet<NguoiDung> NguoiDung { get; set; }
		public DbSet<ChuDe> ChuDe { get; set; }
		public DbSet<BaiViet> BaiViet { get; set; }
		public DbSet<BinhLuanBaiViet> BinhLuanBaiViet { get; set; }
		public DbSet<DichVu> DichVu { get; set; }
		public DbSet<LoaiDichVu> LoaiDichVu { get; set; }
        public DbSet<TuyenDung> TuyenDung { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<NguoiDung>().ToTable("NguoiDung");
			modelBuilder.Entity<ChuDe>().ToTable("ChuDe");
			modelBuilder.Entity<BaiViet>().ToTable("BaiViet");
			modelBuilder.Entity<BinhLuanBaiViet>().ToTable("BinhLuanBaiViet");
			modelBuilder.Entity<DichVu>().ToTable("DichVu");
			modelBuilder.Entity<LoaiDichVu>().ToTable("LoaiDichVu");
            modelBuilder.Entity<TuyenDung>().ToTable("TuyenDung");
        }

	}
}

