using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Billing.Models
{
    public class BillingPlan
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal AnnualPrice { get; set; }
        public SubscriptionTier Tier { get; set; }
        public bool IsActive { get; set; }
        public List<PlanFeature> Features { get; set; }
        public List<PlanLimit> Limits { get; set; }
    }

    public class PlanFeature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsIncluded { get; set; }
        public decimal? AdditionalCost { get; set; }
    }

    public class PlanLimit
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Unit { get; set; }
        public decimal? OverageCost { get; set; }
    }
}