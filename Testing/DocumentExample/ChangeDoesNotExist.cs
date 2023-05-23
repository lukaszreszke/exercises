namespace DocumentExample;

public class ChangeDoesNotExist : Exception
{
    public int ChangeId { get; }

    public ChangeDoesNotExist(int changeId)
    {
        ChangeId = changeId;
    }
}