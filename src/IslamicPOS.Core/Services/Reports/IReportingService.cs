namespace IslamicPOS.Core.Services.Reports;

public interface IReportingService
{
    Task<SalesSummary> GetSalesSummaryAsync(DateTime startDate, DateTime endDate);
    Task<ProfitReport> GetProfitReportAsync(DateTime startDate, DateTime endDate);
    Task<ZakaahReport> GetZakaahReportAsync(DateTime startDate, DateTime endDate);
}