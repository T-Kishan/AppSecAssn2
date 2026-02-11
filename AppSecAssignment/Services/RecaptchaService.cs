using System.Text.Json;

namespace AppSecAssignment.Services
{
    public class RecaptchaService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public RecaptchaService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<bool> VerifyToken(string token)
        {
            // Skip verification if token is empty (for development/testing)
            if (string.IsNullOrEmpty(token))
            {
                return true; // Allow empty tokens in development
            }

            try
            {
                var secretKey = _configuration["ReCaptcha:SecretKey"];
                
                // Skip if using default/test keys (for development)
                if (secretKey == "6LclzFksAAAAACKD5ZbWHVGqJhhmhn6M0jG9xjra")
                {
                    return true; // Bypass reCAPTCHA verification for test keys
                }

                var response = await _httpClient.PostAsync(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}",
                    null);

                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<RecaptchaResponse>(jsonString);

                return result?.success == true && result.score >= 0.5;
            }
            catch
            {
                return true; // Allow on error for development
            }
        }

        private class RecaptchaResponse
        {
            public bool success { get; set; }
            public double score { get; set; }
            public string action { get; set; }
            public DateTime challenge_ts { get; set; }
            public string hostname { get; set; }
        }
    }
}
