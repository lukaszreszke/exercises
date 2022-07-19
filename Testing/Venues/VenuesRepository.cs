using Microsoft.EntityFrameworkCore;

namespace Venues;

public class VenuesRepository : IVenuesRepository
{
    private readonly VenuesContext _venuesContext;

    public VenuesRepository(VenuesContext venuesContext)
    {
        _venuesContext = venuesContext;
    }

    public Venue GetVenueByName(string name)
    {
        return _venuesContext.Venues
            .Include(e => e.Participants)
            .FirstOrDefault(x => x.ShortName == name);
    }

    public void AddVenueParticipant(VenueParticipant participant)
    {
        _venuesContext.VenueParticipants.Add(participant);
        _venuesContext.SaveChanges();
    }
}