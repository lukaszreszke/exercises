namespace DocumentExample;

public class DocumentNotFoundException : Exception
{
    public int DocumentId { get; }

    public DocumentNotFoundException(int documentId)
    {
        DocumentId = documentId;
    }
}