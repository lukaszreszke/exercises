using Moq;
using Tests.Documents;

namespace TestProject1;

public class DocumentsServiceTests
{
    [Fact]
    public void creates_document()
    {
        var documentsRepository = new InMemoryDocumentRepository();
        var usersRepository = new InMemoryUsersRepository();
        var contextAccessorMock = new Mock<IExecutionContextAccessor>();
        var emailGatewayMock = new Mock<IEmailGateway>();
        var user = new User(Guid.NewGuid()) { Email = "tests@possible.com" };
        usersRepository.Save(user);
        contextAccessorMock.Setup(x => x.UserId).Returns(user.Id);
        var documentsService = new DocumentsService(
            documentsRepository,
            contextAccessorMock.Object,
            null,
            emailGatewayMock.Object,
            usersRepository);

        var documentId = documentsService.CreateDocument(DocumentType.MANUAL, "Tests",
            "Are Cool And Possible To Do In Your Code. Check How.");

        var result = documentsRepository.GetById(documentId);
        Assert.Equal("Tests", result.Title);
        Assert.Equal("Are Cool And Possible To Do In Your Code. Check How.", result.Content);
        Assert.Equal(DocumentType.MANUAL, result.DocumentType);
        Assert.Equal($"https://example.com/documents//{user.Id}/Tests", result.AccessLink.ToString());
        emailGatewayMock.Verify(x => x.SendEmail("tests@possible.com", 
            $"https://example.com/documents//{user.Id}/Tests"));
    }
}