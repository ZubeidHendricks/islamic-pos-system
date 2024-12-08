using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Loyalty.Interfaces;
using IslamicPOS.Core.Loyalty.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoyaltyController : ControllerBase
    {
        private readonly ILoyaltyService _loyaltyService;

        public LoyaltyController(ILoyaltyService loyaltyService)
        {
            _loyaltyService = loyaltyService;
        }

        [HttpGet("account/{customerId}")]
        public async Task<ActionResult<LoyaltyAccount>> GetAccount(Guid customerId)
        {
            var account = await _loyaltyService.GetOrCreateAccount(customerId);
            return Ok(account);
        }

        [HttpPost("points/add")]
        public async Task<ActionResult<LoyaltyAccount>> AddPoints(AddPointsRequest request)
        {
            var account = await _loyaltyService.AddPoints(
                request.CustomerId,
                request.Points,
                request.Description,
                request.OrderId);
            return Ok(account);
        }

        [HttpPost("points/redeem")]
        public async Task<ActionResult<LoyaltyAccount>> RedeemPoints(RedeemPointsRequest request)
        {
            try
            {
                var account = await _loyaltyService.RedeemPoints(
                    request.CustomerId,
                    request.Points,
                    request.Description);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("tier/{customerId}")]
        public async Task<ActionResult<LoyaltyTier>> GetCustomerTier(Guid customerId)
        {
            var tier = await _loyaltyService.CalculateCustomerTier(customerId);
            return Ok(tier);
        }

        [HttpGet("discount/{customerId}")]
        public async Task<ActionResult<decimal>> GetDiscountPercentage(Guid customerId)
        {
            var discount = await _loyaltyService.CalculateDiscountPercentage(customerId);
            return Ok(discount);
        }

        [HttpGet("transactions/{customerId}")]
        public async Task<ActionResult<List<LoyaltyTransaction>>> GetTransactionHistory(Guid customerId)
        {
            var transactions = await _loyaltyService.GetTransactionHistory(customerId);
            return Ok(transactions);
        }
    }

    public class AddPointsRequest
    {
        public Guid CustomerId { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }
        public Guid? OrderId { get; set; }
    }

    public class RedeemPointsRequest
    {
        public Guid CustomerId { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }
    }
}