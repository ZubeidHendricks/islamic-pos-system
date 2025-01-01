namespace IslamicPOS.Infrastructure.Repositories;

public interface IPartnerRepository
{
    Task<Partner?> GetByIdAsync(Guid id);
    Task<IEnumerable<Partner>> GetAllAsync();
    Task<Partner> AddAsync(Partner partner);
    Task UpdateAsync(Partner partner);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Partner>> GetActivePartnersAsync();
    Task<decimal> GetTotalInvestmentAsync();
    Task<IEnumerable<Partner>> GetPartnersByProfitShareRangeAsync(decimal minShare, decimal maxShare);
    Task<IEnumerable<Partner>> SearchPartnersAsync(string searchTerm);
}