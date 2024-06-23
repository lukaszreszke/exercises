using Acme.Adapter._2;

namespace Acme.AdapterTests._2;

public class InventoryServiceTests
{
    [Fact]
    public void TestGettingInventoryFromSupplierARestApi()
    {
        // Arrange
        SupplierARestApiClient restApiClient = new SupplierARestApiClient();
        IInventoryService inventoryService = new SupplierARestApiAdapter(restApiClient);

        // Act
        List<InventoryItem> inventory = inventoryService.GetInventory();

        // Assert
        var expectedInventory = new List<InventoryItem>
        {
            new InventoryItem { Name = "Item1 from Supplier A", Quantity = 10 },
            new InventoryItem { Name = "Item2 from Supplier A", Quantity = 5 }
        };

        Assert.Equal(expectedInventory, inventory);
    }

    [Fact]
    public void TestGettingInventoryFromSupplierBSoapService()
    {
        // Arrange
        SupplierBSoapClient soapClient = new SupplierBSoapClient();
        IInventoryService inventoryService = new SupplierBSoapAdapter(soapClient);

        // Act
        List<InventoryItem> inventory = inventoryService.GetInventory();

        // Assert
        var expectedInventory = new List<InventoryItem>
        {
            new InventoryItem { Name = "Item1 from Supplier B", Quantity = 15 },
            new InventoryItem { Name = "Item2 from Supplier B", Quantity = 8 }
        };

        Assert.Equal(expectedInventory, inventory);
    }

    [Fact]
    public void TestCombinedInventoryFromMultipleSuppliers()
    {
        // Arrange
        var inventoryServices = new List<IInventoryService>
        {
            new SupplierARestApiAdapter(new SupplierARestApiClient()),
            new SupplierBSoapAdapter(new SupplierBSoapClient())
        };
        var inventoryManager = new InventoryManager(inventoryServices);

        // Act
        List<InventoryItem> combinedInventory = inventoryManager.GetCombinedInventory();

        // Assert
        var expectedInventory = new List<InventoryItem>
        {
            new InventoryItem { Name = "Item1 from Supplier A", Quantity = 10 },
            new InventoryItem { Name = "Item2 from Supplier A", Quantity = 5 },
            new InventoryItem { Name = "Item1 from Supplier B", Quantity = 15 },
            new InventoryItem { Name = "Item2 from Supplier B", Quantity = 8 }
        };

        Assert.Equal(expectedInventory, combinedInventory);
    }
}
