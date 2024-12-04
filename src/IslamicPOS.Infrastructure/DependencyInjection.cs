using IslamicPOS.Core.Logistics.Interfaces;
using IslamicPOS.Infrastructure.Logistics.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IslamicPOS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogisticsServices(this IServiceCollection services)
        {
            services.AddScoped<IRouteOptimizationService, RouteOptimizationService>();
            services.AddScoped<IVendorDeliveryService, VendorDeliveryService>();

            return services;
        }
    }
}