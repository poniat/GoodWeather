using System.Threading.Tasks;

namespace GoodWeather.Middlewere
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            context.Response.Headers.Add("X-Custom-Header", "GoodWeatherApp");
        }
    }
}