The idea of the adapter pattern is to convert the interface of a class (in this case OldTemperatureSensor)
to another interface that a client expects. In this case, the client expects the to get the temperature
in Celsius, but the OldTemperatureSensor returns the temperature in Fahrenheit. The Adapter class converts
it following the formula.

## Exercise
Use the example and solve the following problem:
One of the clients of the Acme.Adapter projects expects a JSON format with the temperature in Celsius.
The example of expected response when temperature is 23 Celsius is:
```json
{
  "temperature": 23
}
```

However, the other client expects the XML format with the temperature in Celsius, including the Fahrenheit temperature that was used for the conversion.
The example of expected response when temperature is 23 Celsius is:
```xml
<temperature>
  <celsius>23</celsius>
  <fahrenheit>73.4</fahrenheit>
</temperature>
```

Use those values to write tests proving that the solution works as desired.

Use existing Acme.AdapterTests project 