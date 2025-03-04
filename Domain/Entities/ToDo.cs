﻿using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Domain.Entities
{
	public class Todo
	{
		[Key]
		public int Id { get; set; }
		public required string Description { get; set; }
		public bool IsCompleted { get; set; }
	}
}
