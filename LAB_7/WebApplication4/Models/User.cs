
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.Models
{
    public class User
    {
        // Pk
        public int UserId { get; set; }

        // Fk
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string? ProfilePicturePath { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        // Navigation Property
        // Many Users belong to one Role
        public Role Role { get; set; } = null!;


        // Navigation Properties for Courses
        public ICollection<Course> FacultyCourses { get; set; } = new List<Course>();

        public ICollection<Course> CreatedCourses { get; set; } = new List<Course>();

        public ICollection<Course> UpdatedCourses { get; set; } = new List<Course>();


        // Navigation Properties for Assignments
        public ICollection<Assignment> CreatedAssignments { get; set; } = new List<Assignment>();

        public ICollection<Assignment> UpdatedAssignments { get; set; } = new List<Assignment>();


        // Navigation Properties for Submissions
        public ICollection<Submission> StudentSubmissions { get; set; } = new List<Submission>();

        public ICollection<Submission> GradedSubmissions { get; set; } = new List<Submission>();
    }
}