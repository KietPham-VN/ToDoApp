
using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;

namespace ToDoApp.infrastructure
{
	public interface IApplicationDbContext
	{
		DbSet<ToDo> ToDos { get; set; }
		int SaveChanges();
	}
}
