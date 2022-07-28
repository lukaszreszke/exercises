namespace CommunicationSoft;

public interface IEvent
{
}

public interface IHandleEvent<TEvent> where TEvent : class, IEvent 
{
    public Task Handle(TEvent @event);
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