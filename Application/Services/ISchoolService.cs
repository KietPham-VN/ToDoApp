using ToDoApp.Application.Dtos;
using ToDoApp.Domain.Entities;
using ToDoApp.infrastructure;
namespace ToDoApp.Application.Services
{
	public interface ISchoolService
	{
		IEnumerable<SchoolViewModel> GetSchools(int? SchoolId);
		bool CreateSchool(SchoolCreateModel schoolCreateModel);
		bool UpdateSchool(SchoolUpdateModel schoolUpdateModel);
		bool DeleteSchool(int id);
	}

	public class SchoolService(IApplicationDbContext context) : ISchoolService
	{


		public IEnumerable<SchoolViewModel> GetSchools(int? SchoolId)
		{
			var Schools = context.School.AsQueryable();
			if (SchoolId.HasValue)
			{
				Schools = Schools.Where(school => school.Id == SchoolId);
			}
			return [.. Schools.Select(school => new SchoolViewModel
			{
				Name = school.Name,
				Address = school.Address
			})];
		}

		public bool CreateSchool(SchoolCreateModel schoolCreateModel)
		{

			if (schoolCreateModel == null || string.IsNullOrWhiteSpace(schoolCreateModel.Name))
			{
				return false; 
			}

			var school = new School
			{
				Name = schoolCreateModel.Name,
				Address = schoolCreateModel.Address
			};
			context.School.Add(school);
			context.SaveChanges();
			return true; 

		}
	
		public bool UpdateSchool(SchoolUpdateModel schoolUpdateModel)
		{
			if (schoolUpdateModel == null || string.IsNullOrWhiteSpace(schoolUpdateModel.Name))
			{
				return false;
			}
			var school = context.School.FirstOrDefault(school => school.Id == schoolUpdateModel.Id);
			if (school == null)
			{
				return false;
			}
			school.Name = schoolUpdateModel.Name;
			school.Address = schoolUpdateModel.Address;
			context.SaveChanges();
			return true;
		}
	
		public bool DeleteSchool(int id)
		{
			var school = context.School.FirstOrDefault(school => school.Id == id);
			if (school == null)
			{
				return false;
			}
			context.School.Remove(school);
			context.SaveChanges();
			return true;
		}
	}
}
