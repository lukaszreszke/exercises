using FlightReservationAssertObject;

namespace FlightReservationAssertObjectTests;

public static class FlightReservationAssertObject
{
    public static void AssertFlightReservation(this FlightReservation actualReservation, int expectedReservationId, string expectedPassengerName, string expectedFlightNumber, DateTime expectedDepartureTime, bool expectedIsConfirmed)
    {
        Assert.Equal(expectedReservationId, actualReservation.ReservationId);
        Assert.Equal(expectedPassengerName, actualReservation.PassengerName);
        Assert.Equal(expectedFlightNumber, actualReservation.FlightNumber);
        Assert.Equal(expectedDepartureTime, actualReservation.DepartureTime);
        Assert.Equal(expectedIsConfirmed, actualReservation.IsConfirmed);
    }
}
