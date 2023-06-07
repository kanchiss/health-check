using healthcheckcoreapi.Models;
using healthcheckcoreapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace healthcheckcoreapi.Controllers
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
        private readonly IProduct _productRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IProduct productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("getproduct")]
        public List<Product> GetProduct()
        {
            return _productRepository.GetProducts();
        }
    }
}