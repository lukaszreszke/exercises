using Calculator;
using FluentAssertions;

namespace CalculatorTests;

public class CalculatorTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(12000, 12000)]
    public void returns_correctly_calculated_number_for_annual_period(
        decimal annualSalary, decimal expectedValue)
    {
        var calculator = new AnnualFormulaForSalary();

        var result = calculator.Calculate(Period.Annual.Value, annualSalary);

        result.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1000, 12000)]
    public void returns_correctly_calculated_number_for_monthly_period(
        decimal monthlySalary, decimal expectedValue)
    {
        var calculator = new AnnualFormulaForSalary();

        var result = calculator.Calculate(Period.Monthly.Value, monthlySalary);

        result.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1000, 52000)]
    public void returns_correctly_calculated_number_for_weekly_period(
        decimal weeklySalary, decimal expectedValue)
    {
        var calculator = new AnnualFormulaForSalary();

        var result = calculator.Calculate(Period.Weekly.Value, weeklySalary);

        result.Should().Be(expectedValue);
    }


    [Theory]
    [InlineData(0, 0)]
    [InlineData(100, 200400)]
    public void returns_correctly_calculated_number_for_hourly_wage(
        decimal hourlyWage, decimal expectedValue)
    {
        var calculator = new AnnualFormulaForSalary();

        var result = calculator.Calculate(Period.Hourly.Value, hourlyWage);

        result.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(100, 25200)]
    public void returns_correctly_calculated_number_for_daily_period(
        decimal dailySalary, decimal expectedValue)
    {
        var calculator = new AnnualFormulaForSalary();

        var result = calculator.Calculate(Period.Daily.Value, dailySalary);

        result.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(0, 0, "weekly")]
    [InlineData(0, 0, "monthly")]
    [InlineData(0, 0, "annual")]
    [InlineData(0, 0, "hourly")]
    public void is_case_insensitive(
        decimal value, decimal expectedValue, string desiredPeriod)
    {
        var calculator = new AnnualFormulaForSalary();

        var result = calculator.Calculate(desiredPeriod, value);

        result.Should().Be(expectedValue);
    }

    [Fact]
    public void invalid_period()
    {
        var calculator = new AnnualFormulaForSalary();

        var message = Assert.Throws<ArgumentException>(() => calculator.Calculate("invalid", 1337));
        message.Message.Should().Be("Invalid period");
    }
}
