using System;
using System.Threading.Tasks;
using IslamicPOS.Core.MultiTenant.Models;

namespace IslamicPOS.Core.Provisioning.Interfaces
{
    public interface ITenantProvisioner
    {
        Task<ProvisioningResult> ProvisionTenantAsync(TenantRegistration registration);
        Task<bool> DeProvisionTenantAsync(Guid tenantId);
        Task<bool> UpdateTenantResourcesAsync(Guid tenantId, TenantResourceUpdate update);
        Task<ProvisioningStatus> GetProvisioningStatusAsync(Guid tenantId);
    }

    public class ProvisioningResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid TenantId { get; set; }
        public string ConnectionString { get; set; }
        public Dictionary<string, string> Resources { get; set; }
    }

    public class TenantResourceUpdate
    {
        public int? UserLimit { get; set; }
        public int? StorageLimit { get; set; }
        public bool? EnableAdvancedFeatures { get; set; }
        public Dictionary<string, string> CustomSettings { get; set; }
    }

    public enum ProvisioningStatus
    {
        Pending,
        InProgress,
        Completed,
        Failed
    }
}