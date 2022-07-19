namespace Venues;

public record VenueParticipant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostCode { get; set; }
    public string IdentityNumber { get; set; }
    public string Email { get; set; }
    public Venue Venue { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsOnReserveList { get; set; }
}