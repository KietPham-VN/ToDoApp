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
	}
}
