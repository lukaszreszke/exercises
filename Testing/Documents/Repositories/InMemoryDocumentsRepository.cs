namespace Tests.Documents;

public class InMemoryDocumentRepository
{
    private List<Document> _documents = new List<Document>();

    public Document GetById(Guid id)
    {
        return _documents.FirstOrDefault(x => x.Id == id);
    }
    
    public void Save(Document document)
    {
        _documents.Add(document);
    }
}