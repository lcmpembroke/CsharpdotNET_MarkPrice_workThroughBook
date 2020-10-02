using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;

// these packages were added to this project - see ItemGroup in Instrumenting.csproj
// dotnet add package Microsoft.Extensions.Configuration
// dotnet add package Microsoft.Extensions.Configuration.Binder
// dotnet add package Microsoft.Extensions.Configuration.Json 
// dotnet add package Microsoft.Extensions.Configuration.FileExtensions
//
// then add file appsetting.json and add the "PacktSwitch" key

namespace Instrumenting
{
    class Program
    {
        static void Main(string[] args)
        {

            // $ dotnet run --configuration Release   Debug stmts are removed in production, only get the Trace written to log.txt  
            // $ dotnet run --configuration Debug     

            // BOOK: page 130
            Console.WriteLine("Instrumenting console app running.");

            // write to a text file in the current project folder
            Trace.Listeners.Add(new TextWriterTraceListener(File.CreateText("log.txt")));

            // text writer is buffered so set option to call Flush() on all listeners after writing
            Trace.AutoFlush = true;

            Debug.WriteLine("This text is from the Debug.WriteLine instruction");
            Trace.WriteLine("This text is from the Trace.WriteLine instruction");
            
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            IConfigurationRoot configuration = builder.Build();

            var ts = new TraceSwitch(
                displayName: "PacktSwitch",
                description: "This switch is set via a JSON config"
            );

            configuration.GetSection("PacktSwitch").Bind(ts);

            Trace.WriteLineIf(ts.TraceError, "Trace error...");
            Trace.WriteLineIf(ts.TraceWarning, "Trace Warning...");
            Trace.WriteLineIf(ts.TraceInfo, "Trace info...");
            Trace.WriteLineIf(ts.TraceVerbose, "Trace verbose...");
        }
    }
}
