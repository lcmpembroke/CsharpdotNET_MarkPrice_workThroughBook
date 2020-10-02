using System;
using System.Collections.Generic;
using static System.Console;

namespace Packt.Shared
{
    public partial class Person : Object
    {
        public string Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld FavouriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;

        // angle brackets is C# feature called generics - fancy way to make collection strongly typed
        // NOTE: strongly typed DIFFERENT FROM statically typed
        // System.Collection.Generic types are statically typed to contain strongly typed <T> instances
        public List<Person> Children = new List<Person>();

        public const string Species = "Homo Sapien";
        public readonly string HomePlanet = "Earth";
        public readonly DateTime Instantiated;

        // constructors
        public Person()
        {
            // set default values
            Name = "Unknown";
            Instantiated = DateTime.Now;
        }

        public Person(string initialName, string homePlanet)
        {
            // set default values
            Name = initialName;
            HomePlanet = homePlanet;
            Instantiated = DateTime.Now;
        }    

        // methods
        public void WriteToConsole()
        {
            WriteLine($"WriteToConsole() function: {Name} was born on {DateOfBirth:dddd}");
        }    

        public string getOrigin()
        {
            return $"getOrigin() called: {Name} was born on {HomePlanet}.";
        }

        public (string, int) GetFruit()
        {
            return ("Apples", 5);
        }

        public (string Name, int Number) GetVeg()
        {
            return (Name: "Carrots", Number: 44);
        }       

        public string OptionalParameters(
            string command = "Run",
            double number = 0.0,
            bool active = true)
        { 
            return string.Format(
                format: "command is {0}, number is {1}, active is {2}",
                arg0:command,
                arg1:number,
                arg2: active
            );
        } 

        public void PassingParameters(int x, ref int y, out int z)
        {
            // out parameters cannot have a default AND must be initialized within the method
            z = 99;

            x++;
            y++;
            z++;
        }
    }
}


