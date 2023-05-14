using Moq;

namespace ExternalCommunication;

public class Tests
{
    [Fact]
    public void Test_sign()
    {
        var externalProvider = new Mock<IExternalProviderClient>();
        var signOrder =
            new SignOrderService(new SignOrderRepository(), externalProvider.Object, InMemoryUserRepository());
        signOrder.Create();
        signOrder.AddSignee(1, 1);
        signOrder.AddSignee(1, 2);
        externalProvider.Invocations.Clear();

        signOrder.Sign(1, 1);
        
        externalProvider.Verify(x => x.Sign("johndoe@example.com", 1));
        externalProvider.VerifyNoOtherCalls();
    }
    
    [Fact]
    public void Test_all_signed()
    {
        var externalProvider = new Mock<IExternalProviderClient>();
        var signOrder =
            new SignOrderService(new SignOrderRepository(), externalProvider.Object, InMemoryUserRepository());
        signOrder.Create();
        signOrder.AddSignee(1, 1);
        signOrder.AddSignee(1, 2);

        signOrder.Sign(1, 1);
        signOrder.Sign(1, 2);
        
        externalProvider.Verify(x => x.Complete());
    }


    private static InMemoryUserRepository InMemoryUserRepository()
    {
        return new InMemoryUserRepository(
            new List<User>
            {
                new User { Email = "johndoe@example.com", Id = 1, Name = "John Doe" },
                new User { Email = "janedoe@example.com", Id = 2, Name = "Jane Doe" },
            });
    }
}