using IslamicPOS.API.Middleware;
using IslamicPOS.Core.Billing.Interfaces;
using IslamicPOS.Core.MultiTenant.Interfaces;
using IslamicPOS.Infrastructure.Billing.Services;
using IslamicPOS.Infrastructure.Data.MultiTenant;
using IslamicPOS.Infrastructure.MultiTenant.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IslamicPOS.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add database contexts
            services.AddDbContext<TenantDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            // Add multi-tenant services
            services.AddScoped<ITenantManager, TenantManager>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IPaymentProcessor, StripePaymentProcessor>();

            // Add authentication and authorization
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSystemAdmin", policy =>
                    policy.RequireRole("SystemAdmin"));
                options.AddPolicy("RequireTenantAdmin", policy =>
                    policy.RequireRole("TenantAdmin"));
            });

            // Add controllers
            services.AddControllers();

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Islamic POS API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Islamic POS API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Add tenant middleware before authentication
            app.UseMiddleware<TenantMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}