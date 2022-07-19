using Moq;
using Xunit;

namespace Venues;

public class VenuesTests
{
    [Fact]
    public void Test_register_to_venue()
    {
        var mailService = new Mock<IMailService>();
        var pdfService = new Mock<IPdfService>();
        var repo = new Mock<IVenuesRepository>();
        var venue = Venue(3);
        var expectedParticipant = ExpectedParticipant(venue);
        pdfService.Setup(x => x.generatePdf(expectedParticipant)).Returns(new MemoryStream());
        repo.Setup(x => x.GetVenueByName("Test")).Returns(venue);
        var venueService = new VenueService(repo.Object, mailService.Object, pdfService.Object);

        venueService.RegisterToVenue(VenueRegistrationDto);

        mailService.Verify(x => x.SendConfirmationEmail(
            It.Is(expectedParticipant, new ParticipantComparer()), null,
            expectedParticipant.Venue.ConfirmationEmail), Times.Once);
        pdfService.Verify(x => x.generatePdf(It.Is(expectedParticipant, new ParticipantComparer())), Times.Once);
        repo.Verify(x => x.AddVenueParticipant(It.Is(expectedParticipant, new ParticipantComparer())), Times.Once);
    }
    
    [Fact]
    public void Test_register_to_venue_no_success()
    {
        var mailService = new Mock<IMailService>();
        var pdfService = new Mock<IPdfService>();
        var repo = new Mock<IVenuesRepository>();
        var venue = Venue(0);
        var expectedParticipant = ExpectedParticipant(venue);
        pdfService.Setup(x => x.generatePdf(expectedParticipant)).Returns(new MemoryStream());
        repo.Setup(x => x.GetVenueByName("Test")).Returns(venue);
        var venueService = new VenueService(repo.Object, mailService.Object, pdfService.Object);

        venueService.RegisterToVenue(VenueRegistrationDto);

        mailService.Verify(x => x.SendConfirmationEmail(
            It.Is(ExpectedParticipant(venue), new ParticipantComparer()), null,
            expectedParticipant.Venue.ConfirmationEmail), Times.Once);
        pdfService.Verify(x => x.generatePdf(It.Is(expectedParticipant, new ParticipantComparer())), Times.Once);
        repo.Verify(x => x.AddVenueParticipant(It.Is(expectedParticipant, new ParticipantComparer())), Times.Once);
    }

    private class ParticipantComparer : IEqualityComparer<VenueParticipant>
    {
        public bool Equals(VenueParticipant x, VenueParticipant y)
        {
            return x.Id == y.Id && x.Name == y.Name && x.Surname == y.Surname && x.Street == y.Street &&
                   x.City == y.City && x.PostCode == y.PostCode && x.IdentityNumber == y.IdentityNumber &&
                   x.Email == y.Email &&
                   x.IsOnReserveList == y.IsOnReserveList;
        }

        public int GetHashCode(VenueParticipant obj)
        {
            return obj.GetHashCode();
        }
    }

    private VenueRegistrationDto VenueRegistrationDto => new VenueRegistrationDto
    {
        VenueName = "Test",
        City = "Test",
        Email = "Test",
        IdentityNumber = "12345",
        Name = "Test",
        Surname = "Test",
        PostCode = "Test",
        Street = "Test"
    };

    private static VenueParticipant ExpectedParticipant(Venue venue) => new VenueParticipant
    {
        Name = "Test",
        Surname = "Test",
        Street = "Test",
        City = "Test",
        PostCode = "Test",
        IdentityNumber = "12345",
        Email = "Test",
        Venue = venue,
        RegisterDate = DateTime.Now,
        IsOnReserveList = false
    };

    private static Venue Venue(int maxParticipants) => new Venue
    {
        From = DateTime.UtcNow,
        Id = 1234,
        Name = "Test",
        Order = 1,
        Participants = new List<VenueParticipant>(),
        Place = "Palace",
        Price = 12345,
        AgeCategory = "19-20",
        ConfirmationEmail = "confirmation@awesomevenues.br",
        MaxParticipants = 3
    };
}