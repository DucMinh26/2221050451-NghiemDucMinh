using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models.Entities
{
    public class SinhVien
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mã sinh viên không được để tróng")]
        public string MaSV { get; set; } = string.Empty;
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        public string HoTen { get; set; } = string.Empty;
        public string Lop { get; set; } = string.Empty;
        [Range(0, 10, ErrorMessage = "Điểm từ 0 - 10")]
        public double DiemTB { get; set; }
    }
}