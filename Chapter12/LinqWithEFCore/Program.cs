using static System.Console;
using Pembroke.Shared;
//using Microsoft.EntityFrameworkCore;
using System.Linq;              // LINQ extension methods eg Where

namespace LinqWithEFCore
{
    class Program
    {
        static void FilterAndSort()
        {
            using (var db = new Northwind())
            {
                var query = db.Products                 // this is a SELECT * FROM PRODUCTS... (not just columns wanted)
                .Where(p => p.UnitPrice < 10M)          // returns IQueryable<Product>
                .OrderByDescending(p => p.UnitPrice)    // returns IOrderedQueryable<Product>
                .Select(product => new {                // anonymous type created here...adding this select method to return 
                    product.ProductID,                  // only the 3 needed columns makes the database command more efficient
                    product.ProductName,                // as only selects what is requested rather than all data in all columns
                    product.UnitPrice
                } );


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
                // join every product tp its category and return matches
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

        static void Main(string[] args)
        {
            WriteLine("LinqWithEFCore running...");
            FilterAndSort();
            JoinCategoriesAndProducts();
        }
    }
}
