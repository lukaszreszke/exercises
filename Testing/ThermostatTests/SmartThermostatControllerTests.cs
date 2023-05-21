using Moq;
using Thermostat;

namespace ThermostatTests;

public class SmartThermostatControllerTests
{
    [Fact]
    public void Should_turn_on_heat_when_current_temperature_is_significantly_lower_than_expected()
    {
        double currentTemperature = 10;
        double expectedTemperature = 20;
        var weatherMonitoringSystem = new Mock<IWeatherMonitoringSystem>();
        weatherMonitoringSystem.Setup(x => x.GetCurrentTemperature()).Returns(currentTemperature);
        weatherMonitoringSystem.Setup(x => x.GetExpectedTemperature()).Returns(expectedTemperature);
        var heatingCoolingSystem = new Mock<IHeatingCoolingSystem>();
        var smartThermostatController = new SmartThermostatController(
            weatherMonitoringSystem.Object, heatingCoolingSystem.Object);

        smartThermostatController.ControlTemperature();

        heatingCoolingSystem.Verify(x => x.TurnOnHeat(), Times.Once);
        heatingCoolingSystem.VerifyNoOtherCalls();
    }

}