using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ_Producer.Infrastructor.RabbitMQ;
using SharedLibrary;

namespace RabbitMQ_Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRabbitMQProducer _rabbitMQProducer;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApplicationDbContext _context;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRabbitMQProducer rabbitMQProducer, ApplicationDbContext context)
        {
            _logger = logger;
            _rabbitMQProducer = rabbitMQProducer;
            _context = context;
        }

        [HttpGet("GetWeatherForecast", Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var result = await _context.logSystems.ToListAsync();
            //var resp = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();

            return Ok(result);
        }

        [HttpPost("SendWeatherForecast", Name = "SendWeatherForecast")]
        public IActionResult PostWeatherForecast([FromBody] WeatherForecast body)
        {
            _rabbitMQProducer.SendWeatherMessage(body);
            return Ok(body);
            //return Ok("Message sent to RabbitMQ");
        }
    }
}
