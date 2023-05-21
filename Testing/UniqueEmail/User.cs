namespace UniqueEmail;

public class User
{
    public User(string email, IUserRepository userRepository)
    {
        var user = userRepository.FindByEmail(email);
        if (user != null)
            throw new UserEmailNotAvailable(email);
    }
}

public class UserEmailNotAvailable : Exception
{
    public string Email { get; }

    public UserEmailNotAvailable(string email)
    {
        Email = email;
    }
}


public interface IUserRepository
{
    User FindByEmail(string email);
}