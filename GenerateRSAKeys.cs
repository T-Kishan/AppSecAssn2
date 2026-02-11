using System;
using System.Security.Cryptography;

class GenerateRSAKeys
{
    static void Main()
    {
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            string privateKey = rsa.ToXmlString(true);
            string publicKey = rsa.ToXmlString(false);
            
            Console.WriteLine("PRIVATE_KEY:");
            Console.WriteLine(privateKey);
            Console.WriteLine();
            Console.WriteLine("PUBLIC_KEY:");
            Console.WriteLine(publicKey);
        }
    }
}
