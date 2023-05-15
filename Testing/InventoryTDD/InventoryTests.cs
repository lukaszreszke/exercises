namespace InventoryTDD;

using System.Collections.Generic;

public class InventoryTests
{
    [Fact]
    public void Test_AddItem()
    {
        // Arrange
        var inventory = new Inventory();

        // Act
        inventory.AddItem("Apple", 5);
        inventory.AddItem("Apple", 5);

        // Assert
        Assert.True(inventory.IsInStock("Apple"));
        Assert.Equal(10, inventory.GetStock("Apple"));
    }

    [Fact]
    public void Test_RemoveItem()
    {
        // Arrange
        var inventory = new Inventory();
        inventory.AddItem("Apple", 5);
        inventory.AddItem("Grape", 5);

        // Act
        inventory.RemoveItem("Apple", 2);

        // Assert
        Assert.True(inventory.IsInStock("Apple"));
        Assert.True(inventory.IsInStock("Grape"));
        Assert.Equal(3, inventory.GetStock("Apple"));
        Assert.Equal(5, inventory.GetStock("Grape"));
    }

    [Fact]
    public void Test_ItemNotInStock()
    {
        // Arrange
        var inventory = new Inventory();

        // Act
        // No action is needed.

        // Assert
        Assert.False(inventory.IsInStock("Apple"));
    }
}