using System;
using System.Threading;

namespace Packt.Shared
{   

    // Static classes are sealed - cannot be inherited and only inherit from System.Object
    // non-generic class TYPE
    public static class Squarer
    {
        // GENERIC method within non-generic class
        // note IConvertible means it has to implement a ToDouble() method
        public static double Square<T>(T input) where T : IConvertible
        {
            // ToDouble requires a param that implement IFormatProvider
            double d = input.ToDouble(Thread.CurrentThread.CurrentCulture);
            return d * d;
        }
    }
}

// NOTES:
// a static class cannot be instantiated
// useful where no internal instance fields are set, just used
// for utility classes that have methods that just operate on input params


// Creating a static class is therefore basically the same as creating a 
// class that contains only static members and a private constructor. 
// A private constructor prevents the class from being instantiated. 
// Compiler can check no instance members  accidentally added.
// 