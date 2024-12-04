using System;
using System.Linq;
using System.Threading.Tasks;
using IslamicPOS.Core.MultiTenant.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IslamicPOS.API.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantManager tenantManager)
        {
            // Check for tenant identifier in headers or domain
            var tenantId = GetTenantIdentifier(context);
            if (tenantId != null)
            {
                // Validate tenant and subscription
                var tenant = await tenantManager.GetTenantAsync(tenantId.Value);
                if (tenant != null && tenant.IsActive)
                {
                    var isValid = await tenantManager.ValidateTenantSubscriptionAsync(tenantId.Value);
                    if (isValid)
                    {
                        // Add tenant info to context
                        context.Items["TenantId"] = tenantId;
                        context.Items["Tenant"] = tenant;

                        // Add tenant claims to user if authenticated
                        if (context.User.Identity?.IsAuthenticated == true)
                        {
                            var identity = context.User.Identity as System.Security.Claims.ClaimsIdentity;
                            identity?.AddClaim(new System.Security.Claims.Claim("TenantId", tenantId.ToString()));
                            identity?.AddClaim(new System.Security.Claims.Claim("TenantName", tenant.CompanyName));
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 402; // Payment Required
                        return;
                    }
                }
                else
                {
                    context.Response.StatusCode = 404; // Not Found
                    return;
                }
            }

            await _next(context);
        }

        private Guid? GetTenantIdentifier(HttpContext context)
        {
            // Try to get from header
            var tenantHeader = context.Request.Headers["X-Tenant-ID"].FirstOrDefault();
            if (!string.IsNullOrEmpty(tenantHeader) && Guid.TryParse(tenantHeader, out var tenantId))
            {
                return tenantId;
            }

            // Try to get from subdomain
            var host = context.Request.Host.Host.ToLower();
            var parts = host.Split('.');
            if (parts.Length > 2 && Guid.TryParse(parts[0], out tenantId))
            {
                return tenantId;
            }

            return null;
        }
    }
}