using IslamicPOS.Core.Models;
using IslamicPOS.Core.Models.InventoryManagement;

namespace IslamicPOS.Core.Services;

public interface IProductService
{
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid id);
    Task<Product> GetProductById(Guid id);
    Task<Product> GetProductByBarcode(string barcode);
    Task<List<Product>> GetProductsByCategory(int categoryId);
    Task<List<Product>> SearchProducts(string query);
    Task<bool> UpdateStock(Guid id, int quantity);
    Task<List<Product>> GetLowStockProducts(int threshold = 10);
    Task<bool> ImportProducts(List<Product> products);
    Task<byte[]> ExportProductsCsv();
}