using System;
using static System.Console;
using Packt.Shared;             // to use the Crytography library

namespace RandomizingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("How many bytes long do you want the key to be? ");
            string size = ReadLine();

            byte[] key = Protector.GetRandomKeyOrIV(int.Parse(size));
            
            WriteLine($"The key as a byte array is:");

            for (int i = 0; i < key.Length; i++)
            {
                // the argument "X2" is a "format string" that tells the ToString() method how it should format the string.
                // In this case, "x2" indicates the string should be formatted in uppercase Hexadecimal. (x2 for lowercase hexadecimal)
                // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings?redirectedfrom=MSDN
                Write($"{key[i]:X2} ");
                if (((i+1) % 16) == 0) WriteLine(); // 16 to a line
            }
            WriteLine();

            WriteLine($"The key as a string is {key.ToString()}");

        }
    }
}
