using System.Net.Http.Json;
using ContractsManagement;
using Newtonsoft.Json;

namespace ContractsManagementTests;

public class ContractsIntegrationTests : IClassFixture<ApplicationFactory>
{
    private readonly ApplicationFactory _factory;
    private readonly HttpClient _client;

    public ContractsIntegrationTests(ApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Test1()
    {
        await _client.PostAsync("Contracts", null);
        var response = await _client.GetAsync("Contracts");

        response.EnsureSuccessStatusCode();
        var contracts = await response.Content.ReadFromJsonAsync<List<Contract>>();
        var jsonString = await response.Content.ReadAsStringAsync();
        var contracts2 = JsonConvert.DeserializeObject<List<Contract>>(jsonString);
        Assert.Single(contracts);
        Assert.Single(contracts2);
    }
}