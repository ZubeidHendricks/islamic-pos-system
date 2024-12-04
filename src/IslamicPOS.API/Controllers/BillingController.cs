using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IslamicPOS.Core.Billing.Interfaces;
using IslamicPOS.Core.Billing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [Authorize(Roles = "TenantAdmin")]
        [HttpGet("plans")]
        public async Task<ActionResult<List<BillingPlan>>> GetAvailablePlans()
        {
            var plans = await _billingService.GetAvailablePlansAsync();
            return Ok(plans);
        }

        [Authorize(Roles = "TenantAdmin")]
        [HttpGet("subscription")]
        public async Task<ActionResult<SubscriptionDetails>> GetSubscriptionDetails()
        {
            var tenantId = GetCurrentTenantId(); // From JWT token
            var details = await _billingService.GetSubscriptionDetailsAsync(tenantId);
            return Ok(details);
        }

        [Authorize(Roles = "TenantAdmin")]
        [HttpPost("subscribe")]
        public async Task<ActionResult> ChangePlan(Guid planId)
        {
            var tenantId = GetCurrentTenantId();
            var result = await _billingService.ChangePlanAsync(tenantId, planId);
            if (!result)
                return BadRequest("Unable to change plan");
            return Ok();
        }

        [Authorize(Roles = "TenantAdmin")]
        [HttpPost("cancel")]
        public async Task<ActionResult> CancelSubscription()
        {
            var tenantId = GetCurrentTenantId();
            var result = await _billingService.CancelSubscriptionAsync(tenantId);
            if (!result)
                return BadRequest("Unable to cancel subscription");
            return Ok();
        }

        [Authorize(Roles = "TenantAdmin")]
        [HttpGet("invoices")]
        public async Task<ActionResult<List<Invoice>>> GetInvoices()
        {
            var tenantId = GetCurrentTenantId();
            var invoices = await _billingService.GetTenantInvoicesAsync(tenantId);
            return Ok(invoices);
        }

        [Authorize(Roles = "TenantAdmin")]
        [HttpPost("invoices/{invoiceId}/pay")]
        public async Task<ActionResult<InvoicePayment>> ProcessPayment(
            Guid invoiceId, [FromBody] PaymentRequest request)
        {
            var payment = await _billingService.ProcessPaymentAsync(invoiceId, request);
            return Ok(payment);
        }

        private Guid GetCurrentTenantId()
        {
            // Extract tenant ID from JWT token
            var tenantIdClaim = User.FindFirst("TenantId");
            if (tenantIdClaim == null)
                throw new UnauthorizedAccessException("Tenant ID not found");

            return Guid.Parse(tenantIdClaim.Value);
        }
    }
}