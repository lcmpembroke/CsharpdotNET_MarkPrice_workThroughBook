using System;
using static System.Console;
using System.IO;

namespace SelectionStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("---------------- Selection Statements ---------------------");

            // ALWAYS USE CURLY BRACES WITH IF STATEMENTS 9page 81 book - read of potential bug if you don't in SSL
            if (args.Length == 0)
            {
                WriteLine("No arguments passed to Program Main()");
            }
            else
            {
                WriteLine("At least one argument passed to Program Main()...actual number is: " + args.Length + " and value of first one is " + args[0]);
            }

            // BOOK: page 81 Pattern matching with the if statment (use keyword 'is')
            //object o = 3;
            object o = "3";

            int j = 4;

            if (o is int i) // using the keyword is and a temporary/local variable named i...o is assigned to i if true
            {
                WriteLine($"{i} x {j} = {i * j}");
            }
            else
            {
                WriteLine("o is not an int so it cannot multiply!");
            }

            // BOOK: page 82 switch statement
            // case stmt rules:  every case section needs to end with:
            // break, or goto case, or have no statments at all
            // NOTE: goto can be frowned upon...use is sparingly only if nest solution for logic
            A_label:
                var number = (new Random()).Next(1,7);
                WriteLine($"My random number is: {number}");

                switch (number)
                {
                    case 1:
                        WriteLine("number is one");
                        break;  // jump to end of switch stmt
                    case 2:                    
                        WriteLine("number is two...follow with goto case 1");  
                        goto case 1;                  
                    case 3:
                    case 4:
                        WriteLine("number is three or 4 as case 3 left empty...then goto case1");                                        
                        goto case 1;                        
                    case 5:                                  
                        // go to sleep for half a second
                        System.Threading.Thread.Sleep(500);
                        goto A_label;  
                    default:
                        WriteLine("default");
                        break;
                }
            

            // BOOK: page 84 Pattern matching with switch statement
            // there are multiple subtypes of Stream - see chapter 9 for more info...
            //  here since we used File.Open, the stream is a FileStream, and it's writeable doe to the FileMode we specified on creation of stream
            string path = @"/home/lcppembroke/00_00_dotNET/dotNET_MarkPriceBook/Chapter03";
            Stream s = File.Open(Path.Combine(path, "file.txt"),FileMode.OpenOrCreate);


             string message = string.Empty;

             switch (s)
             {
                 case FileStream writeableFile when s.CanWrite:
                    message = "the stream is a file i can write to";
                    break;
                 case FileStream readOnlyFile:
                    message = "the stream is a read-only file";
                    break;
                case MemoryStream memoryStream:
                    message = "the stream is a memory address";
                    break;
                default:    // default ALWAYS EVALUATED LAST EVEN IF NOT PLACED LAST
                    message = "stream is some other type";
                    break;
                case null:
                    message = "the stream is null";
                    break;
             }

             WriteLine(message);

            // BOOK: page 85 simplifying switch stmts with switch expressions
            // C# 8.0
            message = s switch
            {
                FileStream writeableFile when s.CanWrite
                    => "the stream is a file i can write to ***",
                FileStream readOnlyFile 
                    => "the stream is a read-only file ***",
                MemoryStream ms 
                    => "the stream is a memory address ***",
                null 
                    => "the stream is null ***",
                _ 
                    => "stream is some other type ***"
            };

        WriteLine(message);
        }
    }
}
