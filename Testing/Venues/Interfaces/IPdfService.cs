namespace Venues;

public interface IPdfService
{
    MemoryStream generatePdf(VenueParticipant participant);
}