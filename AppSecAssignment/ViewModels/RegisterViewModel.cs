using System.ComponentModel.DataAnnotations;

namespace AppSecAssignment.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCardNo { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNo { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Logic: Min 12 chars, lower, upper, num, special char
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{12,}$",
            ErrorMessage = "Password must be at least 12 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; } // Handles the .JPG file upload

        [Required]
        public string AboutMe { get; set; }
    }
}