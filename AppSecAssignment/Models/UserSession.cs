using System.ComponentModel.DataAnnotations;

namespace AppSecAssignment.Models
{
    public class UserSession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string SessionId { get; set; }

        [Required]
        public string IpAddress { get; set; }

        [Required]
        public string UserAgent { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }

        public DateTime? LogoutTime { get; set; }

        public bool IsActive { get; set; }

        // Navigation property
        public virtual ApplicationUser User { get; set; }
    }
}
