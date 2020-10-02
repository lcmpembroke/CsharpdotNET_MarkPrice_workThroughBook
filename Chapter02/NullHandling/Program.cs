#nullable enable

using System;

namespace NullHandling
{

    class Address
    {
        public string? Building;
        public string Street = string.Empty;
        public string City = string.Empty;
        public string Region = string.Empty;
    }


    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Null Handling");

            int thisCannotBeNull = 4;
            // thisCannotBeNull = null; cannot set an int to null
            Console.WriteLine(thisCannotBeNull);


            int? thisCouldBeNull = null;  // due to way it's declared with ?
            Console.WriteLine(thisCouldBeNull);
            Console.WriteLine(thisCouldBeNull.GetValueOrDefault());

            if (thisCouldBeNull.HasValue)
            {
                Console.WriteLine("thisCouldBeNull.HasValue is true - note that thisCouldBeNull is set to null currently");
            } else
            {
                Console.WriteLine("thisCouldBeNull.HasValue is false - note that thisCouldBeNull is set to null currently");
            }


            thisCouldBeNull = 7; 
            Console.WriteLine(thisCouldBeNull);
            Console.WriteLine(thisCouldBeNull.GetValueOrDefault());


            // BOOK: page 55 reference types can be configured to no longer allow nullable - can enable this at eith project or file level...
            // see element in NullHandling.csproj <Nullable>enable</Nullable>...see #nullable enable at top of this file too

            // BOOK: page 57
            var address = new Address();
            address.Building = null;
            address.Street = string.Empty;
            address.City = "London";
            address.Region = string.Empty;

            if (thisCouldBeNull != null)
            {
                int length = thisCouldBeNull.Value;
                Console.WriteLine("length having checked for null...", length);
            }

            string someName = null;
            int? y = someName?.Length;  // null is assigned to y if someName is null ... instead of getting NullReferenceException
            Console.WriteLine("value of y when someName is null..." + y);

            someName = "testing";
            Console.WriteLine("someName - " + someName);
            y = someName?.Length;  // null is assigned to y if someName is null instead of getting NullReferenceException
            Console.WriteLine("value of y when someName not..." + y);


            string aName = null;
            int defaultLengthToAssign = 0;

            // BOOK: page 58 Null coalescing operator ??   if aName.Length is null, then a default number is assigned    
            defaultLengthToAssign = aName?.Length ?? 99999;  

            Console.WriteLine("defaultLengthToAssign - " + defaultLengthToAssign);          
        }

    }
}
