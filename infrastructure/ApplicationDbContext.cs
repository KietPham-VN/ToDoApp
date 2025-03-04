﻿using Microsoft.EntityFrameworkCore;
using ToDoApp.Domain.Entities;

namespace ToDoApp.infrastructure
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
	{
		public DbSet<Todo> ToDos { get; set; }
		public DbSet<Student> Student { get; set; }
		public DbSet<School> School { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
				.UseSqlServer("Server=KIETPA\\SQLEXPRESS;Database=ToDoApp;Trusted_Connection=True;TrustServerCertificate=True");
		}

		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
	}
}
