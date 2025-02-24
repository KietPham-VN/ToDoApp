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
			// query : select * From Student 
			// Join School on Student.SchoolId = School.Id

			var students = _context.Student.Include(student => student.School)
				.AsQueryable();

			if (SchoolId.HasValue)
			{
				// query : select * From Student 
				// Join School on Student.SchoolId = School.Id
				// Where School.Id = SchoolId
				students = students.Where(student => student.School.Id == SchoolId);

			}
			// IQueryable là 1 interface kế thừa từ IEnumerable
			// đại diện cho 1 tập hợp các phần tử có thể truy vấn

			// SELECT Student.Id,
			// Student.FristName + Student.LastName AS FullName,
			// Student.Age,
			// School.Name AS SchoolName
			// FROM Student
			// JOIN School ON Student.SchoolId = School.Id
			// WHERE School.Id = SchoolId (nếu schoolid bị null)

			// tới khúc này khi được .toList() thì query mới được chạy nè
			// nếu không thì nó chỉ là 1 câu lệnh truy vấn chưa được chạy
			// khúc này chắc chắc bị hỏi
			return [.. students.Select(student => new StudentViewModel
			{
				Id = student.Id,
				FullName = student.FristName + " " + student.LastName,
				Age = student.Age,
				SchoolName = student.School.Name
			})];
		}
	}
}
