using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_NET.Models.Entities
{
    public class ImportDetail
    {
        [Key]
        public string ImportDetailID {get;set;} =default!;
        public string ImportTicketID {get;set;} =default!;
        public string DeviceID{get;set;} =default!;
        public int Quantity{get;set;}=default!;
        public decimal UnitPrice{get;set;} =default!;
        public decimal TotalPrice => Quantity* UnitPrice;
        [ForeignKey("ImportTicketID")]
        public virtual ImportTicket? ImportTicket {get;set;} =default;
        [ForeignKey("DeviceID")]
        public virtual Device? Device {get;set;} =default;
    }
}