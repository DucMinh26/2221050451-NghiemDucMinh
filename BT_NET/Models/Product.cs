using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
    }
}