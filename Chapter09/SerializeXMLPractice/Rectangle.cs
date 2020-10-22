namespace Pembroke.Shared
{  
    public class Rectangle : Shape
    {   
        public override double Area
        {
            get
            {
                return Height * Width;
            }
        }

        public virtual double Height { get; set; }
        public virtual double Width { get; set; }
    }
}