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

## GitHub Actions (CI/CD)
1. Create `.github/workflows/build.yml`
2. With content as below:
```
name: Build and Test GoodWeather

on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

jobs:
  build:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout repo
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: 9.0.x

      - name: Restore dependencies
        run: dotnet restore

      - name: Build project
        run: dotnet build --no-restore --configuration Release

      - name: Run tests
        run: dotnet test --no-build --verbosity normal

      - name: Build Docker image
        run: docker build -t goodweather:latest .
```
## Docker Hub
1. Create Docker Hub account at https://hub.docker.com/ with your unique Docker ID
2. Build a Docker image locally
```
docker build -t goodweather .
```
3. Tag the image with your Docker Hub username
```
docker tag goodweather:latest poniat81/goodweather:latest
```
4. Push the image to Docker Hub
```
docker push poniat81/goodweather:latest
```
5. Other can pull your image
```
docker pull poniat81/goodweather:latest
```



