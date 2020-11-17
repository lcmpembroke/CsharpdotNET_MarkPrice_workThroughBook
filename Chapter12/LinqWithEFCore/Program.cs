using static System.Console;
using Pembroke.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;              // LINQ extension methods eg Where
using System.Xml.Linq;          // generate xml using LINQ to Xml

namespace LinqWithEFCore
{
    class Program
    {
        static void FilterAndSort()
        {
            using (var db = new Northwind())
            {
                // BOOK: page 411
                /*          ...commented out to show use of extension method below
                var query = db.Products                 // this is a SELECT * FROM PRODUCTS... (not just columns wanted)
                .Where(p => p.UnitPrice < 10M)          // returns IQueryable<Product>
                .OrderByDescending(p => p.UnitPrice)    // returns IOrderedQueryable<Product>
                .Select(product => new {                // anonymous type created here...adding this select method to return 
                    product.ProductID,                  // only the 3 needed columns makes the database command more efficient
                    product.ProductName,                // as only selects what is requested rather than all data in all columns
                    product.UnitPrice
                } );
                */

                // BOOK: page 423 - modify LINQ query to call customer chainable extension method (in MyLinqExtensions)
                var query = db.Products
                    .ProcessSequence()
                    .Where(p => p.UnitPrice < 10M)
                    .OrderByDescending(p => p.UnitPrice)
                    .Select(p => new 
                    {
                        p.ProductID,
                        p.ProductName,
                        p.UnitPrice
                    });


                WriteLine("Products that cost less than $10:");
                foreach (var item in query)
                {
                    WriteLine("{0}: {1} costs {2:$#,##0.00}",
                        item.ProductID, 
                        item.ProductName, 
                        item.UnitPrice
                    );                    
                }
                WriteLine();
            }
        }

        static void JoinCategoriesAndProducts()
        {
            // NOTE for parameters for these JOIN and GROUPJOIN extension methods see:
            //          https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.join?view=net-5.0
            //      Join preserves the order of the elements of outer, and for each of these elements, the order of the matching elements of inner.
            // outer: first sequence to join
            // inner: second sequence that joins to the first sequence.
            // outerKeySelector: function to extract the join key from each element of the first sequence.
            // innerKeySelector: function to extract the join key from each element of the second sequence.
            // resultSelector:function to create a result element from two matching elements.            
            using (var db = new Northwind())
            {
                // join every product to its category and return matches
                // note here categories is the outer sequence, and products is the inner sequence
                var queryJoin = db.Categories.Join(
                    inner:              db.Products,
                    outerKeySelector:   category => category.CategoryID,
                    innerKeySelector:   product => product.CategoryID,
                    resultSelector:     (c, p) =>
                        new { c.CategoryName, p.ProductName, p.ProductID })
                    //.OrderBy(cp => cp.CategoryName);
                    .OrderBy(cp => cp.ProductID);


                foreach (var item in queryJoin)
                {
                    WriteLine("{0}: {1} is in {2}",
                        arg0: item.ProductID,
                        arg1: item.ProductName,
                        arg2: item.CategoryName
                    );
                }
            }
        }

        static void GroupJoinCategoriesAndProducts()
        {
            using (var db = new Northwind())
            {
                // group all products by their category to return 8 matches (there are 8 categories in the database) - Category will be OUTER join, product the inner sequence)
                // i.e. Category
                //          product
                //          product
                //          product
                //      Category
                //          product
                //          product
                //          product

                // note that AsEnumerable() needs to be called because not all LINQ extension methods can be converted from expression trees into other query syntax like SQL.
                // So in these cases, we convert from IQueryable<T> to IEnumerable<T> by calling AsEnumerable() which forces query processing to use LINQ to EF Core only
                // to bring the data into the application and then use LINQ to Objects to execute more complex processing in-memory.
                // NOTE - this is often less efficient (ideally get SQL to do the hard work on the database server rather than in memory of program)
                var queryGroup = db.Categories.AsEnumerable().GroupJoin(
                                        inner:              db.Products,
                                        outerKeySelector:   cat => cat.CategoryID,
                                        innerKeySelector:   p => p.CategoryID,
                                        resultSelector:     (c, matchingProducts) => new 
                                                        {
                                                            c.CategoryName,
                                                            Products = matchingProducts.OrderBy(p => p.ProductName)
                                                        });

                foreach (var item in queryGroup)
                {
                    WriteLine("\n{0} has {1} products", arg0: item.CategoryName, arg1: item.Products.Count());
                    foreach (var product in item.Products)
                    {
                        WriteLine("   {0}", arg0: product.ProductName);
                    }
                }
            }
        }

