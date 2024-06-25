namespace Acme.Decorator._2;

public class RestServiceThatThrowsExceptions
{
    public string Get()
    {
        // fetches the data, but if something goes wrong...
        throw new Exception("Boom!");
    }
}