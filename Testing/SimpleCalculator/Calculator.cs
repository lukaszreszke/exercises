namespace SimpleCalculator;

public class DefaultMonthlyFormulaForSalary
{
    private readonly int _numberOfMonthsInYear = 12;
    private readonly int _numberOfWorkWeeksInYear = 52;
    private readonly int _typicalNumberOfHoursInMonth = 167;
    private readonly int _typicalNumberOfDaysInMonth = 21;

    public decimal Calculate(string period, decimal value)
    {
        if (period == null) return value;

        if (string.Equals(period, "Annual", StringComparison.InvariantCultureIgnoreCase))
        {
            return value / 12;
        }

        if (string.Equals(period, "Monthly", StringComparison.InvariantCultureIgnoreCase))
        {
            return value;
        }

        if (string.Equals(period, "Weekly", StringComparison.InvariantCultureIgnoreCase))
        {
            return Math.Round(value * _numberOfWorkWeeksInYear / _numberOfMonthsInYear, 2);
        }

        if (string.Equals(period, "Hourly", StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _typicalNumberOfHoursInMonth;
        }

        if (string.Equals(period, "Daily", StringComparison.InvariantCultureIgnoreCase))
        {
            return value * _typicalNumberOfDaysInMonth;
        }

        throw new ArgumentException("Invalid period");
    }
}