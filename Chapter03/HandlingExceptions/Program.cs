using System;
using static System.Console;

namespace HandlingExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            // BOOK: page 98 handling exceptions
            // NOTE ORDER in which catch excpetions is important - see chapter 5
            WriteLine("Before parsing!!!!");
            Write("what is your age? ");
    
            string input = Console.ReadLine();
            try
            {
                WriteLine($"you are {int.Parse(input)} years old.");
            }
            catch (Exception e)
            {
                WriteLine($"the exception is: {e.Message}");
                //throw;
            }

            WriteLine("After parsing....");

            // BOOK: page 102 checking for overflow exceptions

            // checked keyword is used to explicitly enable overflow checking
            // in a checked context, arithmetic overflow raises a System.OverflowException.

            try
            {
                checked
                {
                    int x = int.MaxValue - 1;
                    WriteLine($"x initial value is: {x}");          //  2147483646
                    x++;
                    WriteLine($"x after increment by 1 is: {x}");   //  2147483647
                    x++;
                    WriteLine($"x after increment by 1 is: {x}");   // -2147483648
                    x++;
                    WriteLine($"x after increment by 1 is: {x}");   // -2147483647
                    
                }                
            }
            catch (OverflowException oe)
            {
                WriteLine($"Code overflowed but the exception was caught...{oe.Message}");
            }

  
        }
    }
}
