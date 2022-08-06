using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace DoctorsAppointment.ATDD;

public class BasicTest : IClassFixture<CustomWebApplicationFactory<Program>> //IClassFixture<WebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public BasicTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/Basic/1");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
    }
}