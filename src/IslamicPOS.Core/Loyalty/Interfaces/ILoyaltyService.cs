using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Loyalty.Models;

namespace IslamicPOS.Core.Loyalty.Interfaces
{
    public interface ILoyaltyService
    {
        Task<LoyaltyAccount> GetOrCreateAccount(Guid customerId);
        Task<LoyaltyAccount> AddPoints(Guid customerId, int points, string description, Guid? orderId = null);
        Task<LoyaltyAccount> RedeemPoints(Guid customerId, int points, string description);
        Task<LoyaltyTier> CalculateCustomerTier(Guid customerId);
        Task<decimal> CalculateDiscountPercentage(Guid customerId);
        Task<List<LoyaltyTransaction>> GetTransactionHistory(Guid customerId);
    }
}