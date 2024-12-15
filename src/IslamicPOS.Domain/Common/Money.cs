namespace IslamicPOS.Domain.Common;

public record Money
{
    public decimal Amount { get; init; }
    public string Currency { get; init; } = "USD";

    public Money(decimal amount, string currency = "USD")
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Zero(string currency = "USD") => new(0, currency);

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot add money with different currencies");
            
        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new InvalidOperationException("Cannot subtract money with different currencies");
            
        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money money, int quantity)
    {
        return new Money(money.Amount * quantity, money.Currency);
    }

    public static Money operator *(Money money, decimal multiplier)
    {
        return new Money(money.Amount * multiplier, money.Currency);
    }
}
