namespace Tests.Documents;

public class InMemoryUsersRepository : IUserRepository
{
    private List<User> _users = new List<User>();

    public User GetById(Guid id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }
    
    public void Save(User user)
    {
        _users.Add(user);
    }
}