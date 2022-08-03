using Moq;

namespace Cart;

public class CartTests
{
    [Fact]
    public void product_is_added_to_cart()
    {
        var userId = 1;
        var cart = new Cart(userId);
        var product = new Mock<Product>();
        
        cart.AddProduct(product.Object, 20);
        
        Assert.Equal(20, cart.GetTotalPrice());
        product.VerifyGet(x => x.Price);
    }

    [Fact]
    public void total_price_increases_when_product_is_added_to_cart()
    {
        var userId = 1;
        var cart = new Cart(userId);
        var product = new Mock<Product>();
        
        cart.AddProduct(product.Object, 20);
        cart.AddProduct(product.Object, 20);

        Assert.Equal(40, cart.GetTotalPrice());
        product.VerifyGet(x => x.Price, () => Times.Exactly(2)); 
    }
}