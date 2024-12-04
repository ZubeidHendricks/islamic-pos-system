using System;
using System.Threading.Tasks;
using IslamicPOS.Core.Provisioning.Interfaces;
using IslamicPOS.Core.MultiTenant.Models;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Provisioning.Services
{
    public class TenantProvisioner : ITenantProvisioner
    {
        private readonly string _masterConnectionString;
        private readonly ILogger<TenantProvisioner> _logger;

        public TenantProvisioner(
            IConfiguration configuration,
            ILogger<TenantProvisioner> logger)
        {
            _masterConnectionString = configuration.GetConnectionString("DefaultConnection");
            _logger = logger;
        }

        public async Task<ProvisioningResult> ProvisionTenantAsync(TenantRegistration registration)
        {
            try
            {
                // Generate tenant database name
                var databaseName = $"tenant_{registration.Domain.Replace(".", "_")}";
                var tenantId = Guid.NewGuid();

                // Create tenant database
                await CreateTenantDatabaseAsync(databaseName);

                // Initialize schema
                var connectionString = GenerateConnectionString(databaseName);
                await InitializeDatabaseSchemaAsync(connectionString);

                // Setup initial data
                await SetupInitialDataAsync(connectionString, registration);

                return new ProvisioningResult
                {
                    Success = true,
                    TenantId = tenantId,
                    ConnectionString = connectionString,
                    Resources = new Dictionary<string, string>
                    {
                        { "DatabaseName", databaseName },
                        { "Domain", registration.Domain }
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to provision tenant {Domain}", registration.Domain);
                return new ProvisioningResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<bool> DeProvisionTenantAsync(Guid tenantId)
        {
            try
            {
                // Get tenant details
                using var context = new TenantDbContext(_masterConnectionString);
                var tenant = await context.Tenants.FindAsync(tenantId);
                if (tenant == null) return false;

                // Drop tenant database
                await DropTenantDatabaseAsync(tenant.ConnectionString);

                // Remove tenant record
                context.Tenants.Remove(tenant);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to deprovision tenant {TenantId}", tenantId);
                return false;
            }
        }

        public async Task<bool> UpdateTenantResourcesAsync(Guid tenantId, TenantResourceUpdate update)
        {
            try
            {
                using var context = new TenantDbContext(_masterConnectionString);
                var tenant = await context.Tenants.FindAsync(tenantId);
                if (tenant == null) return false;

                if (update.UserLimit.HasValue)
                    tenant.Limits.MaxUsers = update.UserLimit.Value;

                if (update.StorageLimit.HasValue)
                    tenant.Limits.StorageLimit = update.StorageLimit.Value;

                if (update.CustomSettings != null)
                {
                    foreach (var setting in update.CustomSettings)
                    {
                        tenant.Settings.CustomSettings[setting.Key] = setting.Value;
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update tenant resources {TenantId}", tenantId);
                return false;
            }
        }

        private async Task CreateTenantDatabaseAsync(string databaseName)
        {
            using var connection = new SqlConnection(_masterConnectionString);
            await connection.OpenAsync();

            var sql = $"CREATE DATABASE [{databaseName}]";
            using var command = new SqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync();
        }

        private async Task InitializeDatabaseSchemaAsync(string connectionString)
        {
            using var context = new ApplicationDbContext(connectionString);
            await context.Database.MigrateAsync();
        }

        private async Task SetupInitialDataAsync(string connectionString, TenantRegistration registration)
        {
            using var context = new ApplicationDbContext(connectionString);

            // Add initial admin user
            var adminUser = new User
            {
                Email = registration.AdminEmail,
                UserName = registration.AdminEmail,
                IsAdmin = true
            };

            context.Users.Add(adminUser);

            // Add default settings
            var settings = new List<Setting>
            {
                new Setting { Key = "CompanyName", Value = registration.CompanyName },
                new Setting { Key = "TimeZone", Value = registration.Settings.TimeZone },
                new Setting { Key = "Currency", Value = registration.Settings.Currency }
            };

            context.Settings.AddRange(settings);

            await context.SaveChangesAsync();
        }

        private string GenerateConnectionString(string databaseName)
        {
            var builder = new SqlConnectionStringBuilder(_masterConnectionString)
            {
                InitialCatalog = databaseName
            };

            return builder.ConnectionString;
        }
    }
}