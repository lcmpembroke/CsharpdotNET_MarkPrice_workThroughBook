using System;                      // DateTime
using System.Collections.Generic;   // List<T>, HashSet<T>
using System.Xml.Serialization;     // XMLSerializer
using System.IO;                    // FileStream
using Pembroke.Shared;              // Person
using static System.Console;
using static System.Environment;
using static System.IO.Path;
using Newtonsoft.Json;              // note there is a higher performing System.Text.Json library that maybe should be used instead bringing in 3rd party package
using System.Threading.Tasks;
using NuJson = System.Text.Json.JsonSerializer;


namespace WorkingWithSerialization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // XML = eXtensible Markup Language - more verbose, better supported in legacy systems
            // JSON - JavaScript Object Notation - more compact, better for web and mobile apps

            Console.WriteLine("\n\nWorkingWithSerialization running...");


            // Create an object graph
            // Notes:   An object graph is a set of objects that reference one another.
            //          Serializing an object graph is tricky (the serializer has to assign a unique ID to every object and then replace references with unique IDs)
            //          Note - when using XmlSerializer, remember only th public fields and properties are included - and the type must have a parameterless contructor.
            //          Output (xml) can be customised with attributes - there are many other ones than given here.
            var people = new List<Person>
            {
                new Person(30000M) {
                    FirstName = "Alice"
                    ,LastName = "Smith"
                    ,DateOfBirth = new DateTime(1974, 3, 14)
                },
                new Person(40000M) {
                    FirstName = "Bob",
                    LastName = "Jones",
                    DateOfBirth = new DateTime(1969, 11, 23)
                },
                new Person(20000M) {
                    FirstName = "Charlie",
                    LastName = "Cox",
                    DateOfBirth = new DateTime(1984, 5, 4),
                    Children = new HashSet<Person> {
                        new Person(0M) {
                            FirstName = "Sally",
                            LastName = "Cox",
                            DateOfBirth = new DateTime(2000, 7, 12)
                        },
                        new Person(0M) {
                            FirstName = "Ted",
                            LastName = "Cox",
                            DateOfBirth = new DateTime(2002, 8, 1)
                        },                        
                    }
                }             
            };

            // create object that will format a List of Persons as XML
            // NOTE:    typeof is an operator keyword which is used to get a type at the compile-time.
            //          This operator takes the Type itself as an argument and returns the marked type of the argument.
            var xmlPersonListSerializer = new XmlSerializer(typeof(List<Person>));

            // create a file to write to
            string filePath = Combine(CurrentDirectory, "people.xml");
            
            using(FileStream xmlFileStream = File.Create(filePath))
            {
                // serialize object graph to the stream
                xmlPersonListSerializer.Serialize(xmlFileStream,people);
            }   // remember File stream is closed and disposed of due to the using stmt
            
            WriteLine($"Written {new FileInfo(filePath).Length} bytes of XML to {filePath}.");

            // display the serialized object graph
            WriteLine(File.ReadAllText(filePath));


            // ------------- DESERIALIZING XML file back into live objects in memory  -----------------
            using(FileStream xmlLoad = File.Open(filePath, FileMode.Open))
            {
                // deserialize and explicitly cast the object graph into a List of Person
                var loadedPeople = (List<Person>)xmlPersonListSerializer.Deserialize(xmlLoad);

                foreach (var item in loadedPeople)
                {
                    WriteLine($"{item.FirstName} {item.LastName} has {item.Children.Count} children.");
                }
            }

            // create file to write to
            string jsonPath = Combine(CurrentDirectory,"people.json");

            using(StreamWriter jsonStream = File.CreateText(jsonPath))
            {
                // create an objec that will format as JSON
                JsonSerializer jsonSerializer = new JsonSerializer();

                // serialize the object graph into a string
                jsonSerializer.Serialize(jsonStream, people);
            }

            WriteLine("\nWritten {0:N0} bytes of Json to {1}",new FileInfo(jsonPath).Length,jsonPath);

            // display serialized object graph
            WriteLine(File.ReadAllText(jsonPath));


            // Deserializing JSON using new APIs
            using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
            {
                // deserialize object graph into a List of Person 
                var loadedPeople = (List<Person>)
                await NuJson.DeserializeAsync(
                    utf8Json: jsonLoad,
                    returnType: typeof(List<Person>));

                foreach (var item in loadedPeople)
                {
                    WriteLine("{0} has {1} children.", item.LastName, item.Children?.Count);
                }
            }
        }
    }
}
