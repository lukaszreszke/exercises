namespace Acme.Builder;

public class ConfigurationBuilder
{
    private readonly Configuration _configuration;

    public ConfigurationBuilder()
    {
        _configuration = new Configuration();
    }

    public ConfigurationBuilder SetDatabaseConnection(string connection)
    {
        _configuration.SetDatabaseConnection(connection);
        return this;
    }

    public ConfigurationBuilder EnableLogging()
    {
        _configuration.SetEnableLogging(true);
        return this;
    }

    public ConfigurationBuilder SetTimeout(int timeout)
    {
        _configuration.SetTimeout(timeout);
        return this;
    }

    public ConfigurationBuilder AddModule(string module)
    {
        _configuration.AddModule(module);
        return this;
    }

    public Configuration Build()
    {
        return _configuration;
    }
}