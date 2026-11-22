using System.ComponentModel.DataAnnotations.Schema;

namespace CourseHub.Models
{
    public class Submission
    {
        // Pk
        public int SubmissionId { get; set; }

        // Fk
        [ForeignKey(nameof(Assignment))]
        public int AssignmentId { get; set; }

        [ForeignKey(nameof(User))]
        public int StudentId { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public int? MarksObtained { get; set; }

        public string? Remarks { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime SubmittedAt { get; set; }

        public DateTime? GradedAt { get; set; }

        // Nullable Foreign Key
        [ForeignKey(nameof(GradedByUser))]
        public int? GradedBy { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }


        // Navigation Properties
        public Assignment Assignment { get; set; } = null!;

        public User Student { get; set; } = null!;

        public User? GradedByUser { get; set; }
    }
}