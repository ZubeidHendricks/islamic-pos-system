namespace IslamicPOS.Core.Services.Reporting;

public interface IReportingService
{
    Task<SalesSummary> GetSalesSummaryAsync(DateTime startDate, DateTime endDate);
    Task<ProfitReport> GetProfitReportAsync(DateTime startDate, DateTime endDate);
    Task<ZakaahReport> GetZakaahReportAsync(DateTime startDate, DateTime endDate);
    Task<PartnerReport> GetPartnerReportAsync(int partnerId, DateTime startDate, DateTime endDate);
    Task<byte[]> ExportReportAsync(ReportType type, DateTime startDate, DateTime endDate, string format);
}