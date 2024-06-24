using Acme.Decorator;
using Moq;

namespace Acme.DecoratorTests;

public class RestServiceDecoratorTest
{
    [Fact]
    public void ShouldReturnValidResponse()
    {
        var logger = new Mock<ILogger>();
        var restService = new RestService();
        var loggingDecorator = new LoggingDecorator(restService, logger.Object);

        Assert.Equal("Hello 1 World", loggingDecorator.Get("1"));
        logger.Verify(x => x.Log(It.IsAny<string>()), Times.Exactly(2));
    }
}