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
            usersRepository,
            null,
            null);

        var documentId = documentsService.CreateDocument(DocumentType.MANUAL, "Tests",
            "Are Cool And Possible To Do In Your Code. Check How.");

        var result = documentsRepository.GetById(documentId);
        Assert.Equal("DRAFT", result.Status.Code);
        Assert.Equal("Tests", result.Title);
        Assert.Equal("Are Cool And Possible To Do In Your Code. Check How.", result.Content);
        Assert.Equal(DocumentType.MANUAL, result.DocumentType);
        Assert.Equal($"https://example.com/documents//{user.Id}/Tests", result.AccessLink.ToString());
        emailGatewayMock.Verify(x => x.SendEmail("tests@possible.com", 
            $"https://example.com/documents//{user.Id}/Tests"));
    }

    [Fact]
    public void verifies_document()
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
            usersRepository,
            null,
            null);
        var documentId = documentsService.CreateDocument(DocumentType.MANUAL, "Tests",
            "Are Cool And Possible To Do In Your Code. Check How.");
        
        documentsService.VerifyDocument(documentId);

        var result = documentsRepository.GetById(documentId);
        Assert.Equal("VERIFIED", result.Status.Code);
        emailGatewayMock.Verify(x => x.SendEmail(user.Email, 
            $"Document Tests has been verified by {user.Id}"));
    }
    
    [Fact]
    public async Task publishes_document()
    {
        var documentsRepository = new InMemoryDocumentRepository();
        var usersRepository = new InMemoryUsersRepository();
        var contextAccessorMock = new Mock<IExecutionContextAccessor>();
        var emailGatewayMock = new Mock<IEmailGateway>();
        var documentsHttpClient = new Mock<IDocumentsHttpClient>();
        var configuration = new Mock<IConfiguration>();
        var user = new User(Guid.NewGuid()) { Email = "tests@possible.com" };
        usersRepository.Save(user);
        contextAccessorMock.Setup(x => x.UserId).Returns(user.Id);
        var time = TimeOnly.Parse("10:00");
        configuration.Setup(x => x.GetPreferredEmailReceivalTimeFor(user)).Returns(time);
        var documentsService = new DocumentsService(
            documentsRepository,
            contextAccessorMock.Object,
            null,
            emailGatewayMock.Object,
            usersRepository,
            configuration.Object);
        var documentId = documentsService.CreateDocument(DocumentType.MANUAL, "Tests",
            "Are Cool And Possible To Do In Your Code. Check How.");
        documentsService.VerifyDocument(documentId);
 
        await documentsService.PublishDocument(documentId, documentsHttpClient.Object);

        var result = documentsRepository.GetById(documentId);
        Assert.Equal("PUBLISHED", result.Status.Code);
        emailGatewayMock.Verify(x => x.ScheduleEmail(user.Email, 
            "Document Tests is printed and waits for you in the office." +
            "You have to come and get it personally." +
            "Your manager," +
            "Control Desire", time));
    }
}