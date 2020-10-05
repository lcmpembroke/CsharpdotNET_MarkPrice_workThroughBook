using System;
using static System.Console;
using System.Text.RegularExpressions;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {

            WriteLine(" -------- Exercise to practice regular expressions -------------");
            do
            {
                WriteLine("Enter a regular expression: (press ENTER to use the default)");                           
                string regexString = ReadLine();
            
                
                // determine which pattern to match against (default tor user)
                if (string.IsNullOrWhiteSpace(regexString))
                {
                    // using @ means it's verbatim string literal so 'escaping' isn't applied
                    //regexString = @"^\d.+$";     
                    regexString = @"^[a-z]+$";     
                } 

                WriteLine("Enter INPUT to see if it matches the regular expression: ");
                string userInput = ReadLine();

                Regex regex = new Regex(regexString);

                WriteLine($"Does {userInput} match {regexString}? {regex.IsMatch(userInput)}");


                // option to end program
                WriteLine("\nPress ESC to exit, or any key to try again");                

            } while (ReadKey().Key !=  ConsoleKey.Escape);
        }
    }


}
