namespace Exercise02
{
    public class Square : Rectangle
    {
        public Square() : base() { }

        public Square(double sideLength) : base(height: sideLength, width: sideLength)
        {
            Height = sideLength;
            Width = sideLength;
        }

        public override double Height
        {
            set
            {
                height = value;
                width = value;
            }
        }

        public override double Width
        {
            set
            {
                width = value;
                height = value;                
            }
        }        
    }
}