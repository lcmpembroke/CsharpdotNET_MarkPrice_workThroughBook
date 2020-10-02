using System;
using System.Collections.Generic;
using static System.Console;
using System.Collections.Immutable;

namespace WorkingWithLists
{
    class Program
    {
        static void Main(string[] args)
        {
            // BOOK: page 270
            var cities = new List<string>();
            cities.Add("London");
            cities.Add("Paris");
            cities.Add("Milan");

            WriteLine("Initial list");
            foreach (string city in cities)
            {
                WriteLine($"  {city}");
            }

            WriteLine($"First city is: {cities[0]}");
            WriteLine($"Last city is: {cities[cities.Count - 1]}");

            cities.Insert(0, "Sydney");
            WriteLine($"After inserting Sydney at index 0:");
            foreach (string city in cities)
            {
                WriteLine($"  {city}");
            }

            cities.RemoveAt(1);
            cities.Remove("Milan");
            WriteLine($"After removing city at index 1, and removing Milan: ");
            foreach (string city in cities)
            {
                WriteLine($"  {city}");
            }

            // BOOK: page 273
            var immutableCities = cities.ToImmutableList();
            var newList = immutableCities.Add("Rio");
            immutableCities.Add("Vancouver");
            
            WriteLine("----------------------------------");
            Write("Immutable list of cities: ");
            foreach (string city in immutableCities)
            {
                Write($" {city}");
            }
            WriteLine();

            Write("New list of cities: ");
            foreach (string city in newList)
            {
                Write($" {city}");
            }
            WriteLine();

        }
    }
}
