namespace ToDoApp.Application.Dtos
{
	public class StudentViewModel
	{
		public int Id { get; set; }
		public required string FullName { get; set; }
		public int Age { get; set; }
		public required string SchoolName { get; set; }
	}
}
