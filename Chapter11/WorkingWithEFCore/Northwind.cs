using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;    // added Nuget package to use...see https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Proxies/


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
            // note below lazy loading (BOOK p384) - not good here a many trips to the databasse to eventually fetch all the data
            //optionsBuilder.UseLazyLoadingProxies().UseSqlite($"Filename={path}");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // here use EF Core Fluent API (instead of EF Core annotation attributes...like for Product.ProductName) to limit length of a categroy name to 15
            modelBuilder.Entity<Category>()
            .Property(category => category.CategoryName)
            .IsRequired()
            .HasMaxLength(15);
            
            modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued)   // global filter defined so any discontinued products are filtered out in the model (programmer doesn't have to add that condition in in where clause - assuming that's what is wanted!)
            .Property(product => product.Cost)
            .HasConversion<double>();

            
        }

    }
}