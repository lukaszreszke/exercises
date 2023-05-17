using Xunit;

namespace SalariesTests 
{
    [CollectionDefinition("SharedTestCollection")]
    public class SharedTestCollection : ICollectionFixture<ApiFactory>
    {
    }
}