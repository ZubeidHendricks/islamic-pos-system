using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Core.Loyalty.Interfaces;
using IslamicPOS.Core.Subscription.Interfaces;
using IslamicPOS.Core.Ticketing.Interfaces;
using IslamicPOS.Core.Services;
using IslamicPOS.Core.Services.Auth;
using IslamicPOS.Core.Services.Financial;
using IslamicPOS.Core.Services.Reports;
using IslamicPOS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace IslamicPOS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Auth Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        // Core Business Services
        services.AddScoped<IProfitDistributionService, ProfitDistributionService>();
        services.AddScoped<IPartnerService, PartnerService>();
        services.AddScoped<IReportingService, ReportingService>();
        services.AddScoped<IZakaahCalculator, ZakaahCalculator>();

        // Integration Services
        services.AddScoped<IBarcodeService, BarcodeService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ILoyaltyService, LoyaltyService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        // Configuration
        services.Configure<JwtSettings>(configuration.GetSection("JWT"));
        services.Configure<ZakaahSettings>(configuration.GetSection("ZakaahSettings"));

        return services;
    }
}