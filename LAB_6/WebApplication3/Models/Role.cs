
namespace CourseHub.Models
{
    public class Role
    {
        // Pk
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;


        // Navigation Property
        // One Role can have multiple Users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}