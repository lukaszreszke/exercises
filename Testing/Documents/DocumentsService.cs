using System.Net.Http.Json;
using log4net;

namespace Tests.Documents;

public class DocumentsService
{
    private const string ROOT_URL = "https://example.com/documents/";
    private readonly IDocumentRepository _documentRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IEmailGateway _emailGateway;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(DocumentsService));

    public DocumentsService(
        IDocumentRepository documentRepository,
        IExecutionContextAccessor executionContextAccessor
    )
    {
        _documentRepository = documentRepository;
        _executionContextAccessor = executionContextAccessor;
        _emailGateway = new EmailGateway();
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
        _emailGateway.SendEmail(user.Email, document.AccessLink);
        _documentRepository.Save(document);

        MessageBus.Publish(new DocumentCreated(document.Id));

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
        _emailGateway.SendEmail(document.User.Email,
            $"Document {document.Title} has been verified by {_executionContextAccessor.UserId}");
        _documentRepository.Save(document);
        MessageBus.Publish(new DocumentVerified(document.Id));
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
                _emailGateway.SendEmail(readerUser.Email,
                    $"Document {document.Title} has been shared with you by {_executionContextAccessor.UserId}");
                MessageBus.Publish(new ReaderAssignedToDocument(document.Id));
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
        
        PrintDocument(document);
        SendEmails(document);

        try
        {
            await httpClient.PostAsJsonAsync("api/DocumentsArchive/publish", document);
        }
        catch (Exception e)
        {
            Logger.Error("Failed to publish document into Documents Archive", e);
            throw e;
        }
    }

    private void SendEmails(Document document)
    {
        foreach (var reader in document.Readers.Concat(new List<User> { document.User }))
        {
            var time = Configuration.GetPreferredEmailReceivalTimeFor(reader);
            _emailGateway.ScheduleEmail(reader.Email,
                $"Document {document.Title} is printed and waits for you in the office." +
                $"You have to come and get it personally." +
                $"Your manager," +
                $"Control Desire", time);
            document.Readers.Remove(reader);
            _documentRepository.Save(document);
        }
    }

    private static void PrintDocument(Document document)
    {
        PrinterFacade printerFacade = new PrinterFacade();
        for (int i = 0; i < document.Readers.Count + 1; i++) // readers + owner
        {
            printerFacade.Print(document);
        }
    }

    public void ArchiveDocument(Guid docId)
    {
        Document document = _documentRepository.GetById(docId);

        document.Status = new Status() {Code = AvailableStatuses.ARCHIVED.ToString()};
        _documentRepository.Save(document);
    }
}