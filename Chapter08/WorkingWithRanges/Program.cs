using System;
using static System.Console;

namespace WorkingWithRanges
{
    class Program
    {

        static void Main(string[] args)
        {
            // CHAPTER 08: PAGE 275 - CONTRAST memory efficiency between substring (not good) and using a span (good)
            // set up string to be split and indices needed
            string name = "Samantha Jones";
            int lengthOfFirst = name.IndexOf(' ');
            int lengthOfLast = name.Length - name.IndexOf(' ') - 1;

            // Using Substring method - creates duplicate string in memory to work from so not as efficient as using span below
            string firstName = name.Substring(
                startIndex: 0,
                length: lengthOfFirst
            );

            string lastName = name.Substring(
                startIndex: name.Length - lengthOfLast,
                length: lengthOfLast
            );

            WriteLine("Using string's Substring method: ");
            WriteLine($"First name: {firstName}, Last name: {lastName}");


            // Using a span - uses memory more efficiently as doesn't create duplicate object in memory to work from 
            // note that ^  (C# 8.0) gives an Index type with fromEnd set to true i.e. ^5   is equiv to    new Index(value: 5, fromEnd: true)
            ReadOnlySpan<char> nameAsSpan = name.AsSpan();
            var firstNameSpan = nameAsSpan[0..lengthOfFirst];
            var lastNameSpan = nameAsSpan[^lengthOfLast..^0];

            WriteLine();
            WriteLine("Using span: ");
            WriteLine("First name: {0}, Last name: {1}",
                arg0: firstNameSpan.ToString(),
                arg1: lastNameSpan.ToString()
            );
        }
    }
}
