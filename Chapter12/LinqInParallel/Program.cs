using System;
using System.Diagnostics;   // for stopwatch
using System.Collections.Generic;
using System.Linq;

namespace LinqInParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var watch = Stopwatch.StartNew();
            Console.WriteLine("Press ENTER to start...");
            Console.ReadLine();
            watch.Start();

            IEnumerable<int> numbers = Enumerable.Range(1, 200_000_000);
            var squares = numbers.AsParallel().Select(number => number * number).ToArray();
            watch.Stop();
            Console.WriteLine("{0:#,##0} milliseconds elapsed",watch.ElapsedMilliseconds);

        }
    }
}
