using IslamicPOS.Infrastructure.Persistence;
using IslamicPOS.Infrastructure.Services;
using IslamicPOS.Infrastructure.Repositories;

namespace IslamicPOS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IPartnerRepository, PartnerRepository>();
        services.AddScoped<IProfitSharingRepository, ProfitSharingRepository>();
        services.AddScoped<IZakatCalculationRepository, ZakatCalculationRepository>();

        // Register services
        services.AddScoped<IFinancialService, FinancialService>();
        services.AddScoped<IZakatService, ZakatService>();
        services.AddScoped<IProfitSharingService, ProfitSharingService>();

        return services;
    }
}