namespace CommunicationSoft;

/*
 * On success does nothing
 */
public interface IMicrosoftTeamsClient
{
    Task AddAsync(string channel, string message);
}