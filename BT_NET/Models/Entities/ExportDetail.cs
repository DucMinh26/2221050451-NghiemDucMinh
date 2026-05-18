using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_NET.Models.Entities
{
    public class ExportDetail
    {
        [Key]
        public string ExportDetailID {get;set;} =default!;
        public string ExportTicketID {get;set;} =default!;
        public string DeviceID{get;set;} =default!;
        public int Quantity{get;set;}=default!;
        public decimal UnitPrice{get;set;} =default!;
        public decimal TotalPrice => Quantity*UnitPrice;
        [ForeignKey("ExportTicketID")]
        public virtual ExportTicket? ExportTicket {get;set;} =default!;
        [ForeignKey("DeviceID")]
        public virtual Device? Device {get;set;} =default!;
    }
}