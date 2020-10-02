
// https://docs.microsoft.com/en-us/dotnet/standard/events/how-to-raise-and-consume-events

//The next example shows how to declare a delegate for an event. The delegate is named ThresholdReachedEventHandler. This is just an illustration. 
//Typically, you do not have to declare a delegate for an event, because you can use either the EventHandler or the EventHandler<TEventArgs> delegate. 
//You should declare a delegate only in RARE scenarios, such as making your class available to legacy code that cannot use generics.
using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Counter c = new Counter(new Random().Next(10));
            Counter c = new Counter(7);
            c.ThresholdReached += c_ThresholdReached;

            Console.WriteLine("press 'a' key to increase total");
            while (Console.ReadKey(true).KeyChar == 'a')
            {
                Console.WriteLine("adding one");
                c.Add(1);
            }
        }

        static void c_ThresholdReached(Object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            Environment.Exit(0);
        }
    }

    class Counter
    {
        private int threshold;
        private int total;

        public Counter(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Threshold = threshold;
                args.TimeReached = DateTime.Now;
                OnThresholdReached(args);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReachedEventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event ThresholdReachedEventHandler ThresholdReached;
    }

    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }
    }

    // DECLARE delegate in rare circumstances for an event - because you can use EventHandler
    public delegate void ThresholdReachedEventHandler(Object sender, ThresholdReachedEventArgs e);
}





// using System;

// namespace ConsoleApplication1
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             //Counter c = new Counter(new Random().Next(10));
//             Counter c = new Counter(8);
//             c.ThresholdReached += c_ThresholdReached;

//             Console.WriteLine("press 'a' key to increase total");
//             while (Console.ReadKey(true).KeyChar == 'a')
//             {
//                 Console.WriteLine("adding one");
//                 c.Add(1);
//             }
//         }

//         static void c_ThresholdReached(object sender, EventArgs e)
//         {
//             Console.WriteLine("The threshold was reached.");
//             Environment.Exit(0); 
//         }
//     }

//     class Counter
//     {
//         private int threshold;
//         private int total;

//         public Counter(int passedThreshold)
//         {
//             threshold = passedThreshold;
//         }

//         public void Add(int x)
//         {
//             total += x;
//             if (total >= threshold)
//             {
//                 OnThresholdReached(EventArgs.Empty);
//             }
//         }

//         protected virtual void OnThresholdReached(EventArgs e)
//         {
//             EventHandler handler = ThresholdReached;
//             if (handler != null)
//             {
//                 handler(this, e);
//             }
//         }

//         public event EventHandler ThresholdReached;
//     }
// }
