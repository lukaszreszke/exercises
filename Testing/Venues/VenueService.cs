namespace Venues;

public class VenueService : IVenueService
{
    private readonly IVenuesRepository _venuesRepository;
    private readonly IMailService _mailService;
    private readonly IPdfService _pdfService;

    public VenueService(IVenuesRepository venuesRepository,
        IMailService mailService,
        IPdfService pdfService)
    {
        _venuesRepository = venuesRepository;
        _mailService = mailService;
        _pdfService = pdfService;
    }

    public void RegisterToVenue(VenueRegistrationDto venueRegistration)
    {
        var ev = _venuesRepository.GetVenueByName(venueRegistration.VenueName);
        var isReserveRegister = ev != null && ev.Participants.Count >= ev.MaxParticipants;

        var participant = new VenueParticipant()
        {
            Name = venueRegistration.Name,
            Surname = venueRegistration.Surname,
            Street = venueRegistration.Street,
            City = venueRegistration.City,
            PostCode = venueRegistration.PostCode,
            IdentityNumber = venueRegistration.IdentityNumber,
            Email = venueRegistration.Email,
            Venue = ev,
            RegisterDate = DateTime.Now,
            IsOnReserveList = isReserveRegister
        };

        _venuesRepository.AddVenueParticipant(participant);

        if (!isReserveRegister)
        {
            var venueCard = _pdfService.generatePdf(participant);
            _mailService.SendConfirmationEmail(participant, venueCard, participant.Venue.ConfirmationEmail);
        }
        else
        {
            _mailService.SendReserveListEmail(participant, participant.Venue.ReserveListEmail);
        }
    }
}