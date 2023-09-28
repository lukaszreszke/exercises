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
}