using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models.Entities
{
    public class Faculty
    {
        [Key]
        [Required(ErrorMessage ="Mã không được để trống")]
        public string FacultyId { get; set; } = default!;
        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Tên Khoa")]
        public string FacultyName { get; set; } = default!;

        public virtual ICollection<Student>? Students { get; set; }
    }
}