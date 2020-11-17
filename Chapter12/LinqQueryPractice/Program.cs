using System;
using Pembroke.Shared;
using System.Linq;

namespace LinqQueryPractice
{
    class Program
    {

        static void GetCityListForCustomers()
        {
            // https://dotnettutorials.net/lesson/linq-distinct-method/
            using (var db = new Northwind())
            {
                 IQueryable<string> distinctCities = db.Customers.Select(c => c.City).Distinct();

                // foreach (string city in distinctCities)
                // {
                //     Console.Write($"{city},  ");
                // }
                // Better way: string.Join concatenates members of a constructed System.Collections.Generic.IEnumerable<out T> collection of type string
                // using the specified separator between each member.
                Console.WriteLine($"{string.Join(", ", distinctCities)}");

            }
        }
        
        static void GetCompaniesForCustomersInCity(string requestedCity)
        {
            using (var db = new Northwind())
            {
                var query = db.Customers
                .Where(c => c.City == requestedCity);

                Console.WriteLine($"There are {query.Count()} customers in {requestedCity}:");
                foreach (var item in query)
                {
                    Console.WriteLine($"{item.CompanyName}, {item.ContactName}, {item.CustomerId}");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("\nLinqQueryPractice running...");

            Console.WriteLine("Cities in which current customers reside: ");
            GetCityListForCustomers();

            Console.Write("Enter a city: ");
            string city = Console.ReadLine();
            
            GetCompaniesForCustomersInCity(city);

        }
    }
}
