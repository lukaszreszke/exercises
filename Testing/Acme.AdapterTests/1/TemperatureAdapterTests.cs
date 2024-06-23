using Acme.Adapter._1;

namespace Acme.AdapterTests._1;

public class TemperatureAdapterTests
{
    [Fact]
    public void TestTemperatureConversion()
    {
        // Arrange
        OldTemperatureSensor oldSensor = new OldTemperatureSensor();
        ITemperatureSensor sensorAdapter = new TemperatureAdapter(oldSensor);

        // Act
        double temperatureInCelsius = sensorAdapter.GetTemperatureInCelsius();

        // Assert
        Assert.Equal(37.78, temperatureInCelsius, 2); //100°F is approximately 37.78°C 
    }
}
