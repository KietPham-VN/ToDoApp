using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Domain.Entities
{
	public class ToDo
	{
		[Key]
		public int Id { get; set; }
		public required string Description { get; set; }
		public bool IsComplete { get; set; }
	}
}
