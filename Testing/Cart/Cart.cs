namespace Cart;

public class Cart
{
    public int Id { get; }
    public int UserId { get; }

    public Cart(int userId)
    {
        UserId = userId;
    }
    
    public List<Product> Products { get; } = new();
    public IDictionary<int, decimal> ProductPrice = new Dictionary<int, decimal>();

    public decimal GetTotalPrice() => ProductPrice.Values.Sum();

    public void AddProduct(Product product, decimal price)
    {
        if (!ProductPrice.ContainsKey(product.Id))
            ProductPrice.Add(product.Id, price);
        else ProductPrice[product.Id] += price;
    }
}

public class Product
{
    public int Id { get; }
    public virtual decimal Price { get; }
    public string Name { get; }
    public string Description { get; }

    public decimal GetPrice() => Price;
}