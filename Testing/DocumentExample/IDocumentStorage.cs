namespace DocumentExample;

public interface IDocumentStorage
{
    bool Exists(int documentId);
    void AddDocument(int documentId);
}