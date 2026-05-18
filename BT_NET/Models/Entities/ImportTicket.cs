using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BT_NET.Models.Entities
{
    public class ImportTicket
    {
        [Key]
        public string ImportTicketID{get;set;} =default!;
        public DateTime ImportTicketDate{get;set;}
        public string SupplierID {get;set;} = default!;
        [ForeignKey("SupplierID")]
        public virtual Supplier? Supplier {get;set;} = default!;
        public virtual ICollection<ImportDetail> ImportDetails{get;set;}=default!;
    }
}