using FlightReservationAssertObject;

namespace FlightReservationAssertObjectTests;

public static class FlightReservationAssertObject
{
    public static void HasCorrectReservationId(this FlightReservation actual, int reservationId)
    {
        Assert.Equal(reservationId, actual.ReservationId);
    }
    
    public static void HasCorrectPassengerName(this FlightReservation actual, string passengerName)
    {
        Assert.Equal(passengerName, actual.PassengerName);
    }
    
    public static void HasCorrectFlightNumber(this FlightReservation actual, string flightNumber)
    {
        Assert.Equal(flightNumber, actual.FlightNumber);
    }
    
    public static void HasCorrectDepartureTime(this FlightReservation actual, DateTime departureTime)
    {
        Assert.Equal(departureTime, actual.DepartureTime);
    }
    
    public static void HasCorrectConfirmed(this FlightReservation actual,bool confirmed)
    {
        Assert.Equal(confirmed, actual.IsConfirmed);
    }
}
