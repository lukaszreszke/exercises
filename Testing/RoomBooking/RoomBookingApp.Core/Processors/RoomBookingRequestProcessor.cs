using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Domain;
using RoomBookingApp.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoomBookingApp.Core.Processors
{
    public class RoomBookingRequestProcessor : IRoomBookingRequestProcessor
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }
            var theRoom = _roomBookingService.GetRoom(bookingRequest.Id);
            var result = CreateRoomBookingObject<RoomBookingResult>(bookingRequest);

            if (theRoom != null && theRoom.RoomBookings.All(x => x.Date != bookingRequest.Date))
            {
                var roomBooking = CreateRoomBookingObject<RoomBooking>(bookingRequest);
                roomBooking.RoomId = theRoom.Id;
                _roomBookingService.SaveBooking(roomBooking);

                result.RoomBookingId = roomBooking.Id;
                result.Flag = BookingResultFlag.Success;
            }
            else
            {
                result.Flag = BookingResultFlag.Failure;
            }

            return result;
        }

        public IEnumerable<Room> Rooms()
        {
            return _roomBookingService.GetRooms();
        }

        public IEnumerable<Room> AvailableRooms(DateTime date)
        {
            return _roomBookingService.GetAvailableRooms(date);
        }

        public Room Room(int id)
        {
            return _roomBookingService.GetRoom(id);
        }

        private static TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest) where TRoomBooking
            : RoomBookingBase, new()
        {
            return new TRoomBooking
            {
                FullName = bookingRequest.FullName,
                Date = bookingRequest.Date,
                Email = bookingRequest.Email,
            };
        }
    }
}