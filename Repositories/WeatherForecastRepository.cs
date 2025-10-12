using Microsoft.EntityFrameworkCore;

namespace GoodWeather.Repositories
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly WeatherDbContext _context;

        public WeatherForecastRepository(WeatherDbContext context)
        {
            _context = context;
        }

        public void Add(WeatherForecast weather)
        {
            _context.Weathers.Add(weather);
            _context.SaveChanges();
        }

        public bool DeleteById(int id)
        {
            //var weather = GetByCity(city);
            var weather = _context.Weathers.FirstOrDefault(w => w.Id == id);
            if (weather == null)
                return false;

            _context.Weathers.Remove(weather);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<WeatherForecast> GetAll()
        {
            return _context.Weathers.ToList();
        }

        public WeatherForecast? GetByCity(string city)
        {
            //get weather by city name with case insensitive search
            return _context.Weathers
                .AsEnumerable()
                .FirstOrDefault(w => string.Equals(w.City, city, StringComparison.OrdinalIgnoreCase));            
        }
    }
}