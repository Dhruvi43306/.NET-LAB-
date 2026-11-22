using CourseHub.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Data
{
    public class CourseHubDbContext : DbContext
    {
        public CourseHubDbContext(DbContextOptions<CourseHubDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Primery Key Configuration
            modelBuilder.Entity<Role>()
                .HasKey(r => r.RoleId);
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseId);
            modelBuilder.Entity<Assignment>()
                .HasKey(a => a.AssignmentId);
            modelBuilder.Entity<Submission>()
                .HasKey(s => s.SubmissionId);


            // Foreign Key Configuration

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Faculty)
                .WithMany(u => u.FacultyCourses)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedCourses)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany(u => u.UpdatedCourses)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Assignment>()
                .HasOne(c => c.Course)
                .WithMany(a => a.Assignments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Assignment>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedAssignments)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Assignment>()
              .HasOne(c => c.UpdatedByUser)
              .WithMany(u => u.UpdatedAssignments)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student)
                .WithMany(u => u.StudentSubmissions)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.GradedByUser)
                .WithMany(u => u.GradedSubmissions)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
