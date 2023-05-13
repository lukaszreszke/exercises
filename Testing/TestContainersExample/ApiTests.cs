using System.Net;
using System.Threading.Tasks;
using Xunit;
using DbTemplate;

namespace TestContainersExample
{
    public class ApiTests : IClassFixture<ApiFactory>
    {
        private readonly ApiFactory _factory;

        public ApiTests(ApiFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointReturnsSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/weatherforecast");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.True(_factory.GetCustomerContext().Customers.ToList().Count == 1);
        }
    }
}