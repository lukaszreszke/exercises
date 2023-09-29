namespace Tests.Documents;

public interface IEmailGateway
{
    void SendEmail(object email, object documentAccessLink);
    void ScheduleEmail(object readerEmail, string s, TimeOnly dateTime);
}