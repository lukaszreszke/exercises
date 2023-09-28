namespace Tests.Documents
{
    public class Document
    {
        public Uri AccessLink { get; set; }
        public Uri PdfAccessLink { get; set; }
        public double PdfVersion { get; set; }
        public Status Status { get; set; }
        public User User { get; set; }
        public Guid Id { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<User> Readers { get; set; } = new List<User>();
        public string Title { get; set; }
        public string Content { get; set; }
    }
}