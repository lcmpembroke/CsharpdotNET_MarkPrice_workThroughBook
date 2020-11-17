using System;
using System.Linq;
using System.Collections.Generic;

namespace LinqWithObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nLinqWithObjects running...");
            //LinqWithArrayOfStrings();
            //LinqWithArrayOfExceptions();
            LinqSelectVersusSelectMany();
        }

        class Course
        {
            public int courseId;
            public string courseName;
            public List<string> students;
        }

        static List<Course> GetCourses() {
            List<Course> courses = new List<Course> {
                new Course {
                    courseId = 1,
                    courseName = "Course 1",
                    students =  new List<string> { "Student 1", "Student 2", "Student 3" }
                },
                new Course {
                    courseId = 2,
                    courseName = "Course 2",
                    students = new List<string> { "Student A", "Student B", "Student C" }
                },
                new Course {
                    courseId = 3,
                    courseName = "Course 3",
                    students = new List<string> { "Student X", "Student Y", "Student Z" }
                }
            };
            return courses;
        }        

        static void LinqSelectVersusSelectMany()
        {
            // When try to list all students from each course using Select, you can see that Select returns three lists of students, one for each course:
            var students = GetCourses().Select(c => c.students);
            foreach (var s in students)
            {
                Console.WriteLine(s.ToString());
            }
            // OUPUT: three lists of students, one for each course
                // System.Collections.Generic.List`1[System.String]
                // System.Collections.Generic.List`1[System.String]
                // System.Collections.Generic.List`1[System.String]            

            // SelectMany will flatten the above into a single list
            var students2 = GetCourses().SelectMany(c => c.students);
            foreach (var s in students2)
            {
                Console.WriteLine(s.ToString());
            }
            // OUPUT: all the students across all the courses
                // Student 1
                // Student 2
                // Student 3
                // Student A
                // Student B
                // Student C
                // Student X
                // Student Y
                // Student Z            



        }

        static void LinqWithArrayOfStrings()
        {
            var names = new string[] { "Michael","Pam","Jim","Dwight","Angela","Kevin","Toby","Creed" };

            //var query = names.Where(new Func<string, bool>(NameLongerThanFour)); **don't have to explicitly instantiate the delegate - see next line
            // var query = names.Where(NameLongerThanFour);  ** don't have to write a separate function, can use a lambda expression (nameless function)
            var query = names
                .Where(name => name.Length > 4)
                .OrderBy(name => name.Length)
                .ThenBy(name => name);  // will sort alphabetically by name after length

            foreach (string item in query)
            {
                Console.WriteLine(item);
            }
        }

        static bool NameLongerThanFour(string name)
        {
            return name.Length > 4;
        }

        static void LinqWithArrayOfExceptions()
        {
            var errors = new Exception[]
            {
                new ArgumentException(),
                new SystemException(),
                new IndexOutOfRangeException(),
                new InvalidOperationException(),
                new NullReferenceException(),
                new InvalidCastException(),
                new OverflowException(),
                new DivideByZeroException(),
                new ApplicationException()
            };

            var numberErrors = errors.OfType<ArithmeticException>();    // this will only select 'overflow' and 'divideByZero' exceptions
            
            foreach (var error in numberErrors)
            {
                Console.WriteLine(error);
            }
        }

    }
}
