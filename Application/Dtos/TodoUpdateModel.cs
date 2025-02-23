namespace ToDoApp.Application.Dtos
{
	public class ToDoUpdateModel
	{
		public int Id { get; set; }
		public required string Description { get; set; }
		public bool IsCompleted { get; set; }
	}
}
