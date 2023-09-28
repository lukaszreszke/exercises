using Tests.Documents;

namespace TestProject1;

public class InMemoryDocumentsRepositoryTests
{
    [Fact]
    public void works_as_intended()
    {
        var repo = new InMemoryDocumentRepository();
        var id = Guid.NewGuid();
        
        repo.Save(new Document { Id =  id});

        Assert.NotNull(repo.GetById(id));
    }

    [Fact]
    public void document_can_be_updated()
    {
        var repo = new InMemoryDocumentRepository();
        var id = Guid.NewGuid();
        repo.Save(new Document { Id = id });
        var document = repo.GetById(id);
        document.Content = "content";

        repo.Save(document);

        Assert.Equal("content", repo.GetById(id).Content);
    }
}