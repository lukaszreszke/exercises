using System.Globalization;

namespace Salaries.Domain;

public class Money
{
    public static Money Zero => new Money(0);

    public decimal Amount { get; }

    public Money(decimal amount)
    {
        Amount = amount;
    }
    
    public Money(string amount)
    {
        Amount = decimal.Parse(amount, CultureInfo.CurrentCulture);
    }

}