using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThucTap.Models
{
	public class DichVu
	{
		[DisplayName("Mã DV")]
		public int ID { get; set; }
		
		[DisplayName("Loai Dịch Vụ ID ")]
		[Required(ErrorMessage = " không được bỏ trống")]
		public int LoaiDichVuID { get; set; }

		[StringLength(255)]
		[DisplayName("Tên Dịch Vụ")]
		[Required(ErrorMessage = "Ten dich vu không được bỏ trống")]
		public string TenDV { get; set; }

		[DisplayName("Tieu De")]
		[Required(ErrorMessage = "Tiêu đề không được bỏ trống")]
		public string TieuDe { get; set; }

		[Column(TypeName = "ntext")]
		[DisplayName("Nội dung")]
		[Required(ErrorMessage = "Nội dung bài viết không được bỏ trống.")]
		[DataType(DataType.MultilineText)]
		public string NoiDung { get; set; }

        public LoaiDichVu?LoaiDichVu { get; set; }
	}
}
