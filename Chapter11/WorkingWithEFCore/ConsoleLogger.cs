using Microsoft.Extensions.Logging;
using System;
using static System.Console;

namespace Packt.Shared
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new ConsoleLogger();
        }

        // if logger uses unmanaged resources then release the memory here
        public void Dispose() { }
    }

    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // avoid overlogging by filtering on the log level
            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Information:
                case LogLevel.None:
                    return false;
                case LogLevel.Debug:
                case LogLevel.Warning:
                case LogLevel.Error:
                case LogLevel.Critical:
                default:
                    return true;
            };
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,  Exception exception, Func<TState, Exception, string> formatter)
        {

            // NOTE: event ID values and what they mean are specific to the .NET data provider (in this case Microsoft.EntityFrameworkCore.SQLite)
            // hwhich was added as a packae reference (and see the OnConfiguring() function in the Northwind class that inherits from DbContext class)
            if (eventId.Id == 20100)    // these events show how the LINQ query was translated into SQL statements
            {
                // log the level and the even identifier
                Write($"Level: {logLevel}, Event ID: {eventId.Id}");

                // only output the state or exception if it exists
                if (state != null)
                {
                    Write($", State: {state}");
                }
                if (exception  != null)
                {
                    Write($", Exception: {exception.Message}");
                }
                WriteLine();
                
            }
        }
    }

}