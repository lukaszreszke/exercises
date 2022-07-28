namespace CommunicationSoft;

public interface IMicrosoftTeamsClient
{
    Task AddAsync(string channel, string message);
}