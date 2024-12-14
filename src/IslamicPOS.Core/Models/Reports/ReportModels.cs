using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Reports;

public enum ReportType
{
    Sales,
    Profit,
    Zakaah,
    Partner,
    Quality,
    Inventory
}

public abstract class Report : Entity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ReportType Type { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}

public class SalesSummary : Report
{
    public decimal TotalRevenue { get; set; }
    public int TotalTransactions { get; set; }
    public decimal AverageTransactionValue { get; set; }
    public Dictionary<string, decimal> RevenueByProduct { get; set; } = new();
    public Dictionary<string, int> TransactionsByDay { get; set; } = new();
}

public class ProfitReport : Report
{
    public decimal TotalRevenue { get; set; }
    public decimal TotalCosts { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal NetProfit { get; set; }
    public decimal ProfitMargin { get; set; }
    public Dictionary<string, decimal> ProfitByCategory { get; set; } = new();
}

public class ZakaahReport : Report
{
    public decimal TotalZakaableAssets { get; set; }
    public decimal ZakaahAmount { get; set; }
    public decimal BusinessAssets { get; set; }
    public decimal CashAndEquivalents { get; set; }
    public decimal Inventory { get; set; }
    public decimal Receivables { get; set; }
    public decimal Deductions { get; set; }
}

public class PartnerReport : Report
{
    public Guid PartnerId { get; set; }
    public string PartnerName { get; set; } = string.Empty;
    public decimal TotalInvestment { get; set; }
    public decimal SharePercentage { get; set; }
    public decimal TotalEarnings { get; set; }
    public decimal ROI { get; set; }
    public List<DistributionEntry> Distributions { get; set; } = new();
}

public class DistributionEntry
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class InspectionSummary : Report
{
    public string InspectorName { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;
    public int TotalItems { get; set; }
    public int PassedItems { get; set; }
    public int FailedItems { get; set; }
    public List<string> Issues { get; set; } = new();
}