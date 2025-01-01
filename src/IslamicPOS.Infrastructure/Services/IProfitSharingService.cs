namespace IslamicPOS.Infrastructure.Services;

public interface IProfitSharingService
{
    Task<ProfitSharing> CreateProfitDistributionAsync(DateTime startDate, DateTime endDate);
    Task<bool> DistributeProfitsAsync(Guid profitSharingId);
    Task<IDictionary<string, decimal>> GetPartnerSharesAsync(Guid profitSharingId);
    Task<decimal> CalculateCharityShareAsync(Guid profitSharingId);
    Task<IEnumerable<ProfitDistributionDetail>> GetDistributionDetailsAsync(Guid profitSharingId);
    Task<decimal> GetTotalDistributedProfitsAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ProfitSharing>> GetUndistributedPeriodsAsync();
}