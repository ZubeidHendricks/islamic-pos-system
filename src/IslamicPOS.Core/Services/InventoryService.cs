using IslamicPOS.Core.Interfaces;
using IslamicPOS.Core.Models;

namespace IslamicPOS.Core.Services;

public class InventoryService
{
    private readonly IProductRepository _productRepository;

    public InventoryService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task UpdateStockLevels(Transaction transaction)
    {
        foreach (var item in transaction.Items)
        {
            await _productRepository.UpdateStockAsync(
                item.ProductId,
                -item.Quantity // Decrease stock by quantity sold
            );
        }
    }

    public async Task<IEnumerable<Product>> GetLowStockProducts(int threshold = 10)
    {
        var products = await _productRepository.GetAllAsync();
        return products.Where(p => p.StockQuantity <= threshold);
    }
}