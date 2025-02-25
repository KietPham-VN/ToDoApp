using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Services;

namespace ToDoApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentController(IStudentService studentService) : ControllerBase
	{
		[HttpGet]
		public IEnumerable<StudentViewModel> GetStudents(int? SchoolId)
		{
			return studentService.GetStudents(SchoolId);
		}

		[HttpPost]
		public bool CreateStudent(StudentCreateModel student)
		{
			return studentService.CreateStudent(student);
		}

		[HttpPut]
		public bool UpdateStudent(StudentUpdateModel student)
		{
			return studentService.UpdateStudent(student);
		}

		[HttpDelete]
		public bool DeleteStudent(int id)
		{
			return studentService.DeleteStudent(id);
		}
	}
}
