namespace Tests.Documents;

public interface IExecutionContextAccessor
{
    Guid UserId { get; set; }
}