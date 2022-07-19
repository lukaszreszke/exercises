namespace Venues;

public interface IMailService
{
    void SendConfirmationEmail(VenueParticipant participant, MemoryStream participantCard, string message);
    void SendReserveListEmail(VenueParticipant participant, string venueReserveListEmail);
}