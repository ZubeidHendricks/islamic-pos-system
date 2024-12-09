namespace IslamicPOS.Core.Models.Reports
{
    public class SalesSummary
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalTransactions { get; set; }
        public decimal AverageTransactionValue { get; set; }
        public List<DailySales> DailySalesBreakdown { get; set; } = new();
    }

    public class DailySales
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int TransactionCount { get; set; }
    }
}