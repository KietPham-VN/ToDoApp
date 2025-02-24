  namespace ToDoApp.Application.Services
{
	public interface ISingletonGenerator
	{
		Guid Generate();
	}
	public class SingletonGenerator(IServiceProvider serviceProvider) : ISingletonGenerator
	{
		private readonly IGuidGenerator? _guidGenerator;

		public Guid Generate() { 
			var guidGenerator = serviceProvider.GetService<IGuidGenerator>();
			return guidGenerator!.Generate();
		}
	}
}
