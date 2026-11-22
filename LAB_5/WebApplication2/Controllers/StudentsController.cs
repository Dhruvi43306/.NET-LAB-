using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private static readonly List<Student> students = new List<Student>
        {
            new Student
            {
                Id = 101,
                Name = "Amit",
                Age = 20,
                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 1, SubjectName = "C#" },
                    new Subject { SubjectId = 2, SubjectName = "Database" }
                }
            },

            new Student
            {
                Id = 102,
                Name = "Priya",
                Age = 21,
                Subjects = new List<Subject>
                {
                    new Subject { SubjectId = 1, SubjectName = "Java" },
                    new Subject { SubjectId = 2, SubjectName = "Python" }
                }
            },

            new Student
            {
                Id = 103,
                Name = "Rahul",
                Age = 22
            },

            new Student
            {
                Id = 104,
                Name = "Neha",
                Age = 20
            },

            new Student
            {
                Id = 105,
                Name = "Jay",
                Age = 21
            }
        };


        [HttpGet]
        public IActionResult GetAllStudent()
        {
            return Ok(students);
        }

        [HttpGet("{id}")]

        public IActionResult GetStudentById(int id)
        {
            Student student = students.FirstOrDefault(s => s.Id == id);
            if(student == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(student);
        }

        [HttpGet("subject/{id}")]

        public IActionResult GetStudentBySubject(int id)
        {
            Student student = students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if(student.Id <= 0 || string.IsNullOrWhiteSpace(student.Name) || student.Age <= 0)
            {
                return BadRequest("Please enter Valid Student details.");
            }

            var exits = students.Any(s => s.Id == student.Id);
            if (exits)
            {
                return Conflict("Student Alredy exits");
            }
             students.Add(student);

            return StatusCode(201,student);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateStudent(int id, Student student)
        {
            if (student.Id <= 0 || string.IsNullOrWhiteSpace(student.Name) || student.Age <= 0)
            {
                return BadRequest("Please enter valid student details.");
            }

            var student1 = students.FirstOrDefault(s => s.Id == id);
            if(student1 == null)
            {
                return NotFound("Student not found!");
            }
            student1.Name = student.Name;
            student1.Age = student.Age;
            student1.Subjects = student.Subjects;

            return NoContent();
        }


        public IActionResult DeleteStudent(int id)
        {
            var s = students.FirstOrDefault(s => s.Id == id);
            if(s == null)
            {
                return NotFound("student is Not Found");
            }
            students.Remove(s);

            return StatusCode(204, s);
        }
    }
}
