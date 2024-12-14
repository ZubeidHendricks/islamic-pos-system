namespace IslamicPOS.Core.Models.Common
{
    public record Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency = "USD")
        {
            return new Money(amount, currency);
        }

        public static implicit operator decimal(Money money) => money.Amount;

        public Money Add(Money other)
        {
            if (other.Currency != Currency)
                throw new InvalidOperationException("Cannot add money with different currencies");

            return Create(Amount + other.Amount, Currency);
        }

        public Money Multiply(decimal factor)
        {
            return Create(Amount * factor, Currency);
        }
    }
}