using System;
using Microsoft.EntityFrameworkCore;

namespace Pembroke.Shared
{
    public class Northwind : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=../Northwind.db");
        }
    }
}
     