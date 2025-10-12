# GoodWeather
Test project to practice C#/NET 9, ASP MCV, xUnit, Docker etc

### New project
```
dotnet new webapi -n GoodWeather
```

### How to expose swagger
```c#
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
```

### Middlewere - logging
```C#
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    Console.WriteLine($"Response: {context.Response.StatusCode}");
});
```

### Middlewere - unhandled exception
```C#
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An error occurred.");
    }
});
```

## Docker
1. Create `dockerfile`
2. Start `Docker desktop` on Windows.
3. Create new docker image
    ```
    docker build -t goodweather .
    ```
4. Start application from docker image
    ```
    docker run -p 8080:8080 goodweather
    ```
5. Open browser, navigate to http://localhost:8080/api/weather



