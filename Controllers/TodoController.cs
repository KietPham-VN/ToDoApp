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
	public class TodoController(IApplicationDbContext _dbContext) : ControllerBase
	{
		[HttpGet]
		public IEnumerable<TodoViewModel> Get(bool isCompleted)
		{
			var data = _dbContext.ToDos
				.Where(x => x.IsCompleted == isCompleted)
				.Select(x => new TodoViewModel
			{
				Description = x.Description,
				IsCompleted = x.IsCompleted
			}).ToList();

			return data;
		}

		[HttpPost]
		public int Post(ToDoCreatedModel todo)
		{
			var data = new Todo
			{
				Description = todo.Description,
			};
			_dbContext.ToDos.Add(data);
			_dbContext.SaveChanges();
			return data.Id;
		}

		[HttpPut]
		public int Put(Todo todo)
		{
			var data = _dbContext.ToDos.Find(todo.Id);
			if (data == null)
			{
				return -1;
			}
			data.Description = todo.Description;

			_dbContext.SaveChanges();
			return todo.Id;
		}

		[HttpDelete]
		public void Delete(int id)
		{
			var data = _dbContext.ToDos.Find(id);
			if (data == null)
			{
				return;
			}

			_dbContext.ToDos.Remove(data);
			_dbContext.SaveChanges();
		}
	}
}
