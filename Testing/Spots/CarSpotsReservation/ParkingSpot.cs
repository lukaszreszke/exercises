namespace CarSpotsReservation;

public class ParkingSpot
{
    private int spotNumber;
    private bool isOccupied;
    private DateTime reservationStart;
    private DateTime reservationEnd;

    public ParkingSpot(int spotNumber)
    {
        this.spotNumber = spotNumber;
        isOccupied = false;
        reservationStart = DateTime.MinValue;
        reservationEnd = DateTime.MinValue;
    }

    public int GetSpotNumber()
    {
        return spotNumber;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void Reserve(DateTime start, DateTime end)
    {
        isOccupied = true;
        reservationStart = start;
        reservationEnd = end;
    }

    public void Release()
    {
        isOccupied = false;
        reservationStart = DateTime.MinValue;
        reservationEnd = DateTime.MinValue;
    }

    public bool IsReservedDuring(DateTime start, DateTime end)
    {
        if (start >= reservationStart && start < reservationEnd)
            return true;
        if (end > reservationStart && end <= reservationEnd)
            return true;
        if (start <= reservationStart && end >= reservationEnd)
            return true;
        return false;
    }
}