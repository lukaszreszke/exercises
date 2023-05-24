namespace ExternalCommunication;

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