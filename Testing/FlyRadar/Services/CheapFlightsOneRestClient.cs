using System.Net;
using FlyRadar.Domain;
using Newtonsoft.Json;
using RestSharp;

namespace FlyRadar.Services;

public class CheapFlightsOneRestClient
{
    private readonly RestClient _client = new RestClient("https://cheapflights.com");

    public List<CheapAirLinesOneFlight> Get(string fromCity, string fromCountry, string destinatedCountry,
        string destinatedCity, DateTime dateFrom, DateTime dateTo)
    {
        var request = new RestRequest("/economy_class");
        request.Parameters.AddParameter(new QueryParameter("from_country", fromCountry));
        request.Parameters.AddParameter(new QueryParameter("to_country", destinatedCountry));
        request.Parameters.AddParameter(new QueryParameter("to_city", destinatedCity));
        request.Parameters.AddParameter(new QueryParameter("from_city", fromCity));
        request.Parameters.AddParameter(new QueryParameter("from", dateFrom.ToShortDateString()));
        request.Parameters.AddParameter(new QueryParameter("to", dateTo.ToShortDateString()));

        var result = _client.Get(request);

        if (result.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<List<CheapAirLinesOneFlight>>(result.Content);
        }

        switch (result.StatusCode)
        {
            case HttpStatusCode.NotFound:
                throw new CheapAirLineOneNotFoundException(result.ErrorMessage);
            case HttpStatusCode.BadRequest:
                throw new CheapAirLineOneBadRequest(result.ErrorMessage);
            default:
                throw new UnknownException(result.ErrorMessage);
        }
    }
}

public class CheapAirLineOneNotFoundException : Exception
{
    public CheapAirLineOneNotFoundException(string? resultErrorMessage) : base(resultErrorMessage)
    {
    }
}

public class CheapAirLineOneBadRequest : Exception
{
    public CheapAirLineOneBadRequest(string? resultErrorMessage) : base(resultErrorMessage)
    {
    }
}

public class UnknownException : Exception
{
    public UnknownException(string? resultErrorMessage) : base(resultErrorMessage)
    {
    }
}