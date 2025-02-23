namespace ToDoApp.Application.Services
{
	public interface ISingletonGenerator
	{
		Guid Generate();
	}
	public class SingletonGenerator : ISingletonGenerator
	{
		private readonly IGuidGenerator? _guidGenerator;
		private readonly IServiceProvider _serviceProvider;

		public SingletonGenerator(IServiceProvider serviceProvider) { 
			_serviceProvider = serviceProvider;
		}

		public Guid Generate() { 
			var guidGenerator = _serviceProvider.GetService<IGuidGenerator>();
			return guidGenerator!.Generate();
		}
	}
}
