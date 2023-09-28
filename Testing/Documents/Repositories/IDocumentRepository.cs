namespace Tests.Documents;

public interface IDocumentRepository
{
    void Save(Document document);
    Document GetById(Guid docId);
}