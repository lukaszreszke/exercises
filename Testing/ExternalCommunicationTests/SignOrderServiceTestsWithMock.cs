using System.Net;
using ExternalCommunication;

namespace ExternalCommunicationTests;

public class SignOrderServiceTestsWithMock
{
    [Fact]
    public void should_create_sign_order_using_external_client()
    {
        // Arrange
        var signOrderRepositoryStub = new InMemorySignOrderRepository();
        var userRepositoryStub =
            new InMemoryUserRepository(new List<User> { new() { Id = 1, Email = "e@e.pl", Name = "user" } });
        var externalClientMock = new Mock<IExternalProviderClient>();
        externalClientMock.Setup(x => x.Create(1)).Returns(HttpStatusCode.OK);
        var signOrderService =
            new SignOrderService(signOrderRepositoryStub, externalClientMock.Object, userRepositoryStub);
        
        // Act
        var createdSignOrderId = signOrderService.Create();
        
        // Assert 
        Assert.Equal(1, createdSignOrderId);
    }

    [Fact]
    public void throws_when_create_fails_in_external_provider()
    {
        // Arrange
        var signOrderRepositoryStub = new InMemorySignOrderRepository();
        var userRepositoryStub =
            new InMemoryUserRepository(new List<User> { new() { Id = 1, Email = "e@e.pl", Name = "user" } });
        var externalClientMock = new Mock<IExternalProviderClient>();
        externalClientMock.Setup(x => x.Create(1)).Returns(HttpStatusCode.Conflict);
        var signOrderService =
            new SignOrderService(signOrderRepositoryStub, externalClientMock.Object, userRepositoryStub);
        
        // Act && Assert
        Assert.Throws<FailedToUpdateExternalProviderException>(() => signOrderService.Create());
    }
}