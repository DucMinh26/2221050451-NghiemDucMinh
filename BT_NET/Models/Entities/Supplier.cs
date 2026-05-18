using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models.Entities
{
    public class Supplier
    {
        [Key]
        public string SupplierID {get;set;} = default!;
        public string SupplierName {get;set;} =default!;
        public string SupplierPhone {get;set;} =default!;
    }
}