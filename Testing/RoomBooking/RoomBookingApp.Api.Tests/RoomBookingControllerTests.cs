using Moq;
using RoomBookingApp.Api.Controllers;
using System;
using System.Collections.Generic;
using Xunit;
using Shouldly;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RoomBookingApp.Core.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Core.Enums;
using RoomBookingApp.Domain;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

namespace RoomBookingApp.Api.Tests
{
    public class RoomBookingControllerTests
    {
        private Mock<IRoomBookingRequestProcessor> _roomBookingProcessor;
        private RoomBookingController _controller;
        private RoomBookingRequest _request;
        private RoomBookingResult _result;

        public RoomBookingControllerTests()
        {
            _roomBookingProcessor = new Mock<IRoomBookingRequestProcessor>();
            _controller = new RoomBookingController(_roomBookingProcessor.Object);
            _request = new RoomBookingRequest();
            _result = new RoomBookingResult();

            _roomBookingProcessor.Setup(x => x.BookRoom(_request)).Returns(_result);
        }

        [Theory]
        [InlineData(1, true, typeof(OkObjectResult), BookingResultFlag.Success)]
        [InlineData(0, false, typeof(BadRequestObjectResult), BookingResultFlag.Failure)]
        public async Task Should_Call_Booking_Method_When_Valid(int expectedMethodCalls, bool isModelValid,
            Type expectedActionResultType, BookingResultFlag bookingResultFlag)
        {
            // Arrange
            if (!isModelValid)
            {
                _controller.ModelState.AddModelError("Key", "ErrorMessage");
            }

            _result.Flag = bookingResultFlag;


            // Act
            var result = await _controller.BookRoom(_request);

            // Assert
            result.ShouldBeOfType(expectedActionResultType);
            _roomBookingProcessor.Verify(x => x.BookRoom(_request), Times.Exactly(expectedMethodCalls));
        }

        [Fact]
        public async Task should_return_rooms()
        {
            var processor = new Mock<IRoomBookingRequestProcessor>();
            var rooms = new List<Room>
                { new Room { Id = 1, Name = "mhm", RoomBookings = null } };
            processor.Setup(x => x.Rooms()).Returns(rooms);
            var controller = new RoomBookingController(processor.Object);

            // Act
            var result = await controller.GetRooms();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<List<Room>>(okResult.Value);
            value.ShouldAllBe(x => rooms.Contains(x));
            value.ShouldBeEquivalentTo(rooms);
        }

        [Fact]
        public async Task should_return_only_available_rooms_for_given_date()
        {
            var processor = new Mock<IRoomBookingRequestProcessor>();
            var tomorrow = DateTime.UtcNow.AddDays(1);
            var rooms = new List<Room>
            {
                new() { Id = 1, Name = "mhm", RoomBookings = null },
            };
            processor.Setup(x => x.AvailableRooms(tomorrow)).Returns(rooms);
            var controller = new RoomBookingController(processor.Object);

            // Act
            var result = await controller.GetAvailableRooms(tomorrow);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<List<Room>>(okResult.Value);
            value.ShouldAllBe(x => rooms.Contains(x));
            value.ShouldBeEquivalentTo(rooms);
        }
    }
}