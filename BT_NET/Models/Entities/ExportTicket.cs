using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models.Entities
{
    public class ExportTicket
    {
        [Key]
        public string ExportTicketID {get;set;} =default!;
        public DateTime ExportDate {get;set;}=default!;
        public string? CustomerName {get;set;}
        public virtual ICollection<ExportDetail> ExportDetails {get;set;} =default!;
    }
}