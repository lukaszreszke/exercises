namespace Tests.Documents;

internal static class Configuration
{
    public static decimal GetSafeValue()
    {
        return new decimal(40000.00);
    }

    public static DateTime GetPreferedEmailReceivalTimeFor(User reader)
    {
        throw new NotImplementedException();
    }
}