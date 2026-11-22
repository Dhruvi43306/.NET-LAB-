using System.Collections.Generic;

namespace StudentManagementAPI.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; } = "";

        public List<Subject> Subjects { get; set; } = new List<Subject>();
    }
}