using System;
using static System.Console;
using Packt.Shared;

namespace SigningApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter some text to sign: ");
            string inputText = ReadLine();
            var signature = Protector.GenerateSignature(inputText);
            WriteLine($"Signature: {signature}");
            WriteLine($"Public key used to check signature: \n{Protector.PublicKey}"); 

            if(Protector.ValidateSignature(inputText, signature))
            {
                WriteLine($"Signature is valid :-)"); 
            }
            else
            {
                WriteLine($"Signature is NOT valid :-("); 
            }

            // simulate a fake signature, replace first char with an X
            var fakeSignature = signature.Replace(signature[0], 'X');
            if(Protector.ValidateSignature(inputText, fakeSignature))
            {
                WriteLine($"Using fakeSignature...signature is valid :-)"); 
            }
            else
            {
                WriteLine($"Using fakeSignature...signature is NOT valid :-(...the fake signature is: \n{fakeSignature}"); 
            }



        }
    }
}
