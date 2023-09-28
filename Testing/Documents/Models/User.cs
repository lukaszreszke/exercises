namespace Tests.Documents;

public class User
{
    public Guid Id { get; }
    public object Email { get; set; }

    public User(Guid id)
    {
        Id = id;
    }
}