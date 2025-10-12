using Microsoft.EntityFrameworkCore;

namespace GoodWeather
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }
        public DbSet<WeatherForecast> Weathers => Set<WeatherForecast>();
    }
}