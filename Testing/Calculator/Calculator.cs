namespace Calculator;

public interface ICalculateSalaryBasedOnPeriod
{
    decimal Calculate(string period, decimal value);
}

public class DefaultAnnualFormulaForSalary : ICalculateSalaryBasedOnPeriod 
{
    private readonly int _numberOfMonthsInYear = 12;
    private readonly int _numberOfWorkWeeksInYear = 52;
    private readonly int _typicalNumberOfHoursInMonth = 167;
    private readonly int _typicalNumberOfDaysInMonth = 21;

    public decimal Calculate(string period, decimal value)
    {
        if (period == null) return value;

        if (string.Equals(period, Period.Annual.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value;
        }

        if (string.Equals(period, Period.Monthly.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _numberOfMonthsInYear;
        }

        if (string.Equals(period, Period.Weekly.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _numberOfWorkWeeksInYear;
        }

        if (string.Equals(period, Period.Hourly.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _typicalNumberOfHoursInMonth * _numberOfMonthsInYear;
        }

        if (string.Equals(period, Period.Daily.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _typicalNumberOfDaysInMonth * _numberOfMonthsInYear;
        }

        throw new ArgumentException("Invalid period");
    }
}

public class DefaultMonthlyFormulaForSalary : ICalculateSalaryBasedOnPeriod
{
    private readonly int _numberOfMonthsInYear = 12;
    private readonly int _numberOfWorkWeeksInYear = 52;
    private readonly int _typicalNumberOfHoursInMonth = 167;
    private readonly int _typicalNumberOfDaysInMonth = 21;

    public decimal Calculate(string period, decimal value)
    {
        if (period == null) return value;

        if (string.Equals(period, Period.Annual.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value / 12;
        }

        if (string.Equals(period, Period.Monthly.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value;
        }

        if (string.Equals(period, Period.Weekly.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return Math.Round(value * _numberOfWorkWeeksInYear / _numberOfMonthsInYear, 2);
        }

        if (string.Equals(period, Period.Hourly.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _typicalNumberOfHoursInMonth;
        }

        if (string.Equals(period, Period.Daily.Value, StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _typicalNumberOfDaysInMonth;
        }

        throw new ArgumentException("Invalid period");
    }
}

public class Period
{
    private Period(string value)
    {
        Value = value;
    }

    public static Period Hourly => new(nameof(Hourly));
    public static Period Daily => new(nameof(Daily));
    public static Period Weekly => new(nameof(Weekly));
    public static Period Monthly => new(nameof(Monthly));
    public static Period Annual => new(nameof(Annual));
    public string Value { get; }
    
}