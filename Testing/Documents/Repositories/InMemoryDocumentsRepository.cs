namespace Tests.Documents;

public class InMemoryDocumentRepository : IDocumentRepository
{
    private List<Document> _documents = new List<Document>();

    public Document GetById(Guid id)
    {
        return _documents.SingleOrDefault(x => x.Id == id);
    }
    
    public void Save(Document document)
    {
        var doc = _documents.FirstOrDefault(x => x.Id == document.Id);
        if (doc == null)
        {
            _documents.Add(document);
        }
        else
        {
            doc = document;
        }
    }
}