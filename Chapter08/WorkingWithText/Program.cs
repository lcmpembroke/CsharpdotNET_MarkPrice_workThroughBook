using System;
using static System.Console;

namespace WorkingWithText
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = "London";
            WriteLine($"{city} is {city.Length} character long");
            WriteLine($"First char is {city[0]} and third is {city[2]}.");


            string cities = "Paris,Berlin,Madrid,New York";
            string[] citiesArray = cities.Split(',');
            foreach (var item in citiesArray)
            {
                WriteLine(item);
            }


            string fullName = "Aled Jones";
            int indexOfTheSpace = fullName.IndexOf(' ');
            string firstName = fullName.Substring(startIndex: 0, length: indexOfTheSpace);
            string lastName = fullName.Substring(startIndex: indexOfTheSpace + 1);
            WriteLine($"{lastName}, {firstName}");

            // BOOK: page 258 Checking for string content: StartsWith, EndsWith, Contains methods
            string company = "Micrsoft";
            bool startsWithM = company.StartsWith("M");
            bool containsN = company.Contains("N");
            WriteLine($"{company} starts with M:{startsWithM}, and contains N: {containsN}");

            // BOOK: page 259 Many string memebrs to join/format strings, some static methods(can only be called from the type, not the variable instance)
            // static examples:     string.Concat, string.Join, string.IsNullOrEmpty, string.IsNullOrWhitespace, string.Empty, string.Format
            // not static examples: Trim, TrimStart,  TrimEnd, ToUpper, ToLower, Insert, Remove, Replace
            string recombined = string.Join(" => ", citiesArray);
            WriteLine(recombined);

            string fruit = "Apples";
            decimal price = 0.39M;
            DateTime when = DateTime.Today;

            WriteLine($"{fruit} cost {price} on {when:dddd}s");

            WriteLine(string.Format("{0} cost {1} on {2:dddd}s",fruit,price,when));

        }
    }
}
