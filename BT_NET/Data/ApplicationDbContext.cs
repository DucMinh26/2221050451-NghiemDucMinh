using BT_NET.Models;
using BT_NET.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace BT_NET.Data 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Student> Students { get; set; }
        //-------------------------------------------------------------------
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetalis { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}