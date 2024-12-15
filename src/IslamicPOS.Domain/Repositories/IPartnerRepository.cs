using IslamicPOS.Domain.Finance;

namespace IslamicPOS.Domain.Repositories;

public interface IPartnerRepository
{
    Task<Partner> GetByIdAsync(Guid id);
    Task<List<Partner>> GetAllAsync();
    Task AddAsync(Partner partner);
    Task UpdateAsync(Partner partner);
    Task DeleteAsync(Guid id);
}