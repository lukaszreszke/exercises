namespace Acme.Adapter._2;

public class InventoryManager
{
    private readonly IEnumerable<IInventoryService> _inventoryServices;

    public InventoryManager(IEnumerable<IInventoryService> inventoryServices)
    {
        _inventoryServices = inventoryServices;
    }

    public List<InventoryItem> GetCombinedInventory()
    {
        var combinedInventory = new List<InventoryItem>();

        foreach (var service in _inventoryServices)
        {
            combinedInventory.AddRange(service.GetInventory());
        }

        return combinedInventory;
    }
}