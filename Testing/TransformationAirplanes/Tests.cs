namespace TransformationAirplanes;

public class Tests
{
    [Fact]
    public void TransformAirlineToAirportDetails_ShouldCreateCorrectDetails()
    {
        var airlineDetails = new AirlineFlightDetails
        {
            FlightNumber = "AA123",
            PlaneModel = "Boeing 737",
            DepartureTime = new DateTime(2023, 6, 1, 10, 0, 0),
            TotalSeats = 200,
            BookedSeats = 50
        };

        var airportDetails = new Transform().TransformAirlineToAirportDetails(airlineDetails);

        Assert.Equal("AA123", airportDetails.FlightCode);
        Assert.Equal("Boeing 737", airportDetails.AircraftType);
        Assert.Equal(new DateTime(2023, 6, 1, 10, 0, 0), airportDetails.Departure);
        Assert.Equal(200, airportDetails.SeatCapacity);
        Assert.Equal(50, airportDetails.ReservedSeats);
    }

}