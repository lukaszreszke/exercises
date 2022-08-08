﻿using RoomBookingApp.Domain;
using System;
using System.Collections.Generic;

namespace RoomBookingApp.Core.DataServices
{
    public interface IRoomBookingService
    {
        void SaveBooking(RoomBooking roomBooking);

        IEnumerable<Room> GetAvailableRooms(DateTime date);
        Room GetRoom(int id);
        IEnumerable<Room> GetRooms();
    }
}
