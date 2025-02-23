using ToDoApp.Application.Services;

namespace ToDoApp.Application.Dtos
{
	public class GuidData
	{
		public IGuidGenerator GuidGenerator { get; set; }
		public Guid GetGuid() {
			return GuidGenerator.Generate();
		}
	}
}
