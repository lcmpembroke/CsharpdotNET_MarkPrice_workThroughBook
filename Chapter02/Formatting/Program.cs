using static System.Console;
using System;


namespace Formatting
{
    class Program
    {

        static void Main(string[] args)
        {
            // format item syntax is:   { index [, alignment ] [ : formatString ]}
            // see https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings?view=netframework-4.8
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings?view=netframework-4.8
            //  e.g.
            // "C" or "c" 	Currency 	Result: A currency value.
            // "D" or "d" 	Decimal 	Result: Integer digits with optional negative sign.
            // "E" or "e" 	Exponential (scientific) 	Result: Exponential notation.
            // "F" or "f" 	Fixed-point 	Result: Integral and decimal digits with optional negative sign.
            // "G" or "g" 	General 	Result: The more compact of either fixed-point or scientific notation.
            // "N" or "n" 	Number 	Result: Integral and decimal digits, group separators, and a decimal separator with optional negative sign.
            // "P" or "p" 	Percent 	Result: Number multiplied by 100 and displayed with a percent symbol.
            // "R" or "r" 	Round-trip 	Result: A string that can round-trip to an identical number.
            // "X" or "x" 	Hexadecimal 	Result: A hexadecimal string.

            WriteLine("Formatting");

            // BOOK: page 59 Format using numbered positional arguments
            int numberOfApples = 12;
            decimal pricePerApple = 0.35M;

            WriteLine(
                format: "using numbered positional arguments...{0} apples cost {1:C}",
                arg0: numberOfApples,
                arg1: pricePerApple * numberOfApples
            );

            string formattedString = string.Format(
                format: "using formattedString ...{0} apples cost {1:C}",
                arg0: numberOfApples,
                arg1: pricePerApple * numberOfApples                    
            );

            WriteToFile(formattedString); 


            // BOOK: page 59 Format using interpolated string using $ sign...(C#6.0 and later)
            WriteLine($"using $ interpolated string...{numberOfApples} apples costs {pricePerApple * numberOfApples}");


            // BOOK: page 60 Understand Format Strings 
            // N0 format    means 1000 separator, no decimal
            // c format     means currency (determined by current thread as to whther you get £, $ etc)
            // format item syntax is:   { index [, alignment ] [ : formatString ]}

            string applesText = "Apples";
            string bananasText = "Bananas";
            string grapesText = "Grapes";
            int applesCount = 1234;
            int bananaCount = 4567;
            int grapesCount = 6666;

            WriteLine(
                format: "{0,-8} {1,6:N0}",
                arg0: "Name",
                arg1: "Count"
            );

            WriteLine(
                format: "{0,-8} {1,6:N0}",
                arg0: applesText,
                arg1: applesCount
            );

            WriteLine(
                format: "{0,-8} {1,6:N0}",
                arg0: bananasText,
                arg1: bananaCount
            );

            WriteLine(
                format: "{0,-8} {1,6:N0}",
                arg0: grapesText,
                arg1: grapesCount
            );

            // BOOK: page 61 get text input from user in console app
            Write("Please type your first name then press ENTER: ");
            string firstName = ReadLine();

            Write("Please type your age then press ENTER: ");
            string age = ReadLine();

            WriteLine($"Hello {firstName}, you are {age} years old.");


            // BOOK: page 63 get key input from user in console app
            Write("Press any key combination: ");
            ConsoleKeyInfo key = ReadKey();
            WriteLine();
            WriteLine("Key: {0}, Char: {1}, Modifiers: {2}",
            arg0: key.Key,
            arg1: key.KeyChar,
            arg2: key.Modifiers
            );            

        }

        static void WriteToFile(string stringToWrite)
        {
            // would need to actually write to a file in here
            WriteLine("inside WriteToFile dummy function" + stringToWrite);
        }
    }
}
