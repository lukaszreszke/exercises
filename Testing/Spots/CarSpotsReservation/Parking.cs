namespace CarSpotsReservation;

public class Parking
{
    private ParkingSpot[] spots;

    public Parking(int capacity)
    {
        spots = new ParkingSpot[capacity];
        for (int i = 0; i < capacity; i++)
        {
            spots[i] = new ParkingSpot(i + 1);
        }
    }

    public bool ReserveSpot(int spotNumber, DateTime start, DateTime end)
    {
        if (spotNumber < 1 || spotNumber > spots.Length)
        {
            return false;
        }

        ParkingSpot spot = spots[spotNumber - 1];
        if (spot.IsOccupied() || IsSpotReservedDuring(spotNumber, start, end))
        {
            return false;
        }

        spot.Reserve(start, end);
        return true;
    }

    public bool ReleaseSpot(int spotNumber)
    {
        if (spotNumber < 1 || spotNumber > spots.Length)
        {
            return false;
        }

        ParkingSpot spot = spots[spotNumber - 1];
        if (!spot.IsOccupied())
        {
            return false;
        }

        return true;
    }

    private bool IsSpotReservedDuring(int spotNumber, DateTime start, DateTime end)
    {
        foreach (var spot in spots)
        {
            if (spot.GetSpotNumber() == spotNumber && spot.IsReservedDuring(start, end))
                return true;
        }

        return false;
        
    }

    public ParkingSpot[] DisplayAvailableSpots(DateTime start, DateTime end)
    {
        return spots.Where(spot => !spot.IsOccupied() || !spot.IsReservedDuring(start, end)).ToArray();
    }
}