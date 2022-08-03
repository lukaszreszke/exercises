using FlyRadar.Domain;

namespace FlyRadar.Services;

public interface IFetchFlightService
{
    public List<Flight> Fetch(string fromCity, string fromCountry, string filterByCity, string filterByCountry,
        DateTime dateFrom, DateTime dateTo);
}