using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models.ViewModels
{
    public class StudentVM
    {
        public string StudentId { get; set; } = default!;
        [Display(Name = "Họ và Tên")]
        public string StudentName { get; set; } = default!;
        [Display(Name = "Khoa")]
        public string FacultyName { get; set; } = default!;
    }
}