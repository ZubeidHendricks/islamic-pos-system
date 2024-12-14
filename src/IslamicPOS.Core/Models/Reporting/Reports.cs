using IslamicPOS.Core.Models.Common;

namespace IslamicPOS.Core.Models.Reporting
{
    public class SalesSummary
    {
        public DateTime Date { get; set; }
        public int TransactionCount { get; set; }
        public Money TotalSales { get; set; }
        public Money AverageTransactionValue { get; set; }
    }

    public class ProfitReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Money GrossProfit { get; set; }
        public Money NetProfit { get; set; }
        public decimal ProfitMargin { get; set; }
    }

    public class ZakaahReport
    {
        public DateTime CalculationDate { get; set; }
        public Money TotalAssessableAmount { get; set; }
        public Money ZakaahDue { get; set; }
        public bool AboveNisab { get; set; }
    }
}