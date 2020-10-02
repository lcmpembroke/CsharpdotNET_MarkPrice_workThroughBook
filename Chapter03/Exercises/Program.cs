using System;
using static System.Console;

namespace Exercises
{
    class Program
    {
        static void Main(string[] args)
        {
            // BOOK: page 105 Exercises 3.1
            // (1) - Divide integer by 0 ==> get System.DivideByZeroException
            try
            {
                int test = 3;
                float result1 = test/0;    
                WriteLine($"result1: {result1}");                             
            }
            catch (Exception e)
            {
                WriteLine($"Exception message from test1: {e.Message}");
            }

            // (2) - Divide double by 0 ==> no exception, get infinity. RECALL - doubles are NOT ACCURATE
            // A double is a floating point number and not an exact value, so what you are really dividing by from the compiler's viewpoint is
            // something approaching zero, but not exactly zero.            
            try
            {
                double test2 = 3.7;
                double result2 = test2/0;     
                WriteLine($"result2: {result2}");                            
            }
            catch (Exception e)
            {
                WriteLine($"Exception message: {e.Message}");
            }            


            try
            {
                decimal test2b = 3.7M;
                decimal result2b = test2b/0;     
                WriteLine($"result2b: {result2b}");                            
            }
            catch (Exception e)
            {
                WriteLine($"Exception message from test2b: {e.Message}");
            }   

            // (3) - Once an int gets to its max value and is then incremented, it overflows to its minimum value (largest -ve) and continues incrementing from there  
            // 2147483647   incrememnts to  -2147483648

            // (4)  x = y++ means set x to y then increment y by one                                Postfix increment operator
            //      x = ++y means set increment y by one, then assign its incremented value to x    Prefix increment operator

            // (4) - Inside a loop:
            //  break       means once reached inside the loop, execution of the loop is terminated even if mroe looping was to be done
            //  continue    means exit loop at this point but go on to the next loop if more loops to do
            //  return      means

            string[] myArray = new string[4]{"apple","banana","pear","grape"};

            WriteLine("before foreach");
            foreach (var item in myArray)
            {
                if (item == "banana")
                {
                    WriteLine("banana found...");    
                    //return;   // breaks out of the whole main() function
                    //continue; // reutns to the start of the next loop so banana is not written from *** statement (just banana found from here)
                    break;       // breaks out of foreach loop to go to %%% (i.e. skips all fruits in array from banana)
                }
                WriteLine($"{item}"); // ***
            }
            WriteLine("after foreach"); // %%%  


            // (7)  =   is assignment
            //      ==  is comparison operator

            // (8)  for(;true;) will compile - an infinite loop

            // (9) underscore _ represents default in a switch expression

            // (10) to be enumerated over, an object must implement IEnumerable (with methods GetEnumerator, Current, MoveNext)

            int max = 500;

            byte maxByte = Byte.MaxValue;
            WriteLine($"maxByte is: {maxByte}");    // maxnumber for byte is 255

            // either use try catch for Overflow exception
            // or use checked keyword

            // byte type is 8-BitConverter unsigned integer

            try
            {
                checked // throws exception on overflow...
                {
                    WriteLine("in checked");
                    for (byte i=0; i < max; i++)
                    {
                        if (i%10 == 0)
                        {
                            WriteLine(i);
                        }
                    }
                }
                WriteLine("outside checked");                   
            }
            catch (System.Exception)
            {
                WriteLine("Exception caught thank you!");
            }
         


            // BOOK: page 106 exercise 3.3
            // FizzBuzz - replace multiple of 3 by fizz, multiple of 5 by buzz, multiple of both by fizzbuzz
            for (int i = 0; i < 101; i++)
            {

                if (i%3 == 0 && i%5 == 0)
                {
                    Write("FizzBuzz ");
                } else if (i%3 == 0)
                {
                    Write("Fizz ");
                } else if (i%5 == 0)
                {
                    Write("Buzz ");
                } else
                {
                    Write($"{i} ");                    
                }

                if (i%10 == 0) Write(",");
                if (i % 10 == 0) WriteLine();                
                
            }

            // BOOK: page 107 exercise 3.4
            WriteLine("Enter number between 0 and 255: ");
            string firstNumber = ReadLine();
            WriteLine("Enter another number between 0 and 255: ");
            string secondNumber = ReadLine();

            try
            {
                int number1 = int.Parse(firstNumber);
                int number2 = int.Parse(secondNumber);  
                int result = number1/number2;
                WriteLine($"{number1} divided by {number2} is {result} ");              
            }
            catch (System.Exception e)
            {
                WriteLine($"{e.GetType()} says {e.Message} " );                                 
            }

            // BOOK: page 107 exercise 3.5
            int x = 2;
            int y = 2 + ++x;
            WriteLine($"x is {x} and y is {y}");    // x will be 3, y will be 5

            x = 3 << 2;     // bit shifting 2 columns left...muliply by 2 two times i.e. x will be 12
            y = 10 >> 1;    // bit shifting column to right once i.e. half  so y will be 5
            WriteLine($"x is {x} and y is {y}");     // output x is 3 and y is 5  

            x = 10 & 8;     // logical AND operator (bitwise operator)
            y = 10 | 7;     // logical OR operator(bitwise operator)
            WriteLine($"x is {x} and y is {y}");    // output x is 8 and y is 15
            //          10  =   00001010    
            //          8   =   00001000    
            //          7   =   00000111
            // x = 10 & 8   =   00001000    = 8
            // y = 10 | 7   =   00001111    = 15



        }

    }
}
