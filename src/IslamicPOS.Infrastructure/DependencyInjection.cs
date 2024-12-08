namespace IslamicPOS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Auth Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        // Business Services
        services.AddScoped<IProfitDistributionService, ProfitDistributionService>();
        services.AddScoped<IPartnerService, PartnerService>();
        services.AddScoped<IReportingService, ReportingService>();
        services.AddScoped<IZakaahCalculator, ZakaahCalculator>();

        // Existing Services
        services.AddScoped<IBarcodeService, BarcodeService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ILoyaltyService, LoyaltyService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        return services;
    }
}