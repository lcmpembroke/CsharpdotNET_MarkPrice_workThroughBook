using System;
using Pembroke.Shared;
using System.Linq;                      // where
using Microsoft.EntityFrameworkCore;

// NOTE I'VE ADDED A REFERENCE TO LinqWithEFCore project so I can use the Pembroke.Shared namespace code for access to the database...
// Ran following command then built the project with the reference added :-)
// dotnet add LinqQueryPractice.csproj reference ../LinqWithEFCore/LinqWithEFCore.csproj

namespace LinqQueryPractice
{
    class Program
    {

        static void GetCompaniesInCity(string city)
        {
            // using (var db = new Northwind())
            // {
            //     var query = db.Customers.Where(c => c.)
            // }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nLinqQueryPractice running...");
            Console.WriteLine("Enter a city: ");
            string city = Console.ReadLine();

            GetCompaniesInCity(city);

        }
    }
}