namespace DocumentExample;

public interface IDocumentRepository
{
    Document GetById(int documentId);
    void Save(Document document);
}