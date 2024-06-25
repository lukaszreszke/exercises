using Acme.Decorator;
using Microsoft.EntityFrameworkCore;

namespace Acme.DecoratorTests;

public class InMemoryContextTest
{
    [Fact]
    public void test_saves()
    {
        var options = new DbContextOptionsBuilder<DecoratorContext>()
            .UseInMemoryDatabase(databaseName: "AcmeContext")
            .Options;
        
        var context = new DecoratorContext(options);

        context.Log.Add(new Log { Message = "test"});

        context.SaveChanges();
    }
}