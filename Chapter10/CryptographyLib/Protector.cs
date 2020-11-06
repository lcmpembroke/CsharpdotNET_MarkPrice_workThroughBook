using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;

namespace Packt.Shared
{
    public static class Protector
    {
        private static Dictionary<string, User> Users = new Dictionary<string, User>();

        // salt size has to be >= 8bytes. Here, use 16 bytes
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");

        //private static readonly string password = "BETHLEHEM01";

        // iterations must be >=1000. Here use 2000.
        private static readonly int iterations = 2000;        

        public static string PublicKey;

        public static string ToXmlStringExtension(this RSA rsa, bool includePrivateParameters)
        {
            // BOOK PAGE 342 - function relates to signing data and checking the signature
            var p = rsa.ExportParameters(includePrivateParameters);
            XElement xml;
            if (includePrivateParameters)
            {
                xml = new XElement("RSAKeyValue",
                    new XElement("Modulus", Convert.ToBase64String(p.Modulus)),
                    new XElement("Exponent", Convert.ToBase64String(p.Exponent)),
                    new XElement("P", Convert.ToBase64String(p.P)),
                    new XElement("Q", Convert.ToBase64String(p.Q)),
                    new XElement("DP", Convert.ToBase64String(p.DP)),
                    new XElement("DQ", Convert.ToBase64String(p.DQ)),
                    new XElement("InverseQ", Convert.ToBase64String(p.InverseQ))
                );
            }
            else
            {
                xml = new XElement("RSAKeyValue",
                    new XElement("Modulus", Convert.ToBase64String(p.Modulus)),
                    new XElement("Exponent", Convert.ToBase64String(p.Exponent))
                );

            }

            // See https://csharp.today/c-6-features-null-conditional-and-and-null-coalescing-operators/
            return xml?.ToString();     // uses null-conditional operator
        }

        public static void FromXmlStringExtension(this RSA rsa, string parameterAsXml)
        {
            var xml = XDocument.Parse(parameterAsXml);
            var root = xml.Element("RSAKeyValue");
            var rsaParams = new RSAParameters 
            {
                Modulus = Convert.FromBase64String(root.Element("Modulus").Value),
                Exponent = Convert.FromBase64String(root.Element("Exponent").Value),
            };

            if (root.Element("P") != null)
            {
                rsaParams.P = Convert.FromBase64String(root.Element("P").Value);
                rsaParams.Q = Convert.FromBase64String(root.Element("Q").Value);
                rsaParams.DP = Convert.FromBase64String(root.Element("DP").Value);
                rsaParams.DQ = Convert.FromBase64String(root.Element("DQ").Value);
                rsaParams.InverseQ = Convert.FromBase64String(root.Element("InverseQ").Value);
            }
            rsa.ImportParameters(rsaParams);
        }

