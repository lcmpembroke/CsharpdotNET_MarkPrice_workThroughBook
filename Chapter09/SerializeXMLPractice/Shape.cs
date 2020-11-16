using System.Xml.Serialization;

namespace Pembroke.Shared
{

    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public abstract class Shape
    {
        public string Colour { get; set; }
        public abstract double Area { get; }    // read-only property
    }
}