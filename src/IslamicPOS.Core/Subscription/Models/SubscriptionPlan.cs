using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Subscription.Models
{
    public class SubscriptionPlan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal AnnualPrice { get; set; }
        public List<PlanBenefit> Benefits { get; set; }
        public DiscountTier DiscountTier { get; set; }
        public bool IsActive { get; set; }
    }

    public class PlanBenefit
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public BenefitType Type { get; set; }
        public decimal Value { get; set; }
    }

    public enum BenefitType
    {
        DiscountPercentage,
        FreeDelivery,
        PrioritySupport,
        ExtraLoyaltyPoints,
        ExclusiveOffers
    }

    public enum DiscountTier
    {
        None,
        Basic,
        Premium,
        Elite
    }
}