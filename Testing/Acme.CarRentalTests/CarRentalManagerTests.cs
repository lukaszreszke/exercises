using Acme.CarRental;
using Xunit.Abstractions;

namespace Acme.CarRentalTests;

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

        manager.RentCar(ModelXCarId, "John Doe", "john.doe@example.com", 7);

        Assert.Contains(ModelXCarId, manager.CarIds);
    }

    [Fact]
    public void RentCar_ThrowsExceptionForInvalidInput()
    {
        var manager = new CarRentalManager();

        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelXCarId, "", "john.doe@example.com", 7));
        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelXCarId, "John Doe", "", 7));
        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelXCarId, "John Doe", "john.doe@example.com", 0));
        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelXCarId, "John Doe", "john.doe", 7));
    }

    [Fact]
    public void ReturnCar_RemovesCarFromRentalList()
    {
        var manager = new CarRentalManager();
        manager.RentCar(ModelXCarId, "John Doe", "john.doe@example.com", 7);

        manager.ReturnCar(ModelXCarId);

        Assert.DoesNotContain(ModelXCarId, manager.CarIds);
    }

    [Fact]
    public void GenerateStatement_ReturnsCorrectStatement()
    {
        var manager = new CarRentalManager();
        manager.RentCar(ModelXCarId, "John Doe", "john.doe@example.com", 7);

        var statement = manager.GenerateStatement("John Doe");

        Assert.Contains("Rental Record for John Doe", statement);
        Assert.Contains("Car: 1", statement);
        Assert.Contains("Total charge: 700", statement);
    }

    [Fact]
    public void NotifyOverdueRentals_SendsNotificationForOverdueRentals()
    {
        var manager = new CarRentalManager();
        manager.RentCar(ModelXCarId, "John Doe", "john.doe@example.com", 1);

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        manager.NotifyOverdueRentals();

        string output = consoleOutput.ToString();
        Assert.Contains("Sending overdue notice for car Model X to john.doe@example.com", output);
    }

    [Fact]
    public void GetAvailableCars_ReturnsAvailableCars()
    {
        var manager = new CarRentalManager();

        var availableCars = manager.GetAvailableCars();

        Assert.Collection(availableCars,
            car =>
            {
                Assert.Equal(2000, car.Id);
                Assert.Equal("Model S", car.Model);
                Assert.Equal("Tesla", car.Brand);
                Assert.Equal(200, car.DailyRate);
            },
            car =>
            {
                Assert.Equal(3000, car.Id);
                Assert.Equal("Model 3", car.Model);
                Assert.Equal("Tesla", car.Brand);
                Assert.Equal(300, car.DailyRate);
            },
            car =>
            {
                Assert.Equal(1000, car.Id);
                Assert.Equal("Model X", car.Model);
                Assert.Equal("Tesla", car.Brand);
                Assert.Equal(100, car.DailyRate);
            },
            car =>
            {
                Assert.Equal(4000, car.Id);
                Assert.Equal("Model Y", car.Model);
                Assert.Equal("Tesla", car.Brand);
                Assert.Equal(400, car.DailyRate);
            }
        );
    }

    [Fact]
    public void CannotReserveTheSameCardForTheSamePeriod()
    {
        var manager = new CarRentalManager();
        manager.RentCar(ModelXCarId, "John Doe", "johndoe@example.com", 7, DateTime.Now.AddDays(-1));
        
        Assert.Throws<ArgumentException>(() => manager.RentCar(ModelXCarId, "John Doe", "johndoe@example.com", 7, DateTime.Now));
    }

    private int ModelXCarId => 1000;

    class TestClock : IClock
    {
        public TestClock(DateTime now)
        {
            Now = now;
        }

        public DateTime Now { get; }
    }
}