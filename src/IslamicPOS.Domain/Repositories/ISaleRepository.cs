using IslamicPOS.Domain.Sales;

namespace IslamicPOS.Domain.Repositories;

public interface ISaleRepository
{
    Task<Sale> GetByIdAsync(Guid id);
    Task<List<Sale>> GetAllAsync();
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteAsync(Guid id);
}