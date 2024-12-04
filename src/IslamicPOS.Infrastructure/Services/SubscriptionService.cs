using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IslamicPOS.Core.Subscription.Interfaces;
using IslamicPOS.Core.Subscription.Models;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ApplicationDbContext _context;

        public SubscriptionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubscriptionPlan>> GetAvailablePlans()
        {
            return await _context.SubscriptionPlans
                .Where(p => p.IsActive)
                .Include(p => p.Benefits)
                .ToListAsync();
        }

        public async Task<SubscriptionPlan> GetPlan(Guid planId)
        {
            return await _context.SubscriptionPlans
                .Include(p => p.Benefits)
                .FirstOrDefaultAsync(p => p.Id == planId);
        }

        public async Task<CustomerSubscription> SubscribeCustomer(Guid customerId, Guid planId, BillingPeriod period)
        {
            var plan = await GetPlan(planId);
            if (plan == null)
                throw new Exception("Subscription plan not found");

            var existingSubscription = await GetCustomerSubscription(customerId);
            if (existingSubscription?.IsActive == true)
                throw new Exception("Customer already has an active subscription");

            var subscription = new CustomerSubscription
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                PlanId = planId,
                StartDate = DateTime.UtcNow,
                EndDate = period == BillingPeriod.Monthly 
                    ? DateTime.UtcNow.AddMonths(1) 
                    : DateTime.UtcNow.AddYears(1),
                BillingPeriod = period,
                IsActive = true,
                Price = period == BillingPeriod.Monthly 
                    ? plan.MonthlyPrice 
                    : plan.AnnualPrice
            };

            _context.CustomerSubscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            return subscription;
        }

        public async Task<CustomerSubscription> GetCustomerSubscription(Guid customerId)
        {
            return await _context.CustomerSubscriptions
                .Where(s => s.CustomerId == customerId && s.IsActive)
                .OrderByDescending(s => s.StartDate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CancelSubscription(Guid subscriptionId)
        {
            var subscription = await _context.CustomerSubscriptions
                .FirstOrDefaultAsync(s => s.Id == subscriptionId);

            if (subscription == null || !subscription.IsActive)
                return false;

            subscription.IsActive = false;
            subscription.EndDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalculateDiscountedPrice(Guid customerId, decimal originalPrice)
        {
            var subscription = await GetCustomerSubscription(customerId);
            if (subscription == null || !subscription.IsActive)
                return originalPrice;

            var plan = await GetPlan(subscription.PlanId);
            var discountPercentage = plan.DiscountTier switch
            {
                DiscountTier.Elite => 0.20m,
                DiscountTier.Premium => 0.15m,
                DiscountTier.Basic => 0.10m,
                _ => 0m
            };

            return originalPrice * (1 - discountPercentage);
        }
    }
}