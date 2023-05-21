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