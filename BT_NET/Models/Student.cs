using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models
{
    public class Student{
        [Key]
        public string StudentCode{get; set;} = default!;
        public string? Fullname{get; set;} 
        public int? Age { get; set; }
    
    }
}