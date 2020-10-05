using MVC_SampleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MVC_SampleApp.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<CustomerReview>().ToTable("CustomerReview");

            modelBuilder.Entity<CustomerReview>()
                .HasKey(c => new { c.ProductID, c.CustomerID });

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductNumber).IsUnique();
        }
    }
}