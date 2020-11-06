using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    // class to manage connection to the database - it communicates with database and dynamically generate DQL stmts to query/manipulate data
    // needs to inherit from DbContext
    public class Northwind : DbContext
    {
        // properties mapping to tables in database
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, "Northwind.db");
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // here use Fluent API (instead of attributes...like for Product.ProductName) to limit length of a categroy name to 15
            modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired()
            .HasMaxLength(15);
        }

    }
}