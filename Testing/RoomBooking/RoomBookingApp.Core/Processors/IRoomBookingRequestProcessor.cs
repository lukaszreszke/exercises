using System;
using System.Collections.Generic;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain;

namespace RoomBookingApp.Core.Processors
{
    public interface IRoomBookingRequestProcessor
    {
        RoomBookingResult BookRoom(RoomBookingRequest bookingRequest);

        IEnumerable<Room> Rooms();
        IEnumerable<Room> AvailableRooms(DateTime date);
        Room Room(int id);
    }
}