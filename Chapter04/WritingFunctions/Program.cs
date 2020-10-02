using System;
using static System.Console;

namespace WritingFunctions
{
    class Program
    {
        static int Factorial(int number)
        {
            // using recursion  - function calls itsel within its implementation 
            // recursion - clever but take care as can lead to stack overflow with too many function calls
            if (number < 1)
            {
                return 0;
            } 
            else if (number == 1)
            {
                return 1;
            }
            else
            {
                return number * Factorial(number - 1);
            }
        }

        static void RunFactorial()
        {
            bool isNumber;
            do
            {
                Write("Enter number: ");      
                string entered = ReadLine();
                isNumber = byte.TryParse(entered,out byte number);

                if (isNumber)
                {
                    WriteLine($"{number:N0}! = {Factorial(number):N0}");
                } else
                {
                    WriteLine($"You entered: {entered} which is invalid");
                }                
            } while (isNumber);            

        }

        /// <summary>
        /// Pass a 32-bit integer and it will be converted into its ordinal equivalent.
        /// </summary>
        /// <param name="number">Number is a cardinal value e.g. 1,2,3, and so on.</param>
        /// <returns>Number as an ordinal string e.g. 1st, 2nd, 3rd, and so on.</returns>
        static string CardinalToOrdinal(int number)
        {
            string numberText = number.ToString();
            char finalChar = numberText[numberText.Length -1];
            string numberTextSuffixed;
            switch (finalChar)
            {
                case '1':
                    numberTextSuffixed = numberText + "st";
                    break;
                case '2':
                    numberTextSuffixed = numberText + "nd";
                    break;
                case '3':
                    numberTextSuffixed = numberText + "rd";
                    break;                
                
                default:
                    numberTextSuffixed = numberText + "th";
                    break;
            }
            return numberTextSuffixed;
        }

        static void RunCardinalToOrdinal()
        {
            for (int i = 1; i < 41; i++)
            {
                Write($"{CardinalToOrdinal(i)}  ");
            }
        }

        static decimal CalculateBMI()
        {
            decimal bmi;
            // Body Mass Index is a simple calculation using a person's height and weight.
            // The formula is BMI = kg/m2 where kg is a person's weight in kilograms and m2 is their height in metres squared.
            Write("Enter your height in m: ");      
            string heightEntered = ReadLine();            

            Write("Enter your weight in kg: ");      
            string weightEntered = ReadLine();            

            try
            {
                decimal height = decimal.Parse(heightEntered);
                decimal weight = decimal.Parse(weightEntered); 

                bmi = weight/(height*height);
            }
            catch (System.Exception ex)
            {
                bmi = 0;
                WriteLine($"{ex.GetType()} states the error: {ex.Message}");
            }
            return bmi;
        }

        static void TimesTable(byte number)
        {
            Write($"This is the {number} times table");
            for (int i = 1; i < 13; i++)
            {
                WriteLine($"{i} x {number} =  {i * number}");
            }
        }

        static void RunTimesTimesTable()
        {
            bool isNumber;
            do
            {
                Write("Enter number between 1 and 255: ");      
                string entered = ReadLine();
                isNumber = byte.TryParse(entered,out byte number);


                if (isNumber)
                {
                    WriteLine($"the number you entered was: {number}");
                    TimesTable(number);
                } else
                {
                    WriteLine($"You entered was: {entered}");
                    WriteLine("You did not enter a suitable number");
                }                
            } while (isNumber);

        }

        static void Main(string[] args)
        {
            //RunTimesTimesTable();
            //Console.WriteLine($"Your BMI is: {Math.Round(CalculateBMI(),2)}");
            RunCardinalToOrdinal();
            RunFactorial();
        }
    }
}
