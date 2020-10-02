using System;
using System.Xml;
using System.IO;

namespace Variables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Variables");

            // BOOK: page 48    Storing any type of object - better alternative will be generics (see Chapter 6)
            object height = 1.88;   // storing a double in an object
            object name = "Amir";   // stor-ing a string in an object
            Console.WriteLine($"{name} is {height} metres tall");

            //int length1 = name.Length;              // gives compile error
            int length2 = ((string)name).Length;    // tell compiler it's a string
            Console.WriteLine($"{name} has {length2} characters");


            // BOOK: page 49    Storing Dynamic types (flexibility at cost of performance)
            // no Intellisense to help with dynamic types as compiler can't check what type is during Build time
            dynamic anotherName = "Katherine";                  // storing a string in dynamic object
            int lengthOfDynamicString  = anotherName.Length;    // if later store a data type that doesn't have property Length --> runtime exception


            // BOOK: page 50    Specifying vs inferring (using var) the type of a local variable
            // using var for local variables only when type is obvious
            // xml1 is good use of var loaclly as avoid repeated type in more verbose 2nd statement
            var xml1 = new XmlDocument();
            XmlDocument xml2 = new XmlDocument();

            // below is bad use of var as cannot tell type - should use specific type declaration as per second statement for file2
            var file1 = File.CreateText(@"C:\something.txt");
            StreamWriter file2 = File.CreateText(@"C:\something.txt");


            // BOOK: page 51    Getting Default Values for types - use default() operator
            // most primitive types are value types (must have a value)
            // but...string is a reference type - contains memory address of value, not value itself (like pointer in C++?) - null is default value
            Console.WriteLine($"default(int) = {default(int)}");            // default int      is 0
            Console.WriteLine($"default(bool) = {default(bool)}");          // default bool     is false
            Console.WriteLine($"default(DateTime) = {default(DateTime)}");  // default DateTime is 01/01/0001 00:00:00
            Console.WriteLine($"default(string) = {default(string)}");      // default string   is                         no output


        }
    }
}
