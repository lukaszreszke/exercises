namespace Thermostat;

public interface IWeatherMonitoringSystem
{
    double GetCurrentTemperature();
    double GetExpectedTemperature();
}

public interface IHeatingCoolingSystem
{
    void TurnOnHeat();
    void TurnOffHeat();
    void TurnOnCooling();
    void TurnOffCooling();
    bool IsHeatOn();
    bool IsCoolingOn();
}

public class SmartThermostatController
{
    private IWeatherMonitoringSystem _weatherMonitoringSystem;
    private IHeatingCoolingSystem _heatingCoolingSystem;

    public SmartThermostatController(IWeatherMonitoringSystem weatherMonitoringSystem, IHeatingCoolingSystem heatingCoolingSystem)
    {
        _weatherMonitoringSystem = weatherMonitoringSystem;
        _heatingCoolingSystem = heatingCoolingSystem;
    }

    public void ControlTemperature()
    {
        double currentTemperature = _weatherMonitoringSystem.GetCurrentTemperature();
        double expectedTemperature = _weatherMonitoringSystem.GetExpectedTemperature();

        if (currentTemperature < expectedTemperature - 2)
        {
            if (!_heatingCoolingSystem.IsHeatOn())
            {
                _heatingCoolingSystem.TurnOnHeat();
            }

            if (_heatingCoolingSystem.IsCoolingOn())
            {
                _heatingCoolingSystem.TurnOffCooling();
            }
        }
        else if (currentTemperature > expectedTemperature + 2)
        {
            if (_heatingCoolingSystem.IsHeatOn())
            {
                _heatingCoolingSystem.TurnOffHeat();
            }

            if (!_heatingCoolingSystem.IsCoolingOn())
            {
                _heatingCoolingSystem.TurnOnCooling();
            }
        }
        else
        {
            if (_heatingCoolingSystem.IsHeatOn())
            {
                _heatingCoolingSystem.TurnOffHeat();
            }

            if (_heatingCoolingSystem.IsCoolingOn())
            {
                _heatingCoolingSystem.TurnOffCooling();
            }
        }
    }
}

