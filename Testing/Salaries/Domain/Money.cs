using System.Globalization;

namespace Salaries.Domain;

public class Money
{
    public static Money Zero => new Money(0);

    public decimal Amount { get; }

    public Money(string amount)
    {
        if (!decimal.TryParse(amount, NumberStyles.Number, CultureInfo.InvariantCulture,out var parsedAmount))
            throw new ArgumentException("Amount must be a valid decimal number", nameof(amount));

        if (parsedAmount < 0)
            throw new ArgumentException("Amount cannot be negative", nameof(amount));

        this.Amount = parsedAmount;
    }

    public Money(decimal amount)
    {
        Amount = amount;
    }

    public Money Add(Money other)
    {
        decimal result = Amount + other.Amount;
        return new Money(result);
    }

    public static Money operator *(Money money, int multiplier)
    {
        decimal result = money.Amount * multiplier;
        return new Money(result);
    }

    public static Money operator *(int multiplier, Money money)
    {
        return money * multiplier;
    }

    public static Money operator +(Money money1, Money money2)
    {
        decimal result = money1.Amount + money2.Amount;
        return new Money(result);
    }

    public static Money operator *(Money money, decimal multiplier)
    {
        decimal result = money.Amount * multiplier;
        return new Money(result);
    }

    public static Money operator *(decimal multiplier, Money money)
    {
        return money * multiplier;
    }

    public static Money operator -(Money money1, Money money2)
    {
        decimal result = money1.Amount - money2.Amount;
        return new Money(result);
    }
}

