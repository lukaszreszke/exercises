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