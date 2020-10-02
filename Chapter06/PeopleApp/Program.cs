using System;
using Packt.Shared;             // homemade classes
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        // method name that handle events are of the form ObjectName_EventName
        private static void Harry_Shout(object sender,  EventArgs e)
        {
            Person p = (Person)sender;  // explicity cast sender to type Person
            WriteLine($"{p.Name} has an anger lever of: {p.AngerLevel}");
        }

        static void Main(string[] args)
        {
            var harry = new Person("Harry");
            var mary = new Person("Mary");
            var jill = new Person("Jill");

            var baby1 = mary.ProcreateWith(harry);
            var baby2 = Person.Procreate(harry,jill);
            // multiply operator overloaded with the Person class:
            var baby3 = harry * mary;

            WriteLine($"{harry.Name} has {harry.Children.Count} children.");
            WriteLine($"{mary.Name} has {mary.Children.Count} children.");
            WriteLine($"{jill.Name} has {jill.Children.Count} children.");
            WriteLine(
                format: "{0}'s first child is called {1}",
                arg0: harry.Name,
                arg1: harry.Children[0].Name
            );
            
            WriteLine($"5! is {Person.Factorial(5)}");

            
            // assign the method defined in this program to the delegate field
            // note: delegates are multicast so can assign multiple delegates to a single delegate field although no control as to order called
            // so could have used += operator instead of just the = assignment operator (book page 189)
            harry.Shout += Harry_Shout;  

            harry.Poke();
            harry.Poke();
            harry.Poke();
            harry.Poke();

            // BOOK: page 192 INTERFACES
            Person[] people = 
            {
                new Person("Simon"),
                new Person("Jenny"),
                new Person("Adam"),
                new Person("Richard")
            };

            WriteLine("People unsorted: ");
            foreach (var person in people)
            {
                Write($"{person.Name} ");
            }
            WriteLine();

            WriteLine("People sorted - uses IComparable implementation to sort: ");
            Array.Sort(people);
            foreach (var person in people)
            {
                Write($"{person.Name} ");
            }
            WriteLine();


            WriteLine("People sorted - uses PersonComparer (implements IComparer) class sort: ");
            Array.Sort(people,new PersonComparer());
            foreach (var person in people)
            {
                Write($"{person.Name} ");
            }
            WriteLine();

            // BOOK: page 198 making types safely reusable with GENERICS
            var t1 = new Thing();
            t1.Data = 42;
            WriteLine($"Thing with an integer: {t1.Process(42)}");

            var t2 = new Thing();
            t2.Data = "apple";
            WriteLine($"Thing with an string: {t2.Process("apple")}");      

            // on instantiating instance of generic type, must pass a type parameter
            var gt1 = new GenericThing<int>();
            gt1.Data = 42;
            WriteLine($"GenericThing with an integer: {gt1.Process(42)}");

            var gt2 = new GenericThing<string>();
            gt2.Data = "apple";
            WriteLine($"GenericThing with an string: {gt2.Process("apple")}");                   

            string number1 = "4";
            WriteLine("{0} squared is {1}",
            arg0: number1,
            arg1: Squarer.Square<string>(number1));

            byte number2 = 3;
            WriteLine("{0} squared is {1}",
            arg0: number1,
            arg1: Squarer.Square<byte>(number2));

            var vector1 = new DisplacementVector(3,4);
            var vector2 = new DisplacementVector(-2,1);
            var vector3 = vector1 + vector2;
            WriteLine($"vector3: {vector3.X}, {vector3.Y}");

            Employee john = new Employee
            {
                Name = "John Jones",
                DateOfBirth = new DateTime(1990,7,28)
            };
            john.WriteToConsole();
            john.EmployeeCode = "JJ001";
            john.HireDate =  new DateTime(2014,11,23);
            WriteLine($"{john.Name} was hired on {john.HireDate:dd/MM/yy} with code {john.EmployeeCode}");

            WriteLine(john.ToString());
            WriteLine();

            // BOOK: page 212 Understanding polymorphism ---------------------------------------------------------------------

            // implicit casting section ****
            // an instance of a derived type (Employee) can be stored in a variable of the base type (Person) - IMPLICIT CASTING
            Employee aliceEmployee = new Employee{ Name = "Alice", EmployeeCode = "AA123"};
            Person alicePerson = aliceEmployee;  // IMPLICIT casting

            WriteLine("Understanding Polymorphism section: ");
            WriteLine("alicePerson has just been assigned to aliceEmployee i.e. alicePerson = aliceEmployee"); 
            WriteLine();
            WriteLine("Now calling aliceEmployee.WriteToConsole(), then alicePerson.WriteToConsole()");
            aliceEmployee.WriteToConsole();       // Employee.WriteToConsole(): Alice was born on 01/01/01 and hired on 01/01/01
            alicePerson.WriteToConsole();         // Person.WriteToConsole(): Alice was born on a Monday
            WriteLine("Calling aliceEmployee.ToString(): " + aliceEmployee.ToString());  // Employee.ToString(): Alice's code is AA123
            WriteLine("Calling alicePerson.ToString(): " + alicePerson.ToString());    // Employee.ToString(): Alice's code is AA123

            // explicit casting section - and avoiding Casting Exceptions two options: use 'is' or 'as' keywords ****     
            
            // use keyword 'is'
            if (alicePerson is Employee)
            {
                WriteLine($"{nameof(alicePerson)} IS an Employee - because of earlier assignment: alicePerson = aliceEmployee");
                // safe to cast
                Employee explicitAlice = (Employee)alicePerson;     
            }

            Employee aliceAsEmployee = alicePerson as Employee; // keyword 'as' used to explicity cast --> need to check for null though
            if (aliceAsEmployee != null)
            {
                WriteLine($"{nameof(alicePerson)} AS an Employee - because of earlier assignment: Employee aliceAsEmployee = alicePerson as Employee");
            }



            // BOOK: page 216 Inheriting exceptions  ---------------------------------------------------------------------
            try
            {
                john.TimeTravel(new DateTime(1999,12,31));
                john.TimeTravel(new DateTime(1950,12,25));
            }
            catch (PersonException ex)
            {
                WriteLine(ex.Message);
            }



            // BOOK: page 218 Extending types when you cannot inherit (sealed)   ---------------------------------------------------------------------
            string email1 = "test@test.com";
            string email2 = "test&test.com";
            WriteLine("{0} is a valid email address: {1}",
            arg0: email1,
            arg1: StringExtensions.IsValidEmail(email1));

            WriteLine("{0} is a valid email address: {1}",
            arg0: email2,
            arg1: StringExtensions.IsValidEmail(email2));

            WriteLine("{0} is a valid email address: {1}",
            arg0: email1,
            arg1: email1.IsValidEmail());

            WriteLine("{0} is a valid email address: {1}",
            arg0: email2,
            arg1: email2.IsValidEmail());            

        }
    }
}
