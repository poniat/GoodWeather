using GoodWeather.Models;

namespace GoodWeather.Repositories
{
    public interface IWeatherForecastRepository
    {
        IEnumerable<WeatherForecast> GetAll();
        WeatherForecast? GetByCity(string city);
        void Add(WeatherForecast weather);
        bool DeleteById(int id);
    }
}