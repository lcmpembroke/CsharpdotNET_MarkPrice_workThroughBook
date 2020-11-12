using System;
using Microsoft.EntityFrameworkCore;  // dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 3.1.9
using ExportDataDifferentSerialization.Models;
using System.Linq;                  // for IQueryable<T>
using static System.IO.Path;        // Combine() function for filePath
using static System.Environment;    // current directory to work out file path
using System.IO;                    // for FileStream
using System.Xml;                   // for XmlWriter
using System.Text.Json;             // for Json serialization

namespace ExportDataDifferentSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ExportDataDifferentSerialization running....");
            Console.WriteLine("¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬¬....");

            Console.WriteLine("Creating files with different serialization for categories and their products...");

            // OUTPUT file sizes below - notice for the xml files, attributes is smaller, and csv smaller than Json
            //  Written  11380 bytes of XML  to categories-and-products.xml.
            //  Written   6174 bytes of XML  to categories-and-products_attributes.xml.
            //  Written  12255 bytes of JSON to categories-and-products.json.
            //  Written   5893 bytes of csv  to categories-and-products.csv.

            // Query database
            using (var db = new Northwind())
            {
                // query to get all categories with their related products
                IQueryable<Category> categories =  db.Categories.Include(c => c.Products);
                
                XMLSerialize(categories);

                // this writes the file out as with attributes instead of different child elements...(gives smaller file size)
                XMLSerializeUsingDelegate(categories, true);

                JSONSerialize(categories);

                CsvSerialize(categories);

            }


        }


        public static void XMLSerialize(IQueryable<Category> categories)
        {
            try
            {
                string filePath = Combine(CurrentDirectory, "categories-and-products.xml");

                using(FileStream xmlFileStream = File.Create(filePath))
                {
                    using(XmlWriter xmlWriter = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true }))
                    {
                        xmlWriter.WriteStartDocument();

                        xmlWriter.WriteStartElement("categories");
                        foreach (Category category in categories)
                        {
                            xmlWriter.WriteStartElement("category");
                            xmlWriter.WriteElementString("id", category.CategoryId.ToString());
                            xmlWriter.WriteElementString("name", category.CategoryName);
                            xmlWriter.WriteElementString("desc", category.Description);
                            xmlWriter.WriteElementString("product_count", category.Products?.Count.ToString());

                            xmlWriter.WriteStartElement("products");
                            foreach (Product product in category.Products)
                            {
                                xmlWriter.WriteStartElement("product");    
                                xmlWriter.WriteElementString("id", product.ProductID.ToString());
                                xmlWriter.WriteElementString("name", product.ProductName);
                                xmlWriter.WriteElementString("cost", product.Cost.ToString());
                                xmlWriter.WriteElementString("stock", product.Stock?.ToString());
                                xmlWriter.WriteEndElement(); // for product
                            }
                            xmlWriter.WriteEndElement(); // for products
                            xmlWriter.WriteEndElement(); // for category  
                        }
                        xmlWriter.WriteEndElement(); // for categories
                        xmlWriter.WriteEndDocument();
                    }                

                }  
                
                Console.WriteLine($"Written {new FileInfo(filePath).Length} bytes of XML to {filePath}.");             
                //Console.WriteLine(File.ReadAllText(filePath));   
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} has message {ex.Message}");
            }

        }

        // kind of a function pointer type to delegate work
        private delegate void WriteDataDelegate(string name, string value);

        public static void XMLSerializeUsingDelegate(IQueryable<Category> categories, bool useAttributes)
        {

            try
            {
                string filePath = Combine(CurrentDirectory, "categories-and-products_attributes.xml");
                WriteDataDelegate writeMethod;

                using(FileStream xmlFileStream = File.Create(filePath))
                {
                    using(XmlWriter xmlWriter = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true, OmitXmlDeclaration = false }))
                    {
                        if (useAttributes)
                        {
                            writeMethod = xmlWriter.WriteAttributeString;
                        }
                        else
                        {
                            writeMethod = xmlWriter.WriteElementString;
                        }
                        xmlWriter.WriteStartDocument();

                        xmlWriter.WriteStartElement("categories");
                        foreach (Category category in categories)
                        {
                            xmlWriter.WriteStartElement("category");
                            writeMethod("id", category.CategoryId.ToString());
                            writeMethod("name", category.CategoryName);
                            writeMethod("desc", category.Description);
                            writeMethod("product_count", category.Products?.Count.ToString());

                            xmlWriter.WriteStartElement("products");
                            foreach (Product product in category.Products)
                            {
                                xmlWriter.WriteStartElement("product");    
                                writeMethod("id", product.ProductID.ToString());
                                writeMethod("name", product.ProductName);
                                writeMethod("cost", product.Cost?.ToString());
                                writeMethod("stock", product.Stock?.ToString());
                                xmlWriter.WriteEndElement(); // for product
                            }
                            xmlWriter.WriteEndElement(); // for products
                            xmlWriter.WriteEndElement(); // for category  
                        }
                        xmlWriter.WriteEndElement(); // for categories
                        xmlWriter.WriteEndDocument();
                    }                

                }  
                
                Console.WriteLine($"Written {new FileInfo(filePath).Length} bytes of XML to {filePath}.");
                //Console.WriteLine(File.ReadAllText(filePath));                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} has message {ex.Message}");
            }

        }

        public static void JSONSerialize(IQueryable<Category> categories)
        {
            // JSON has the following styles options:
            // Object - unordered "name/value" assembly. 
            //          e.g.  var user = {"name":"Manas","gender":"Male","birthday":"1987-8-8"}   
            // Array -  value order set. 
            //          e.g. var userlist = [{"user":{"name":"Manas","gender":"Male","birthday":"1987-8-8"}},    
            //                               {"user":{"name":"Mohapatra","Male":"Female","birthday":"1987-7-7"}}
            //                                ]    
            // String - any quantity Unicode character assembly which is enclosed with quotation marks. It uses backslash to escape.
            //             e.g. var userlist = "{\"ID\":1,\"Name\":\"Manas\",\"Address\":\"India\"}"      


            string filePath = Combine(CurrentDirectory, "categories-and-products.json");
            using(FileStream fs = File.Create(filePath))
            {
                var jsonWriter = new Utf8JsonWriter(fs, new JsonWriterOptions { Indented = true });
                using(jsonWriter)
                {
                    jsonWriter.WriteStartObject();          // root object
                    jsonWriter.WriteStartArray("categories");   // categories array

                    foreach (Category category in categories)
                    {
                        jsonWriter.WriteStartObject();              // category object

                        jsonWriter.WriteNumber("id",category.CategoryId);
                        jsonWriter.WriteString("name",category.CategoryName);
                        jsonWriter.WriteString("desc",category.Description);
                        jsonWriter.WriteNumber("product_count",category.Products.Count);
                        

                        jsonWriter.WriteStartArray("products");         // products array
                        foreach (Product product in category.Products)
                        {
                            jsonWriter.WriteStartObject();                  // product object

                            jsonWriter.WriteNumber("id", product.ProductID);
                            
                            jsonWriter.WriteString("name", product.ProductName);
                            jsonWriter.WriteNumber("cost", product.Cost.Value);
                            jsonWriter.WriteNumber("stock", product.Stock.Value);
                            jsonWriter.WriteBoolean("discontinued", product.Discontinued);  // shouldn't need as filtered out in fluent API in Northwind.cs ini OnModelCreating()

                            jsonWriter.WriteEndObject();                    // product object
                        }
                        jsonWriter.WriteEndArray();                        // products array
                        
                        
                        jsonWriter.WriteEndObject();                // category object
                    }
                    jsonWriter.WriteEndArray();                 // categories array
                    jsonWriter.WriteEndObject();            // root object
                }
                Console.WriteLine($"Written {new FileInfo(filePath).Length} bytes of JSON to {filePath}.");   
            }

        }        

        public static void CsvSerialize(IQueryable<Category> categories)
        {
            // note will have category information repeated for each product as has to be lineear structure for csv...
            string filePath = Combine(CurrentDirectory,"categories-and-products.csv");

            using(FileStream csvStream = File.Create(filePath))
            {
                using(StreamWriter csv = new StreamWriter(csvStream))
                {
                    foreach (Category c in categories)
                    {
                        foreach (Product p in c.Products)
                        {
                            csv.Write("{0},\"{1}\",\"{2}\",",
                                arg0: c.CategoryId.ToString(),
                                arg1: c.CategoryName,
                                arg2: c.Description);

                            csv.Write("{0},\"{1}\",{2},",
                                arg0: p.ProductID.ToString(),
                                arg1: p.ProductName,
                                arg2: p.Cost.Value.ToString());

                            csv.WriteLine("{0},{1}",
                                arg0: p.Stock.ToString(),
                                arg1: p.Discontinued.ToString());      
                        }
                    }
                }
            }
            Console.WriteLine($"Written {new FileInfo(filePath).Length} bytes of csv to {filePath}.");   
        }
    }
}
