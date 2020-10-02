using System;
using static System.Console;
using PrimeFactors;

namespace PrimeFactors
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Program in the PrimeFactors namespace from Program.cs in the  PrimeFactorsApp folder");

            var Primes = new Primes();

            if (args.Length == 0)
            {
                WriteLine($"Prime factors for 186 are: {Primes.PrimeFactors(186)}");
            } else
            {
                bool isNumber = int.TryParse(args[0],out int number);

                try
                {
                    if (isNumber && number > 1)
                    {
                        WriteLine($"Prime factors for {number} are: {Primes.PrimeFactors(number)}");    
                    } else
                    {
                        WriteLine($"You entered invalid number to factorise - program stopping.");
                    }                      
                }
                catch (System.Exception ex)
                {
                    Write($"Exception is {ex.GetType()} with message {ex.Message}");
                }


            }


        }
    }
}
