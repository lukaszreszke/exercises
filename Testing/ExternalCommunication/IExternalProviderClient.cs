using System.Net;

namespace ExternalCommunication;

public interface IExternalProviderClient
{
    HttpStatusCode Complete(int orderId);
    HttpStatusCode Cancel(int orderId);
    HttpStatusCode AddParticipant(string userEmail);
    HttpStatusCode Sign(string userEmail, int orderId);
    HttpStatusCode Create(int signOrderId);
}