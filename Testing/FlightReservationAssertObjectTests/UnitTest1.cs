using System;
using FlightReservationAssertObject;
using FlightReservationAssertObjectTests;
using Xunit;

public class FlightReservationTests
{
    [Fact]
    public void given_unconfirmed_reservation_when_confirmation_requested_and_departure_in_future_then_confirm_reservation()
    {
        // Arrange
        var departureTime = DateTime.Now.AddDays(7);
        var reservation = new FlightReservation
        {
            ReservationId = 1,
            PassengerName = "John Doe",
            FlightNumber = "ABC123",
            DepartureTime = departureTime,
        };

        // Act
        reservation.ConfirmReservation();

        // Assert
        Assert.True(reservation.IsConfirmed);
        reservation.AssertFlightReservation(1, "John Doe", "ABC123", departureTime, true);
    }

    [Fact]
    public void given_confirmed_reservation_when_confirmation_requested_then_throw_exception()
    {
        // Arrange
        var reservation = new FlightReservation
        {
            ReservationId = 2,
            PassengerName = "Jane Doe",
            FlightNumber = "XYZ789",
            DepartureTime = DateTime.Now.AddDays(14),
        };
        reservation.ConfirmReservation();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => reservation.ConfirmReservation());
    }

    [Fact]
    public void given_unconfirmed_reservation_when_confirmation_requested_and_departure_in_past_then_throw_exception()
    {
        // Arrange
        var reservation = new FlightReservation
        {
            ReservationId = 3,
            PassengerName = "Bob",
            FlightNumber = "DEF456",
            DepartureTime = DateTime.Now.AddDays(-1),
        };

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => reservation.ConfirmReservation());
    }
}