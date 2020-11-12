using System;
using System.Linq;

namespace LinqWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nLinqWithObjects running...");
            //LinqWithArrayOfStrings();
            LinqWithArrayOfExceptions();
        }

        static void LinqWithArrayOfStrings()
        {
            var names = new string[] { "Michael","Pam","Jim","Dwight","Angela","Kevin","Toby","Creed" };

            //var query = names.Where(new Func<string, bool>(NameLongerThanFour)); **don't have to explicitly instantiate the delegate - see next line
            // var query = names.Where(NameLongerThanFour);  ** don't have to write a separate function, can use a lambda expression (nameless function)
            var query = names
                .Where(name => name.Length > 4)
                .OrderBy(name => name.Length)
                .ThenBy(name => name);  // will sort alphabetically by name after length

            foreach (string item in query)
            {
                Console.WriteLine(item);
            }
        }

        static bool NameLongerThanFour(string name)
        {
            return name.Length > 4;
        }

        static void LinqWithArrayOfExceptions()
        {
            var errors = new Exception[]
            {
                new ArgumentException(),
                new SystemException(),
                new IndexOutOfRangeException(),
                new InvalidOperationException(),
                new NullReferenceException(),
                new InvalidCastException(),
                new OverflowException(),
                new DivideByZeroException(),
                new ApplicationException()
            };

            var numberErrors = errors.OfType<ArithmeticException>();    // this will only select 'overflow' and 'divideByZero' exceptions
            
            foreach (var error in numberErrors)
            {
                Console.WriteLine(error);
            }
        }

    }
}
