using System;

namespace PrimeFactors
{
    public class Primes
    {
        private static int[] PrimeNumbersAscending = new[]
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 
            47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 
            107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 
            167, 173, 179, 181, 191, 193, 197, 199
        };        
 

        public static string PrimeFactors(int number)
        {
            string returnFactors = string.Empty;
            try
            {
                int numberToBeFactored = number;
                bool carryNumberIsPrimeNumber = false;
                int carryNumber;
                int smallestPrimeFactor;

                do
                {
                    smallestPrimeFactor = getSmallestPrimeNumberFactor(numberToBeFactored);
                    if (smallestPrimeFactor > 0)
                    {
                        returnFactors += smallestPrimeFactor.ToString() + " ";
                        carryNumber = numberToBeFactored / smallestPrimeFactor;

                        if (Array.Exists(PrimeNumbersAscending, element => element == carryNumber))
                        {
                            carryNumberIsPrimeNumber = true;
                            returnFactors += carryNumber.ToString() + " ";
                        }  else
                        {
                            numberToBeFactored = carryNumber;
                        }
                    }
                    else
                    {
                        carryNumberIsPrimeNumber = true;
                    }

                } while (!carryNumberIsPrimeNumber);
            }
            catch (System.Exception ex)
            {
                Console.Write($"Exception is {ex.GetType()} with message {ex.Message}");
            }
            return returnFactors;                
        }

        private static int getSmallestPrimeNumberFactor(int number)
        {
            int smallestPrimeFactor = 0;
            foreach (int primeNumber in PrimeNumbersAscending)
            {
                if (number % primeNumber == 0)
                {
                    smallestPrimeFactor = primeNumber;  
                    break;   
                }
            }
            return smallestPrimeFactor;
        }
    }
}
