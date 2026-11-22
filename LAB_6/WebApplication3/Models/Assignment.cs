using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.Models
{
    public class Assignment
    {
        // Pk
        public int AssignmentId { get; set; }

        [ForeignKey(nameof(Course))]
        // Fk
        public int CourseId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public int MaxMarks { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(CreatedByUser))]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(UpdatedByUser))]
        public DateTime? UpdatedAt { get; set; }

        // Fk
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }


        // Navigation Properties
        public Course Course { get; set; } = null!;

        public User CreatedByUser { get; set; } = null!;

        public User? UpdatedByUser { get; set; }


        // One Assignment can have multiple Submissions
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
}