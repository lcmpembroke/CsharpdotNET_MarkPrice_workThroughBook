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
                    Console.WriteLine("Key number \"4\" is not found");
                }



			Console.WriteLine("\n\n\n");
			Console.WriteLine("~~~~~~~Sorted Dictionary with Custom Comparer: sorted by key DESC~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
			Console.WriteLine("\n\n\n");


			//############### Sorted Dictionary with Custom Comparer: sorted by key DESC #############
			SortedDictionary<int, int> MyDescendingDictionary = new SortedDictionary<int, int>( new MyComparer() );

			// add some sample data
			MyDescendingDictionary.Add(1,11);
			MyDescendingDictionary.Add(6,20);
			MyDescendingDictionary.Add(11,99);
			MyDescendingDictionary.Add(7,3);

            WriteLine();
            WriteLine("Initial dictionary:");

			// loop through the descending sorted dictionary to print to screen
			foreach (var i in MyDescendingDictionary)
			{
				WriteLine("Key is {0}, Value is {1}", i.Key, i.Value);
			}

			// get min key in sorted dictionary
			WriteLine("The min key in descending sorted dictionary is: {0} (should be 1)", MyDescendingDictionary.Keys.Last());

			// get max key in sorted dictionary
			WriteLine("The max key in descending sorted dictionary is: {0} (should be 11) ", MyDescendingDictionary.Keys.First());

            WriteLine();
            WriteLine("Updated dictionary:");

			// remove the key-value pair with min key from sorted dictionary and then check the min key again
			MyDescendingDictionary.Remove(MyDescendingDictionary.Keys.Last());
			WriteLine("The min key in descending sorted dictionary is: {0} (should be 6 now because removal of key 1)", MyDescendingDictionary.Keys.Last());

			// add a new key-value pair and check the max key again
			MyDescendingDictionary.Add(15, 6);
			WriteLine("The max key in descending sorted dictionary is: {0} (should be 15 now as added key 15)", MyDescendingDictionary.Keys.First());

            WriteLine();
            WriteLine("Updated dictionary:");

			// loop through the descending sorted dictionary to print to screen
			foreach (var i in MyDescendingDictionary)
			{
				WriteLine("Key is {0}, Value is {1}", i.Key, i.Value);
			}


            }
            catch (System.Exception ex)
            {
                WriteLine($"{ex.GetType().ToString()} says {ex.Message}");
            }
        }
    }
}
