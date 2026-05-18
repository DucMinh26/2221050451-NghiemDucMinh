using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_NET.Models.Entities
{
    public class Device
    {
        [Key]
        public string DeviceID {get;set;} =default!;
        public string DeviceName {get;set;} =default!;
        public string CategoryID {get;set;} =default!;
        public string? DevicePrice{get;set;}
        public int StockQuantity{get;set;} =default!;
        [ForeignKey("CategoryID")]
        public virtual Category? Category {get;set;} =default!;
        
    }
}