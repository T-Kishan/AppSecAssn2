using System.Security.Cryptography;
using System.Text;

namespace AppSecAssignment.Services
{
    public class DigitalSignature
    {
        private static RSACryptoServiceProvider rsa;
        private static string _privateKey;
        private static string _publicKey;

        // FIXED: Hardcoded RSA keys that persist across application restarts
        // WARNING: In production, these should be stored in Azure Key Vault or secure configuration
        // For academic purposes, hardcoding is acceptable to demonstrate RSA signature functionality
        private const string PRIVATE_KEY = @"<RSAKeyValue><Modulus>6232pwYWrdUnqDMX949Lfj9ivn4/eTGbi7zry4Zi5GNit6TiWhicZsaC4lJk40ETCo2yPa/kntukTPv3lWYO0khiXCqW/jZA7b5xkeewGh8nDV8L3vQnt+ieHIWzgJzl6LZe37FM7lyfyhD92bg3RgkPSZsjKB8HIj6/ZxKllPoa7n5Ul0qQVJKNpnxEoJNpRUIwdgGHiV3XChlP4LX7T45GE18ulDToyrCBiuLcyk074xecA0T1CQ5oHnZq3BBHPMsHwbk8GL8gmVSJR5Pb7qSTvbInbzW4pkcSXXhYEmApqdqb3oxKAV4AxooEbw6c+oniBNVE/nhobNygVcPljQ==</Modulus><Exponent>AQAB</Exponent><P>7pO6Vk0Uts1S7UikMq6JW2ORz+nWLibqxpAu/nqE5iVAB8Yd4C7kIurzwbMZIfrcBAaRCRcYFl9BGkDRiyW7GrkHQYUWo8P8OBoMZVEOHlSdT67cQagJHn5KpMXwpWGWhrDyRZ+Ywt7ePvcnZH7wtfvsZI/jQqEw/Pt8R7EqMp8=</P><Q>/J9kTN0B/ROjCwfwGJNFpgkgcFxgLeRsG8s06Slhmnc6uutrN5BIUbpYTqvRL8EBfAaVMiOGWvcn4sXTLq2jfs0m0LaSqeo+vVtduFGSM9tbzSFCbsDejljviyk09z5MbhtycDlSZerWKoOAbpRX/xABSKwS4gMewB6DOCLVBFM=</Q><DP>D9BK8Z16WgOSE5hVCeb3w879w0JEqBp8RV0ZQFTuqjKd4+YF5O42wPnB3nz4lYADRWEKDAgz2IfH0O2q9gYyOLs69+TSc1hyR2WynKSawvfo1XdJsjBpKlRErldQdCXbzGG+LU8+2Ovc31+ShN7FJcrlCGVhMvFsRdiFjFr56RE=</DP><DQ>RKuzYS2Gt9vZq0r6GkQWX78FYWXLA06fkKKZfHQyQwH8JgmOuKjw775++Mu97oRBugw0DFAZ2qpq3js2X+71jaeI0J2Ams3BRkoK3OrXml8bwlQzMdYd6YYFf8ewzTDTJ+8wGY+LdYbzttTOWcDbdstL/gxAic2dU044D09wpl8=</DQ><InverseQ>EMdEkuxmibs8hKhEGx5v/5D7VxrSvSZIZU/oAc9wxcuFEWQUddN3ZuPZLZUGYyEZVXbt+tmCEJikwk9ZQn6JDykFQsQqng5fL5PSASkw2SmkCXdNYHrjey9i6Z4JFyLlbvMOvlsIVfRYnDuiJAhc8xGpPfM6m5tyLDKs0nQZu08=</InverseQ><D>Ju4PDD1ogKQvo5OXUfti+2RV2rQboNnAI9JAl24IakW47C8astVA/4mx2OTRA/cdw+/8WlD8l/wv5f8ASLcQmlsX0K/GOhEUmXuVYhASwa8HESH3X422B6Yyhvrg3NKU/e8yLwEtfHjaf1ph75+TnEQhQAWkQZ0n29TOZ6fgs4p6shw3xZv+xytMUmFCjhYiKeBbUjvhXDygN0eODggs/PGsFwCE1ANK2WgLgr7dl6yIPe9lFqjaPC84Tz8AbDSdSgYYX2vA++IRFCZOWH9g9qN5zf1FgEhlkX5K7v1FO8BSfpBhUer8CC1F9GlZyhgmqwEvhUvIGMCC2/fuySaRzQ==</D></RSAKeyValue>";
        private const string PUBLIC_KEY = @"<RSAKeyValue><Modulus>6232pwYWrdUnqDMX949Lfj9ivn4/eTGbi7zry4Zi5GNit6TiWhicZsaC4lJk40ETCo2yPa/kntukTPv3lWYO0khiXCqW/jZA7b5xkeewGh8nDV8L3vQnt+ieHIWzgJzl6LZe37FM7lyfyhD92bg3RgkPSZsjKB8HIj6/ZxKllPoa7n5Ul0qQVJKNpnxEoJNpRUIwdgGHiV3XChlP4LX7T45GE18ulDToyrCBiuLcyk074xecA0T1CQ5oHnZq3BBHPMsHwbk8GL8gmVSJR5Pb7qSTvbInbzW4pkcSXXhYEmApqdqb3oxKAV4AxooEbw6c+oniBNVE/nhobNygVcPljQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

        static DigitalSignature()
        {
            rsa = new RSACryptoServiceProvider(2048);
            // Load the persistent keys instead of generating new ones
            rsa.FromXmlString(PRIVATE_KEY);
            _privateKey = PRIVATE_KEY;
            _publicKey = PUBLIC_KEY;
        }

        // 1. Create the Signature (Sign Data)
        public static string SignData(string dataToSign)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(dataToSign);

            // Hash the data first, then sign the hash
            byte[] signature = rsa.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signature);
        }

        // 2. Verify the Signature (Check if data was tampered)
        public static bool VerifyData(string originalData, string signatureFromDb)
        {
            byte[] bytesToVerify = Encoding.UTF8.GetBytes(originalData);
            byte[] signatureBytes = Convert.FromBase64String(signatureFromDb);

            return rsa.VerifyData(bytesToVerify, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
    }
}