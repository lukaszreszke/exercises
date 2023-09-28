namespace Tests.Documents;

public class UserRepository
{
    public User GetById(Guid userId)
    {
        return new User(userId);
    }
}