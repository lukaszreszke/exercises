using System.Net.Http.Json;
using log4net;

namespace Tests.Documents
{
    public class DocumentsService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IExecutionContextAccessor _executionContextAccessor;
        private readonly IEmailGateway _emailGateway;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DocumentService));

        public DocumentsService(
            IDocumentRepository documentRepository,
            IExecutionContextAccessor executionContextAccessor
        )
        {
            _documentRepository = documentRepository;
            _executionContextAccessor = executionContextAccessor;
            _emailGateway = new EmailGateway();
        }

        public Guid CreateDocument(DocumentType documentType, string title)
        {
            var draftStatus = new Status {Code = AvailableStatuses.DRAFT.ToString()};
            var user = new UserRepository().GetById(_executionContextAccessor.UserId);
            var document = new Document
            {
                Id = Guid.NewGuid(),
                Status = draftStatus,
                DocumentType = documentType,
                User = user,
                Title = title
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
            document.Status = new Status() {Code = AvailableStatuses.PUBLISHED.ToString()};
            _documentRepository.Save(document);
            PrinterFacade printerFacade = new PrinterFacade();
            for (int i = 0; i < document.Readers.Count + 1; i++) // readers + owner
            {
                printerFacade.Print(document);
            }

            foreach (var reader in document.Readers.Concat(new List<User> {document.User}))
            {
                var time = Configuration.GetPreferedEmailReceivalTimeFor(reader);
                _emailGateway.ScheduleEmail(reader.Email,
                    $"Document {document.Title} is printed and waits for you in the office", time);
                document.Readers.Remove(reader);
                _documentRepository.Save(document);
            }

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

        public void ArchiveDocument(Guid docId)
        {
            Document document = _documentRepository.GetById(docId);

            document.Status = new Status() {Code = AvailableStatuses.ARCHIVED.ToString()};
            _documentRepository.Save(document);
        }
    }
    #region interfaces
    internal class Configuration
    {
        public static decimal GetSafeValue()
        {
            return new decimal(40000.00);
        }

        public static DateTime GetPreferedEmailReceivalTimeFor(User reader)
        {
            throw new NotImplementedException();
        }
    }

    public class PrinterFacade
    {
        public void Print(Document document)
        {
            throw new NotImplementedException();
        }
    }

    public class ReaderAssignedToDocument
    {
        public ReaderAssignedToDocument(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }

    public class DocumentVerified
    {
        public DocumentVerified(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }

    public class DocumentCreated
    {
        public DocumentCreated(Guid documentId)
        {
            throw new NotImplementedException();
        }
    }

    public static class MessageBus
    {
        public static void Publish(object message)
        {
        }
    }

    public class CannotVerifyPublishedDocument : Exception
    {
    }

    public class UserRepository
    {
        public User GetById(Guid userId)
        {
            return new User(userId);
        }
    }

    public class EmailGateway : IEmailGateway
    {
        public void SendEmail(object email, object documentAccessLink)
        {
            throw new NotImplementedException();
        }

        public void ScheduleEmail(object readerEmail, string s, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEmailGateway
    {
        void SendEmail(object email, object documentAccessLink);
        void ScheduleEmail(object readerEmail, string s, DateTime dateTime);
    }

    public interface IExecutionContextAccessor
    {
        Guid UserId { get; set; }
    }

    public interface IDocumentRepository
    {
        void Save(Document document);
        Document GetById(Guid docId);
    }


    public class Document
    {
        public Uri AccessLink { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public Guid Id { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<User> Readers { get; set; } = new List<User>();
        public string Title { get; set; }
    }

    public enum DocumentType
    {
        MANUAL,
        QUALITY_BOOK
    }

    public class User
    {
        public Guid Id { get; }
        public object Email { get; set; }

        public User(Guid id)
        {
            Id = id;
        }
    }

    public class Status
    {
        public string Code { get; set; }

        public Status()
        {
        }
    }

    public enum AvailableStatuses
    {
        DRAFT,
        VERIFIED,
        PUBLISHED,
        ARCHIVED
    }

    public class DocumentService
    {
    }

    public class IDontKnow
    {
        public async Task<string> GetSpecialParameter(HttpClient httpClient)
        {
            return (await httpClient.GetFromJsonAsync<List<string>>("api/params")).First();
        }
    }
    
    #endregion
}