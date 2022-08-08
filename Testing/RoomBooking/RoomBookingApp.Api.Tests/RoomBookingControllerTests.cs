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
        public async Task Should_not_allow_overlapping_bookings()
        {
            var httpContextAccessor = new Mock<IHttpContextAccessor>();
            httpContextAccessor.Setup(x => x.HttpContext.User).Returns(new ClaimsPrincipal());
            var dbContext = new RoomBookingAppDbContext(new DbContextOptionsBuilder<RoomBookingAppDbContext>()
                .UseInMemoryDatabase("test").Options);
            dbContext.Rooms.Add(new Room { Name = "Romantica", RoomBookings = new List<RoomBooking>() });
            dbContext.SaveChanges();

            var controller =
                new RoomBookingController(new RoomBookingRequestProcessor(new RoomBookingService(dbContext)));

            await controller.BookRoom(new RoomBookingRequest
            {
                Date = DateTime.UtcNow.AddDays(1).Date, Email = "example@example.com", FullName = "Jan Kowalski", Id = 1
            });

            var result = await controller.BookRoom(new RoomBookingRequest
            {
                Date = DateTime.UtcNow.AddDays(1).Date, Email = "example@example.com", FullName = "Jan Kowalski", Id = 1
            });

            result.ShouldBeOfType<BadRequestObjectResult>();
        }
    }
}