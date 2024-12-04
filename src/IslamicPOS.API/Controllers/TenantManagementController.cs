using System;
using System.Threading.Tasks;
using IslamicPOS.Core.MultiTenant.Interfaces;
using IslamicPOS.Core.MultiTenant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IslamicPOS.API.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    [ApiController]
    [Route("api/[controller]")]
    public class TenantManagementController : ControllerBase
    {
        private readonly ITenantManager _tenantManager;

        public TenantManagementController(ITenantManager tenantManager)
        {
            _tenantManager = tenantManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Tenant>> RegisterTenant([FromBody] TenantRegistration registration)
        {
            try
            {
                var tenant = await _tenantManager.CreateTenantAsync(registration);
                return Ok(tenant);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{tenantId}")]
        public async Task<ActionResult<Tenant>> GetTenant(Guid tenantId)
        {
            var tenant = await _tenantManager.GetTenantAsync(tenantId);
            if (tenant == null)
                return NotFound();
            return Ok(tenant);
        }

        [HttpPut("{tenantId}")]
        public async Task<ActionResult> UpdateTenant(Guid tenantId, [FromBody] TenantUpdate update)
        {
            var result = await _tenantManager.UpdateTenantAsync(tenantId, update);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpPost("{tenantId}/deactivate")]
        public async Task<ActionResult> DeactivateTenant(Guid tenantId)
        {
            var result = await _tenantManager.DeactivateTenantAsync(tenantId);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpPost("{tenantId}/addons")]
        public async Task<ActionResult> AddAddonFeature(Guid tenantId, [FromBody] AddonFeature addon)
        {
            var result = await _tenantManager.AddAddonFeatureAsync(tenantId, addon);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpGet("{tenantId}/usage")]
        public async Task<ActionResult<TenantUsage>> GetTenantUsage(Guid tenantId)
        {
            var usage = await _tenantManager.GetTenantUsageAsync(tenantId);
            if (usage == null)
                return NotFound();
            return Ok(usage);
        }
    }
}