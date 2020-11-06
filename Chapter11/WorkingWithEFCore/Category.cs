using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // available due to Package Reference added "Microsoft.EntityFrameworkCore.Sqlite" in csproj

namespace Packt.Shared
{
    public class Category
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        // defines a navigation property for related rows
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            // enables developer to add products to a Category. Have to initialise navigation property to an empty list
            this.Products = new List<Product>();
        }
    }
}