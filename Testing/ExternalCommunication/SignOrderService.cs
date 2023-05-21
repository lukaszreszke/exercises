using System.Net;

namespace ExternalCommunication;

public class SignOrderService
{
    private readonly ISignOrderRepository _repository;
    private readonly IExternalProviderClient _externalProviderClient;
    private readonly IUserRepository _userRepository;

    public SignOrderService(
        ISignOrderRepository repository,
        IExternalProviderClient externalProviderClient,
        IUserRepository userRepository)
    {
        _repository = repository;
        _externalProviderClient = externalProviderClient;
        _userRepository = userRepository;
    }

    public void AddSignee(int orderId, int signeeId)
    {  
        var signOrder = _repository.GetById(orderId);
        signOrder.AddSignee(signeeId);
        var user = _userRepository.FindById(signeeId);
        var result = _externalProviderClient.AddParticipant(user.Email);
        if (result != HttpStatusCode.OK)
        {
            throw new FailedToUpdateExternalProviderException();
        }

        _repository.SaveChanges();
    }

    public void Sign(int orderId, int signeeId)
    {
        var signOrder = _repository.GetById(orderId);
        signOrder.Sign(signeeId);
        var user = _userRepository.FindById(signeeId);
        _externalProviderClient.Sign(user.Email, orderId);

        if (signOrder.IsCompleted())
        {
            var result = _externalProviderClient.Complete(orderId);
            if (result != HttpStatusCode.OK)
            {
                throw new FailedToUpdateExternalProviderException();
            }

            _repository.SaveChanges();
        }
    }

    public void Cancel(int orderId)
    {
        var signOrder = _repository.GetById(orderId);
        if (!signOrder.IsCompleted())
        {
            signOrder.Cancel();
            var result = _externalProviderClient.Cancel(orderId);
            if (result != HttpStatusCode.OK)
            {
                throw new FailedToUpdateExternalProviderException();
            }

            _repository.SaveChanges();
        }
    }

    public int Create()
    {
        var signOrder = _repository.Create();
        var result = _externalProviderClient.Create(signOrder.Id);
        if (result != HttpStatusCode.OK)
        {
            throw new FailedToUpdateExternalProviderException();
        }

        _repository.SaveChanges();

        return signOrder.Id;
    }
}

public class FailedToUpdateExternalProviderException : Exception
{
}

public interface IUserRepository
{
    User FindById(int signeeId);
    void SaveChanges();
}

public class InMemoryUserRepository : IUserRepository
{
    private List<User> _users;

    public InMemoryUserRepository(List<User> users)
    {
        _users = users;
    }
    
    public User FindById(int signeeId)
    {
        return _users.First(x => x.Id == signeeId);
    }

    public void SaveChanges()
    {
    }
}

public interface IExternalProviderClient
{
    HttpStatusCode Complete(int orderId);
    HttpStatusCode Cancel(int orderId);
    HttpStatusCode AddParticipant(string userEmail);
    HttpStatusCode Sign(string userEmail, int orderId);
    HttpStatusCode Create(int signOrderId);
}

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

public enum SignOrderStatus
{
    Draft,
    Completed,
    Cancelled
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}

public interface ISignOrderRepository
{
    SignOrder Create();
    SignOrder GetById(int orderId);
    List<SignOrder> GetSignOrders();
    void SaveChanges();
}

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