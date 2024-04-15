using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ThucTap.Models
{
    public class TuyenDung
    {

        [DisplayName("Mã Tuyển Dụng")]
        public int ID { get; set; }

        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Nội dung không được bỏ trống.")]
        [DataType(DataType.MultilineText)]
        public string NoiDung { get; set; }
    }
}
