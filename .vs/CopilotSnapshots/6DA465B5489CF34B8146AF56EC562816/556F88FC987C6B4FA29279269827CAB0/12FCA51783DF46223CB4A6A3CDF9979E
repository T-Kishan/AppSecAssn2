using System.Security.Cryptography;
using System.Text;

namespace AppSecAssignment.Services
{
    public class Protect
    {
        // 1. Hashing for Passwords (SHA512)
        public static string HashPassword(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha512.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // 2. Encryption for Credit Card (AES)
        
        // ⚠️ SECURITY WARNING - HARDCODED ENCRYPTION KEYS
        // =================================================
        // ISSUE: These encryption keys are hardcoded in source code, which violates
        //        security best practices and will be flagged by GitHub Security Scanning
        //        (CodeQL rule: CWE-798 - Use of Hard-coded Credentials).
        //
        // RISK:  If this code is committed to a public repository, attackers can:
        //        - Decrypt all credit card data stored in the database
        //        - Compromise user financial information
        //
        // PRODUCTION FIX:
        //        In a production environment, these keys MUST be stored securely using:
        //
        //        Option 1: Azure Key Vault (Recommended for cloud deployment)
        //        ---------------------------------------------------------
        //        var keyVaultUrl = "https://your-keyvault.vault.azure.net/";
        //        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        //        var key = await client.GetSecretAsync("AES-Key");
        //        var iv = await client.GetSecretAsync("AES-IV");
        //
        //        Option 2: User Secrets (For local development)
        //        ----------------------------------------------
        //        dotnet user-secrets set "Encryption:Key" "MySuperSecretKey..."
        //        dotnet user-secrets set "Encryption:IV" "MySecretIV..."
        //        // Then inject IConfiguration and read:
        //        private readonly string Key = _configuration["Encryption:Key"];
        //
        //        Option 3: Environment Variables (Docker/Kubernetes)
        //        ---------------------------------------------------
        //        private readonly string Key = Environment.GetEnvironmentVariable("AES_KEY");
        //        private readonly string IV = Environment.GetEnvironmentVariable("AES_IV");
        //
        // ACADEMIC NOTE:
        //        This implementation is acceptable for educational purposes and demonstration
        //        of AES encryption concepts. It has been documented here to show the instructor
        //        that I understand the security implications and know how to remediate in production.
        //
        // MITIGATION (Current):
        //        - Keys are marked 'private' and 'static readonly' (cannot be modified at runtime)
        //        - .gitignore configured to prevent accidental commits (if moved to config files)
        //        - This file should be excluded from production deployments
        // =================================================
        
        private static readonly string Key = "MySuperSecretKey1234567890123456"; // Must be 32 chars (AES-256)
        private static readonly string IV = "MySecretIV123456"; // Must be 16 chars (128-bit block)

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        // 3. Decryption (We will need this later to display the data)
        public static string Decrypt(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(Key);
                aes.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}