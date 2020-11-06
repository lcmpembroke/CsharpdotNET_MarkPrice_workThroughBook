using System;
using static System.Console;
using Packt.Shared;             //command line command to add project reference to this: dotnet add PracticeEncryptHash.csproj reference ../CryptographyLib/CryptographyLib.csproj
using System.IO;                // for file stream
using static System.IO.Path;    // for working out filepaths given client environent
using static System.Environment;// for getting current directory path
using System.Xml.Serialization; // for XmlSerializer
using System.Threading.Tasks;   // for Task
using System.Xml;               // for XmlReaderSettings
using System.Threading;

namespace PracticeEncryptHash
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("\nPracticeEncryptHash running ...");

            // get customer data
            Write("Enter your name: ");
            string name = ReadLine();
            Write("Enter credit card number to be encrypted: ");
            string cardNumber = ReadLine();           
            Write("Enter password to be salted and hashed: ");
            Console.WriteLine();
            string password = ReadLine();           

            // ensure credit card number is encrypted, password is salted and hashed
            
            Customer protectedCustomer = Protector.CreateCustomer(name, cardNumber, password);

            // write protected data to file
            WritetoXmlFile(protectedCustomer);

            Write("Enter name of customer in current directory to be read: ");
            string customerName = ReadLine();
            Write("Enter password: ");
            string customerPassword = ReadLine();

            //  
            if (Protector.CheckPassword(customerName,customerPassword))
            {
                customerXMLFileReader(customerName, customerPassword);
            }
            else
            {
                WriteLine("Wrong password.");
                return;
            }

            //TODO: put in delete file function!
        }

        private static void WritetoXmlFile(Customer protectedCustomer)
        {
            // in reality, would need to consider altering name to remove whitespace etc for file name...
            string xmlCustomerFilePath = Combine(CurrentDirectory, protectedCustomer.Name + ".xml");
            var xmlSerializer = new XmlSerializer(typeof(Customer));

            using (FileStream fileStream = File.Create(xmlCustomerFilePath))
            {
                xmlSerializer.Serialize(fileStream, protectedCustomer);
            }
        }   

        private static void customerXMLFileReader(string customerName, string password)
        {
            string filePath = Combine(CurrentDirectory, customerName + ".xml");

            var xmlCustomerSerializer = new XmlSerializer(typeof(Customer));

            using(FileStream xmlLoad = File.Open(filePath, FileMode.Open))
            {
                var loadedCustomer = (Customer)xmlCustomerSerializer.Deserialize(xmlLoad);
                
                Console.WriteLine($"In customerXMLFileReader function: {loadedCustomer.Name}, {loadedCustomer.Roles.ToString()}, {loadedCustomer.EncryptedCardNumber}");

                string cardNumber = Protector.Decrypt(loadedCustomer.EncryptedCardNumber,password);
                
                WriteLine($"{loadedCustomer.Name}, {cardNumber}, {loadedCustomer.Salt}");
            }  
        }
    }
}
