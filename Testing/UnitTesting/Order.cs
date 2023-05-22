namespace UnitTesting;

public class Order
{
    public Order()
    {
        Id = Guid.NewGuid();
        Status = OrderStatus.Draft;
        OrderItems = new List<OrderItem>();
    }

    public void AddItem(OrderItem orderItem)
    {
        if (Status == OrderStatus.Draft)
        {
            OrderItems = OrderItems.Append(orderItem).ToList();
        }
    }

    public void Place()
    {
        if (Status == OrderStatus.Paid)
            throw new OrderAlreadyPaidException();
        if (Status == OrderStatus.Placed)
            throw new OrderAlreadyPlacedException();
        if (!OrderItems.Any())
            throw new EmptyOrderCannotBePlacedException();
        Status = OrderStatus.Placed;
    }

    public decimal GetTotalPrice() => OrderItems.Sum(x => x.Price);

    public Dictionary<OrderItem, int> GetUniqOrderItemCount()
    {
        var orderItemsCountDict = new Dictionary<OrderItem, int>();

        foreach (var item in OrderItems)
        {
            if (orderItemsCountDict.ContainsKey(item))
            {
                orderItemsCountDict[item]++;
            }
            else
            {
                orderItemsCountDict[item] = 1;
            }
        }

        return orderItemsCountDict;
    }

    public Guid Id { get; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    public OrderItem(decimal price)
    {
        Id = Guid.NewGuid();
        Price = price;
    }

    public decimal Price { get; }
    public Guid Id { get; }    
}

public class EmptyOrderCannotBePlacedException : Exception
{
}

public class OrderAlreadyPlacedException : Exception
{
}

public class OrderAlreadyPaidException : Exception
{
}

public enum OrderStatus
{
    Draft,
    Placed,
    Paid,
}