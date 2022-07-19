namespace Venues;

public interface IVenuesRepository
{
    Venue GetVenueByName(string name);
    void AddVenueParticipant(VenueParticipant participant);
}