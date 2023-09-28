using Tests.Documents;

namespace TestProject1;

public class InMemoryUsersRepositoryTests
{
    [Fact]
    public void works_as_intended()
    {
        var repo = new InMemoryUsersRepository();
        var id = Guid.NewGuid();
        
        repo.Save(new User(id));

        Assert.NotNull(repo.GetById(id));
    }
}