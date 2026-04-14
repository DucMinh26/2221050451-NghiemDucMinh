using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public string ProductId { get; set; } = default!;

        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [Display(Name = "Tên sản phẩm")]
        public string ProductName { get; set; } = default!;

        [Required(ErrorMessage = "Giá sản phẩm không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn hoặc bằng 0")]
        [Display(Name = "Đơn giá")]
        public decimal Price { get; set; }
    }
}