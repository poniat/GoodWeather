public class WeatherForecast
{
    public WeatherForecast(double temperatureInCelsius, string city)
    {
        TemperatureInCelsius = temperatureInCelsius;
        City = city;
    }
    public double TemperatureInCelsius { get; }
    public string City { get; }
}