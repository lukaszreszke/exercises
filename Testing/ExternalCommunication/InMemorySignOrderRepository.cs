namespace ExternalCommunication;

public class InMemorySignOrderRepository : ISignOrderRepository
{
    private List<SignOrder> _signOrders = new();
    private List<SignOrder> _signOrdersToBeAdded = new();
    

    public SignOrder Create()
    {
        var signOrder = new SignOrder(_signOrders.Count + 1);
        _signOrdersToBeAdded.Add(signOrder);
        return signOrder;
    }

    public SignOrder GetById(int orderId)
    {
        return _signOrders.FirstOrDefault(x => x.Id == orderId);
    }

    public List<SignOrder> GetSignOrders()
    {
        return _signOrders.ToList();
    }

    public void SaveChanges()
    {
        _signOrders.AddRange(_signOrdersToBeAdded);
        _signOrdersToBeAdded.Clear();
    }
}