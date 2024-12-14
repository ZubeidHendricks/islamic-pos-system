using IslamicPOS.Core.Models.Transaction;

namespace IslamicPOS.Core.Services.Reporting
{
    public interface IReportingService
    {
        Task<IEnumerable<Transaction>> GetTransactionHistory(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalSales(DateTime startDate, DateTime endDate);
        Task<decimal> GetTotalZakat(DateTime startDate, DateTime endDate);
        Task<IEnumerable<TransactionSummary>> GetDailySummary(DateTime date);
    }

    public class TransactionSummary
    {
        public DateTime Date { get; set; }
        public int TransactionCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ZakatAmount { get; set; }
    }
}