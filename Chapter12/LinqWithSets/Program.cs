using System;
using static System.Console;
using System.Collections.Generic;   // for IEnumerable<T>
using System.Linq;                  // for LINQ extension methods


namespace LinqWithSets
{
    class Program
    {
        static void Output(IEnumerable<string> cohort, string description = "")
        {
            if (!string.IsNullOrEmpty(description))
            {
                WriteLine(description);
            }
            WriteLine(string.Join(", ", cohort.ToArray()));
            WriteLine("");

        }

        static void UnderstandZip()
        {
            WriteLine("UnderstandZip() running...");            
            int[] numbers = { 1, 2, 3, 4 };
            string[] words = { "one", "two", "three" };
            // ---------------------
            var numbersAndWords = numbers.Zip(words, (first, second) => first + " " + second);
            foreach (var item in numbersAndWords)
                WriteLine(item);            
            WriteLine();
                                    // output from foreach loop...
                                    // 1 one
                                    // 2 two
                                    // 3 three

            // ---------------------
            var wordsAndNumbers = words.Zip(numbers, (first, second) => first + " " + second);
            foreach (var item in wordsAndNumbers)
                WriteLine(item);    
            WriteLine();                
                                    // output from foreach loop...
                                    // one 1
                                    // two 2
                                    // three 3       
            // ---------------------                                                           
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nLinqWithSets running...\n\n");

            var cohort1 = new string[]
                { "Rachel", "Gareth", "Jonathan", "George" };

            var cohort2 = new string[]
                { "Jack", "Stephen", "Daniel", "Jack", "Jared" };

            var cohort3 = new string[]
                { "Desmond", "Jack", "Jack", "Jasmine", "Conor" };

            Output(cohort1, "Cohort 1");
            Output(cohort2, "Cohort 2");
            Output(cohort3, "Cohort 3");
            WriteLine();

            Output(cohort2.Distinct(), "cohort2.Dinstinct()");
            Output(cohort2.Union(cohort3), "cohort2.Union(cohort3)");
            Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3)");
            Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3)");
            Output(cohort2.Except(cohort3), "cohort2.Except(cohort3)");
            Output(cohort1.Zip(cohort2,(c1, c2) => $"{c1} matched with {c2}"),  "cohort1.Zip(cohort2)"); // Rachel matched with Jack, Gareth matched with Stephen, Jonathan matched with Daniel, George matched with Jack

            //NOTE: with zip, if there are unequal numbers of items, then some items won't have a matching partner and so won't be included in the result
            UnderstandZip();    
        }
    }


}
