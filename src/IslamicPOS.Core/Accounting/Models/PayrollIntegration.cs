using System;
using System.Collections.Generic;

namespace IslamicPOS.Core.Accounting.Models
{
    public class PayrollIntegration
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public PayrollSoftware Software { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string BaseUrl { get; set; }
        public Dictionary<string, string> Configuration { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastSyncAt { get; set; }
        public IntegrationStatus Status { get; set; }
        public PayrollMappingConfiguration Mappings { get; set; }
    }

    public enum PayrollSoftware
    {
        SagePayroll,
        ADP,
        Workday,
        Paychex,
        Custom
    }

    public class PayrollMappingConfiguration
    {
        public Dictionary<string, string> EmployeeFields { get; set; }
        public Dictionary<string, string> PaymentTypes { get; set; }
        public Dictionary<string, string> DeductionTypes { get; set; }
        public Dictionary<string, string> TaxCodes { get; set; }
        public Dictionary<string, string> Departments { get; set; }
        public BenefitsMapping Benefits { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
    }

    public class BenefitsMapping
    {
        public Dictionary<string, string> HealthInsurance { get; set; }
        public Dictionary<string, string> RetirementPlans { get; set; }
        public Dictionary<string, string> OtherBenefits { get; set; }
    }
}