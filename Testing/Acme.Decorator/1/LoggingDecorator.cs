namespace Acme.Decorator;

public class LoggingDecorator : RestService
{
    private readonly RestService _restService;
    private readonly ILogger _logger;

    public LoggingDecorator(RestService restService, ILogger logger)
    {
        _restService = restService;
        _logger = logger;
    }

    public string Get(string id)
    {
        _logger.Log("Before call");
        var result = _restService.Get(id);
        _logger.Log("After call");
        return result;
    }
}