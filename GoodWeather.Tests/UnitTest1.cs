using GoodWeather.Models;

namespace GoodWeather.Tests
{
    public class WeatherTests
    {
        public class ModelTests
        {
            [Fact]
            public void CreatedModel_Contain_NonEmptyCity()
            {
                var weather = new WeatherForecast();

                Assert.Equal(string.Empty, weather.City);
            }
        }
    }
}
