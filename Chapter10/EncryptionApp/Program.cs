using System;
using static System.Console;
using Packt.Shared;
using System.Security.Cryptography;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a message you want to encrypt:");
            string message = ReadLine();
            Write("Enter a password:");
            string password = ReadLine();

            string cryptoText = Protector.Encrypt(message,password);
            WriteLine($"\nEncrypted text: {cryptoText}");
            Write("Enter the password:");
            string password2 = ReadLine();

            try
            {
                string clearText = Protector.Decrypt(cryptoText,password2);
                WriteLine($"\nDecrypted text: {clearText}");
            }
            catch (CryptographicException ex)
            {
                WriteLine($"Wrong password entered. {ex.Message}");
            }
            catch (System.Exception ex)
            {
                WriteLine($"Non crytographic exception: {ex.GetType()} has message {ex.Message}");
            }
        }
    }
}
