namespace SadOrder;

public class Money
{
    public static readonly Currency DefaultCurrency = Currency.GetInstance("EUR");

    public static readonly Money ZERO = new Money(decimal.Zero);

    public virtual decimal Value { get; private set; } = 0;

    private String _currencyCode;

    protected Money()
    {
    }

    public virtual decimal GetValue() => Value;

    public Money(decimal value, Currency currency) : this(value, currency.GetCurrencyCode())
    {
    }

    private Money(decimal value, String currencyCode)
    {
        this.Value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
        this._currencyCode = currencyCode;
    }

    public Money(decimal value) : this(value, DefaultCurrency)
    {
    }

    public Money(double value, Currency currency) : this(new decimal(value), currency.GetCurrencyCode())
    {
    }

    public Money(double value): this(value, DefaultCurrency)
    {
    }

    public Money MultiplyBy(double multiplier)
    {
        return MultiplyBy(new decimal(multiplier));
    }

    public Money MultiplyBy(decimal multiplier)
    {
        return new Money(decimal.Multiply(Value, multiplier), _currencyCode);
    }

    public Money Add(Money money)
    {
        if (!CompatibleCurrency(money))
        {
            throw new Exception("Currency mismatch");
        }

        return new Money(decimal.Add(Value, money.Value), DetermineCurrencyCode(money));
    }

    public Money Subtract(Money money)
    {
        if (!CompatibleCurrency(money))
            throw new Exception("Currency mismatch");

        return new Money(decimal.Subtract(Value, money.Value), DetermineCurrencyCode(money));
    }

    /**
	 * Currency is compatible if the same or either money object has zero value.
	 */
    private bool CompatibleCurrency(Money money)
    {
        return IsZero(Value) || IsZero(money.Value) || _currencyCode.Equals(money.GetCurrencyCode());
    }

    private bool IsZero(decimal testedValue)
    {
        return decimal.Zero.CompareTo(testedValue) == 0;
    }

    /**
	 * @return currency from this object or otherCurrencyCode. Preferred is the
	 *         one that comes from Money that has non-zero value.
	 */
    private Currency DetermineCurrencyCode(Money otherMoney)
    {
        String resultingCurrenctCode = IsZero(Value) ? otherMoney._currencyCode : _currencyCode;
        return Currency.GetInstance(resultingCurrenctCode);
    }

    public virtual String GetCurrencyCode()
    {
        return _currencyCode;
    }

    public Currency GetCurrency()
    {
        return Currency.GetInstance(_currencyCode);
    }

    public bool GreaterThan(Money other)
    {
        Validate.IsTrue(CompatibleCurrency(other));
        return Value.CompareTo(other.Value) > 0;
    }

    public bool LessThan(Money other)
    {
        Validate.IsTrue(CompatibleCurrency(other));
        return Value.CompareTo(other.Value) < 0;
    }

    public bool LessOrEquals(Money other)
    {
        Validate.IsTrue(CompatibleCurrency(other));
        return Value.CompareTo(other.Value) <= 0;
    }

    public override bool Equals(Object obj)
    {
        if (obj is Money) {
            Money money = (Money)obj;
            return CompatibleCurrency(money) && Value.CompareTo(money.Value) == 0;
        }
        return false;
    }

    public override string ToString()
    {
        return string.Format("%0$.2f %s", Value, GetCurrency().GetSymbol());
    }
}