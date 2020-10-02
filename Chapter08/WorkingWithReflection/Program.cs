using System;
using System.Reflection;
using static System.Console;
using System.Linq; // to use OrderByDescending
using System.Runtime.CompilerServices;  // to use CompilerGeneratedAttribute

namespace WorkingWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            // BOOK: p281 Reading Assembly MetaData (info about code) - Reflection = programming feature to let code understand itself - like looking in a mirror!
            WriteLine("Assembly metadata:");
            Assembly assembly = Assembly.GetEntryAssembly();
            WriteLine($"   Full name: {assembly.FullName}");
            WriteLine($"   Location: {assembly.Location}");

            var attributes = assembly.GetCustomAttributes();
            WriteLine("Assembly Attributes");
            foreach (Attribute a in attributes)
            {
                WriteLine($"   {a.GetType()}");
            }
    
            // asking for specific attributes that we know decorate the assembly (from output above)
            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();

            WriteLine();
            WriteLine($"   Version: {version.InformationalVersion}");
            WriteLine($"   Company: {company.Company}");
            // note that old way to set these wasin C# source code, but in .NET Core, using Roslyn compiler, the attributes are set
            // automatically, but can be modified in the project file i.e. the .csproj file

            WriteLine();
            WriteLine($"*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
            WriteLine($"Types:");
            Type[] types = assembly.GetTypes();

            // loop through the types in this assembly
            foreach (Type type in types)
            {

                var compilerGenerated = type.GetCustomAttribute<CompilerGeneratedAttribute>();
                WriteLine(value: $"Custom Attributes in string: {type.GetCustomAttributes().ToString()}");
                if (compilerGenerated != null) break;

                WriteLine();
                WriteLine(value: $"Type: {type.FullName}, {type.Attributes.ToString()}, {type.GetCustomAttributes().ToString()}");
                
                // For each types in the assembly, list the all the public members it has
                MemberInfo[] members = type.GetMembers();
                foreach (MemberInfo member in members)
                {
                    WriteLine("{0}: {1} {2}",
                        arg0: member.MemberType,
                        arg1: member.Name,
                        arg2: member.DeclaringType.Name
                    );

                    // for each member, list the coders that are part of custom attributes for that member
                    var coders = member.GetCustomAttributes<CoderAttribute>().OrderByDescending(c => c.LastModified);
                    foreach (CoderAttribute coder in coders)
                    {
                        WriteLine("-> Modified by: {0} on  {1}",
                            arg0: coder.Coder,
                            arg1: coder.LastModified.ToShortDateString()
                        );                        
                        
                    }

                }
                
            }
        }

        // BOOK: page 283 Creating custom attributes - se new class called CoderAttribute created
        [Coder("Mark Price", "22 August 2019")]
        [Coder("John Smith", "13 September 2019")]
        public static void DoStuff()
        {

        }
    }
}
