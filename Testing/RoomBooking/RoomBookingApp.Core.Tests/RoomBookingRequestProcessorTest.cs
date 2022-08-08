using Moq;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Core.Models;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace RoomBookingApp.Core
{
    public class RoomBookingRequestProcessorTest
    {
        private RoomBookingRequestProcessor _processor;
        private RoomBookingRequest _request;
        private Mock<IRoomBookingService> _roomBookingServiceMock;
        private List<Room> _availableRooms;

        public RoomBookingRequestProcessorTest()
        {
            //Arrange
            _request = new RoomBookingRequest
            {
                FullName = "Test Name",
                Email = "test@request.com",
                Date = new DateTime(2021, 10, 20),
                Id = 1
            };
            _availableRooms = new List<Room>() { new Room() { Id = 1 } };

            _roomBookingServiceMock = new Mock<IRoomBookingService>();
            _roomBookingServiceMock.Setup(q => q.GetAvailableRooms(_request.Date))
                .Returns(_availableRooms);

            _processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
        }

        [Fact]
        public void Should_Return_Room_Booking_Response_With_Request_Values()
        {
            //Act
            RoomBookingResult result = _processor.BookRoom(_request);

            //Assert
            result.ShouldNotBeNull();
            result.FullName.ShouldBe(_request.FullName);
            result.Email.ShouldBe(_request.Email);
            result.Date.ShouldBe(_request.Date);
        }

        [Fact]
        public void Should_Throw_Exception_For_Null_Request()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));
            exception.ParamName.ShouldBe("bookingRequest");
        }

        [Fact]
        public void Should_Save_Room_Booking_Request()
        {
            var roomId = 1;

            _roomBookingServiceMock.Setup(x => x.GetRoom(1)).Returns(new Room
                { Id = 1, Name = "Romantika", RoomBookings = new List<RoomBooking>() });

            var bookingDate = DateTime.UtcNow.AddDays(1).Date;
            
            var result = _processor.BookRoom(new RoomBookingRequest
            {
                Date = bookingDate, Email = "email@example.com", FullName = "Jan Kowalski", Id = roomId
            });

            result.Flag.ShouldBe(BookingResultFlag.Success);
            result.Email.ShouldBe("email@example.com");
            result.FullName.ShouldBe("Jan Kowalski");
            result.Date.Date.ShouldBe(bookingDate);
        }

        [Fact]
        public void Should_Not_Save_Room_Booking_Request_If_None_Available()
        {
            _availableRooms.Clear();
            _processor.BookRoom(_request);
            _roomBookingServiceMock.Verify(q => q.SaveBooking(It.IsAny<RoomBooking>()), Times.Never);
        }
    }
}