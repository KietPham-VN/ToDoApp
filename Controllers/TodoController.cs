using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Services;
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
	public class TodoController(
		IApplicationDbContext dbContext,
		ITodoService todoService,
		IGuidGenerator guidGenerator,
		ISingletonGenerator singletonGenerator,
		GuidData guidData) : ControllerBase
	{
		[HttpGet]
		public IEnumerable<TodoViewModel> Get(bool isCompleted)
		{
			var data = dbContext.ToDos.Where(x => x.IsCompleted == isCompleted).Select(x => new TodoViewModel
			{
				Description = x.Description,
				IsCompleted = x.IsCompleted
			}).ToList();

			return data;
		}

		[HttpPost]
		public int Post(ToDoCreatedModel todo)
		{
			return todoService.Post(todo);
		}

		[HttpPut]
		public int Put(Todo todo)
		{
			var data = dbContext.ToDos.Find(todo.Id);
			if (data == null)
			{
				return -1;
			}
			data.Description = todo.Description;

			dbContext.SaveChanges();
			return todo.Id;
		}

		[HttpGet("guid")]
		public Guid[] GetGuid()
		{
			guidData.GuidGenerator = new GuidGenerator();
			return
			[
				//_guidGenerator.Generate(), // transient
				singletonGenerator.Generate(), // singleton
				// => không nên inject 1 tk có life time bé vào 1 tk có life time lớn vì thằng bé sẽ bị cuốn theo tk lớn
				guidData.GetGuid() // scoped
			];
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
