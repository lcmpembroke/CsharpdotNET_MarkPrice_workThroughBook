using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Packt.Shared
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        // here we effectively RENAME the Product table's UnitPrice column, so the C# code class refers to it as Cost. 
        // The database table's actual column name is identified by decorating the property with the [Column] attribute, and specifying its column name in that
        // This uses te EF Core annotation attributes
        //      (an alternative way to do same thing is to use EF Core Fluent API... in OnModelCreatiing() method of the database context class see Northwind.cs in this case
        [Column("UnitPrice", TypeName = "money")]
        public decimal? Cost { get; set; }

        [Column("UnitsInStock")]
        public short? Stock { get; set; }

        public bool Discontinued {  get; set; }

        // Foreign Key relationship to the Categories table below:
        // NOTE Category is defined as virtual to allow EFCore to inherit and override the properties so it can do features like lazy loading
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }


        
    }
}