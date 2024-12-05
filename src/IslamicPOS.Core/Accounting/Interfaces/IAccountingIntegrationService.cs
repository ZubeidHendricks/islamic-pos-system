using System;
using System.Threading.Tasks;
using IslamicPOS.Core.Accounting.Models;

namespace IslamicPOS.Core.Accounting.Interfaces
{
    public interface IAccountingIntegrationService
    {
        Task<AccountingIntegration> ConfigureIntegrationAsync(AccountingIntegrationRequest request);
        Task<bool> SyncTransactionsAsync(Guid tenantId, DateTime startDate, DateTime endDate);
        Task<bool> SyncInventoryAsync(Guid tenantId);
        Task<bool> SyncChartOfAccountsAsync(Guid tenantId);
        Task<bool> SyncTaxRatesAsync(Guid tenantId);
        Task<TransactionSyncResult> ProcessTransactionAsync(SaleTransaction transaction);
        Task<bool> ValidateConfigurationAsync(AccountingIntegration integration);
        Task<IntegrationStatus> GetIntegrationStatusAsync(Guid tenantId);
    }

    public class AccountingIntegrationRequest
    {
        public Guid TenantId { get; set; }
        public AccountingSoftware Software { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string BaseUrl { get; set; }
        public MappingConfiguration Mappings { get; set; }
        public List<string> EnabledFeatures { get; set; }
    }

    public class TransactionSyncResult
    {
        public bool Success { get; set; }
        public string ReferenceId { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
    }
}