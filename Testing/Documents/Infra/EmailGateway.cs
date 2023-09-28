namespace Tests.Documents;

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