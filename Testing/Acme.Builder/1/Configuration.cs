namespace Acme.Builder;

public class Configuration
{
    public string DatabaseConnection { get; private set; }
    public bool EnableLogging { get; private set; }
    public int Timeout { get; private set; }
    public List<string> IncludedModules { get; private set; }

    public Configuration()
    {
        IncludedModules = new List<string>();
    }

    internal void SetDatabaseConnection(string connection) => DatabaseConnection = connection;
    internal void SetEnableLogging(bool enableLogging) => EnableLogging = enableLogging;
    internal void SetTimeout(int timeout) => Timeout = timeout;
    internal void AddModule(string module) => IncludedModules.Add(module);
}
