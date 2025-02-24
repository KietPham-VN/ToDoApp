using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Dtos;
using ToDoApp.infrastructure;

namespace ToDoApp.Application.Services
{
	public interface IStudentService
	{
		IEnumerable<StudentViewModel> GetStudents(int? SchooId);
	}
	public class StudentService : IStudentService
	{
		private readonly IApplicationDbContext _context;

		public StudentService(IApplicationDbContext context)
		{
			_context = context;
		}
		public IEnumerable<StudentViewModel> GetStudents(int? SchoolId)
		{
			var students = _context.Student.Include(student => student.School);

			if (SchoolId.HasValue)
			{
				return students.Where(student => student.School.Id == SchoolId)
					.Select(student => new StudentViewModel
					{
						Id = student.Id,
						FullName = student.FristName + " " + student.LastName,
						Age = student.Age,
						SchoolName = student.School.Name
					}).ToList();
			}
			return students.Select(student => new StudentViewModel
			{
				Id = student.Id,
				FullName = student.FristName + " " + student.LastName,
				Age = student.Age,
				SchoolName = student.School.Name
			}).ToList();
		}


	}
}
