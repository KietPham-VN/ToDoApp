using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Dtos;
using ToDoApp.Domain.Entities;
using ToDoApp.infrastructure;

namespace ToDoApp.Controllers
{
	// GET, POST, PUT, DELETE
	// GET, PUT, DELETE có Idempotency
	// POST không có Idempotency
	// Client ----> Server, Server ----> DB:OK
	// Server ----> Client: 200 OK
	[ApiController]
	[Route("[controller]")]
	public class TodoController(IApplicationDbContext dbContext) : ControllerBase
	{
		[HttpGet]
		public IEnumerable<TodoViewModel> Get(bool isCompleted)
		{
			var data = dbContext.ToDos
				.Where(x => x.IsCompleted == isCompleted)
				.Select(x => new TodoViewModel
			{
				Description = x.Description,
				IsCompleted = x.IsCompleted
			}).ToList();

			return data;
		}

		[HttpPost]
		public int Post(ToDo todo)
		{
			dbContext.ToDos.Add(todo);
			dbContext.SaveChanges();
			return todo.Id;
		}

		[HttpPut]
		public int Put(ToDo todo)
		{
			var data = dbContext.ToDos.Find(todo.Id);
			if (data == null)
			{
				return -1;
			}
			data.Description = todo.Description;
			data.IsComplete = todo.IsComplete;
			dbContext.SaveChanges();
			return todo.Id;
		}

		[HttpDelete]
		public void Delete(int id)
		{
			var data = dbContext.ToDos.Find(id);
			if (data == null)
			{
				return;
			}

			dbContext.ToDos.Remove(data);
			dbContext.SaveChanges();
		}
	}
}
