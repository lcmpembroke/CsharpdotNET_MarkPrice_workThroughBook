using System;
using static System.Console;

namespace Arguments
{
    class Program
    {
        static void Main(string[] args)
        {
            // arguments are entered in the terminal when running program
            // eg  dotnet run firstarg secondarg third:arg "fourth arg" fifth-arg
            WriteLine($"There are {args.Length} arguments");

            foreach (string arg in args)
            {
                WriteLine(arg);
            }

            if (args.Length < 4)
            {
                WriteLine("You need to specify two colours and dimensions eg red yellow 80 40");
                WriteLine("dotnet run red yellow 80 40");
                return;
            }

            ForegroundColor = (ConsoleColor)Enum.Parse(
                enumType: typeof(ConsoleColor),
                value: args[0],
                ignoreCase: true
            );

            BackgroundColor = (ConsoleColor)Enum.Parse(
                enumType: typeof(ConsoleColor),
                value: args[0],
                ignoreCase: true
            );  

            WindowWidth = int.Parse(args[2])          ;
            WindowHeight = int.Parse(args[3])          ;            
        }
    }
}
