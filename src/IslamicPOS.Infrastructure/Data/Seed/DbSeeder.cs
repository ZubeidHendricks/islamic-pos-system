using IslamicPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace IslamicPOS.Infrastructure.Data.Seed;

public class DbSeeder
{
    private readonly ApplicationDbContext _context;

    public DbSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Ensure database is created and apply migrations
        await _context.Database.MigrateAsync();

        // Add your seeding logic here
        await SeedCategoriesAsync();
        await SeedProductsAsync();
        
        await _context.SaveChangesAsync();
    }

    private async Task SeedCategoriesAsync()
    {
        // Add seeding logic for categories
    }

    private async Task SeedProductsAsync()
    {
        // Add seeding logic for products
    }
}
