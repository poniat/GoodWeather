# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and publish
COPY . ./
RUN dotnet publish -c Release -o out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/out ./

# Create folder for database files
RUN mkdir -p /app/data

# Create environment variable for database path
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/GoodWeather.db"

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Declare volume for data persistence
VOLUME ["/app/data"]

ENTRYPOINT ["dotnet", "GoodWeather.dll"]
