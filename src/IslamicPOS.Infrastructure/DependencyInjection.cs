using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Core.Loyalty.Interfaces;
using IslamicPOS.Core.Subscription.Interfaces;
using IslamicPOS.Core.Ticketing.Interfaces;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IslamicPOS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Register Core Services
            services.AddScoped<IZakaahCalculator, ZakaahCalculator>();
            services.AddScoped<IProfitDistributionService, ProfitDistributionService>();

            // Register Barcode Service
            services.AddScoped<IBarcodeService, BarcodeService>();

            // Register Ticketing Service
            services.AddScoped<ITicketService, TicketService>();

            // Register Loyalty Service
            services.AddScoped<ILoyaltyService, LoyaltyService>();

            // Register Subscription Service
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            return services;
        }
    }
}