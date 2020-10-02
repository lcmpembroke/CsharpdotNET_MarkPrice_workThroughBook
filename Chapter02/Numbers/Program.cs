using System;

namespace Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Looking at numbers in C#...");

            // BOOK: page 41
            uint naturalNumber = 23;
            int integerNumber = -23;
            float realNumber = 2.3F;
            double anotherRealNumber = 2.3;

            // BOOK: page 42
            // 3 ways to strore the number 2 million...
            int decimalNotation = 2_000_000;
            int binaryNotation = 0b_0001_1110_1000_0100_1000_0000;
            int hexadecimalNotation = 0x_001E_8480;


            Console.WriteLine(decimalNotation);
            Console.WriteLine(binaryNotation);
            Console.WriteLine(hexadecimalNotation);
            Console.WriteLine($"{ decimalNotation == binaryNotation}");
            Console.WriteLine($"{ decimalNotation == hexadecimalNotation}");


            // BOOK: page 44
            // int      4bytes
            // double   8bytes - can store biggest numbers despite only half space in memory of a decimal...not guaranteed to be accurate
            // decimal  16bytes
            Console.WriteLine($"int uses {sizeof(int)} bytes and can store numbers in the range {int.MinValue:N0} to {int.MaxValue:N0}");
            Console.WriteLine($"double uses {sizeof(double)} bytes and can store numbers in the range {double.MinValue:N0} to {double.MaxValue:N0}");
            Console.WriteLine($"decimal uses {sizeof(decimal)} bytes and can store numbers in the range {decimal.MinValue:N0} to {decimal.MaxValue:N0}");

            // BOOK: page 45
            // doubles not guaranteed to be accurate
            Console.WriteLine("----------- Using doubles----------- ");
            double a = 0.1;
            double b = 0.2;
            
            if (a + b == 0.3)
            {
                Console.WriteLine($"{a} + {b} equals 0.3");    
            }
            else
            {
                Console.WriteLine($"{a} + {b} does NOT equal 0.3");    
            }
            



            // BOOK: page 46
            // decimals accurate as number stored as large integer and decimal point is shifted
            Console.WriteLine("----------- Using decimals ----------- ");
            decimal c = 0.1M;   // M suffix means decimal literal value
            decimal d = 0.2M;
            
            if (c + d == 0.3M)
            {
                Console.WriteLine($"{c} + {d} equals 0.3");    
            }
            else
            {
                Console.WriteLine($"{c} + {d} does NOT equal 0.3");    
            }

            // SUMMARY
            // use int for whole numbers
            // use double for real numbers that won't be compared to other values - has useful special values eg double.NaN, double.Epsilon, double.Infinity
            // use decimal for money, CAD drawings, engineering...whereever accuracy important
        }
    }
}
