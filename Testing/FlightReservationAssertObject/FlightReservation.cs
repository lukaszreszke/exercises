namespace FlightReservationAssertObject;

public class FlightReservation
{
    public int ReservationId { get; set; }
    public string PassengerName { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureTime { get; set; }
    public bool IsConfirmed { get; private set; }

    public void ConfirmReservation()
    {
        if (!IsConfirmed && DepartureTime > DateTime.Now)
        {
            IsConfirmed = true;
        }
        else
        {
            throw new InvalidOperationException();
        }
    }
}
