using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
	public class TodoController : ControllerBase
	{
		private readonly IApplicationDbContext _dbContext;
		private readonly ITodoService _todoService;
		private readonly IGuidGenerator _guidGenerator;
		private readonly ISingletonGenerator _singletonGenerator;
		private readonly GuidData _guidData;
		public TodoController(
			IApplicationDbContext dbContext,
			ITodoService todoService,
			IGuidGenerator guidGenerator,
			ISingletonGenerator singletonGenerator,
			GuidData guidData)
		{
			_dbContext = dbContext;
			_todoService = todoService;
			_guidGenerator = guidGenerator;
			_singletonGenerator = singletonGenerator;
			_guidData = guidData;
		}


		[HttpGet]
		public IEnumerable<TodoViewModel> Get(bool isCompleted)
		{
			var data = _dbContext.ToDos.Where(x => x.IsCompleted == isCompleted).Select(x => new TodoViewModel
			{
				Description = x.Description,
				IsCompleted = x.IsCompleted
			}).ToList();

			return data;
		}

		[HttpPost]
		public int Post(ToDoCreatedModel todo)
		{
			return _todoService.Post(todo);
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

		[HttpGet("guid")]
		public Guid[] GetGuid()
		{
			_guidData.GuidGenerator = new GuidGenerator();
			return
			[
				//_guidGenerator.Generate(), // transient
				_singletonGenerator.Generate(), // singleton
				// => không nên inject 1 tk có life time bé vào 1 tk có life time lớn vì thằng bé sẽ bị cuốn theo tk lớn
				_guidData.GetGuid() // scoped
			];
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
