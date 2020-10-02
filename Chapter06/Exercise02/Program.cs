using System;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape rectangleShape = new Rectangle(3, 4.5);
            Console.WriteLine($"rectangleShape has height {rectangleShape.Height}, width {rectangleShape.Width}, area {rectangleShape.Area}");

            Shape squareShape = new Square(5);
            Console.WriteLine($"squareShape has height {squareShape.Height}, width {squareShape.Width}, area {squareShape.Area}");

            Shape circleShape = new Circle(5);
            Console.WriteLine($"circleShape has height {circleShape.Height}, width {circleShape.Width}, area {circleShape.Area:N}");            

        }
    }
}
