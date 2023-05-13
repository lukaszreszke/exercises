using Xunit;

namespace TestContainersExample
{
    [CollectionDefinition("SharedTestCollection")]
    public class SharedTestCollection : ICollectionFixture<ApiFactory>
    {
    }
}