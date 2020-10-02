using System;
using System.Linq;
using System.Reflection;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Data.DataSet dataSet;
            System.Net.Http.HttpClient client;

            Console.WriteLine("Learning some C# basics!");
            Console.WriteLine("Temp on {0:D} is {1} degreeC ", DateTime.Today, 23.4);
            Console.WriteLine();


            // loop through the assemblies this application references
            foreach (var currentAssembly in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                // load assembly to read its details
                var assembly = Assembly.Load(new AssemblyName(currentAssembly.FullName));

                int methodCount = 0;

                foreach (var currentType in assembly.DefinedTypes)
                {
                    methodCount += currentType.GetMethods().Count();
                }

                // output the count of types and their methods
                Console.WriteLine(
                    "{0:N0} types with {1:N0} methods in {2} assembly. ",
                    arg0: assembly.DefinedTypes.Count(),
                    arg1: methodCount,
                    arg2: currentAssembly.Name                                    
                );
                
            }

        }
    }
}
