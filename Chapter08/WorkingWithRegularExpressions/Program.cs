using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace WorkingWithRegularExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("please enter your age: ");
            string input = "3";//ReadLine();

            // Regular expression symbols:
            // ^            start of input
            // \d           single digit
            // \w           whitespace
            // .            any single character
            // $            end of input
            // \D           a single NON-digit
            // \W           Non-whitespace
            // \^           character
            // \.           dot character
            // [A-Za-z0-9]  range of characters
            // [aeiou]      set of characters
            // [^aeiou]     not in a set of characters
            //
            // Regular expression quantifiers: they affect the previous symbols in a regular expression
            // +        one or more
            // {3}      exactly 3
            // {3,}     at least 3
            // ?        one or none
            // {3,5}    3 to 5
            // {,3}     up to 3

            // @        sign means that escape characters are disabled i.e. disable \n = new line,   \t = tab...so a backslash is a backslash
            // @"\d"    expression means enter any characters you want as long as you enter at least one digit
            // @"^\d$"  reject anything except a single digit
            // @"^\d+$" reject anything one or more single digits
            var ageChecker = new Regex(@"^\d+$");

            if (ageChecker.IsMatch(input))
            {
                WriteLine("Thanks!");
            }
            else
            {
                WriteLine($"This is not a valid age: {input}");
            }

            // BOOK: page 264 Splitting a complex comma-separated string
            // "Monsters, Inc.","I, Tonya","Lock, Stock and Two Smoking Barrels"
            string films = "\"Monsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";
            string[] filmsDumb = films.Split(",");
            WriteLine($"Dumb attempt at splitting:");
            foreach (string film in filmsDumb)
            { 
              WriteLine(film);
            }

            var csv = new Regex(
            "(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");

            MatchCollection filmsSmart = csv.Matches(films);
            WriteLine();
            WriteLine("Smart attempt at splitting:");
            foreach (Match film in filmsSmart)
            {
                WriteLine(film.Groups[2].Value);
            }
            


        }
    }
}
