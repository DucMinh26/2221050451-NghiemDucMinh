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
        //--------------------------------------------------------------------
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ImportTicket> ImportTickets { get; set; }
        public DbSet<ImportDetail> ImportDetails { get; set; }
        public DbSet<ExportTicket> ExportTickets { get; set; }
        public DbSet<ExportDetail> ExportDetails { get; set; }

        public DbSet<Book> Books {get;set;}

        public DbSet<SinhVien> sinhViens{get;set;}
    }
}