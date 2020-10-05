using System;
using static System.Console;
using System.Text.RegularExpressions;
using MyCompany.SharedUtils;
using static System.Convert;

namespace Exercise03
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                do
                {    

                    WriteLine("Enter and number (press ESC to finish):");
                    string userInput = ReadLine();
                    
                    if (Regex.IsMatch(userInput, @"^-?[\d]+$")) // takes positive and negative integers
                    {
                        
                        long number = System.Int64.Parse(userInput);
                        WriteLine($"The number in words is: {NumberToWords.ToWords(number)}");
                    }
                    else
                    {
                        WriteLine("Invalid number entered.");
                    }
                    
                }
                while (ReadKey().Key != ConsoleKey.Escape);

            }
            catch (System.Exception ex)
            {
                    
                 WriteLine($"{ex.GetBaseException().ToString()} has message {ex.Message}");
            }   
        }
    }
}
