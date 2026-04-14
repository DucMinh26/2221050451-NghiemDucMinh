using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BT_NET.Models.Entities
{
    public class Order
    {
        [Key]
        [Display(Name = "Mã đơn hàng")]
        public string OrderId { get; set; } = default!;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày đặt")]
        public DateTime OrderDate { get; set; } = default!;
        
        [Display(Name = "Khách hàng")]
        public string CustomerId { get; set; } = default!;
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; } = default!;
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; } = default!;
    }
}