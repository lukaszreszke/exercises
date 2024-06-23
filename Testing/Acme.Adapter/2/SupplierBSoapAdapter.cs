namespace Acme.Adapter._2;

public class SupplierBSoapAdapter : IInventoryService
{
    private readonly SupplierBSoapClient _soapClient;

    public SupplierBSoapAdapter(SupplierBSoapClient soapClient)
    {
        _soapClient = soapClient;
    }

    public List<InventoryItem> GetInventory()
    {
        var soapResponse = _soapClient.GetInventoryFromSoapService();
        return soapResponse.InventoryList.Select(item => new InventoryItem
        {
            Name = item.Description,
            Quantity = item.Stock
        }).ToList();
    }
}