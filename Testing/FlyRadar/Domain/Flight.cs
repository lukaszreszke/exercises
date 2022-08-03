namespace FlyRadar.Domain;

public class Flight
{
    public int Id { get; set; }
    public string DestinationCity { get; set; }
    public string DestinationCountry { get; set; }
    public string OriginCity { get; set; }
    public string OriginCountry { get; set; }
    public DateTime LeavesAt { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
}

public class CheapAirLinesOneFlight
{
    public int Id { get; set; }
    public DateTime LeavesAt { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string LeavesFrom { get; set; }
    public string LeavesFromCountry { get; set; }
}

public class CheapAirLinesTwoFlight
{
    public string StartsFromCountry { get; set; }
    public string StartsFromCity { get; set; }
    public DateTime LeavesAt { get; set; }
    public string DestinationCountry { get; set; }
    public string Currency { get; set; }
    public decimal Price { get; set; }
    public string DestinationCity { get; set; }
}