using Microsoft.AspNetCore.Mvc;

namespace i_think_so.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        [HttpGet(Name = "GetWeatherForecast")]
        public int Get()
        {
            return 2;
        }
    }
}