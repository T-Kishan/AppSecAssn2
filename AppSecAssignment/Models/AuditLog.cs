using System.ComponentModel.DataAnnotations;

namespace AppSecAssignment.Models
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } // Stores the User's Email or ID
        public string Action { get; set; } // e.g., "Login", "Logout", "Failed Login"
        public DateTime Timestamp { get; set; }
    }
}