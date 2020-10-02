using System;

namespace Exercise02
{
    public class Circle : Shape
    {
        private double radius;

        public Circle() : base() {}

        public Circle(double diameter)
        {
            height = diameter;
            width = diameter;
            radius = height/2;
        } 

        public override double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                radius = value/2;
            }
        }

        public override double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                radius = value/2;
            }
        }

        public override double Area
        {
            get
            {
                return Math.PI * Math.Pow(radius, 2);
            }
        }
        
    }
}