using Microsoft.AspNetCore.Mvc;

namespace Logging.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
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


        [Produces(typeof(IEnumerable<WeatherForecast>))]
        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            _logger.LogWarning("GetRequest Warn", new
            {
                Id = 34,
                Name = "dsfg"
            });

            _logger.LogError(new Exception("No more message"), "GetRequest Warn");

            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(int), 204)]
        [ProducesResponseType(404)]
        public IActionResult Get([FromRoute] int id)
        {
         
            
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(WeatherForecast), 200)]
        public IActionResult Post([FromBody] WeatherForecast forecase)
        {


            return Ok();
        }
    }
}

