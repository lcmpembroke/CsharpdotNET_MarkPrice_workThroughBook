using System;
using Packt.Shared;             // homemade classes
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // bob
            var bob = new Person();
            bob.Name = "Bob Smith";
            bob.DateOfBirth = new DateTime(1970,11,30);
            bob.FavouriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
            bob.BucketList = WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;
            bob.Children.Add(new Person {Name = "Alf"});
            bob.Children.Add(new Person {Name = "Zoe"});    
            WriteLine();
            WriteLine("Bob-----------------------------------------");
            WriteLine(bob.getOrigin());
            bob.WriteToConsole();        
            WriteLine(
                format: "{0} was born on {1:dddd, d MMMM yyyy}",
                arg0: bob.Name,
                arg1: bob.DateOfBirth
            );
            WriteLine(
                format: "{0}'s favourite wonder is {1}, it's integer is {2}",
                arg0: bob.Name,
                arg1: bob.FavouriteAncientWonder,
                arg2: (int)bob.FavouriteAncientWonder
            );
            WriteLine($"{bob.Name}'s bucket list is: {bob.BucketList}");
            WriteLine($"{bob.Name} has {bob.Children.Count} children");
            foreach (Person child in bob.Children)
            {
                WriteLine($"child name: {child.Name}");    
            }
            WriteLine($"{bob.Name} is a {Person.Species}");
            WriteLine($"{bob.Name} is from {bob.HomePlanet}");
            
            WriteLine();
            WriteLine("Tuples-----------------------------------------");
            (string, int) fruit = bob.GetFruit();
            WriteLine($"USING TUPLES: {fruit.Item1}, {fruit.Item2} there are.");
            var vegetablesNamed = bob.GetVeg();
            WriteLine($"USING named TUPLES: {vegetablesNamed.Name}, {vegetablesNamed.Number} there are.");

            // Deconstructing tuples
            (string name, int fruitNumber) = bob.GetFruit();
            WriteLine($"Deconstructed TUPLE: {name}, {fruitNumber}");

            // Inferring tuple names
            var thing1 = ("Neville", 4);
            WriteLine($"{thing1.Item1} has {thing1.Item2} children.");
            
            var thing2 = (bob.Name, bob.Children.Count);
            WriteLine($"{thing2.Item1} has {thing2.Item2} children-");            

            // optional parameters
            WriteLine();
            WriteLine("Optional Parameters------------------------------");
            WriteLine(bob.OptionalParameters());
            WriteLine(bob.OptionalParameters("Jump", 2.4));
            WriteLine(bob.OptionalParameters(number: 2.4, active: false));
            WriteLine(bob.OptionalParameters(active: false, command: "Fly"));
            WriteLine(bob.OptionalParameters("Walk", active: false));


            // alice
            var alice = new Person();
            alice.Name = "Alice Smith";
            alice.DateOfBirth = new DateTime(1968,10,22);

            WriteLine();
            WriteLine("Alice------------------------------");
            WriteLine(
                format: "{0} was born on {1:dd, MMM yy}",
                arg0: alice.Name,
                arg1: alice.DateOfBirth
            );         

            WriteLine();
            WriteLine("Constructors----------------------------");
            // blank Person to use constructor
            var blankPerson = new Person();
            WriteLine(
                format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: blankPerson.Name,
                arg1: blankPerson.HomePlanet,
                arg2: blankPerson.Instantiated
            ); 

            var gunny = new Person("Gunny", "Mars");
            WriteLine(
                format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: gunny.Name,
                arg1: gunny.HomePlanet,
                arg2: gunny.Instantiated
            );                          


            // bank account class use
            BankAccount.InterestRate = 0.012M;

            var jonesAccount = new BankAccount();
            jonesAccount.AccountName = "Mrs Jones";
            jonesAccount.Balance = 2400;

            var smithAccount = new BankAccount();
            smithAccount.AccountName = "Mrs Smith";
            smithAccount.Balance = 800;

            WriteLine();
            WriteLine("public STATIC property - shared across all instances of class - bank interest rate demonstrateS ---------------");
            WriteLine(
                format: "{0} earned {1:C} interest",
                arg0: jonesAccount,
                arg1: jonesAccount.Balance * BankAccount.InterestRate
            );
            WriteLine(
                format: "{0} earned {1:C} interest",
                arg0: smithAccount,
                arg1: smithAccount.Balance * BankAccount.InterestRate
            );


            WriteLine();
            WriteLine("PARAMETERS  - 3 ways: by value, reference or an an out parameter ---------------");
            int a = 10;
            int b = 20;
            int c = 30; // no point in setting value of c as gets repoaced from within the method
            bob.PassingParameters(a, ref b, out c);
            WriteLine($"a: {a}, b: {b}, c {c}");

            int d = 10;
            int e = 20;
            
            WriteLine($"d: {d}, e: {e}, f doesn't exist yet");
            bob.PassingParameters(d, ref e, out int f);
            WriteLine($"d: {d}, e: {e}, f: {f}");


            WriteLine();
            WriteLine("Partial classes and properties, and indexers ---------------");            
            var sam = new Person
            {
                Name = "Sam",
                DateOfBirth = new DateTime(1972, 1 ,27)
            };
            WriteLine(sam.Origin);
            WriteLine(sam.Greeting);
            WriteLine(sam.Age);
            sam.FavouriteIceCream = "Mint choc chip";
            sam.FavouritePrimaryColour = "Red";

            WriteLine($"{sam.Name}'s favourite ice cream is {sam.FavouriteIceCream} and fav primary colour is {sam.FavouritePrimaryColour}");
            sam.Children.Add(new Person {Name = "Charlie"});
            sam.Children.Add(new Person {Name = "Ella"});            

            // if you had no indexer set up for Children:
            WriteLine($"{sam.Name}'s first child is {sam.Children[0].Name}");
            WriteLine($"{sam.Name}'s second- child is {sam.Children[1].Name}");
            
            // if you have indexer set up for Children - does it really add much value? I think it reduces ease of reading code...            
            WriteLine($"{sam.Name}'s first child is {sam[0].Name}");
            WriteLine($"{sam.Name}'s second- child is {sam[1].Name}");



        }
    }
}
