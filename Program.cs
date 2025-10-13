using GoodWeather;
using GoodWeather.Middlewere;
using GoodWeather.Repositories;
using GoodWeather.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// services
builder.Services.AddSingleton<IEmailSender, EmailSender>();

// data
builder.Services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddDbContext<WeatherDbContext>(options => options.UseSqlite("Data Source=good-weather.db"));

var app = builder.Build();

// automatic migration
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
    db.Database.Migrate();
}

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
