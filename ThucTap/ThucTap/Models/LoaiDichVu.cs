using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ThucTap.Models
{
	public class LoaiDichVu
	{
		[DisplayName("Mã chủ Loai DV")]
		public int ID { get; set; }

		[StringLength(255)]
		[Required(ErrorMessage = "Tên loai dich vy không được bỏ trống.")]
		[DisplayName("Tên Loai Dich Vu")]
		public string TenLoai { get; set; }
		
		public ICollection<DichVu>? DichVu { get; set; }
	}
}
