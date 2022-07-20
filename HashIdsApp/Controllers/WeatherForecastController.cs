using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace HashIdsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var hashids = new Hashids("this is my salt");

            //var newId = Guid.NewGuid();
            var newId = Random.Shared.Next(5, 55);
            var hash = hashids.Encode(newId);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = newId,
                ShortId = hash,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}