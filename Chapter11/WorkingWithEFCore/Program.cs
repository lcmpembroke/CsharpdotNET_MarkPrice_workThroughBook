using System;
using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;                     
using System.Collections.Generic;                       // for IEnumerable<>
using Microsoft.EntityFrameworkCore.Storage;            // for IDbContextTransaction interface

namespace WorkingWithEFCore
{
    class Program
    {

        static void QueryingCategories_pre_explicitLoading()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Categories and how many products they have:");

                // get all categories and their related products. 
                IQueryable<Category> categories = db.Categories
                //.Include(c => c.Products)   // NB using the "Include" method means EAGER LOADING (aka early loading) is done for the related products BOOK p383
                .TagWith("Get all categories and their related products");  // BOOK page 380: logging with query tags (annotates LINQ query to add SQL comment to log)

                foreach (Category c in categories)
                {
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products");
                }

            }
        }

        static void QueryingCategories()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Categories and how many products they have:");

                // get all categories and their related products. 
                IQueryable<Category> categories;
                db.ChangeTracker.LazyLoadingEnabled = false;

                Write("Enable eager loading? Y/N: ");
                bool eagerLoading = (ReadKey().Key == ConsoleKey.Y);
                bool explicitLoading = false;
                WriteLine();

                if (eagerLoading)
                {
                    categories = db.Categories.Include(c => c.Products);
                }
                else
                {
                    categories = db.Categories;
                    Write("Enable explicit loading? Y/N: ");
                    explicitLoading = (ReadKey().Key == ConsoleKey.Y);
                    WriteLine();
                }

                // foreach (Category c in categories)
                // {
                //     WriteLine($"{c.CategoryName} has {c.Products.Count} products");
                // }
                foreach (Category c in categories)
                {
                    if (explicitLoading)
                    {
                        Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                        ConsoleKeyInfo key = ReadKey();
                        WriteLine();

                        if (key.Key == ConsoleKey.Y)
                        {
                            var products = db.Entry(c).Collection(thisCategory => thisCategory.Products);
                            if (!products.IsLoaded) products.Load();
                        }
                    }
                    WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
                }                

            }
        }        

        static void QueryingProducts()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                WriteLine("Products that cost more than a price, highest at top:");

                // get user to input a valid price
                string input;
                decimal price;
                do
                {
                    Write("Enter a product price: ");
                    input = ReadLine();
                    
                } while (!decimal.TryParse(input, out price));

                // Query database
                IOrderedEnumerable<Product> prods = db.Products
                .AsEnumerable()                
                .Where(product => product.Cost > price)
                .OrderByDescending(product => product.Cost);

                foreach (Product p in prods)
                {
                    WriteLine($"{p.ProductID}: {p.ProductName} costs {p.Cost:$#,##0.00}, and has {p.Stock} items in stock");
                }

            }
        }

        static void QueryingWithLike()
        {
            using (var db = new Northwind())
            {
                var loggerFactory = db.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(new ConsoleLoggerProvider());

                Write("Enter part of a product name: ");
                string input = ReadLine();

                IQueryable<Product> products = db.Products.Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
                // NOTE: further reading on case (in)sensitivity...different defaults for different databases...
                //       https://docs.microsoft.com/en-us/ef/core/miscellaneous/collations-and-case-sensitivity

               
                WriteLine($"Number of products that met the criteria of \"{input}\": {products.AsQueryable().Count()}");

                foreach (Product item in products)
                {
                    WriteLine("{0} had {1} units in stock. Discontinued? {2}", item.ProductName, item.Stock, item.Discontinued);
                }
            }
        }

        static bool AddProduct(int categoryID,  string productName, decimal? price)
        {
            using (var db = new Northwind())
            {
                var newProduct = new Product
                {
                    CategoryID = categoryID,
                    ProductName = productName,
                    Cost = price
                };
                // mark product as added in change tracking
                db.Products.Add(newProduct);

                // save tracked changes to database
                int affected = db.SaveChanges();
                return (affected == 1);
            }
        }

        static void ListProducts()
        {

            // notes for below:
            // 1,-35    means left align    argument number 1 within a 35 char wide column
            // 3,5      means right align   argument number 3 within a 5 character wide column
            using (var db = new Northwind())
            {
                WriteLine("{0,-3} {1, -35} {2,8} {3,5} {4}", "ID", "Product name", "Cost", "Stock", "Disc.");

                foreach (var item in db.Products.OrderByDescending(p => p.Cost))
                {
                    WriteLine("{0:000} {1, -35} {2,8:$#,##0.00} {3,5} {4}", item.ProductID, item.ProductName, item.Cost, item.Stock, item.Discontinued);           
                }
            }
        }

        static bool IncreaseProductPrice(string name, decimal amountIncrease)
        {
            using (var db = new Northwind())
            {
                // get first product who name starts with name
                Product updateProduct = db.Products.First(p => p.ProductName.StartsWith(name));

                // change to this product that was returned is now tracked
                updateProduct.Cost += amountIncrease;

                // save tracked changes to database
                int affected = db.SaveChanges();
                return (affected == 1);

            }
        }

        static int DeleteProducts(string name)
        {
            using(var db = new Northwind())
            {
                using(IDbContextTransaction t = db.Database.BeginTransaction())
                {

                    WriteLine($"Transaction isolation level: {t.GetDbTransaction().IsolationLevel}");

                    IEnumerable<Product> products = db.Products.Where(p => p.ProductName.StartsWith(name));

                    db.Products.RemoveRange(products);  // deletes multiple entities

                    // save tracked changes to database
                    int affected = db.SaveChanges();

                    // commit the explicit transaction
                    t.Commit();
                    return affected;   
                }
            }
        }

        static void Main(string[] args)
        {
            WriteLine("\nRunning WorkingWithEFCore program....");
            WriteLine("-------------------------------------\n");
            // QueryingCategories();
            // QueryingProducts();
            // QueryingWithLike();

            // if (AddProduct(6, "Bob's burgers", 500M))
            // {
            //     WriteLine("Product added was successful.");
            // }

            // if (IncreaseProductPrice("Bob", 20M))
            // {
            //     WriteLine("Product price increase was successful.");
            // }

            int deleted = DeleteProducts("Bob");
            WriteLine($"{deleted} products were deleted.");
            ListProducts();
        }
    }


}
