using IslamicPOS.Core.Models;

namespace IslamicPOS.Core.Interfaces;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product?> GetByBarcodeAsync(string barcode);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> SearchAsync(string query);
    Task UpdateStockAsync(Guid id, int quantity);
}