namespace Venues.Interfaces;

public interface IMessageSession
{
    void Publish(object message);
}