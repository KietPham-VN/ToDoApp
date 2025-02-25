using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Services;

namespace ToDoApp.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SchoolController(ISchoolService schoolService) : ControllerBase
    {
		[HttpGet]
		public IEnumerable<SchoolViewModel> GetSchools(int? SchoolId)
		{
			return schoolService.GetSchools(SchoolId);
		}

		[HttpPost]
		public bool CreateSchool(SchoolCreateModel school)
		{
			return schoolService.CreateSchool(school);
		}

		[HttpPut]
		public bool UpdateSchool(SchoolUpdateModel school)
		{
			return schoolService.UpdateSchool(school);
		}

		[HttpDelete]
		public bool DeleteSchool(int id)
		{
			return schoolService.DeleteSchool(id);
		}
	}
}
