using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Subscription.Interfaces;
using IslamicPOS.Core.Subscription.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("plans")]
        public async Task<ActionResult<List<SubscriptionPlan>>> GetAvailablePlans()
        {
            var plans = await _subscriptionService.GetAvailablePlans();
            return Ok(plans);
        }

        [HttpGet("plans/{planId}")]
        public async Task<ActionResult<SubscriptionPlan>> GetPlan(Guid planId)
        {
            var plan = await _subscriptionService.GetPlan(planId);
            if (plan == null)
                return NotFound();
            return Ok(plan);
        }

        [HttpPost("subscribe")]
        public async Task<ActionResult<CustomerSubscription>> Subscribe(SubscribeRequest request)
        {
            try
            {
                var subscription = await _subscriptionService.SubscribeCustomer(
                    request.CustomerId,
                    request.PlanId,
                    request.BillingPeriod);
                return Ok(subscription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<CustomerSubscription>> GetCustomerSubscription(Guid customerId)
        {
            var subscription = await _subscriptionService.GetCustomerSubscription(customerId);
            if (subscription == null)
                return NotFound();
            return Ok(subscription);
        }

        [HttpPost("{subscriptionId}/cancel")]
        public async Task<ActionResult> CancelSubscription(Guid subscriptionId)
        {
            var result = await _subscriptionService.CancelSubscription(subscriptionId);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpPost("calculate-price")]
        public async Task<ActionResult<decimal>> CalculateDiscountedPrice(CalculatePriceRequest request)
        {
            var price = await _subscriptionService.CalculateDiscountedPrice(
                request.CustomerId,
                request.OriginalPrice);
            return Ok(price);
        }
    }

    public class SubscribeRequest
    {
        public Guid CustomerId { get; set; }
        public Guid PlanId { get; set; }
        public BillingPeriod BillingPeriod { get; set; }
    }

    public class CalculatePriceRequest
    {
        public Guid CustomerId { get; set; }
        public decimal OriginalPrice { get; set; }
    }
}