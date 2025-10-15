using System.Diagnostics;

namespace GoodWeather.Middlewere
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();
            Console.WriteLine($"Request took {sw.ElapsedMilliseconds} ms");
        }
    }
}