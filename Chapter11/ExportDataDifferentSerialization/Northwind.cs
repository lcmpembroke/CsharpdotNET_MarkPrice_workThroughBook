using Microsoft.EntityFrameworkCore;

namespace ExportDataDifferentSerialization.Models
{
    public class Northwind : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            string databasePath = System.IO.Path.Combine("..","Northwind.db");

            options.UseSqlite($"Data Source={databasePath}");        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired()
            .HasMaxLength(15);
            
            modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued)   // global filter: here, filters out all discontinued products in the model itself before data accessed by app
            .Property(product => product.Cost)
            .HasConversion<double>();            
        }
            
    }
}