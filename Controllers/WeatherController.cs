using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    [HttpGet()]
    public IEnumerable<WeatherForecast> Get()
    {
        return new List<WeatherForecast>
        {
            new WeatherForecast(temperatureInCelsius: 20, city: "Katowice"),
            new WeatherForecast(temperatureInCelsius: 15, city: "Białystok"),
            new WeatherForecast(temperatureInCelsius: 23, city: "Zakopane"),

        };
    }

    [HttpGet("{city}")]
    public WeatherForecast GetByCity(string city)
    {
        throw new NotImplementedException("Sorry, working of Cities. Try other ways.");
        // return new WeatherForecast(
        //             temperatureInCelsius: 20,
        //             city: "Katowice");
    }
}