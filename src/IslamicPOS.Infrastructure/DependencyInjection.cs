using IslamicPOS.Core.Barcoding.Interfaces;
using IslamicPOS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IslamicPOS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBarcodingServices(this IServiceCollection services)
        {
            services.AddScoped<IBarcodeService, BarcodeService>();
            services.AddScoped<IBarcodeGenerationService, BarcodeGenerationService>();
            
            return services;
        }
    }
}