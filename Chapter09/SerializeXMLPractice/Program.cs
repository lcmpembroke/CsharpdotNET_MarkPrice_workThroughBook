using Pembroke.Shared;
using System;
using static System.Console;
using System.Collections.Generic;   // List<T>
using System.IO;                    // FileStream
using static System.IO.Path;        
using static System.Environment;
using System.Xml.Serialization;     // XmlSerializer


namespace SerializeXMLPractice
{
    class Program
    {       
        static void Main(string[] args)
        {
            WriteLine("\nSerializeXMLPractice running ...");

            // create list of shapes to serialize
            var listOfShapes = new List<Shape>()
            {
                new Rectangle {Colour = "Yellow", Height = 2.0, Width = 3.0},
                new Circle { Colour = "Red", Radius = 2.5},
                new Rectangle {Colour = "Yellow", Height = 20.0, Width = 11.0},
                new Circle { Radius = 12.3, Colour = "Purple"},
                new Circle {Colour = "Orange", Radius = 4},
                new Square {Colour = "Blue", Width = 10}
            };

            string xmlShapesFile = Combine(CurrentDirectory, "shapes.xml");

            // create object that formats the list of shapes as XML
            WriteLine($"... about to create new xmlSerializer ...");
            // ERROR THROWN NEXT LINE DUE TO NULL REFERENCE ...works if use  typeof(List<Circle>), but not typeof(List<Shape>),typeof(List<Rectangle>),typeof(List<Square>)
            var xmlSerializier = new XmlSerializer(typeof(List<Shape>));
            WriteLine($"... HIP HIP HOORAY finished creating the xmlSerializer ...");

            using (FileStream fileStream = File.Create(xmlShapesFile))
            {
                WriteLine("\n...about to serialize the listOfShapes to file...");
                // serialize the object graph to the stream
                xmlSerializier.Serialize(fileStream, listOfShapes);
            } 

            WriteLine($"... just serialized ..."); 
            // // deserialize to output list of shapes with their areas and colour
            using (FileStream fileStream = File.Open(xmlShapesFile,FileMode.Open))
            {
                WriteLine("\n...about to deserialize the listOfShapes ...");
                // either syntax for explicit type conversion
                //List<Shape> loadedXmlShapes = (List<Shape>)xmlSerializier.Deserialize(fileStream);
                List<Shape> loadedXmlShapes = xmlSerializier.Deserialize(fileStream) as List<Shape>;

                foreach (Shape item in loadedXmlShapes)
                {
                    WriteLine($"{item.GetType().Name} has area: {item.Area} and colour: {item.Colour}"); 
                }                
            } 
        }
    }
}
