using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage ="Mã sinh viên không được để trống")]
        [StringLength(10,MinimumLength =2, ErrorMessage ="Mã sinh viên phải từ 2 đến 10 ký tự")]
        public string StudentCode { get; set; } = default!;

        [Required(ErrorMessage ="Tên sinh viên không được để trống")]
        [StringLength(50,MinimumLength =5, ErrorMessage ="Tên sinh viên phải từ 5 đến 50 ký tự")]
        public string? FullName { get; set; }

        [Range(18,100, ErrorMessage ="Tuổi phải từ 18 đến 100")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        public string? Email { get; set; }
    }
}