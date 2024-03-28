using System;
using CarSpotsReservation;
using Xunit;

public class ParkingTests
{
    [Fact]
    public void given_available_parking_spot_when_reserving_spot_then_reservation_should_be_successful()
    {
        // Arrange
        int capacity = 10;
        Parking parking = new Parking(capacity);
        DateTime start = DateTime.Today.AddHours(10);
        DateTime end = DateTime.Today.AddHours(12);

        // Act
        bool reservationResult = parking.ReserveSpot(3, start, end);

        // Assert
        Assert.True(reservationResult);
    }

    [Fact]
    public void given_occupied_parking_spot_when_attempting_reservation_then_reservation_should_fail()
    {
        // Arrange
        int capacity = 10;
        Parking parking = new Parking(capacity);
        DateTime start = DateTime.Today.AddHours(10);
        DateTime end = DateTime.Today.AddHours(12);
        parking.ReserveSpot(3, start, end);

        // Act
        bool reservationResult = parking.ReserveSpot(3, start, end);

        // Assert
        Assert.False(reservationResult);
    }

    [Fact]
    public void given_occupied_parking_spot_when_releasing_spot_then_spot_should_be_released()
    {
        // Arrange
        int capacity = 10;
        Parking parking = new Parking(capacity);
        DateTime start = DateTime.Today.AddHours(10);
        DateTime end = DateTime.Today.AddHours(12);
        parking.ReserveSpot(3, start, end);

        // Act
        bool releaseResult = parking.ReleaseSpot(3);

        // Assert
        Assert.True(releaseResult);
    }

    [Fact]
    public void given_unoccupied_parking_spot_when_releasing_spot_then_release_should_fail()
    {
        // Arrange
        int capacity = 10;
        Parking parking = new Parking(capacity);

        // Act
        bool releaseResult = parking.ReleaseSpot(3);

        // Assert
        Assert.False(releaseResult);
    }

    [Fact]
    public void given_some_reserved_spots_when_displaying_available_spots_then_should_return_available_spots()
    {
        // Arrange
        int capacity = 10;
        Parking parking = new Parking(capacity);
        DateTime start = DateTime.Today.AddHours(10);
        DateTime end = DateTime.Today.AddHours(12);
        parking.ReserveSpot(3, start, end);

        // Act
        var availableSpots = parking.DisplayAvailableSpots(start, end);

        // Assert
        Assert.NotNull(availableSpots);
        Assert.NotEmpty(availableSpots);
        Assert.DoesNotContain(availableSpots, spot => spot.GetSpotNumber() == 3);
    }

    [Fact]
    public void given_all_spots_available_when_displaying_available_spots_then_should_return_all_spots()
    {
        // Arrange
        int capacity = 10;
        Parking parking = new Parking(capacity);
        DateTime start = DateTime.Today.AddHours(10);
        DateTime end = DateTime.Today.AddHours(12);

        // Act
        var availableSpots = parking.DisplayAvailableSpots(start, end);

        // Assert
        Assert.NotNull(availableSpots);
        Assert.Equal(capacity, availableSpots.Length);
    }
}
