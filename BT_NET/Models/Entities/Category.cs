using System.ComponentModel.DataAnnotations;

namespace BT_NET.Models.Entities
{
    public class Category
    {
        [Key]
        public string CategoryID {get;set;}=default!;
        public string CategoryName {get;set; }=default!;
    }
}