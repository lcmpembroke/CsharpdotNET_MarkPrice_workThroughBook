using System;
using static System.Console;
using System.Threading.Tasks;
using SharpPad;

namespace Dumping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var complexObject = new 
            {
                Firstname = "Jane",
                BirthDate = new DateTime(year: 1972, month: 12, day: 25),
                Friends = new [] {"Anna", "Bob", "Charlie", "Dina"}
            };

            WriteLine($"Dumping {nameof(complexObject)} to SharpPad");
            await complexObject.Dump();
        }
    }
}
