namespace ToDoApp.Application.Dtos
{
	public class TodoViewModel
	{
		public required string  Description { get; set; }
		public bool IsCompleted { get; set; }
	}
}
