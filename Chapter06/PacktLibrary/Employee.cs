using System;
using static System.Console;

namespace Packt.Shared
{
    public class Employee : Person
    {
        public string EmployeeCode { get; set; }
        
        public DateTime HireDate { get; set; }

        // applying new keyword to function indicates that method of the base class is deliberately being replaced (not overriden...use keyword override for that)
        // Difference between new and override that override extends the method in derived class but new only hides the method of base class in derived class. 
        // https://tutorial.techaltum.com/difference-between-override-and-new-in-c-sharp.html

        // 'new' used as access modifier
        public new void WriteToConsole()
        {
            WriteLine(
                format:"Employee.WriteToConsole(): {0} was born on {1:dd/MM/yy} and hired on {2:dd/MM/yy}",
                arg0: Name,
                arg1: DateOfBirth,
                arg2: HireDate
            );
        }

        // 'override' used as access modifier
        public override string ToString()
        {
            return $"Employee.ToString(): {Name}'s code is {EmployeeCode}";
        }
    }
}