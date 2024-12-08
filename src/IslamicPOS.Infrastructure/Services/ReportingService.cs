using IslamicPOS.Core.Interfaces;

namespace IslamicPOS.Infrastructure.Services;

public class ReportingService
{
    private readonly ITransactionRepository _transactionRepository;

    public ReportingService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<SalesReport> GenerateSalesReport(DateTime startDate, DateTime endDate)
    {
        var transactions = await _transactionRepository.GetByDateRangeAsync(startDate, endDate);

        return new SalesReport
        {
            StartDate = startDate,
            EndDate = endDate,
            TotalSales = transactions.Sum(t => t.TotalAmount),
            TransactionCount = transactions.Count(),
            AverageTransactionValue = transactions.Any() 
                ? transactions.Average(t => t.TotalAmount)
                : 0,
            PaymentMethodBreakdown = transactions
                .GroupBy(t => t.PaymentMethod)
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(t => t.TotalAmount)
                )
        };
    }
}

public class SalesReport
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalSales { get; set; }
    public int TransactionCount { get; set; }
    public decimal AverageTransactionValue { get; set; }
    public Dictionary<PaymentMethod, decimal> PaymentMethodBreakdown { get; set; }
}