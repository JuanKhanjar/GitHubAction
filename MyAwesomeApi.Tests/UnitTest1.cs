
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using System.Collections.Generic;
namespace MyAwesomeApi.Tests;

public class WeatherForecastControllerTests
{
    private readonly WebApplicationFactory<Program> _factory;

    public WeatherForecastControllerTests()
    {
        _factory = new WebApplicationFactory<Program>();
    }

    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

        var jsonString = await response.Content.ReadAsStringAsync();
        var forecasts = JsonSerializer.Deserialize<List<object>>(jsonString);

        Assert.NotNull(forecasts);
        Assert.NotEmpty(forecasts);
    }
}
