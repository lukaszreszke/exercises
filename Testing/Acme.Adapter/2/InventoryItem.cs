namespace Acme.Adapter._2;

public class InventoryItem : IEquatable<InventoryItem>
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public bool Equals(InventoryItem other)
    {
        if (other == null) return false;
        return this.Name == other.Name && this.Quantity == other.Quantity;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as InventoryItem);
    }

    public override int GetHashCode()
    {
        return (Name, Quantity).GetHashCode();
    }
}