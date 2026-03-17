using BT_NET.Models;
using Microsoft.EntityFrameworkCore;
namespace BT_NET.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        // public DbSet<Product> Products { get; set; }
        // Ví dụ: public DbSet<Student> Students { get; set; }
    }
}