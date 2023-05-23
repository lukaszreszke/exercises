using DocumentExample;
using Moq;

namespace DocumentExampleTests;

public class DocumentTests
{
    [Fact]
    public void should_remove_change_if_document_exists()
    {
        var document = new Document();
        var repository = new Mock<IDocumentRepository>();
        repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(document);
        var storage = new Mock<IDocumentStorage>();
        storage.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
        var documentService = new DocumentService(repository.Object, storage.Object);
        documentService.AddChange(1, "change");

        documentService.RemoveChange(1, 1);

        Assert.Empty(document.Changes);
    }

    [Fact]
    public void should_remove_change_if_document_does_not_exist_in_blob()
    {
        var document = new Document();
        var repository = new Mock<IDocumentRepository>();
        repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(document);
        var storage = new Mock<IDocumentStorage>();
        storage.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);
        var documentService = new DocumentService(repository.Object, storage.Object);
        documentService.AddChange(1, "change");

        documentService.RemoveChange(1, 1);

        Assert.Empty(document.Changes);
    }

    [Fact]
    public void should_not_remove_change_if_it_does_not_exist()
    {
        var document = new Document();
        var repository = new Mock<IDocumentRepository>();
        repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(document);
        var storage = new Mock<IDocumentStorage>();
        storage.Setup(x => x.Exists(It.IsAny<int>())).Returns(true);
        var documentService = new DocumentService(repository.Object, storage.Object);
        documentService.AddChange(1, "change");

        Assert.Throws<ChangeDoesNotExist>(() => documentService.RemoveChange(1, 2));
    }

    [Fact]
    public void should_not_remove_change_if_it_does_not_exist_and_document_not_exist_in_blob()
    {
        var document = new Document();
        var repository = new Mock<IDocumentRepository>();
        repository.Setup(x => x.GetById(It.IsAny<int>())).Returns(document);
        var storage = new Mock<IDocumentStorage>();
        storage.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);
        var documentService = new DocumentService(repository.Object, storage.Object);
        documentService.AddChange(1, "change");

        Assert.Throws<ChangeDoesNotExist>(() => documentService.RemoveChange(1, 2));
    }


    [Fact]
    public void does_not_remove_change_when_document_doesnt_exist()
    {
        var repository = new Mock<IDocumentRepository>();
        var storage = new Mock<IDocumentStorage>();
        storage.Setup(x => x.Exists(It.IsAny<int>())).Returns(false);
        var documentService = new DocumentService(repository.Object, storage.Object);

        Assert.Throws<DocumentNotFoundException>(() => documentService.RemoveChange(1, 1));
    }
}