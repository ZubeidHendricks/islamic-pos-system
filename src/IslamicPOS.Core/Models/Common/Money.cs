namespace IslamicPOS.Core.Models.Common;

public class Money
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";

    public Money(decimal amount, string currency = "USD")
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Zero(string currency = "USD") => new(0, currency);
}