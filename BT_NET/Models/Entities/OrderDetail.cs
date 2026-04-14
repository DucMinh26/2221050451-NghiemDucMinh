using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_NET.Models.Entities
{
    public class OrderDetail
    {
        [Key]
        [Display(Name = "Mã chi tiết đơn hàng")]
        public string OrderDetailId { get; set; } = default!;
        
        [Required]
        [Display(Name = "Mã đơn hàng")]
        public string OrderId { get; set; } = default!;
        
        [Required]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; } = default!;
        
        public virtual Order? Order { get; set; } = default!;

        public string ProductId { get; set; } = default!;

        [ForeignKey("ProductId")]
        public Product? Product { get; set; } = default!;
    }
}