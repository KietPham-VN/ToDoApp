
using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;

namespace ToDoApp.infrastructure
{
	public interface IApplicationDbContext
	{
		DbSet<Todo> ToDos { get; set; }
		public DbSet<Student> Student { get; set; }
		public DbSet<School> School { get; set; }
		int SaveChanges();
	}
}
