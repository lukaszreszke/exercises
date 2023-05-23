namespace DocumentExample;

public class DocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDocumentStorage _documentStorage;

    public DocumentService(IDocumentRepository documentRepository, IDocumentStorage documentStorage)
    {
        _documentRepository = documentRepository;
        _documentStorage = documentStorage;
    }

    public void AddChange(int documentId, string content)
    {
        var document = _documentRepository.GetById(documentId);
        if (document == null) throw new DocumentNotFoundException(documentId);
        var documentExistsInStorage = _documentStorage.Exists(documentId);
        if (!documentExistsInStorage)
        {
            _documentStorage.AddDocument(documentId);
        }

        document.AddChange(content);

        _documentRepository.Save(document);
    }

    public void RemoveChange(int documentId, int changeId)
    {
        var document = _documentRepository.GetById(documentId);
        if (document == null) throw new DocumentNotFoundException(documentId);
        var documentExistsInStorage = _documentStorage.Exists(documentId);
        if (!documentExistsInStorage)
        {
            _documentStorage.AddDocument(documentId);
        }

        document.RemoveChange(changeId);

        _documentRepository.Save(document); 
    }

    public void Accept(int documentId, Side side)
    {
        var document = _documentRepository.GetById(documentId);
        if (document == null) throw new DocumentNotFoundException(documentId);
        var documentExistsInStorage = _documentStorage.Exists(documentId);
        if (!documentExistsInStorage)
        {
            _documentStorage.AddDocument(documentId);
        }

        document.Accept(side);

        _documentRepository.Save(document);  
    }
}