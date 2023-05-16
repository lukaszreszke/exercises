using Moq;

namespace Gear;

public class AutomaticTransmissionControllerTest
{
    [Fact]
    public void Should_reduce_gear_when_acceleration_rapidly_on_low_RPN()
    {
        int currentGear = 5;
        var engineMonitoringSystem = new Mock<IEngineMonitoringSystem>();
        engineMonitoringSystem.Setup(x => x.GetCurrentRPM()).Returns(LOW_RPM);
        var automaticGearBox = new Mock<IAutomaticGearBox>();
        automaticGearBox.Setup(x => x.GetGear()).Returns(currentGear);
        var controller = new AutomaticTransmissionController(
            MODE, automaticGearBox.Object, engineMonitoringSystem.Object);
        

        controller.HandleGas(RAPID_ACCELERATION);

        int desiredGear = currentGear - 1;
        automaticGearBox.Verify(x => x.GetGear(), Times.Once);
        automaticGearBox.Verify(x => x.ChangeGear(desiredGear), Times.Once);
        automaticGearBox.VerifyNoOtherCalls();
    }

    private const int RAPID_ACCELERATION = 100;

    private const int LOW_RPM = 2000;
    
    private const AutomaticTransmissionController.DrivingMode MODE = AutomaticTransmissionController.DrivingMode.Sport;
}