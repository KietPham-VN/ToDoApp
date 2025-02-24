using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
	private static readonly string[] Summaries =
	[
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

	[HttpGet(Name = "GetWeatherForecast")]
	public IEnumerable<WeatherForecast> Get()
	{
		return
		[
			new WeatherForecast {
				Date = DateTime.Now,
				TemperatureC = -1,
				Summary = Summaries[0]
			},
		];

	}
}
