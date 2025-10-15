using GoodWeather.Models;
using GoodWeather.Repositories;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherForecastRepository _repository;

    public WeatherController(IWeatherForecastRepository repository)
    {
        _repository = repository;
    }

    [HttpGet()]
    public IActionResult Get()
    {
        return Ok(_repository.GetAll());
    }

    [HttpGet("{city}")]
    public IActionResult GetByCity(string city)
    {
        var weather = _repository.GetByCity(city);
        if (weather == null)
        {
            return NotFound();
        }
        return Ok(weather);
    }

    [HttpPost]
    public IActionResult Create(WeatherForecast weather)
    {
        _repository.Add(weather);
        return CreatedAtAction(nameof(GetByCity), new { city = weather.City }, weather);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (_repository.DeleteById(id))
        {
            return NoContent();
        }
        return NotFound();
    }
}