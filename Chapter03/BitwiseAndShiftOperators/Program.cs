using static System.Console;
using System;

namespace BitwiseAndShiftOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            // BOOK: page77 Bitwise and Binary Shuft operators
            int a = 10; // 0000 1010
            int b = 6;  // 0000 0110
            // a & b       0000 0010 = 2
            // a | b       0000 1110 = 8+4+2 = 14
            // a ^ b       0000 1100 = 8+4   = 12    

            // why need bitwise and shift operators --- EFFICIENCY??? QUICKER
            // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators
            // http://www.blackwasp.co.uk/CSharpShiftOperators.aspx     
            
            WriteLine($"a = {a}");
            WriteLine($"b = {b}");
            WriteLine($"a & b = {a & b}"); // 2-bit column only
            WriteLine($"a | b = {a | b}"); // 8,4 and 2-bit columns            
            WriteLine($"a ^ b = {a ^ b}"); // 8 and 4 bit columns

            // move bits by 3 columns to left like doubling 3 times as base 2... 10 --double--> 20 --double--> 40 --double--> 80
            // 128 64 32 16 8 4 2 1
            // 0   1   0 1  0 0 0 0 = 64 + 16 = 80
            WriteLine($"a << 3 = {a << 3}");    // output a << 3 = 80
            WriteLine($"a * 8 = {a * 8}");      // output a * 3 = 80..note 8 is 2^3
            WriteLine($"b >> 1 = {b >> 1 }");   // output b >> 1 = 3 as divide by 2^1..ie half it

            // note CPUs can perform bit shift faster than multiplication/dividing...
            
            // BOOK: page 78 Miscellaneous Operators: 
            // nameof and sizeof
            // .    member access operator (the dot between a varible and its memebers)
            // =    assignment operator
            // ()   invocation operator
            // []   index access operator
            //

            
        }
    }
}
