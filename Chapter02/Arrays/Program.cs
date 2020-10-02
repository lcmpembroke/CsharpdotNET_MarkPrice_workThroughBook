using System;

namespace Arrays
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ARRAYS");

            // BOOK: page 52 Store multiple values (array)
            string[] names;         // references any array of strings
            names = new string[4];  // allocates memory for 4 strings in an array - arrays always fixed size..use collections if need add/remove (chapter 8)
            names[0] = "Kate0";
            names[1] = "Kate1";
            names[2] = "Kate2";
            names[3] = "Kate3";

            foreach (string s in names)
            {
                Console.WriteLine(s);
            }

        }
    }
}
