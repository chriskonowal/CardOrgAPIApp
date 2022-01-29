using CardOrgAPI.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Controllers
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var rng = new Random();
            //CardOrgContext context = new CardOrgContext();
            //var card = context.Cards
            //           .Where(s => s.PricePaid > 0 )
            //           .FirstOrDefault<Card>();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = card.TimeStamp,
            //    TemperatureC = card.Copies,
            //    Summary = card.FrontCardThumbnailImagePath
            //})
            //.ToArray();
            return null;
        }
    }
}
