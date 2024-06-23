using Acme.CarRental;
using Xunit.Abstractions;

namespace Acme.CarRentalStep1Tests;

public class CarRentalManagerTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CarRentalManagerTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void RentCar_AddsCarToRentalList()
    {
        var manager = new CarRentalManager();

        manager.RentCar(ModelX, "John Doe", "john.doe@example.com", 7);

        Assert.Contains(ModelX, manager.Cars);
    }

    [Fact]
    public void RentCar_ThrowsExceptionForInvalidInput()
    {
        var manager = new CarRentalManager();

        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelX, "", "john.doe@example.com", 7));
        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelX, "John Doe", "", 7));
        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelX, "John Doe", "john.doe@example.com", 0));
        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelX, "John Doe", "john.doe", 7));
    }

    [Fact]
    public void ReturnCar_RemovesCarFromRentalList()
    {
        var manager = new CarRentalManager();
        manager.RentCar(ModelX, "John Doe", "john.doe@example.com", 7);

        manager.ReturnCar(ModelX);

        Assert.DoesNotContain(ModelX, manager.Cars);
    }

    [Fact]
    public void GenerateStatement_ReturnsCorrectStatement()
    {
        var manager = new CarRentalManager();
        manager.RentCar(ModelX, "John Doe", "john.doe@example.com", 7);

        var statement = manager.GenerateStatement("John Doe");

        Assert.Contains("Rental Record for John Doe", statement);
        Assert.Contains("Car: Model X", statement);
        Assert.Contains("Total charge: 700", statement);
    }

    [Fact]
    public void NotifyOverdueRentals_SendsNotificationForOverdueRentals()
    {
        var manager = new CarRentalManager();
        manager.RentCar(ModelX, "John Doe", "john.doe@example.com", 1);

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        manager.NotifyOverdueRentals();

        string output = consoleOutput.ToString();
        Assert.Contains("Sending overdue notice for car Model X to john.doe@example.com", output);
    }
    
    private string ModelX => "Model X";
}