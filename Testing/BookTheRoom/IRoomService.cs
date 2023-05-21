namespace BookTheRoom;

public interface IRoomService
{
    bool IsRoomAvailable(int roomId, DateTime startDate, DateTime endDate);
    void BookRoom(int roomId, DateTime startDate, DateTime endDate);
}