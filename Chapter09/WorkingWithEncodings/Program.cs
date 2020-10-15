using System;
using System.Text;
using static System.Console;

namespace WorkingWithEncodings
{
    class Program
    {
        static void Main(string[] args)
        {
            // BOOK: PAGE 316
            // NOTE When choosing encoding (often there won't be a choice as depends what the system to use the file needs)
            // then choose the one that'd use the least number of bytes but can store every character you need
            WriteLine("\n\nWorkingWithEncodings program running...");

            WriteLine("\nEncodings");
            WriteLine("[1] ASCII");
            WriteLine("[2] UTF-7");
            WriteLine("[3] UTF-8");
            WriteLine("[4] UTF-16 (Unicode)");
            WriteLine("[5] UTF-32");            
            WriteLine("[any other key] Default");            

            // choose an encoding
            Write("Press a number to choose an encoding: ");
            ConsoleKey number = ReadKey(intercept: false).Key;
            WriteLine("\n\n");

            Encoding encoder = number switch
            {
                ConsoleKey.D1 => Encoding.ASCII,
                ConsoleKey.D2 => Encoding.UTF7,
                ConsoleKey.D3 => Encoding.UTF8,
                ConsoleKey.D4 => Encoding.Unicode,
                ConsoleKey.D5 => Encoding.UTF32,
                _             => Encoding.Default
            };

            // define a string to encode
            string message = "A pint of milk costs £1.99";

            // encode the string into a byte array
            byte[] encodedMessage = encoder.GetBytes(message);

            // check how many bytes the encoding needed
            WriteLine($"{encoder.GetType().Name},{encodedMessage.Length}");

            // enumerate each byte
            WriteLine("BYTE HEX  CHAR");
            foreach (byte b in encodedMessage)
            {
                WriteLine($"{b,4} {b.ToString("X"),4} {(char)b,5}");
            }

            // DEOCDE THE BYTE ARRAY back into a string to display
            WriteLine(encoder.GetString(encodedMessage));
        }
    }
    
}
