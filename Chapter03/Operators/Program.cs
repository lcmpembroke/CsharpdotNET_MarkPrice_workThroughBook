using static System.Console;
using System;

namespace Operators
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            int a = 3;
            int b = a++;                            // postfix operator (assign then do the increment of a by one)
            WriteLine($"a is {a} and b is {b}");    // output a is 4 (as a incremented after assignment to b) and b is 3 (was assigned before a was incremented)

            int c = 3;
            int d = ++c;                            // postfix operator (assign then do the increment of a by one)
            WriteLine($"c is {c} and d is {d}");    // output c is 4, d is 4 

            // note it is better to split assignment and increment/decrement into separate statements for clarity


        }
    }
}
