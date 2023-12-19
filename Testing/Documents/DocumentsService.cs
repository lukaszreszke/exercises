using System.Net.Http.Json;

namespace Tests.Documents;

public class DocumentsService
{
    private const string ROOT_URL = "https://example.com/documents/";
    private readonly IDocumentRepository _documentRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public DocumentsService(
        IDocumentRepository documentRepository,
        IExecutionContextAccessor executionContextAccessor
    )
    {
        _documentRepository = documentRepository;
        _executionContextAccessor = executionContextAccessor;
    }
    public Guid CreateDocument(DocumentType documentType, string title, string content)
    {
        var draftStatus = new Status {Code = AvailableStatuses.DRAFT.ToString()};
        var user = new UserRepository().GetById(_executionContextAccessor.UserId);
        var document = new Document
        {
            Id = Guid.NewGuid(),
            Status = draftStatus,
            DocumentType = documentType,
            User = user,
            Title = title,
            Content = content,
            AccessLink = new Uri(ROOT_URL)
        };

        document.AccessLink = new Uri($"{document.AccessLink}/{user.Id}/{document.Title}");
        _documentRepository.Save(document);

        return document.Id;
    }

    public void VerifyDocument(Guid docId)
    {
        Document document = _documentRepository.GetById(docId);

        if (document.Status.Code != AvailableStatuses.DRAFT.ToString())
        {
            throw new CannotVerifyPublishedDocument();
        }

        document.Status = new Status() { Code = AvailableStatuses.VERIFIED.ToString() };
        _documentRepository.Save(document);
    }

    public void AssignReader(Guid docId, Guid readerId)
    {
        Document document = _documentRepository.GetById(docId);

        if (document.Status.Code != AvailableStatuses.PUBLISHED.ToString())
        {
            throw new UnpublishedDocumentCannotBeShared();
        }
            
        if (document.User.Id == _executionContextAccessor.UserId)
        {
            var readerUser = new UserRepository().GetById(readerId);
            if (!document.Readers.Contains(readerUser))
            {
                document.Readers.Add(readerUser);
                _documentRepository.Save(document);
            }
        }
    }

    public async Task PublishDocument(Guid docId, HttpClient httpClient)
    {
        Document document = _documentRepository.GetById(docId);
        if (document.Status.Code != AvailableStatuses.VERIFIED.ToString())
        {
            throw new CannotPublishUnverifiedDocument();
        }
        document.Status = new Status() {Code = AvailableStatuses.PUBLISHED.ToString()};
        _documentRepository.Save(document);
        
        try
        {
            await httpClient.PostAsJsonAsync("api/DocumentsArchive/publish", document);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    public void ArchiveDocument(Guid docId)
    {
        Document document = _documentRepository.GetById(docId);

        document.Status = new Status() {Code = AvailableStatuses.ARCHIVED.ToString()};
        _documentRepository.Save(document);
    }
}