        static void AggregateProducts()
        {
            // LINQ extension methods to perform aggregation functions:
            using (var db = new Northwind())
            {
                WriteLine("{0, -25} {1, 10}", arg0: "Product count", arg1: db.Products.Count());

                WriteLine("{0, -25} {1, 10:$#,##0.00}", arg0: "Highest product price", arg1: db.Products.Max(p => p.UnitPrice));

                WriteLine("{0, -25} {1, 10:N0}", arg0: "Sum of units in stock", arg1: db.Products.Sum(p => p.UnitsInStock));

                WriteLine("{0, -25} {1, 10:N0}", arg0: "Sum of units on order", arg1: db.Products.Sum(p => p.UnitsOnOrder));

                WriteLine("{0, -25} {1, 10:$#,##0.00}", arg0: "Average unit price", arg1: db.Products.Average(p => p.UnitPrice));

                WriteLine("{0, -25} {1, 10:$#,##0.00}", arg0: "Value of units in stock", arg1: db.Products.AsEnumerable().Sum(p => (p.UnitPrice * p.UnitsInStock)));

            }
        }

        static void CustomExtensionsMethods()
        {
            using (var db = new Northwind())
            {
                WriteLine("Mean units in stock: {0:N0}",db.Products.Average(p=> p.UnitsInStock));
                WriteLine("Mean unit price: {0:$#,##0.00}",db.Products.Average(p=> p.UnitPrice));

                WriteLine("Median units in stock: {0:N0}", db.Products.Median(p => p.UnitsInStock));
                WriteLine("Median unit price: {0:$#,##0.00}", db.Products.Median(p => p.UnitPrice));

                WriteLine("Mode units in stock: {0:N0}", db.Products.Mode(p => p.UnitsInStock));    // TODO: check this....gives answer zero
                WriteLine("Mode unit price: {0:$#,##0.00}", db.Products.Mode(p => p.UnitPrice));

            }
        }
        // LINQ query comprehension syntax (syntactic sugar - provides C# keywords for most common LINQ features)

        static void OutputProductAsXml()
        {
            using (var db = new Northwind())
            {
                var productsForXml = db.Products.ToArray();

                var xml = new XElement("products",
                    from p in productsForXml
                    select new XElement("product",
                        new XAttribute("id", p.ProductID),
                        new XAttribute("price", p.UnitPrice),
                        new XElement("name", p.ProductName)
                    )
                );

                WriteLine(xml.ToString());
            }
        }

        static void ProcessSettings()
        {
            XDocument doc = XDocument.Load("settings.xml");

            var appSettings = doc.Descendants("appSettings")
                .Descendants("add")
                .Select(node => new 
                {
                    Key = node.Attribute("key").Value,
                    Value = node.Attribute("value").Value
                }).ToArray();

                foreach (var item in appSettings)
                {
                    WriteLine($"{item.Key}: {item.Value}");
                }
        }

        static void Main(string[] args)
        {
            WriteLine("LinqWithEFCore running...");
             // FilterAndSort();
            // JoinCategoriesAndProducts();
            // GroupJoinCategoriesAndProducts();
            // AggregateProducts();
            //CustomExtensionsMethods();
            // OutputProductAsXml();
            ProcessSettings();
        }
    }
}
