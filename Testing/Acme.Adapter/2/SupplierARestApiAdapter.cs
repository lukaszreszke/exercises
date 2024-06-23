namespace Acme.Adapter._2;

public class SupplierARestApiAdapter : IInventoryService
{
    private readonly SupplierARestApiClient _restApiClient;

    public SupplierARestApiAdapter(SupplierARestApiClient restApiClient)
    {
        _restApiClient = restApiClient;
    }

    public List<InventoryItem> GetInventory()
    {
        var apiResponse = _restApiClient.FetchInventoryFromApi();
        return apiResponse.Items.Select(item => new InventoryItem
        {
            Name = item.Name,
            Quantity = item.Quantity
        }).ToList();
    }
}