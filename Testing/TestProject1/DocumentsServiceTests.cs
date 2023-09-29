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
        usersRepository.Save(new User(Guid.NewGuid()){ Email = "tests@possible.com"});
        var documentsService = new DocumentsService(
            documentsRepository,
            contextAccessorMock.Object,
            null,
            emailGatewayMock.Object);

        var documentId = documentsService.CreateDocument(DocumentType.MANUAL, "Tests",
            "Are Cool And Possible To Do In Your Code. Check How.");

        var result = documentsRepository.GetById(documentId);
        Assert.Equal("Tests", result.Title);
        Assert.Equal("Are Cool And Possible To Do In Your Code. Check How.", result.Content);
        Assert.Equal(DocumentType.MANUAL, result.DocumentType);
        emailGatewayMock.Verify(x => x.SendEmail("tests@possible.com", documentId));
    }
}