        public static string GenerateSignature(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            var sha = SHA256.Create();
            var hashedData = sha.ComputeHash(dataBytes);
            var rsa = RSA.Create();
            PublicKey = rsa.ToXmlStringExtension(false);    // set the Public Key value (exclude the private key) - this must match what is used in ValidateSignature()
            return Convert.ToBase64String(rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }

        public static bool ValidateSignature(string data, string signature)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            var sha = SHA256.Create();
            var hashedData = sha.ComputeHash(dataBytes);
            byte[] signatureBytes = Convert.FromBase64String(signature)            ;
            var rsa = RSA.Create();
            rsa.FromXmlStringExtension(PublicKey);

            return rsa.VerifyHash(hashedData,signatureBytes,HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public static User Register(string username, string password, string[] roles = null)
        {
            // generate a random salt (string saltText)
            var rng = RandomNumberGenerator.Create();
            var randomSaltBytes = new byte[16];
            rng.GetBytes(randomSaltBytes);  // takes a struct so argument must be by reference
            var saltText = Convert.ToBase64String(randomSaltBytes);

            // generate the salted and hashed password
            var saltedHashedPassword = SaltAndHashPassword(password,saltText);

            var newUser = new User { Name = username, Salt = saltText, SaltedHashedPassword = saltedHashedPassword, Roles = roles};

            Users.Add(newUser.Name, newUser);   // note this is not persisting data anywhere, just saving in current running program

            return newUser;
        }

        public static Customer CreateCustomer(string username, string password, string cardNumber)
        {
            // generate a random salt (string saltText)
            var rng = RandomNumberGenerator.Create();
            var randomSaltBytes = new byte[16];
            rng.GetBytes(randomSaltBytes);  // takes a struct so argument must be by reference
            var saltText = Convert.ToBase64String(randomSaltBytes);

            // generate the salted and hashed password
            var saltedHashedPassword = SaltAndHashPassword(password,saltText);

            string encryptedCardNumber = Encrypt(cardNumber, password); 

            var newCustomer = new Customer { Name = username, Salt = saltText, SaltedHashedPassword = saltedHashedPassword, EncryptedCardNumber = encryptedCardNumber};

            Users.Add(newCustomer.Name, newCustomer);   // note this is not persisting data anywhere, just saving in current running program

            return newCustomer;
        }        

        public static void Login(string username,  string password)
        {
            if (CheckPassword(username, password))
            {
                // use a generic identity and principal to assign the authenticated user to the current thread
                var identity = new GenericIdentity(username,"PackAuth");              // identity is equivalent to a user
                var principal = new GenericPrincipal(identity, Users[username].Roles);  
                System.Threading.Thread.CurrentPrincipal = principal;
            }
        }

        private static string SaltAndHashPassword(string password, string salt)
        {
            var sha = SHA256.Create();
            Console.WriteLine(sha.ToString());
            var saltedPassword = password + salt;   // attach the salt onto the end of the password before hashing
            Console.WriteLine(saltedPassword);
            string saltedHashedPassword = Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
            return saltedHashedPassword;
        }

        public static bool CheckPassword(string username, string password)
        {
            // check the user is registered
            if (Users.ContainsKey(username))
            {
                // find the user
                var user = Users[username];
                
                Console.WriteLine();
                Console.WriteLine("In Check Password()....");
                Console.WriteLine("USER FOUND:" + user.Name);
                Console.WriteLine("USER salt: " + user.Salt);
                Console.WriteLine("USER SaltedHashedPassword  : " + user.SaltedHashedPassword);
                Console.WriteLine("password: " + password);
                
                // generate the salted and hashed password to check, having got the salt from the registered user
                string saltedHashedPasswordToCheck = SaltAndHashPassword(password,user.Salt);
                Console.WriteLine("saltedHashedPasswordToCheck: " + saltedHashedPasswordToCheck);
                bool x = string.Equals(saltedHashedPasswordToCheck,user.SaltedHashedPassword);


                Console.WriteLine("*************x: " + x);
                Console.WriteLine();
                // compare the stored salted and hashed passwords with the one just regenerated
                return string.Equals(saltedHashedPasswordToCheck,user.SaltedHashedPassword);                
            }
            else
            {
                return false;
            }
        }       

        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password,salt,iterations);
            aes.Key = pbkdf2.GetBytes(32);  // set a 256-bit key
            aes.IV = pbkdf2.GetBytes(16);   // set a 128-bit Initialisation Vector

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cryptoText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password,salt,iterations);
            aes.Key = pbkdf2.GetBytes(32);  // 256-bit key
            aes.IV = pbkdf2.GetBytes(16);   // 128-bit Initialisation Vector

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }
            return Encoding.Unicode.GetString(plainBytes);
        }        

        public static byte[] GetRandomKeyOrIV(int size)
        {
            var r = RandomNumberGenerator.Create();
            var data = new byte[size];
            r.GetNonZeroBytes(data);    // Fills an array of bytes with a cryptographically strong sequence of random nonzero values.
            return data;                // so data is now an array filled with cryptographically strong random bytes
        }
    }
}
