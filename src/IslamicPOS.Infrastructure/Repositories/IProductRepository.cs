namespace IslamicPOS.Infrastructure.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Product>> GetHalalProductsAsync();
    Task<IEnumerable<Product>> GetByCategory(string category);
    Task<IEnumerable<Product>> GetLowStockProducts(int threshold = 10);
}