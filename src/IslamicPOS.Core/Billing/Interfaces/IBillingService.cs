using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Billing.Models;

namespace IslamicPOS.Core.Billing.Interfaces
{
    public interface IBillingService
    {
        Task<Invoice> GenerateInvoiceAsync(Guid tenantId);
        Task<Invoice> GetInvoiceAsync(Guid invoiceId);
        Task<List<Invoice>> GetTenantInvoicesAsync(Guid tenantId);
        Task<InvoicePayment> ProcessPaymentAsync(Guid invoiceId, PaymentRequest request);
        Task<bool> CancelSubscriptionAsync(Guid tenantId);
        Task<SubscriptionDetails> GetSubscriptionDetailsAsync(Guid tenantId);
        Task<List<BillingPlan>> GetAvailablePlansAsync();
        Task<BillingPlan> GetPlanDetailsAsync(Guid planId);
        Task<bool> ChangePlanAsync(Guid tenantId, Guid newPlanId);
    }

    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public string PaymentToken { get; set; }
        public Dictionary<string, string> PaymentMetadata { get; set; }
    }

    public class SubscriptionDetails
    {
        public Guid TenantId { get; set; }
        public BillingPlan CurrentPlan { get; set; }
        public DateTime NextBillingDate { get; set; }
        public decimal NextBillingAmount { get; set; }
        public bool AutoRenew { get; set; }
        public string PaymentMethod { get; set; }
        public List<UsageMetric> CurrentUsage { get; set; }
    }

    public class UsageMetric
    {
        public string Name { get; set; }
        public int Current { get; set; }
        public int Limit { get; set; }
        public string Unit { get; set; }
    }
}