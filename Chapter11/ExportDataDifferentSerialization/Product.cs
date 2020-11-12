using System.ComponentModel.DataAnnotations;        // for Required and StringLength data annotations - adds futher rules about a property
using System.ComponentModel.DataAnnotations.Schema; // for Column data annotation....Schema efffectively modifies structure of the table

namespace ExportDataDifferentSerialization.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Column("UnitPrice", TypeName = "money")]
        public decimal? Cost { get; set; }

        [Column("UnitsInStock")]
        public short? Stock { get; set; }

        public bool Discontinued {  get; set; }

        // Foreign Key relationship to the Categories table below:
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }


    }
}

