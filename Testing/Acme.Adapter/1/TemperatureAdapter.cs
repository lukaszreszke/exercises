namespace Acme.Adapter._1;

public class TemperatureAdapter : ITemperatureSensor
{
    private readonly OldTemperatureSensor _oldSensor;

    public TemperatureAdapter(OldTemperatureSensor oldSensor)
    {
        _oldSensor = oldSensor;
    }

    public double GetTemperatureInCelsius()
    {
        double fahrenheit = _oldSensor.GetTemperatureInFahrenheit();
        return (fahrenheit - 32) * 5.0 / 9.0; // Conversion formula from Fahrenheit to Celsius
    }
}