using static System.Console;
using System;

namespace BooleanOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a = true;
            bool b = false;

            
            WriteLine($"AND    | a (true)  | b (false)      ");
            WriteLine($"a      | {a & a, -5}     |{a & b, -5}       ");
            WriteLine($"b      | {a & b, -5}     |{b & b, -5}       ");
            WriteLine();
            WriteLine($"OR    | a (true)  | b (false)      ");
            WriteLine($"a      | {a | a, -5}     |{a | b, -5}       ");
            WriteLine($"b      | {a | b, -5}     |{b | b, -5}       ");
            WriteLine();
            WriteLine($"XOR   | a (true)  | b (false)      ");
            WriteLine($"a      | {a ^ a, -5}     |{a ^ b, -5}       ");
            WriteLine($"b      | {a ^ b, -5}     |{b ^ b, -5}       ");
            WriteLine();


        }
    }
}
