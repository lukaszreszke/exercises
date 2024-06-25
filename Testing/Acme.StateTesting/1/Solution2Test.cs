using Acme.State;
using Microsoft.EntityFrameworkCore;

namespace Acme.StateTesting;

public class UnitTest1
{
    [Fact]
    public void state_changes_with_db_context()
    {
        var options = new DbContextOptionsBuilder<HumidifierContext>()
            .UseInMemoryDatabase(databaseName: "HumidityDatabase")
            .Options;
        var context = new HumidifierContext(options);
        var humidifier = new HumidifierOn();
        
        var needsCleaning = humidifier.NeedsCleaning();
        
        context.Humidifiers.Add(needsCleaning);
        context.SaveChanges();
        
        Assert.Equal(1, needsCleaning.Id);
        Assert.Equal(HumidifierState.NeedsCleaning, needsCleaning.State);
        
        var humidifier2 = context.Humidifiers.Find(1);
        Assert.Equal(HumidifierState.NeedsCleaning, humidifier2.State);
    }
    
    [Fact]
    public void can_be_turned_off_when_on()
    {
        // Arrange
        var humidifier = new HumidifierOn();

        // Act
        var result = humidifier.TurnOff();

        // Assert
        Assert.Equal(HumidifierState.Off, result.State);
    }

    [Fact]
    public void can_be_switched_into_needs_cleaning_when_on()
    {
        // Arrange
        var humidifier = new HumidifierOn();

        // Act
        var result = humidifier.NeedsCleaning();

        // Assert
        Assert.Equal(HumidifierState.NeedsCleaning, result.State);
    }

    [Fact]
    public void can_be_short_on_water_when_on()
    {
        // Arrange
        var humidifier = new HumidifierOn();

        // Act
        var result = humidifier.WaterShortage();

        // Assert
        Assert.Equal(HumidifierState.WaterShortage, result.State);
    }

    [Fact]
    public void can_be_turned_on_when_off()
    {
        // Arrange
        var humidifier = new HumidifierOff();

        // Act
        humidifier.TurnOn();

        // Assert
        Assert.Equal(HumidifierState.On, humidifier.State);
    }

    [Fact]
    public void cleaning_brings_it_back_to_on_state()
    {
        // Arrange
        var humidifier = new HumidifierNeedsCleaning();

        // Act
        humidifier.Clean();

        // Assert
        Assert.Equal(HumidifierState.On, humidifier.State);
    }

    [Fact]
    public void fill_water_brings_it_back_to_on_state()
    {
        // Arrange
        var humidifier = new HumidifierWaterShortage();

        // Act
        humidifier.FillWater();

        // Assert
        Assert.Equal(HumidifierState.On, humidifier.State);
    }
}