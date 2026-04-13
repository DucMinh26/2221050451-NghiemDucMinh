using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BT_NET.Models.Entities;

namespace BT_NET.Models.Entities
{
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Mã không được để trống")]
        public string StudentId { get; set; } = default!;
        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Họ và Tên")]
        public string StudentName { get; set; } = default!;
        [ForeignKey("Faculty")]
        public string FacultyId { get; set; } = default!;
        [Display(Name = "Tên Khoa")]
        public virtual Faculty? Faculty { get; set; }
    }
}