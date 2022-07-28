namespace CommunicationSoft;

/*
 * On success, returns "ts", which is posted message unique identifier.
 * On failure returns null.
 */
public interface ISlackClient
{
    Task<int?> PostMessage(string channel, string message);
}
