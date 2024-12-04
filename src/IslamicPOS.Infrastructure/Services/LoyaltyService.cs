using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IslamicPOS.Core.Loyalty.Interfaces;
using IslamicPOS.Core.Loyalty.Models;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Services
{
    public class LoyaltyService : ILoyaltyService
    {
        private readonly ApplicationDbContext _context;
        private const int POINTS_PER_CURRENCY = 1;
        private const int BRONZE_THRESHOLD = 0;
        private const int SILVER_THRESHOLD = 1000;
        private const int GOLD_THRESHOLD = 5000;
        private const int PLATINUM_THRESHOLD = 10000;

        public LoyaltyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoyaltyAccount> GetOrCreateAccount(Guid customerId)
        {
            var account = await _context.LoyaltyAccounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.CustomerId == customerId);

            if (account == null)
            {
                account = new LoyaltyAccount
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerId,
                    Points = 0,
                    Tier = LoyaltyTier.Bronze,
                    LastActivity = DateTime.UtcNow,
                    Transactions = new List<LoyaltyTransaction>()
                };

                _context.LoyaltyAccounts.Add(account);
                await _context.SaveChangesAsync();
            }

            return account;
        }

        public async Task<LoyaltyAccount> AddPoints(Guid customerId, int points, string description, Guid? orderId = null)
        {
            var account = await GetOrCreateAccount(customerId);

            var transaction = new LoyaltyTransaction
            {
                Id = Guid.NewGuid(),
                LoyaltyAccountId = account.Id,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Earned,
                Points = points,
                Description = description,
                OrderId = orderId
            };

            account.Points += points;
            account.LastActivity = DateTime.UtcNow;
            account.Transactions.Add(transaction);
            account.Tier = await CalculateCustomerTier(customerId);

            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<LoyaltyAccount> RedeemPoints(Guid customerId, int points, string description)
        {
            var account = await GetOrCreateAccount(customerId);

            if (account.Points < points)
                throw new Exception("Insufficient points");

            var transaction = new LoyaltyTransaction
            {
                Id = Guid.NewGuid(),
                LoyaltyAccountId = account.Id,
                TransactionDate = DateTime.UtcNow,
                Type = TransactionType.Redeemed,
                Points = -points,
                Description = description
            };

            account.Points -= points;
            account.LastActivity = DateTime.UtcNow;
            account.Transactions.Add(transaction);

            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<LoyaltyTier> CalculateCustomerTier(Guid customerId)
        {
            var account = await GetOrCreateAccount(customerId);
            var totalPoints = account.Points;

            if (totalPoints >= PLATINUM_THRESHOLD)
                return LoyaltyTier.Platinum;
            if (totalPoints >= GOLD_THRESHOLD)
                return LoyaltyTier.Gold;
            if (totalPoints >= SILVER_THRESHOLD)
                return LoyaltyTier.Silver;

            return LoyaltyTier.Bronze;
        }

        public async Task<decimal> CalculateDiscountPercentage(Guid customerId)
        {
            var tier = await CalculateCustomerTier(customerId);
            return tier switch
            {
                LoyaltyTier.Platinum => 0.15m,
                LoyaltyTier.Gold => 0.10m,
                LoyaltyTier.Silver => 0.05m,
                _ => 0m
            };
        }

        public async Task<List<LoyaltyTransaction>> GetTransactionHistory(Guid customerId)
        {
            var account = await GetOrCreateAccount(customerId);
            return account.Transactions
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }
    }
}