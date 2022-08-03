using FlyRadar.Domain;
using Newtonsoft.Json;
using RestSharp;

namespace FlyRadar.Services;

public static class CheapFlightsTwoRestClient
{
    public static List<CheapAirLinesTwoFlight> Get(string fromCity, string fromCountry, string country, string city,
        DateTime dateFrom, DateTime dateTo)
    {
        var request = new RestRequest("/economy_class");
        request.Parameters.AddParameter(new QueryParameter("from_country", fromCountry));
        request.Parameters.AddParameter(new QueryParameter("country", country));
        request.Parameters.AddParameter(new QueryParameter("city", city));
        request.Parameters.AddParameter(new QueryParameter("from_city", fromCity));
        request.Parameters.AddParameter(new QueryParameter("from", dateFrom.ToShortDateString()));
        request.Parameters.AddParameter(new QueryParameter("to", dateTo.ToShortDateString()));

        var result = new RestClient("https://cheapairlinetwo.com").Get(request);

        return result.IsSuccessful
            ? JsonConvert.DeserializeObject<List<CheapAirLinesTwoFlight>>(result.Content)
            : new List<CheapAirLinesTwoFlight>();
    }
}