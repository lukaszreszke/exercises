namespace Acme.Builder._2;

public class EmailSol
{
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public string Recipient { get; private set; }
    public string Sender { get; private set; }
    public List<string> Attachments { get; private set; }

    public EmailSol()
    {
        Attachments = new List<string>();
    }

    internal void SetSubject(string subject) => Subject = subject;
    internal void SetBody(string body) => Body = body;
    internal void SetRecipient(string recipient) => Recipient = recipient;
    internal void SetSender(string sender) => Sender = sender;
    internal void AddAttachment(string attachment) => Attachments.Add(attachment);
}

public class EmailBuilder
{
    private readonly EmailSol _email;

    public EmailBuilder()
    {
        _email = new EmailSol();
    }

    public EmailBuilder WithSubject(string subject)
    {
        _email.SetSubject(subject);
        return this;
    }

    public EmailBuilder WithBody(string body)
    {
        _email.SetBody(body);
        return this;
    }

    public EmailBuilder WithRecipient(string recipient)
    {
        _email.SetRecipient(recipient);
        return this;
    }

    public EmailBuilder WithSender(string sender)
    {
        _email.SetSender(sender);
        return this;
    }

    public EmailBuilder AddAttachment(string attachment)
    {
        _email.AddAttachment(attachment);
        return this;
    }

    public EmailSol Build()
    {
        return _email;
    }
}
