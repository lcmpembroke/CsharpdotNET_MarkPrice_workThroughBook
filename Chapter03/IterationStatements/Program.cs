using System;
using static System.Console;

namespace IterationStatements
{
  class Program
  {
    static void Main(string[] args)
    {
      // BOOK: page 88
      // Looping with the while statement

      int x = 0;

      while (x < 10)
      {
        WriteLine(x);
        x++;
      }

      // Looping with the do statement

      string password = string.Empty;
      int attempts = 0;

      do
      {
        attempts++;
        Write("Enter your password: ");
        password = ReadLine();
      }
      while ((password != "Pa$$w0rd") & (attempts < 10));

      if (attempts < 10)
      {
        WriteLine("Correct!");
      }
      else
      {
        WriteLine("You have used 10 attempts!");
      }

      // Looping with the for statement

      for (int y = 1; y <= 10; y++)
      {
        WriteLine(y);
      }

      // Looping with the foreach statement
      // note the type must have a method GetEnumerator that returns an object
      //      the returned object must have a property name Current, and a method name MoveNext
      //      the MoveNext method has to return true is more items left to enumerate through or false is no more items

      // There are interfaces named IEnumerable and IEnumerable<T> that formally define these rules but techincally the compiler doesn't
      // need these interfaces to be implemented - see page 89 book

      string[] names = { "Adam", "Barry", "Charlie" };

      foreach (string name in names)
      {
        WriteLine($"{name} has {name.Length} characters.");
      }
    }
  }
}