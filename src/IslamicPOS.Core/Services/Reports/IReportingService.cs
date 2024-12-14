using IslamicPOS.Core.Models.Reporting;

namespace IslamicPOS.Core.Services.Reports
{
    public interface IReportingService
    {
        Task<SalesSummary> GetDailySalesSummary(DateTime date);
        Task<ProfitReport> GetProfitReport(DateTime startDate, DateTime endDate);
        Task<ZakaahReport> GetZakaahReport(DateTime date);
    }
}