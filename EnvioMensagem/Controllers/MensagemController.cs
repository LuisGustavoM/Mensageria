using Microsoft.AspNetCore.Mvc;

namespace EnvioMensagem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MensagemController : Controller
    {
        private static readonly string[] Summaries = new[]
       {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly ILogger<MensagemController> _logger;

        public MensagemController(ILogger<MensagemController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetMenssagem")]
        public IEnumerable<WeatherForecast> GetMenssagem()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
