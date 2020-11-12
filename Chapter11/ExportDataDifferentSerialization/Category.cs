using System.Collections.Generic;                   // for ICollection<Type>
using System.ComponentModel.DataAnnotations.Schema; // for Column data annotation
using System.Xml.Serialization;                     // to use XmlAttribute to allow compact XML to be generated

namespace ExportDataDifferentSerialization.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}