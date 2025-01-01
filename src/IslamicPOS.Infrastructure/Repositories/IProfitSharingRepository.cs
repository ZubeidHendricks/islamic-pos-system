namespace IslamicPOS.Infrastructure.Repositories;

public interface IProfitSharingRepository
{
    Task<ProfitSharing?> GetByIdAsync(Guid id);
    Task<IEnumerable<ProfitSharing>> GetAllAsync();
    Task<ProfitSharing> AddAsync(ProfitSharing profitSharing);
    Task UpdateAsync(ProfitSharing profitSharing);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ProfitSharing>> GetUndistributedProfitsAsync();
    Task<decimal> GetTotalDistributedProfitAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ProfitSharing>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<ProfitDistributionDetail>> GetDistributionDetailsByProfitSharingIdAsync(Guid profitSharingId);
}