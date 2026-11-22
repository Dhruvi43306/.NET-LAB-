using System.Collections.Generic;
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Data
{
    public static class StudentData
    {
        public static List<Student> Students { get; } = new List<Student>
        {
            // Student 1
            new Student
            {
                StudentId = 101,
                StudentName = "Amit Patel",

                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 1, SubjectName = "Mathematics" },
                    new Subject { SubjectId = 2, SubjectName = "Programming" },
                    new Subject { SubjectId = 3, SubjectName = "Database" }
                }
            },

            // Student 2
            new Student
            {
                StudentId = 102,
                StudentName = "Priya Shah",

                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 1, SubjectName = "Mathematics" },
                    new Subject { SubjectId = 4, SubjectName = "English" },
                    new Subject { SubjectId = 5, SubjectName = "Networking" }
                }
            },

            // Student 3
            new Student
            {
                StudentId = 103,
                StudentName = "Rahul Patel",

                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 2, SubjectName = "Programming" },
                    new Subject { SubjectId = 3, SubjectName = "Database" },
                    new Subject { SubjectId = 6, SubjectName = "Operating System" }
                }
            },

            // Student 4
            new Student
            {
                StudentId = 104,
                StudentName = "Neha Joshi",

                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 1, SubjectName = "Mathematics" },
                    new Subject { SubjectId = 5, SubjectName = "Networking" },
                    new Subject { SubjectId = 6, SubjectName = "Operating System" }
                }
            },

            // Student 5
            new Student
            {
                StudentId = 105,
                StudentName = "Jay Mehta",

                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 2, SubjectName = "Programming" },
                    new Subject { SubjectId = 4, SubjectName = "English" },
                    new Subject { SubjectId = 3, SubjectName = "Database" }
                }
            }
        };
    }
}