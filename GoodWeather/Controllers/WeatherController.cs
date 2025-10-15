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
    public IActionResult Get([FromQuery] int page = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] int? temperatureFrom = null,
        [FromQuery] int? temperatureTo = null)
    {
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and PageSize must be greater than 0.");
        }

        var query = _repository.GetAll().AsQueryable();

        // filtering
        if (temperatureFrom.HasValue)
        {
            query = query.Where(w => w.TemperatureInCelsius >= temperatureFrom.Value);
        }

        if (temperatureTo.HasValue)
        {
            query = query.Where(w => w.TemperatureInCelsius <= temperatureTo.Value);
        }

        var totalCount = query.Count();

        // paging
        var items = query
            .OrderBy(w => w.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var response = new
        {
            totalCount,
            page,
            pageSize,
            totalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            items
        };
        
        return Ok(response);
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