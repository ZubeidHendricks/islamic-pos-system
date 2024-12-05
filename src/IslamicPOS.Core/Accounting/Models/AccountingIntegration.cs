using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Accounting.Models
{
    public class AccountingIntegration
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public AccountingSoftware Software { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string BaseUrl { get; set; }
        public Dictionary<string, string> Configuration { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastSyncAt { get; set; }
        public IntegrationStatus Status { get; set; }
        public List<string> EnabledFeatures { get; set; }
        public MappingConfiguration Mappings { get; set; }
    }

    public enum AccountingSoftware
    {
        Sage,
        QuickBooks,
        Xero,
        Zoho,
        Tally,
        Custom
    }

    public enum IntegrationStatus
    {
        Configured,
        Active,
        Error,
        Suspended
    }

    public class MappingConfiguration
    {
        public AccountMapping Accounts { get; set; }
        public TaxMapping Taxes { get; set; }
        public CategoryMapping Categories { get; set; }
        public PaymentMapping PaymentMethods { get; set; }
    }

    public class AccountMapping
    {
        public string SalesAccount { get; set; }
        public string PurchaseAccount { get; set; }
        public string InventoryAccount { get; set; }
        public string PayrollAccount { get; set; }
        public string ZakaahAccount { get; set; }
        public Dictionary<string, string> CustomAccounts { get; set; }
    }

    public class TaxMapping
    {
        public Dictionary<string, string> TaxRates { get; set; }
        public string DefaultTaxCode { get; set; }
    }

    public class CategoryMapping
    {
        public Dictionary<string, string> ExpenseCategories { get; set; }
        public Dictionary<string, string> IncomeCategories { get; set; }
    }

    public class PaymentMapping
    {
        public Dictionary<string, string> PaymentMethods { get; set; }
        public string DefaultPaymentMethod { get; set; }
    }
}