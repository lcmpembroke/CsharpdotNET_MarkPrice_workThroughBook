using System;
using static System.Console;
using Packt.Shared;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Security.Claims;
using System.Collections.Generic;

namespace SecureApp
{
    class Program
    {
        static void SecureFeature()
        {
            if(Thread.CurrentPrincipal == null) // check user is authenticated
            {
                throw new SecurityException("A user must be logged in to access this feature");
            }

            if(!Thread.CurrentPrincipal.IsInRole("Admins"))  // check user is authorized
            {
              throw new SecurityException("User must be a member of Admins to access this feature")  ;
            }
            WriteLine("You have access to this secure feature.");
        }
        static void Main(string[] args)
        {
            // Register three users in various roles
            Protector.Register("Alice","Pa$$w0rd", new[] { "Admins"} );
            Protector.Register("Bob","Pa$$w0rd", new[] { "Sales", "TeamLeads"} );
            Protector.Register("Eve","Pa$$w0rd");

            Write("Enter your user name: ");
            string username = ReadLine();
            Write("Enter your user password: ");
            string password = ReadLine();

            Protector.Login(username, password);    //  login function uses a generic identity and principal to assign the authenticated user to the current thread
            if (Thread.CurrentPrincipal == null)
            {
                WriteLine("Failed login.");
                return; // ends function and therefore ends the program running
            }

            // consider the current principal (associates an authenticated user with their roles...)
            var currentPrincipal = Thread.CurrentPrincipal;
            WriteLine($"IsAuthenticated: {currentPrincipal.Identity.IsAuthenticated}");
            WriteLine($"Authentication type: {currentPrincipal.Identity.AuthenticationType}");
            WriteLine($"name: {currentPrincipal.Identity.Name}");
            WriteLine($"Is in role \"Admins\": {currentPrincipal.IsInRole("Admins")}");
            WriteLine($"Is in role \"Sales\": {currentPrincipal.IsInRole("Sales")}");
            WriteLine($"Is in role \"TeamLeads\": {currentPrincipal.IsInRole("TeamLeads")}");

            if (currentPrincipal is ClaimsPrincipal)
            {
                WriteLine($"{currentPrincipal.Identity.Name} has the following claims:");

                foreach (Claim claim in (currentPrincipal as ClaimsPrincipal).Claims)
                {
                    WriteLine($"{claim.Type}: {claim.Value}, {claim.Subject}");
                }
            }

            try
            {
                SecureFeature();
            }
            catch (System.Exception ex)
            {
                Write($"{ex.GetType()}: {ex.Message}");
            }
        }
    }
}
