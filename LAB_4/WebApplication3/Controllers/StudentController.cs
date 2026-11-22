using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Data;
using StudentManagementAPI.Models;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Student>> GetAllStudent()
        {
            return Ok(StudentData.Students);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Student> GetStudenById(int id)
        {
            Student student = StudentData.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound("Student not found!");
            }
            return Ok(student);
        }
        [HttpGet("{id:int}/subjects")]
        public ActionResult<List<Subject>> GetStudentSubjects(int id)
        {
            Student student = StudentData.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound("Student not found!");
            }
            return Ok(student.Subjects);
        }
    }
}
