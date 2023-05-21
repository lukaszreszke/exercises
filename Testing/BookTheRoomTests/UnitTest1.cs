using BookTheRoom;

namespace BookTheRoomTests;

using Moq;
using Xunit;

public class RoomBookingControllerTests
{
    [Fact]
    public void HandleBooking_WhenRoomIsAvailable_ShouldBookTheRoom()
    {
        // Arrange
        var roomId = 1;
        var startDate = new DateTime(2023, 1, 1);
        var endDate = new DateTime(2023, 1, 7);
        var bookingRequest = new BookingRequest
        {
            RoomId = roomId,
            StartDate = startDate,
            EndDate = endDate
        };

        var roomServiceMock = new Mock<IRoomService>();
        roomServiceMock.Setup(service => service.IsRoomAvailable(roomId, startDate, endDate))
            .Returns(true);

        var roomBookingController = new RoomBookingController(roomServiceMock.Object);

        // Act
        roomBookingController.HandleBooking(bookingRequest);

        // Assert
        roomServiceMock.Verify(service => service.IsRoomAvailable(roomId, startDate, endDate), Times.Once);
        roomServiceMock.Verify(service => service.BookRoom(roomId, startDate, endDate), Times.Once);
        roomServiceMock.VerifyNoOtherCalls();
    }
    
    [Fact]
    public void HandleBooking_WhenRoomIsNotAvailable_ShouldThrowException()
    {
        // Arrange
        var roomId = 1;
        var startDate = new DateTime(2023, 1, 1);
        var endDate = new DateTime(2023, 1, 7);
        var bookingRequest = new BookingRequest
        {
            RoomId = roomId,
            StartDate = startDate,
            EndDate = endDate
        };

        var roomServiceMock = new Mock<IRoomService>();
        roomServiceMock.Setup(service => service.IsRoomAvailable(roomId, startDate, endDate))
            .Returns(false);

        var roomBookingController = new RoomBookingController(roomServiceMock.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => roomBookingController.HandleBooking(bookingRequest));
    }
}
