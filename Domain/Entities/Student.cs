using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Domain.Entities
{
	[Table("Students")] // đặt tên bảng
	public class Student
	{
		[Key] // primary key
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(255)] // độ dài tối đa
		[Column("Surname")]
		public string? FirstName { get; set; }

		[StringLength(255)] // độ dài tối đa(chỉ dùng cho chuỗi)
		public string? LastName { get; set; }
		//[MaxLength(2000)]
		//public byte[] Image { get; set; }

		public DateTime DateOfBirth { get; set; }
		// cái dấu chấm hỏi ở đây là nullable

		public string? Address { get; set; }
		[NotMapped]// không map với database
		public int Age { get => (DateTime.Now - DateOfBirth).Days / 365; }

		[ForeignKey("School")] // foreign key khóa ngoại nè
		public int SchoolId { get; set; } 
		public School? School { get; set; } 
	}
}
