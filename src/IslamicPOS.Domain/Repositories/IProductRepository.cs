using IslamicPOS.Domain.Inventory;

namespace IslamicPOS.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}