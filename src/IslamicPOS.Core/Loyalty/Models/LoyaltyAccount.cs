using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Loyalty.Models
{
    public class LoyaltyAccount
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Points { get; set; }
        public LoyaltyTier Tier { get; set; }
        public DateTime LastActivity { get; set; }
        public List<LoyaltyTransaction> Transactions { get; set; }
    }

    public class LoyaltyTransaction
    {
        public Guid Id { get; set; }
        public Guid LoyaltyAccountId { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }
        public Guid? OrderId { get; set; }
    }

    public enum LoyaltyTier
    {
        Bronze,
        Silver,
        Gold,
        Platinum
    }

    public enum TransactionType
    {
        Earned,
        Redeemed,
        Expired,
        Bonus
    }
}