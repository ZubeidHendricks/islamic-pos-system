using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Subscription.Models;

namespace IslamicPOS.Core.Subscription.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<SubscriptionPlan>> GetAvailablePlans();
        Task<SubscriptionPlan> GetPlan(Guid planId);
        Task<CustomerSubscription> SubscribeCustomer(Guid customerId, Guid planId, BillingPeriod period);
        Task<CustomerSubscription> GetCustomerSubscription(Guid customerId);
        Task<bool> CancelSubscription(Guid subscriptionId);
        Task<decimal> CalculateDiscountedPrice(Guid customerId, decimal originalPrice);
    }

    public class CustomerSubscription
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
    }

    public enum BillingPeriod
    {
        Monthly,
        Annual
    }
}