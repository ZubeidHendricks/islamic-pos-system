using Microsoft.EntityFrameworkCore;
using IslamicPOS.Core.Models.InventoryManagement;
using IslamicPOS.Core.Services;
using IslamicPOS.Infrastructure.Data;

namespace IslamicPOS.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductCategory>> GetAllCategories()
    {
        return await _context.Categories
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<ProductCategory> GetCategoryById(int id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id)
            ?? throw new KeyNotFoundException($"Category {id} not found");
    }

    public async Task<ProductCategory> CreateCategory(ProductCategory category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<ProductCategory> UpdateCategory(ProductCategory category)
    {
        var existing = await GetCategoryById(category.Id);
        
        existing.Name = category.Name;
        existing.Description = category.Description;
        existing.IsActive = category.IsActive;
        
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var category = await GetCategoryById(id);
        category.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}