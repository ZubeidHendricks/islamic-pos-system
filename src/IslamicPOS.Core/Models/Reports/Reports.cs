namespace IslamicPOS.Core.Models.Reports
{
    public class ProfitReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<ProfitEntry> Entries { get; set; } = new();
    }

    public class ProfitEntry
    {
        public DateTime Date { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal Profit { get; set; }
    }

    public class ZakaahReport
    {
        public DateTime CalculationDate { get; set; }
        public decimal TotalWealth { get; set; }
        public decimal ZakaahAmount { get; set; }
        public bool IsZakaahDue { get; set; }
        public List<AssetEntry> Assets { get; set; } = new();
    }

    public class AssetEntry
    {
        public string AssetType { get; set; } = string.Empty;
        public decimal Value { get; set; }
    }

    public class PartnerReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PartnerEntry> Entries { get; set; } = new();
    }

    public class PartnerEntry
    {
        public string PartnerId { get; set; } = string.Empty;
        public string PartnerName { get; set; } = string.Empty;
        public decimal Investment { get; set; }
        public decimal ProfitShare { get; set; }
    }
}