using System;
using System.Numerics;
using static System.Console;

namespace WorkingWithNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            // BOOK: page 254 showing that BigInteger type holds far larger than the max value of a ulong (=UInt64) type
            var largest = ulong.MaxValue;
            var atomsInUniverse = BigInteger.Parse("012345678901234567890123456789");

            WriteLine($"{largest,40:N0}");
            WriteLine($"{atomsInUniverse,40:N0}");

            // BOOK: page 255 Working with complex numbers
            var c1 = new Complex(4, 2);
            var c2 = new Complex(3, 7);            
            var c3 = c1 + c2;

            WriteLine($"{c1} added to {c2} is {c3}");
        }
    }
}
