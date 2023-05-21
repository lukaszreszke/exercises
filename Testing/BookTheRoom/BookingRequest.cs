namespace BookTheRoom;

public class BookingRequest
{
    public int RoomId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}