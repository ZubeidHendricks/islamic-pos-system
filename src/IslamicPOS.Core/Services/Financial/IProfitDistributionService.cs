namespace IslamicPOS.Core.Services.Financial;

public interface IProfitDistributionService
{
    Task<ProfitDistribution> CalculateDistributionAsync(DateTime startDate, DateTime endDate);
    Task<DistributionResult> ProcessDistributionAsync(ProfitDistribution distribution);
    Task<List<DistributionHistory>> GetDistributionHistoryAsync(DateTime? startDate = null);
    Task<PartnerShare> GetPartnerShareAsync(int partnerId, DateTime startDate, DateTime endDate);
}