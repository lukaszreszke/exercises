namespace Gear;

public interface IEngineMonitoringSystem
{
    int GetCurrentRPM();
    bool IsOn();
}

public interface IAutomaticGearBox
{
    int GetMaxGears();
    void ChangeGear(int gear);
    int GetGear();
    void Drive();
    void Park();
    void Neutral();
    void Reverse();
}

public class AutomaticTransmissionController
{
    private readonly IAutomaticGearBox _gearBox;
    private readonly IEngineMonitoringSystem _engineMonitoringSystem;

    public AutomaticTransmissionController(DrivingMode drivingMode, IAutomaticGearBox gearBox, IEngineMonitoringSystem engineMonitoringSystem)
    {
        _gearBox = gearBox;
        _engineMonitoringSystem = engineMonitoringSystem;
    }
    
    public enum DrivingMode
    {
        Eco,
        Normal,
        Sport
    }

    public void Start()
    {
    }

    public void Stop()
    {
    }

    public void ChangeDrivingMode(DrivingMode drivingMode)
    {
    }

    public void HandleGas(int throttle)
    {
        var currentRPM = _engineMonitoringSystem.GetCurrentRPM();

        if (currentRPM <= 2000 && throttle > 80)
        {
            var currentGear = _gearBox.GetGear();
            _gearBox.ChangeGear(currentGear - 1);
        }
    }

    public void HandleBreaks(int breakingForce)
    {
    }
}

public interface GearCalculator
{
    public int Calculate(int currentRPM, double throttle, double breakingForce);
}