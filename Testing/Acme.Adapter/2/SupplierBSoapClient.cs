namespace Acme.Adapter._2;

public class SupplierBSoapClient
{
    public SupplierBResponse GetInventoryFromSoapService()
    {
        // Simulate fetching complex inventory data from a SOAP service
        return new SupplierBResponse
        {
            InventoryList = new SupplierBItem[]
            {
                new SupplierBItem { Description = "Item1 from Supplier B", Stock = 15 },
                new SupplierBItem { Description = "Item2 from Supplier B", Stock = 8 }
            }
        };
    }
}