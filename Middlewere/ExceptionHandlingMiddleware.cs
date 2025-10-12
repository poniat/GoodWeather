using GoodWeather.Services;

namespace GoodWeather.Middlewere
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IEmailSender _emailSender;

        public ExceptionHandlingMiddleware(RequestDelegate next, IEmailSender emailSender)
        {
            _next = next;
            _emailSender = emailSender;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Sorry, something went wrong on the server. Try other endpoint... or try again later...");

                _emailSender.Send(
                    "Unhandled Exception in GoodWeather",
                    $"Exception: {ex.Message}\n\nStackTrace:\n{ex.StackTrace}"
                );
            }
        }
    }
}