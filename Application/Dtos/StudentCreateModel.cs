using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Application.Dtos
{
	public class StudentCreateModel
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public required string Address { get; set; }
		public int SchoolId { get; set; }
	}
}
