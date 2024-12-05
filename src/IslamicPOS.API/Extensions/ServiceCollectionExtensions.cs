using IslamicPOS.Core.Email.Interfaces;
using IslamicPOS.Core.Email.Models;
using IslamicPOS.Infrastructure.Email.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IslamicPOS.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEmailServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure email settings
            services.Configure<EmailSettings>(configuration.GetSection("Email"));

            // Register email services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITemplateRenderer, HandlebarsTemplateRenderer>();

            // Register email queue service
            services.AddHostedService<EmailQueueService>();

            return services;
        }
    }
}