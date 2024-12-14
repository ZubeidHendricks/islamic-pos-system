using IslamicPOS.Core.Models.IslamicFinance;

namespace IslamicPOS.Core.Services.Financial
{
    public interface IProfitDistributionService
    {
        Task<ProfitSharing> CalculateDistribution(decimal totalAmount);
        Task<IEnumerable<ProfitSharing>> GetDistributionHistory(DateTime startDate, DateTime endDate);
    }
}