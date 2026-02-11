using System.ComponentModel.DataAnnotations;

namespace AppSecAssignment.ViewModels
{
    public class TwoFactorViewModel
    {
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Code must be 6 digits")]
        public string Code { get; set; }

        public string Email { get; set; }
    }
}
