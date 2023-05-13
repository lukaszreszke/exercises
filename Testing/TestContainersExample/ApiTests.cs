using System.Net;
using System.Threading.Tasks;
using Xunit;
using DbTemplate;

namespace TestContainersExample
{
    [Collection("SharedTestCollection")]
    public class ApiTests : IAsyncLifetime
    {
        private readonly ApiFactory _factory;
        private readonly Func<Task> _resetDatabase;

        public ApiTests(ApiFactory factory)
        {
            _factory = factory;
            _resetDatabase = factory.ResetDatabaseAsync;
        }

        [Fact]
        public async Task Get_EndpointReturnsSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.HttpClient;

            // Act
            var response = await client.GetAsync("/weatherforecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.True(_factory.GetCustomerContext().Customers.ToList().Count == 1);
         }

        public Task InitializeAsync() => Task.CompletedTask;
        public Task DisposeAsync() => _resetDatabase();
    }
}