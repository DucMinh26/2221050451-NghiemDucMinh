using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models.Entities
{
    public class Customer
    {
        [Key]
        [Display(Name = "Mã khách hàng")]
        public string CustomerId { get; set; } = default!;
        
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        [Display(Name = "Họ và Tên")]
        public string CustomerName { get; set; } = default!;
        public string Email { get; set; } = default!;
        //1 Khách hàng có thể có nhiều đơn hàng
        public virtual ICollection<Order>? Orders { get; set; }
    }
}