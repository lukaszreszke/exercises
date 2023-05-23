namespace UnitTesting;

public class OrderTests
{
   [Fact]
   public void Adds_items_to_order()
   {
      var orderItem = new OrderItem(10);
      var order = new Order();
      
      order.AddItem(orderItem);
      order.AddItem(orderItem);
      order.AddItem(orderItem);
      
      Assert.Equal(3, order.OrderItems.Count);
   }

   [Fact]
   public void cannot_submit_orders_without_items()
   {
      var order = new Order();

      Assert.Throws<EmptyOrderCannotBePlacedException>(() => order.Place());
   }

   [Fact]
   public void can_place_order_with_items()
   {
      var orderItem = new OrderItem(10);
      var order = new Order();
      order.AddItem(orderItem);

      order.Place();
      
      Assert.Equal(OrderStatus.Placed, order.Status);
   }
   
   [Fact]
   public void cannot_place_placed_order()
   {
      var order = new Order();
      order.AddItem(new OrderItem(10));
      order.Place();
      
      Assert.Throws<OrderAlreadyPlacedException>(() => order.Place());
   }
   
   [Fact]
   public void returns_total_price()
   {
      var orderItem = new OrderItem(10);
      var order = new Order();
      order.AddItem(orderItem);

      Assert.Equal(10, order.GetTotalPrice());
   }

   [Fact]
   public void returns_uniq_order_item_count()
   {
      var orderItem = new OrderItem(10);
      var order = new Order();
      order.AddItem(orderItem);
 
      Assert.Single(order.GetUniqOrderItemCount().Keys);
      Assert.Single(order.GetUniqOrderItemCount());
   }
}