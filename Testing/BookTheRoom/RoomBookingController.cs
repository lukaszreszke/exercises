namespace BookTheRoom;

public class RoomBookingController
{
    private IRoomService _roomService;

    public RoomBookingController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    public void HandleBooking(BookingRequest bookingRequest)
    {
        if (_roomService.IsRoomAvailable(
                bookingRequest.RoomId, 
                bookingRequest.StartDate, 
                bookingRequest.EndDate))
        {
            _roomService.BookRoom(
                bookingRequest.RoomId, 
                bookingRequest.StartDate, 
                bookingRequest.EndDate);
        }
        else
        {
            throw new Exception("Room is not available for the given dates.");
        }
    }
}