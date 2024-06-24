using Acme.Decorator;

namespace Acme.DecoratorTests;

public class RestServiceTest
{
    [Fact]
    public void ShouldReturnValidResponse()
    {
        var restService = new RestService();

        Assert.Equal("Hello 1 World", restService.Get("1"));
    }
}