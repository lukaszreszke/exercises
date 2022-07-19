using FluentAssertions;
using Xunit;

namespace Venues;

public class Venue
{
    public int Id { get; set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public string Place { get; set; }
    public int MaxParticipants { get; set; }
    public ICollection<VenueParticipant> Participants { get; set; }
    public string ConfirmationEmail { get; set; }
    public string ReserveListEmail { get; set; }
    public int Order { get; set; }
    public string AgeCategory { get; set; }
    public int Price { get; set; }
}

public class VenueParticipants
{
    public int VenueId { get; }
    public ICollection<VenueParticipant> Participants { get; }
    public int MaxParticipants { get; }

    public VenueParticipants(int maxParticipants)
    {
        MaxParticipants = maxParticipants;
        VenueId = Random.Shared.Next();
        Participants = new List<VenueParticipant>();
    }
    
    public void Register(VenueParticipant participant)
    {
        if(Participants.Count < MaxParticipants)
            Participants.Add(participant);
    }
}

public class VenueParticipantsTests
{
    [Fact]
    public void can_register()
    {
        // Given
        var venue = new VenueParticipants(2);
        venue.Register(new VenueParticipant());
        
        //When
        venue.Register(new VenueParticipant());
        
        //Then
        venue.Participants.Count.Should().Be(2);
    }

    [Fact]
    public void cannot_register_because_of_limit()
    {
        // Given
        var venue = new VenueParticipants(1);
        venue.Register(new VenueParticipant());
        
        //When
        venue.Register(new VenueParticipant());
        
        //Then
        venue.Participants.Count.Should().Be(1);
    }
}