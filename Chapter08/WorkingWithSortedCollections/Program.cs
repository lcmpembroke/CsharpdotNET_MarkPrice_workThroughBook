using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace WorkingWithSortedCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");

            try
            {
                SortedDictionary<int, string> myFirstDictionary = new SortedDictionary<int, string>();    

                string insertString;
                // populate 
                for (int i = 0; i < 12; i++)
                {
                    insertString = "I am a string with number " + i.ToString(); 
                    myFirstDictionary.Add(i,insertString);    
                }
                
                //myFirstDictionary.Add(3,"try adding a duplicate key...should throw an exception");     
                
                foreach (var item in myFirstDictionary)
                {
                    WriteLine($"Key is: {item.Key.ToString()} and the value is {item.Value}");
                }

                // get min key in sorted dictionary
			    WriteLine("The min key in sorted dictionary {0} should be 1 with value {1}", myFirstDictionary.Keys.First(), myFirstDictionary[myFirstDictionary.Keys.First()]);

			    // get max key in sorted dictionary
			    WriteLine("The max key in sorted dictionary {0} should be 11 with value {1}", myFirstDictionary.Keys.Last(), myFirstDictionary[myFirstDictionary.Keys.Last()]);

                myFirstDictionary.Remove(4);

                if (!myFirstDictionary.ContainsKey(4))
                {
                    Console.WriteLine("Key number \"4\" is not found.");
                }
            }
            catch (System.Exception ex)
            {
                WriteLine($"{ex.GetType().ToString()} says {ex.Message}");
            }
        }
    }
}
