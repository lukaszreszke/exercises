namespace Acme.Decorator;

public class RestService  
{
    public string Get(string id)
    {
        // real call 
        return $"Hello {id} World";
    }
}