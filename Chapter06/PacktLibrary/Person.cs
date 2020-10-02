using System;
using System.Collections.Generic;
using static System.Console;

namespace Packt.Shared
{
    public partial class Person : IComparable<Person>
    {
        public string Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld FavouriteAncientWonder;
        public WondersOfTheAncientWorld BucketList;
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

        public Person(string initialName)
        {
            // set default values
            Name = initialName;
            HomePlanet = "Earth";
            Instantiated = DateTime.Now;
        } 

        public Person(string initialName, DateTime dob)
        {
            // set default values
            Name = initialName;
            DateOfBirth = dob;
            Instantiated = DateTime.Now;
        }

        public Person(string initialName, string homePlanet)
        {
            // set default values
            Name = initialName;
            HomePlanet = homePlanet;
            Instantiated = DateTime.Now;
        }    

        // Chapter 06 methods -------------------------------------------------------------------------------------------------------------------

        // STATIC METHOD to "multiply" - called on the Person class (no instance needed)
        public static Person Procreate(Person p1, Person p2)
        {
            // classes are always reference types so the baby object is just stored once and p1 and p2 point to the same object not a clone of it
            var baby = new Person
            {
                Name = $"Baby of {p1.Name} and {p2.Name}"
            };

            p1.Children.Add(baby);
            p2.Children.Add(baby);

            return baby;
        }

        // INSTANCE METHOD to "multiply" - when called, uses dot operator on the instance
        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }


        // OPERATOR OVERLOADING to "multiply" - won't appear in Intellisense, not supported for every language compiler
        public static Person operator *(Person p1, Person p2)
        {
            return Person.Procreate(p1,p2);
        }

        // method with local function (nested or inner function)
        public static int Factorial(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException($"{nameof(number)} cannot be less than zero.");
            }
            return localFactorial(number);

            int localFactorial(int localNumber)
            {
                if (localNumber < 1) return 1;
                return localNumber * localFactorial(localNumber -1);
            }
        
        }

        // BOOK: page 188 Defining and handling DELEGATES (kind of object-oriented, type-safe function pointer)
        public event EventHandler Shout; // predefined Microsoft delegate    public delegate void EventHandler(object sender, EventArgs e);
        public int AngerLevel;

        public void Poke()
        {
            AngerLevel++;
            if (AngerLevel >= 3)
            {
                if (Shout != null)
                {
                    Shout(this, EventArgs.Empty);
                }
                // inline way to do above from C#6.0
                // Shout?.Invoke(this, EventArgs.Empty);
            }

        }

        public int CompareTo(Person other)
        {
            return Name.CompareTo(other.Name);
            //throw new NotImplementedException();
        }        

        // BOOK: page 210 overridden methods - note it's a good prctice to make methods virtual so they can be overriden (see p 211)
        public override string ToString()
        {
            return $"Person.ToString(): {Name} is a {base.ToString()}";
        }

        // BOOK: page 216 Inheriting exceptions
        public void TimeTravel(DateTime when)
        {
            if (when <= DateOfBirth)
            {
                throw new PersonException("Cannot travel back in time before your own birth!");
            }
            else
            {
                WriteLine($"Welcome to year: {when:yyyy}");
            }
        }

        // Chapter 05 methods ------------------------------------------------------------------------------------------------------
        public virtual void WriteToConsole()
        {
            WriteLine($"Person.WriteToConsole(): {Name} was born on a {DateOfBirth:dddd}");
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


