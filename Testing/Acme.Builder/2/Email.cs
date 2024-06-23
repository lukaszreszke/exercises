namespace Acme.Builder._2;

public class Email
{
    public string Subject { get; private set; }
    public string Body { get; private set; }
    public string Recipient { get; private set; }
    public string Sender { get; private set; }
    public List<string> Attachments { get; private set; }

    public Email(string subject)
    {
        Subject = subject;
    }

    public Email(string subject, string body)
    {
        Subject = subject;
        Body = body;
    }

    public Email(string subject, string body, string recipient)
    {
        Subject = subject;
        Body = body;
        Recipient = recipient;
    }

    public Email(string subject, string body, string recipient, string sender)
    {
        Subject = subject;
        Body = body;
        Recipient = recipient;
        Sender = sender;
    }

    public Email(string subject, string body, string recipient, string sender, List<string> attachments)
    {
        Subject = subject;
        Body = body;
        Recipient = recipient;
        Sender = sender;
        Attachments = attachments;
    }
}
