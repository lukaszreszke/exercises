namespace Acme.Adapter._2;

public class SupplierARestApiClient
{
    public ApiInventoryResponse FetchInventoryFromApi()
    {
        // Simulate fetching complex inventory data from a REST API
        return new ApiInventoryResponse
        {
            Items = new List<ApiItem>
            {
                new ApiItem { Name = "Item1 from Supplier A", Quantity = 10 },
                new ApiItem { Name = "Item2 from Supplier A", Quantity = 5 }
            }
        };
    }
}