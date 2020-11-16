//command line command to add project reference: dotnet add PracticeEncryptHash.csproj reference ../CryptographyLib/CryptographyLib.csproj
using System;
using static System.Console;
using Packt.Shared;             
using System.IO;                // for file stream
using static System.IO.Path;    // for working out filepaths given client environent
using static System.Environment;// for getting current directory path
using System.Xml;               // for XmlReaderSettings
using System.Collections.Generic;

namespace PracticeEncryptHash
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("\nPracticeEncryptHash running ...");
            
            WriteLine("\nEnter password for encryption/decryption: ");
            string passwordToEncrypt = ReadLine();

            // Create customer data for two example customers and note they have the same password
            var customers = new List<Customer>
            {
                new Customer
                {
                Name = "Bob Smith",
                CreditCard = "1234-5678-9012-3456",
                Password = "Pa$$w0rd",
                },
                new Customer
                {
                Name = "Leslie Knope",
                CreditCard = "8002-5265-3400-2511",
                Password = "Pa$$w0rd",
                }
            };

            // write protected data to file
            WritetoXmlFile(customers, passwordToEncrypt);

            //TODO: put in delete file function!
        }

        private static void WritetoXmlFile(List<Customer> customers, string passwordToEncrypt)
        {
            // define an XML file to write to
            string xmlFile = Combine(CurrentDirectory, "..", "protected-customers.xml");

            var xmlWriter = XmlWriter.Create(xmlFile,new XmlWriterSettings { Indent = true });

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("customers");    

            foreach (Customer customer in customers)
            {
                xmlWriter.WriteStartElement("customer");
                xmlWriter.WriteElementString("name", customer.Name);

                xmlWriter.WriteElementString("creditcard",  Protector.Encrypt(customer.CreditCard, passwordToEncrypt));
                        
                // to protect the password we must salt and hash it, and store the random salt used
                var user = Protector.Register(customer.Name, customer.Password);
                xmlWriter.WriteElementString("password", user.SaltedHashedPassword);
                xmlWriter.WriteElementString("salt", user.Salt);
                xmlWriter.WriteEndElement();    // customer
            }
            xmlWriter.WriteEndElement();    // customers
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }   

        private static void customerXMLFileReader(string customerName, string password)
        {
            //TODO
        }
    }
}
