using System.Collections.Generic;
using System.Linq;

namespace RoomBookingApp.Domain
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<RoomBooking> RoomBookings { get; set; }
    }

    public static class RoomExtensions
    {
        public static int[] AllRoomBookings(this Room room) => room.RoomBookings.Select(x => x.Id).ToArray();
    }
}