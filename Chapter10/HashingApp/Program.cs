using System;
using static System.Console;
using Packt.Shared;

namespace HashingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Registering Alice@gmail.com with Pa$$w0rd");
            
            var alice = Protector.Register("Alice@gmail.com","Pa$$w0rd");

            WriteLine($"Name: {alice.Name}");
            WriteLine($"Salt: {alice.Salt}");
            WriteLine($"SaltedAndHashed password: {alice.SaltedHashedPassword}\n");

            WriteLine("Enter a new user to register: ");
            string username = ReadLine();
            WriteLine($"Enter password for {username}: ");
            string password = ReadLine();

            var newUser = Protector.Register(username, password);
            WriteLine($"New user: {newUser.Name}");
            WriteLine($"Salt: {newUser.Salt}");
            WriteLine($"SaltedAndHashed password: {newUser.SaltedHashedPassword}\n");


            bool correctPassword = false;
            while (!correctPassword)
            {
                WriteLine($"Enter a username to log in with: ");
                string loginUsername = ReadLine();
                WriteLine($"Enter the password for {loginUsername}: ");
                string loginPassword = ReadLine();
                correctPassword = Protector.CheckPassword(loginUsername, loginPassword);                
                if (correctPassword)
                {
                    WriteLine($"{loginUsername} has successfully logged in.");
                }
                else
                {
                    WriteLine($"{loginUsername} cannot log in with details provided. Try again. \n");
                }
            }
            
            WriteLine("End of HashingApp :-)");
            
        }
    }
}
