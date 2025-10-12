using GoodWeather.Middlewere;
using GoodWeather.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middlewere
app.UseMiddleware<RequestTimingMiddleware>();
//app.UseMiddleware<ApiKeyMiddleware>();

//app.UseMiddleware<CustomHeaderMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
// app.Use(async (context, next) =>
// {
//     Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
//     await next.Invoke();
//     Console.WriteLine($"Response: {context.Response.StatusCode}");
// });

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
