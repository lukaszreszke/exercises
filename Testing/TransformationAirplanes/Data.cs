namespace TransformationAirplanes;

public class AirlineFlightDetails
{
    public string FlightNumber { get; set; }
    public string PlaneModel { get; set; }
    public DateTime DepartureTime { get; set; }
    public int TotalSeats { get; set; }
    public int BookedSeats { get; set; }
}

public class AirportFlightDetails
{
    public string FlightCode { get; set; }
    public string AircraftType { get; set; }
    public DateTime Departure { get; set; }
    public int SeatCapacity { get; set; }
    public int ReservedSeats { get; set; }

}

public class Transform
{
    public AirportFlightDetails TransformAirlineToAirportDetails(AirlineFlightDetails airlineDetails)
    {
        return new AirportFlightDetails
        {
            FlightCode = airlineDetails.FlightNumber,
            AircraftType = airlineDetails.PlaneModel,
            Departure = airlineDetails.DepartureTime,
            SeatCapacity = airlineDetails.TotalSeats,
            ReservedSeats = airlineDetails.BookedSeats
        };
    }
}
