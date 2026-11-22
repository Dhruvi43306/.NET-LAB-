using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.Models
{
    public class Course
    {
        // Primary Key
        public int CourseId { get; set; }

        public string CourseCode { get; set; } = string.Empty;

        public string CourseTitle { get; set; } = string.Empty;

        // Foreign Key - Faculty
        [ForeignKey(nameof(Faculty))]
        public int FacultyId { get; set; }

        public int Credits { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        // Foreign Keys - User
        [ForeignKey(nameof(CreatedByUser))]
        public int? CreatedBy { get; set; }

        [ForeignKey(nameof(UpdatedByUser))]
        public int? UpdatedBy { get; set; }


        // Navigation Property
        public User Faculty { get; set; } = null!;

        public User? CreatedByUser { get; set; }

        public User? UpdatedByUser { get; set; }


        // One Course can have multiple Assignments
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}