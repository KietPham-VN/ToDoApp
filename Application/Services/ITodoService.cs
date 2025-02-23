using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Dtos;
using ToDoApp.Domain.Entities;
using ToDoApp.infrastructure;

namespace ToDoApp.Application.Services
{
	public interface ITodoService
	{
		public int Post(ToDoCreatedModel todo);
		public Guid Generate(); 
	}

	public class TodoService(IApplicationDbContext context, IGuidGenerator guidGenerator) : ITodoService
	{
		public int Post(ToDoCreatedModel todo)
		{
			var data = new Todo
			{
				Description = todo.Description,
			};
			context.ToDos.Add(data);
			context.SaveChanges();
			return data.Id;
		}
		public Guid Generate() { 
			return guidGenerator.Generate();
		}
	}
}
