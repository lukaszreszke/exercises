namespace ExternalCommunication;

public class SignOrder
{
    public int Id { get; }
    public SignOrderStatus Status { get; private set; }
    private Dictionary<int, bool> _signedBy;

    public SignOrder(int id)
    {
        Id = id;
        Status = SignOrderStatus.Draft;
        _signedBy = new Dictionary<int, bool>();
    }

    public void AddSignee(int signeeId)
    {
        _signedBy.Add(signeeId, false);

        if (_signedBy.Values.All(x => x.Equals(true)))
        {
            Status = SignOrderStatus.Draft;
        }
    }

    public void Sign(int signeeId)
    {
        if (_signedBy.ContainsKey(signeeId))
        {
            _signedBy[signeeId] = true;
        }
        else
        {
            throw new InvalidDataException(nameof(signeeId));
        }

        if (_signedBy.Values.All(x => x.Equals(true)))
        {
            Status = SignOrderStatus.Completed;
        }
    }

    public bool IsCompleted()
    {
        return Status == SignOrderStatus.Completed;
    }

    public void Cancel()
    {
        Status = SignOrderStatus.Cancelled;
    }
}