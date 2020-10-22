// C# program to illustrate the use of 'is' operator keyword 
//                      and the use of 'as' operator keyword 
using System; 

public class P { } 
public class P1 : P { } // class derived from P 
public class P2 { } 

class Y { } 
class Z { } 
  
// Driver Class 
public class GFG { 
  
    // Main Method 
    public static void Main() 
    { 
        Console.WriteLine("IsAndAs running....testing the 'is' keyword:");
        // creating an instance of class P 
        P objectP = new P(); 
  
        // creating an instance of class P1 
        P1 objectP1 = new P1(); 
  
        Console.WriteLine(objectP is P);        // True
        Console.WriteLine(objectP is Object);   // True
        Console.WriteLine(objectP is P1);       // False
        Console.WriteLine(objectP1 is Object);  // True
        Console.WriteLine(objectP1 is P1);      // True
        Console.WriteLine(objectP is P2);       // False
        Console.WriteLine(objectP1 is P2);      // False

//------------------------------------------------------------------------------------------------------
        Console.WriteLine("\n\nIsAndAs running....testing the 'as' keyword:");    

        // creating and initializing object array 
        object[] o = new object[5]; 
        o[0] = new Y(); 
        o[1] = new Z(); 
        o[2] = "Hello"; 
        o[3] = 4759.0; 
        o[4] = null; 
  
        for (int q = 0; q < o.Length; ++q) { 
  
            // using as operator 
            string str1 = o[q] as string; 
  
            Console.Write("{0}:", q); 
  
            // checking for the result 
            if (str1 != null) { 
                Console.WriteLine("'" + str1 + "'...is a string"); 
            } 
            else { 
                Console.WriteLine($"Is is not a string. The value is {o[q]}"); 
            } 
        }      
            // Output for as is as follows:------------------------------
            // IsAndAs running....testing the 'as' keyword:
            // 0:Is is not a string. The value is Y
            // 1:Is is not a string. The value is Z
            // 2:'Hello'...is a string
            // 3:Is is not a string. The value is 4759
            // 4:Is is not a string. The value is 
    } 
} 