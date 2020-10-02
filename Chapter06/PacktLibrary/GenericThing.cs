using System;

namespace Packt.Shared
{
    // GenericThing has a type parameter T which is 
    // any type that implements IComparable...ie must have CompareTo() method
    public class GenericThing<T> where T : IComparable
    {
        // GenericThing has a T field named Data
        public T Data = default(T);

        public string Process(T input)
        {
            if (Data.CompareTo(input) == 0)
            {
                 return "Generic: Data and input are the same";
            }
            else
            {
                return "Generic: Data and input are NOT the same";
            }
        }


    }
}