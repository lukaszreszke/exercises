using System.Text.Json.Serialization;
using FlyRadar.Domain;

namespace FlyRadar.Services;

public class FetchFlightService : IFetchFlightService
{
    public List<Flight> Fetch(string fromCity, string fromCountry, string filterByCity, string filterByCountry,
        DateTime dateFrom, DateTime dateTo)
    {
        var flights = new List<Flight>();
        var cheapOneClient =
            new CheapFlightsOneRestClient().Get(fromCity, fromCountry, filterByCountry, filterByCity, dateFrom, dateTo);
        var cheapTwoFlights =
            CheapFlightsTwoRestClient.Get(fromCity, fromCountry, filterByCountry, filterByCity, dateFrom, dateTo);

        flights.AddRange(cheapOneClient.Select(x => new Flight
        {
            DestinationCity = filterByCity, DestinationCountry = filterByCountry, Price = x.Price,
            Currency = x.Currency,
            LeavesAt = x.LeavesAt,
            OriginCity = x.LeavesFrom,
            OriginCountry = x.LeavesFromCountry
        }));

        flights.AddRange(cheapTwoFlights.Select(x => new Flight
        {
            Currency = x.Currency, Price = x.Price, DestinationCity = x.DestinationCity,
            DestinationCountry = x.DestinationCountry, LeavesAt = x.LeavesAt, OriginCity = x.StartsFromCity,
            OriginCountry = x.StartsFromCountry
        }));

        return flights;
    }
}
