using System;
using static System.Console;
using static System.Convert;

namespace CastingConverting
{
    class Program
    {
        static void Main(string[] args)
        {

            // Casting explicitly or using System.Convert type

            // BOOK: page 90 - implicit casting - safe, no info loast and explicit casting - manual as data can be lost  so have to accept the risk
            int a = 1;
            double b = a;   // safe goin from int to double - this happens implicitly as safe
            WriteLine(b);

            double c = 9.8;
            //int d = c;  // compiler error - needs explicit casting as data will be lost
            int d = (int)c;
            WriteLine(d);

            // BOOK: page 91
            long e = 10;        // long 64-bit variable
            int f = (int)e;     // int 32-bit variable being cast from long, fine with small number
            WriteLine($"e is {e:N0} and f is {f:N0}");

            e = long.MaxValue;  // too large number to be cast to int
            f = (int)e;         // int 32-bit variable being cast from long, fine with small number
            WriteLine($"e is {e:N0} and f is {f:N0}");

            e = 5_000_000_000;  // 5 billion
            f = (int)e;         // 
            WriteLine($"e is {e:N0} and f is {f:N0}");           

            e = int.MaxValue;  // 2,147,483,647
            f = (int)e;        // 2,147,483,647
            WriteLine($"e is {e:N0} and f is {f:N0}");      

            e = 2_147_483_648;  // one above int max value!
            f = (int)e;        // -2,147,483,648 ... goes to the negative one bigger than the positive max!
            WriteLine($"e is {e:N0} and f is {f:N0}");            

            // BOOK: page 92 -  converting using System.Convert type (see using stmt at top)
            double g = 9.8;
            int h = ToInt32(g); // rounds up to 10 instead of trimming decimal point to give 9
            // it's Banker's rounding (slightly diff from rounding taught in primary school - see page 93)
            WriteLine($"g is {g} and h is {h}");

            // Understanding the default rounding rules - Banker's rounding
            double[] doubles = new[] 
                { 9.49, 9.5, 9.51, 10.49, 10.5, 10.51 };

            foreach (double n in doubles)
            {
                WriteLine($"ToInt({n}) is {ToInt32(n)}");
            }            

            // Taking control of rounding
            foreach (double n in doubles)
            {
                WriteLine(format: 
                "Math.Round(value: {0}, digits: 0, mode: MidpointRounding.AwayFromZero) is {1}",
                arg0: n,
                arg1: Math.Round(value: n, digits: 0, mode: MidpointRounding.AwayFromZero));
            }            

            // Converting any type to a string - all types have a ToString() method they sinherit from System.Object
            int number = 12;
            bool boolean = true;
            DateTime now = DateTime.Now;
            object me = new object();

            WriteLine("ToString() ---------- ");
            WriteLine(number.ToString());
            WriteLine(boolean.ToString());
            WriteLine(now.ToString());
            WriteLine(me.ToString());   // no sensible representation as text so returns the namespace and type i.e. System.Object

            // When have binary object (like image/video) to store or transmit, don't send the raw bits - safest to convert the binary
            // object into string of safe characters (called Base64 encoding)
            

            // BOOK; page 95 Converting from a binary object to a string
            WriteLine("Binary object converting to string --------- ");
            byte[] binaryArray = new byte[128];    // allocate array of 128 bytes

            // NextBytes() function - fills the elements of a specified array of bytes with random numbers.
            (new Random()).NextBytes(binaryArray);
            
            WriteLine("Binary object AS BYTES: ");            
            
            for (int i = 0; i < binaryArray.Length; i++)
            {
                Write(binaryArray[i]);
            }
            WriteLine();

            // format the value using hexadecimal notation
            for (int i = 0; i < binaryArray.Length; i++)
            {
                Write($"{binaryArray[i]:X} ");
            }
            WriteLine();

            // convert to Base64 string and output as text
            string encoded = Convert.ToBase64String(binaryArray);
            WriteLine($"**binaryArray as base64 string: {encoded} ");   

            WriteLine();
            // testing converting it back to a binary object (byteArray...)
            byte[] reconstructedBinaryArray; // = new byte[128];    // allocate array of 128 bytes     
            reconstructedBinaryArray = Convert.FromBase64String(encoded);
            for (int i = 0; i < reconstructedBinaryArray.Length; i++)
            {
                Write($"{reconstructedBinaryArray[i]:X} ");
            }
            WriteLine();


            // BOOK: page 96 - PARSING from strings to numbers or dates/times
            // opposite of ToString is Parse
            // note Parse can give errors if string not formatted correctly - can use TryParse...
            int age = int.Parse("27");
            DateTime birthday = DateTime.Parse("4 July 1980");

            WriteLine($" i was born {age} years ago");
            WriteLine($" my birthday is {birthday} ");      // gives 04/07/1980 00:00:00 default ouput is short date and time
            WriteLine($" my birthday is {birthday:D} ");    // gives Friday, 4 July 1980 can output just long form date part using D format code

            // EXCEPTION IF INPUT TO PARSE NOT CORRECT FORMAT
            //int count = int.Parse("abc");   // Unhandled exception. System.FormatException: Input string was not in a correct format.
            //WriteLine($"count is {count}");

            // BOOK: page 98 TryParse
            Write("how many eggs?");
            int eggCount;
            string input = Console.ReadLine();

            if (int.TryParse(input,out eggCount))
            {
                WriteLine($"{eggCount} eggs.");
            } else
            {
                WriteLine("couldnt parse input number");
            }
            
        }
    }
}
