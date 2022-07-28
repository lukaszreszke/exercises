namespace CommunicationSoft;

public interface IEvent
{
}

public class TenantConfigured : IEvent
{
    public TenantConfigured(Guid tenantId)
    {
        TenantId = tenantId;
    }

    public Guid TenantId { get; }
}

public class CustomerHaventLoggedInForCriticalPeriodOfTime : IEvent
{
    public CustomerHaventLoggedInForCriticalPeriodOfTime(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; set; }
}