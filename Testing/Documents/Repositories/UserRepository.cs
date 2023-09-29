namespace Tests.Documents;

public class UserRepository : IUserRepository
{
    public User GetById(Guid userId)
    {
        return new User(userId);
    }
}

public interface IUserRepository
{
    User GetById(Guid userId);
}