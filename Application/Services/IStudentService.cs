using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Dtos;
using ToDoApp.Domain.Entities;
using ToDoApp.infrastructure;

namespace ToDoApp.Application.Services
{
	public interface IStudentService
	{
		IEnumerable<StudentViewModel> GetStudents(int? SchoolId);
		bool CreateStudent(StudentCreateModel student);
		bool UpdateStudent(StudentUpdateModel student);
		bool DeleteStudent(int id);
	}
	public class StudentService(IApplicationDbContext context) : IStudentService
	{
		public IEnumerable<StudentViewModel> GetStudents(int? SchoolId)
		{
			// query : select * From Student 
			// Join School on Student.SchoolId = School.Id

			var students = context.Student.Include(student => student.School)
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
				FullName = student.FirstName + " " + student.LastName,
				Age = student.Age,
				SchoolName = student.School!.Name!
			})];
		}
		public bool CreateStudent(StudentCreateModel student)
		{
			var data = new Student
			{
				FirstName = student.FirstName,
				LastName = student.LastName,
				DateOfBirth = student.DateOfBirth,
				Address = student.Address,
				SchoolId = student.SchoolId
			};
			context.Student.Add(data);
			context.SaveChanges();
			return true;
		}
		public bool UpdateStudent(StudentUpdateModel student)
		{


			var Student = context.Student.Find(student.Id);
			if (Student == null)
			{
				return false;
			}

			Student.FirstName = student.FirstName;
			Student.LastName = student.LastName;
			Student.DateOfBirth = student.DateOfBirth;
			Student.Address = student.Address;
			Student.SchoolId = student.SchoolId;
			context.SaveChanges();
			return true;
		}
		public bool DeleteStudent(int id)
		{
			if (id <= 0)
			{
				return false;
			}

			var student = context.Student.Find(id);
			if (student == null)
			{
				return false;
			}


			context.Student.Remove(student);
			context.SaveChanges();
			return true;

		}
	}
}